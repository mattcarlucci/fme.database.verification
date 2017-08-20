using Fme.Library.Extensions;
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using Fme.Library.Comparison;
using System.Diagnostics;

namespace Fme.Library.Repositories
{
    public class CompareModelRepository : CompareRows
    {
        //CancellationTokenSource CancelToken { get; set; }
        CompareModel Model { get; set; }

        public EventHandler<EventArgs> Error;
        protected virtual void OnError(object sender, EventArgs e)
        {
            Error?.Invoke(sender, e);
        }

        public EventHandler<EventArgs> Cancelled;
        protected virtual void OnCancelled(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }
        public EventHandler<CompareStartEventArgs> CompareStart;
        protected virtual void OnCompareStart(object sender, CompareStartEventArgs e)
        {
            CompareStart?.Invoke(sender, e);
        }
        public EventHandler<DataTableEventArgs> SourceLoadComplete;
        protected virtual void OnSourceLoadComplete(object sender, DataTableEventArgs e)
        {
            SourceLoadComplete?.Invoke(sender, e);
        }
        public EventHandler<DataTableEventArgs> TargetLoadComplete;
        protected virtual void OnTargetLoadComplete(object sender, DataTableEventArgs e)
        {
            TargetLoadComplete?.Invoke(sender, e);
        }

        public EventHandler<DataTableEventArgs> CompareComplete;
        protected virtual void OnCompareComplete(object sender, DataTableEventArgs e)
        {
            CompareComplete?.Invoke(sender, e);
        }

        public EventHandler<CompareModelStatusEventArgs> CompareModelStatus;       
        protected virtual void OnCompareModelEventArgs(object sender, CompareModelStatusEventArgs e)
        {
            CompareModelStatus?.Invoke(sender, e);
        }
        //public EventHandler<CompareHelperEventArgs> StatusEvent { get; set; }

        public override void OnStatusEvent(object sender, CompareHelperEventArgs e)
        {
            Debug.Print("Current Row is " + e.CurrentRow);

            var col = Model.ColumnCompare[e.CurrentRow];
            col.CompareResults = e.Results;

            col.Errors = e.ErrorCount.ToString();
            col.StartTime = e.StartTime;
            col.Status = e.Status + " " + e.Duration.ToString();

            StatusEvent?.Invoke(sender, e);
            base.OnStatusEvent(sender, e);
        }
        public CompareModelRepository(CompareModel model)
        {
            this.Model = model;
        }
        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="side">The side.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="pairs">The pairs.</param>
        /// <returns>System.String.</returns>
        public string GetQueryString(DataSourceModel source, string side, string[] fields, List<CompareMappingModel> pairs)
        {
            QueryBuilder query = source.DataSource.GetQueryBuilder();

            return query.BuildSql(source.Key, fields,
                source.SelectedTable, side, source.Key, Model.GetIdsFromFile());
        }

        /// <summary>
        /// Executes the specified cancel token.
        /// </summary>
        /// <param name="cancelToken">The cancel token.</param>
        public async void Execute(CancellationTokenSource cancelToken)
        {
            try
            {
                await Task.Run(() =>
                 {

                     //  var pairs = CompareMappingHelper.GetPairs(Model.Source.SelectedSchema(), Model.Target.SelectedSchema());

                     var pairs = Model.ColumnCompare.Where(w=> w.IsCalculated == false).ToList();

                     QueryBuilder query = Model.Source.DataSource.GetQueryBuilder();

                 //var sql1 = GetQueryString(Model.Source, "left", pairs.Select(s => s.LeftSide).ToArray(), pairs);
                 //var sql2 = GetQueryString(Model.Source, "right", pairs.Select(s => s.RightSide).ToArray(), pairs);

                 var select1 = query.BuildSql(Model.Source.Key, pairs.Select(s => s.LeftSide).ToArray(),
                      Model.Source.SelectedTable, "left", Model.Source.Key, Model.GetIdsFromFile());

                     var table1 = Model.Source.DataSource.ExecuteQuery(select1, cancelToken.Token).Tables?[0];

                     var select2 = query.BuildSql(Model.Target.Key, pairs.Select(s => s.RightSide).ToArray(),
                         Model.Target.SelectedTable, "right", Model.Target.Key, table1.SelectKeys<string>("primary_key"));


                     var table2 = Model.Target.DataSource.ExecuteQuery(select2, cancelToken.Token).Tables?[0];

                     if (table1.Rows.Count == 0 || table2.Rows.Count == 0)
                         throw new DataException("No matching records found to complete the request");

                     table1.InnerJoin<string>("primary_key", table2);

                     var dupset1 = table1.RemoveDuplicates<string>("primary_key");
                     var dupset2 = table2.RemoveDuplicates<string>("primary_key");

                     table1.SetPrimaryKey("primary_key", table2);

                     Model.ExecuteCalculatedFields(table1, table2, cancelToken);

                     var sourceData = table1.AsEnumerable().CopyToDataTable();
                     sourceData.RemoveEmptyColumns();

                     var targetData = table2.AsEnumerable().CopyToDataTable();
                     targetData.RemoveEmptyColumns();

                     table1.Merge(table2, false, MissingSchemaAction.AddWithKey);
                     CompareMappingHelper.OrderColumns(table1, pairs);


                     OnCompareStart(this, new CompareStartEventArgs() { Pairs = null });
                     OnSourceLoadComplete(this, new DataTableEventArgs() { Table = sourceData });
                     OnTargetLoadComplete(this, new DataTableEventArgs() { Table = targetData });



                     CompareMappingHelper.CompareColumns(this, table1, pairs, cancelToken);

                     OnCompareComplete(this, new DataTableEventArgs() { Table = table1, Pairs = null });
                 }
                  ); //.Wait(cancelToken.Token);

            }
            catch (OperationCanceledException ex)
            {
                OnError(ex, EventArgs.Empty);

            }
            catch (Exception ex)
            {

                OnError(ex, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the compare results.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Dictionary&lt;System.String, System.Int32&gt;.</returns>
        public static  Dictionary<string, int> GetCompareResults(CompareModel model)
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();

            var results = model.ColumnCompare.SelectMany(s => s.CompareResults).ToList();
            foreach (var item in results)
            {
                keys.Add(item.Row + item.LeftSide, 0);               
            }
            return keys;
        }
    }
}
