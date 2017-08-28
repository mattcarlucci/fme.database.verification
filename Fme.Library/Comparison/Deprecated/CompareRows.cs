using Fme.Library.Enums;
using Fme.Library.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fme.Library.Comparison
{

    /// <summary>
    /// Class CompareRows.
    /// </summary>
    public class CompareRows
    {
        /// <summary>
        /// The pairs
        /// </summary>
        public ConcurrentDictionary<string, int> pairs = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// Gets or sets the status event.
        /// </summary>
        /// <value>The status event.</value>
        public EventHandler<CompareHelperEventArgs> StatusEvent { get; set; }
        /// <summary>
        /// Handles the <see cref="E:StatusEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CompareHelperEventArgs"/> instance containing the event data.</param>
        public virtual void OnStatusEvent(object sender, CompareHelperEventArgs e)
        {
            if (StatusEvent != null)
                StatusEvent(this, e);
        }

        /// <summary>
        /// Gets the cancel token.
        /// </summary>
        /// <value>The cancel token.</value>
        public CancellationTokenSource CancelToken { get; private set; }

        /// <summary>
        /// Gets the lookup list.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>

        public void CancelProcess()
        {
            if (CancelToken == null) return;

            CancelToken.Cancel();
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(object obj)
        {
            return Convert.ToString(obj);
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lookup">The lookup.</param>
        /// <returns>System.String.</returns>
        private string GetValue(string value, Dictionary<string, string> lookup)
        {
            if (string.IsNullOrEmpty(value) || lookup == null)
                return value ?? "";

            if (lookup.ContainsKey(value))
                return lookup[value];

            return value;
        }

        public void CompareColumn(int currentRow, DataTable table, CompareModel model, CancellationToken cancelToken)
        {

        }

        /// <summary>
        /// Compares the columns.
        /// </summary>
        /// <param name="currentRow">The current row.</param>
        /// <param name="table">The table.</param>
        /// <param name="compare">The compare.</param>
        /// <param name="model">The model.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>List&lt;CompareResultModel&gt;.</returns>
        public List<CompareResultModel> CompareColumns2(int currentRow, DataTable table, CompareMappingModel compare, CompareModel model, CancellationTokenSource cancelToken)
        {
            List<CompareResultModel> results = new List<CompareResultModel>();
            GenericCompare comparer = new GenericCompare();

            string statusMessage = "Completed";
            this.CancelToken = cancelToken;
            DateTime startTime = DateTime.Now;

            CompareParameters parms = new CompareParameters(compare.Operator);
            parms.Add(model.Source.TimeZoneOffset, model.Target.TimeZoneOffset, compare.ToDictionary(compare.LeftLookupFile), compare.ToDictionary(compare.RightLookupFile));            

            OnStatusEvent(this, NewEventStatus(table, compare, currentRow, startTime));
            pairs.AddOrUpdate(compare.LeftSide, 0, (existingKey, existingVal) => 0);
            
            for (int row = 0; row < table.Rows.Count; row++)
            {
                var sourceValue = ToString(table.Rows[row][compare.LeftAlias]);
                var targetValue = ToString(table.Rows[row][compare.RightAlias]);
                
                if (comparer.ContainsKey(compare.CompareType))
                {
                    if (!comparer[compare.CompareType](sourceValue, targetValue, parms.Operator, parms))
                    {
                        var primary_key = table.Rows[row].Field<string>(Alias.Primary_Key);
                        results.Add(new CompareResultModel(primary_key, compare.LeftSide, compare.RightSide, sourceValue, targetValue, row));
                        var detail = string.Format("{0} {1} {2}\r\n\t   Values {3} {1} {4}", compare.LeftAlias, compare.Operator, compare.RightAlias, sourceValue, targetValue);

                        if (parms.Exception != null)
                            model.ErrorMessages.Add(new ErrorMessageModel("Comparison", detail, parms.GetException()));

                        string key = compare.LeftSide;
                        pairs.AddOrUpdate(key, 1, (existingKey, existingVal) => existingVal + 1);
                    }
                }
                else
                {
                    statusMessage = "Skipped";
                    pairs.AddOrUpdate(compare.LeftSide, 1, (existingKey, existingVal) => existingVal);
                }
            }
            OnStatusEvent(this, NewEventStatus(table, compare, results, currentRow, statusMessage, startTime));
            return results;

        }
        /// <summary>
        /// Compares the columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="compare">The model.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>List&lt;CompareResultModel&gt;.</returns>
        /// <exception cref="System.OperationCanceledException">A cancellation token associated with this operation was canceled</exception>
        public List<CompareResultModel> CompareColumns(int currentRow, DataTable table, CompareMappingModel compare, CompareModel model, CancellationTokenSource cancelToken)
        {
            // try
            {
                
                List<CompareResultModel> results = new List<CompareResultModel>();
               
                string statusMessage = "Completed";
                this.CancelToken = cancelToken;
                DateTime startTime = DateTime.Now;

                var sourceLookup = compare.ToDictionary(compare.LeftLookupFile);
                var targetLookup = compare.ToDictionary(compare.RightLookupFile);

               
                OnStatusEvent(this, NewEventStatus(table, compare, currentRow, startTime));               


                ParallelOptions options = new ParallelOptions()
                { MaxDegreeOfParallelism = Environment.ProcessorCount * 4 };
                          

                options.CancellationToken = CancelToken.Token;

                pairs.AddOrUpdate(compare.LeftSide, 0, (existingKey, existingVal) => 0);
#if !NO_PARA
                for (int row = 0; row < table.Rows.Count; row++)
#else
                Parallel.For(1, table.Rows.Count, options, row =>
#endif
                {
                    #region loop
                    if (cancelToken.IsCancellationRequested)
                        throw new OperationCanceledException("A cancellation token associated with this operation was canceled");

                    var sourceValue = table.Rows[row][compare.LeftAlias];
                    var targetValue = table.Rows[row][compare.RightAlias];

                    compare.LeftTimeZoneOffset = model.Source.TimeZoneOffset;
                    compare.RightTimeZoneOffset = model.Target.TimeZoneOffset;

                    //TODO: this is an issue if key is not a string
                    var primary_key = table.Rows[row].Field<string>(Alias.Primary_Key);

                    if (compare.CompareType == ComparisonTypeEnum.None)
                    {
                        statusMessage = "Skipped";
                        pairs.AddOrUpdate(compare.LeftSide, 1, (existingKey, existingVal) => existingVal);
#if !NO_PARA
                        continue;
#else
                        return;
#endif
                    }

                    var left = GetValue(ToString(sourceValue), sourceLookup);
                    var right = GetValue(ToString(targetValue), targetLookup);
                    
                    if (!CompareCells.IsEqual(left, right, compare.CompareType, compare.Operator, compare.IgnoreChars, compare.LeftTimeZoneOffset, compare.RightTimeZoneOffset))
                    {
                       // string field = GetValue(ToString(sourceValue), sourceLookup).Replace("left_", "");
                        results.Add(new CompareResultModel(primary_key, compare.LeftSide,compare.RightSide, left, right, row));
                                               
                        //excelWorkSheet1.Range[sourceCol + row.ToString()].Interior.Color = 255;
                        //excelWorkSheet1.Range[targetCol + row.ToString()].Interior.Color = 5296274;

                       // int index = row == 1 ? 0 : 1;
                        string key = compare.LeftSide;
                        pairs.AddOrUpdate(key, 1, (existingKey, existingVal) => existingVal + 1);
                    }
                    #endregion
                }
#if NO_PARA
                );
#endif
                if (pairs.ContainsKey(compare.LeftSide))
                    Console.WriteLine("OK");

                OnStatusEvent(this, NewEventStatus(table, compare, results, currentRow, statusMessage, startTime));

                return results;

            }

        }

        /// <summary>
        /// News the event status.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="results">The results.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="statusMessage">The status message.</param>
        /// <param name="startTime">The start time.</param>
        /// <returns>CompareHelperEventArgs.</returns>
        private CompareHelperEventArgs NewEventStatus(DataTable table, CompareMappingModel model, List<CompareResultModel> results, int currentRow, string statusMessage, DateTime startTime)
        {
            return new CompareHelperEventArgs()
            {
                CurrentRow = currentRow,
                StartTime = startTime,
                EndTime = DateTime.Now,
                Status = statusMessage,
                Source = model.LeftSide,
                Current = table.Rows.Count,
                ErrorCount = !pairs.ContainsKey(model.LeftSide) ? 0 : pairs[model.LeftSide],
                RowCount = table.Rows.Count,
                Results = results
            };
        }

        /// <summary>
        /// News the event status.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="startTime">The start time.</param>
        /// <returns>CompareHelperEventArgs.</returns>
        private static CompareHelperEventArgs NewEventStatus(DataTable table, CompareMappingModel model, int currentRow, DateTime startTime)
        {
            return new CompareHelperEventArgs()
            {
                CurrentRow = currentRow,
                StartTime = startTime,
                Status = "Running",
                Source = model.LeftSide,
                Current = table.Rows.Count,
                ErrorCount = 0,
                RowCount = table.Rows.Count
            };
        }
    }
}
