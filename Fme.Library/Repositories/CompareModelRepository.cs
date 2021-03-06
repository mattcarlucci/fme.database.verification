﻿using Fme.Library.Extensions;
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
            var col = Model.ColumnCompare[e.CurrentRow];
            col.CompareResults = e.Results;           
            col.StartTime = e.StartTime;
            col.Status = e.Status + " " + e.Duration.ToString();

            StatusEvent?.Invoke(sender, e);
            base.OnStatusEvent(sender, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareModelRepository"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
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
        //public string GetQueryString(DataSourceModel source, string side, string[] fields, List<CompareMappingModel> pairs)
        //{
        //    QueryBuilder query = source.DataSource.GetQueryBuilder();

        //    return query.BuildSql(source.Key, fields,
        //        source.SelectedTable, side, source.MaxRows, source.Key, Model.GetIdsFromFile());
        //}

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
                    
                    cancelToken.Token.ThrowIfCancellationRequested();
                    #region Initalize and Reset prior results
                    var pairs = Model.ColumnCompare.Where(w => w.IsCalculated == false && w.Selected).ToList();               
                    Model.ColumnCompare.ForEach(item => item.CompareResults.Clear());                  
                    OnCompareStart(this, new CompareStartEventArgs());
                    #endregion
                                        
                    #region Construct Source, Fetch Data and set Aliases

                    QueryBuilder query = Model.Source.DataSource.GetQueryBuilder();            
                    query.IncludeVersion = Model.Source.IncludeVersions;
                                        
                    var select1 = query.BuildSql(Model.Source, pairs.Select(s => s.LeftSide).ToArray(),Model.GetSourceIds(), Model.GetSourceFilter());

                    if (Model.Source.SelectedSchema().IsCustom)
                        select1 = Model.Source.SelectedSchema().Query;

                    LogQuery(Model.Source, select1, Alias.Left);
                    OnCompareModelStatus(this, new CompareModelStatusEventArgs(Model.Source, select1, "Executing Source Query..."));
                                                            
                    DateTime start1 = DateTime.Now;
                    var data1 = Model.Source.DataSource.ExecuteQuery(select1, cancelToken.Token);
                    DateTime done1 = DateTime.Now;
                    
                    if (data1 == null || data1.Tables == null || data1.Tables.Count == 0)
                        throw new Exception("No data was returned for the selected table " + Model.Source.SelectedTable);

                    var table1 = data1.MergeAll();
                    var sourceData = table1.CopyToDataTable();
                    OnSourceLoadComplete(this, new DataTableEventArgs(sourceData));

                    Model.Source.DataSource.SetAliases(table1, Alias.Left);                    
                    Model.MapColumnCaptions(table1, (map, index) => map[index].LeftAlias);
                    Model.MapColumnsKeys(table1, (map, index, value) => map[index].LeftKey = value);

                    #endregion
                    /******************************************/
                    #region Construct Target, Fetch Data and set Aliases
                    
                    query = Model.Target.DataSource.GetQueryBuilder();
                    query.IncludeVersion = Model.Target.IncludeVersions;
                    
                    var select2 = query.BuildSql(Model.Target, pairs.Select(s => s.RightSide).ToArray(), 
                        table1.SelectKeys<string>(Alias.Primary_Key), null);

                    if (Model.Target.SelectedSchema().IsCustom)
                        select2 = Model.Target.SelectedSchema().Query;

                    LogQuery(Model.Target, select2, Alias.Right);

                    OnCompareModelStatus(this, new CompareModelStatusEventArgs(Model.Target, select2, "Executing Target Query..."));

                    DateTime start2 = DateTime.Now;
                    var data2 = Model.Target.DataSource.ExecuteQuery(select2, cancelToken.Token);
                    DateTime done2 = DateTime.Now;
                    
                    if (data2 == null || data2.Tables == null || data2.Tables.Count == 0)
                        throw new Exception("No data was returned for the selected table " + Model.Target.SelectedTable);

                    var table2 = data2.MergeAll();
                    var targetData = table2.CopyToDataTable();
                    OnTargetLoadComplete(this, new DataTableEventArgs(targetData));

                    Model.Target.DataSource.SetAliases(table2, Alias.Right);
                    Model.MapColumnCaptions(table2, (map, index) => map[index].RightAlias);
                    Model.MapColumnsKeys(table2, (map, index, value) => map[index].RightKey = value);
                    
                    #endregion

                    #region Check for matching records
             
                    if (table1.Rows.Count == 0 || table2.Rows.Count == 0)
                        throw new DataException("No matching records found to complete the request");
                    #endregion

                    #region Merge and Compare Results
                    
                    table1.InnerJoin<string>(Alias.Primary_Key, table2);

                    var dupset1 = table1.RemoveDuplicates<string>(Alias.Primary_Key);
                    var dupset2 = table2.RemoveDuplicates<string>(Alias.Primary_Key);

                    if (string.IsNullOrEmpty(dupset1) == false)
                        Model.ErrorMessages.Add(new ErrorMessageModel("Left Query has duplicate Keys", 
                            dupset1, table1.Rows.Count + " record(s) remain after removing duplicates"));

                    if (string.IsNullOrEmpty(dupset2) == false)
                        Model.ErrorMessages.Add(new ErrorMessageModel("Right Query has duplicate Keys", 
                            dupset2, table2.Rows.Count + " record(s) remain after removing duplicates"));

                    table1.SetPrimaryKey(Alias.Primary_Key, table2);

                    if (table1.Rows.Count == 0 || table2.Rows.Count == 0)
                        throw new DataException("No valid records found to complete the request. Please see the error log for more details");

                    Model.CompareModelStatus += this.CompareModelStatus;
                    Model.ExecuteCalculatedFields(table1, table2, cancelToken);

                    Model.Queries.Add(new QueryMessageModel("Source Query", start1, done1, select1, data1.Table().Rows.Count));
                    Model.Queries.Add(new QueryMessageModel("Target Query", start2, done2, select2, data2.Table().Rows.Count));

                    table1.Merge(table2, false, MissingSchemaAction.AddWithKey);
                    CompareMappingHelper.OrderColumns(table1, Model.ColumnCompare.Where(w=> w.Selected).ToList());                    
                    CompareMappingHelper.CompareColumns(this, table1, Model, cancelToken);
                    
                    OnCompareComplete(this, new DataTableEventArgs(table1));
                    #endregion
                });
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
                string key1 = item.Row + item.LeftKey; // "left_" + item.LeftSide;
                string key2 = item.Row + item.RightKey; // "right_" + item.RightSide;

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
