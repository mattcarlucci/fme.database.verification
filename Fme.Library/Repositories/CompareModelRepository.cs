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
using System.IO;
using Fme.Library.Enums;

namespace Fme.Library.Repositories
{
    public class CompareModelRepository : CompareExecuter
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
        protected virtual void OnCompareModelStatus(object sender, CompareModelStatusEventArgs e)
        {
            CompareModelStatus?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the <see cref="E:StatusEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CompareHelperEventArgs" /> instance containing the event data.</param>
        public override void OnStatusEvent(object sender, CompareHelperEventArgs e)
        {
           // Debug.Print("Current Row is " + e.CurrentRow);

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
                source.SelectedTable, side, source.MaxRows, source.Key, Model.GetIdsFromFile());
        }

        /// <summary>
        /// Executes the wait.
        /// </summary>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>Task.</returns>
        public async Task ExecuteWait(CancellationTokenSource cancelToken)
        {
            await Execute(cancelToken);
        }
        //private async Task RequestExecute
        /// <summary>
        /// Executes the specified cancel token.
        /// </summary>
        /// <param name="cancelToken">The cancel token.</param>
        public async Task Execute(CancellationTokenSource cancelToken)
        {
            
            try
            {
                await Task.Run(() =>
                {
                    var pairs = Model.ColumnCompare.Where(w => w.IsCalculated == false && w.Selected).ToList();
                    QueryBuilder query = Model.Source.DataSource.GetQueryBuilder();

                    ///////////////////////////////////////////////////////////////
                    #region Construct Source, Fetch Data and set Aliases

                    query.IncludeVersion = Model.Source.IncludeVersions;

                    var select1 = query.BuildSql(Model.Source.Key, pairs.Select(s => s.LeftSide).ToArray(),
                      Model.Source.SelectedTable, "", Model.Source.MaxRows, Model.Source.Key, Model.GetIdsFromFile());

                    LogQuery(Model.Source, select1, "Left");

                    OnCompareModelStatus(this, new CompareModelStatusEventArgs()
                    { DataSource = Model.Source, Data = select1, StatusMessage = "Executing Query" });
                    
                    var data1 = Model.Source.DataSource.ExecuteQuery(select1, cancelToken.Token);
                    if (data1.Tables == null || data1.Tables.Count == 0)
                        throw new Exception("No data was returned for the selected table " + Model.Source.SelectedTable);

                    Model.Source.DataSource.SetAliases(data1, Alias.Left);                    
                    Model.MapTableColumns(data1, (map, index) => map[index].LeftAlias);

                    #endregion
                    ///////////////////////////////////////////////////////////////

                    DataTable table1 = data1.Table();

                    #region Construct Target, Fetch Data and set Aliases
                    
                    query = Model.Target.DataSource.GetQueryBuilder();
                    query.IncludeVersion = Model.Target.IncludeVersions;

                    var select2 = query.BuildSql(Model.Target.Key, pairs.Select(s => s.RightSide).ToArray(),
                        Model.Target.SelectedTable, "", Model.Target.MaxRows, Model.Target.Key,  table1.SelectKeys<string>(Alias.Primary_Key));

                    LogQuery(Model.Target, select2, Alias.Right);

                    OnCompareModelStatus(this, new CompareModelStatusEventArgs()
                    { DataSource = Model.Target, Data = select1, StatusMessage = "Executing Query" });

                    var data2 = Model.Target.DataSource.ExecuteQuery(select2, cancelToken.Token);                    
                    if (data2.Tables == null || data2.Tables.Count == 0)
                        throw new Exception("No data was returned for the selected table " + Model.Target.SelectedTable);

                    Model.Target.DataSource.SetAliases(data2, Alias.Right);
                    Model.MapTableColumns(data2, (map, index) => map[index].RightAlias);
                    #endregion

                    #region Check for matching records
                    DataTable table2 = data2.Table();
                 
                    if (table1.Rows.Count == 0 || table2.Rows.Count == 0)
                        throw new DataException("No matching records found to complete the request");
                    #endregion


                    table1.InnerJoin<string>(Alias.Primary_Key, table2);
                    var dupset1 = table1.RemoveDuplicates<string>(Alias.Primary_Key);
                    var dupset2 = table2.RemoveDuplicates<string>(Alias.Primary_Key);

                    if (string.IsNullOrEmpty(dupset1) == false)
                        Model.ErrorMessages.Add(new ErrorMessageModel("Left Query has duplicate Keys", dupset1, ""));

                    if (string.IsNullOrEmpty(dupset2) == false)
                        Model.ErrorMessages.Add(new ErrorMessageModel("Right Query has duplicate Keys", dupset2, ""));

                    table1.SetPrimaryKey(Alias.Primary_Key, table2);
                         
                    
                    Model.ExecuteCalculatedFields(table1, table2, cancelToken);

                    var sourceData = table1.CopyToDataTable();
                    var targetData = table2.CopyToDataTable();

                    //  Model.MapTableColumns(sourceData, (map, index) => map[index].LeftAlias);
                    //   Model.MapTableColumns(targetData, (map, index) => map[index].RightAlias);

                    OnCompareStart(this, new CompareStartEventArgs() { Pairs = null });
                    OnSourceLoadComplete(this, new DataTableEventArgs() { Table = sourceData });
                    OnTargetLoadComplete(this, new DataTableEventArgs() { Table = targetData });

                    table1.Merge(table2, false, MissingSchemaAction.AddWithKey);
                    CompareMappingHelper.OrderColumns(table1, Model.ColumnCompare.Where(w=> w.Selected).ToList());
                    
                 
                    
                    CompareMappingHelper.CompareColumns(this, table1, Model, cancelToken);
                    

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
        /// Logs the query.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="select">The select.</param>
        /// <param name="side">The side.</param>
        private void LogQuery(DataSourceModel model, string select, string side)
        {
            //var name = model.Name.Split(new string[] { "\\"}, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            //if (string.IsNullOrEmpty(name))
            //    name = "Untitled";
            try
            {
                if (Directory.Exists(@".\logs") == false)
                    Directory.CreateDirectory(@".\logs");

                File.WriteAllText(string.Format(@".\logs\{0}Query_{1}_{2}.sql", side, model.SelectedTable, model.Key), select);
            }
            catch(Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Gets the compare results.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Dictionary&lt;System.String, System.Int32&gt;.</returns>
        public static  Dictionary<string, string> GetCompareResults(CompareModel model)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();

            var results = model.ColumnCompare.SelectMany(s => s.CompareResults).ToList();
            foreach (var item in results)
            {
                string key1 = item.Row + "left_" + item.LeftSide;
                string key2 = item.Row + "right_" + item.RightSide;

                if (keys.ContainsKey(key1))
                    model.ErrorMessages.Add(new
                        ErrorMessageModel("Get Compare Results", string.Format("The left key {0} already exists", key1), "Please report this error"));

                if (keys.ContainsKey(key2))
                    model.ErrorMessages.Add(new
                        ErrorMessageModel("Get Compare Results", string.Format("The right key {0} already exists", key2), "Please report this error"));

             
                if (keys.ContainsKey(key1)== false) keys.Add(key1, "Red");
                if (keys.ContainsKey(key2) == false)  keys.Add(key2, "LightGreen");
            }
            return keys;
        }
    }
}
