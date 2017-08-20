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

        /// <summary>
        /// Compares the columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>List&lt;CompareResultModel&gt;.</returns>
        /// <exception cref="System.OperationCanceledException">A cancellation token associated with this operation was canceled</exception>
        public List<CompareResultModel> CompareColumns(int currentRow, DataTable table, CompareMappingModel model, CancellationTokenSource cancelToken)
        {
            // try
            {
                
                List<CompareResultModel> results = new List<CompareResultModel>();
               
                string statusMessage = "Completed";
                this.CancelToken = cancelToken;
                DateTime startTime = DateTime.Now;

                var sourceLookup = model.ToDictionary(model.LeftLookupFile);
                var targetLookup = model.ToDictionary(model.RightLookupFile);

               
                OnStatusEvent(this, NewEventStatus(table, model, currentRow, startTime));               


                ParallelOptions options = new ParallelOptions()
                { MaxDegreeOfParallelism = Environment.ProcessorCount * 4 };
                          

                options.CancellationToken = CancelToken.Token;

                pairs.AddOrUpdate(model.LeftSide, 0, (existingKey, existingVal) => 0);
#if !NO_PARA
                for (int row = 0; row < table.Rows.Count; row++)
#else
                Parallel.For(1, table.Rows.Count, options, row =>
#endif
                {
                    if (cancelToken.IsCancellationRequested)
                        throw new OperationCanceledException("A cancellation token associated with this operation was canceled");

                    var sourceValue = table.Rows[row][model.LeftAlias];
                    var targetValue = table.Rows[row][model.RightAlias];
                    var primary_key = table.Rows[row].Field<string>("primary_key");

                    if (model.CompareType == ComparisonTypeEnum.None)
                    {
                        statusMessage = "Skipped";
                        pairs.AddOrUpdate(model.LeftSide, 1, (existingKey, existingVal) => existingVal);
#if !NO_PARA
                        continue;
#else
                        return;
#endif
                    }

                    var left = GetValue(ToString(sourceValue), sourceLookup);
                    var right = GetValue(ToString(targetValue), targetLookup);
                    
                    if (!CompareCells.IsEqual(left, right, model.CompareType, model.Operator, model.IgnoreChars))
                    {
                       // string field = GetValue(ToString(sourceValue), sourceLookup).Replace("left_", "");
                        results.Add(new CompareResultModel(primary_key, model.LeftSide,model.RightSide, left, right, row));

                       
                        //excelWorkSheet1.Range[sourceCol + row.ToString()].Interior.Color = 255;
                        //excelWorkSheet1.Range[targetCol + row.ToString()].Interior.Color = 5296274;

                        int index = row == 1 ? 0 : 1;
                        string key = model.LeftSide;
                        pairs.AddOrUpdate(key, 1, (existingKey, existingVal) => existingVal + index);
                    }
                }
#if NO_PARA
                );
#endif
                if (pairs.ContainsKey(model.LeftSide))
                    Console.WriteLine("OK");

                OnStatusEvent(this, NewEventStatus(table, model, results, currentRow, statusMessage, startTime));


                return results;

            }

        }

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
