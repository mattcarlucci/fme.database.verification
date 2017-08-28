// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-24-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-27-2017
// ***********************************************************************
// <copyright file="GenericCompare.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fme.Library.Models;
using System.Threading;
using System.Collections.Concurrent;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class CompareExecuter.
    /// </summary>
    public class CompareExecuter
    {
        /// <summary>
        /// The pairs
        /// </summary>
        public ConcurrentDictionary<string, int> pairs = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// The table
        /// </summary>
        private DataTable Table;
        /// <summary>
        /// The model
        /// </summary>
        private CompareModel Model;
        /// <summary>
        /// The cancel token
        /// </summary>
        private CancellationTokenSource CancelToken;
        /// <summary>
        /// The comparer
        /// </summary>
        private readonly GenericCompare comparer = new GenericCompare();
        /// <summary>
        /// The status message
        /// </summary>
        private string StatusMessage = "Completed";
        
        /// <summary>
        /// Gets or sets the status event.
        /// </summary>
        /// <value>The status event.</value>
        public EventHandler<CompareHelperEventArgs> StatusEvent { get; set; }

        /// <summary>
        /// Called when [status event].
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <param name="results">The results.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="statusMessage">The status message.</param>
        /// <param name="startTime">The start time.</param>
        private void OnStatusEvent(CompareMappingModel mapping, List<CompareResultModel> results, int currentRow, string statusMessage, DateTime startTime)
        {
            var e = new CompareHelperEventArgs()
            {
                CurrentRow = currentRow,
                StartTime = startTime,
                EndTime = DateTime.Now,
                Status = statusMessage,
                Source = mapping.LeftSide,
                Current = Table.Rows.Count,
                ErrorCount = !pairs.ContainsKey(mapping.LeftSide) ? 0 : pairs[mapping.LeftSide],
                RowCount = Table.Rows.Count,
                Results = results
            };
            OnStatusEvent(this, e);
        }
        /// <summary>
        /// Called when [status event].
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="startTime">The start time.</param>
        private void OnStatusEvent(CompareMappingModel model, int currentRow, DateTime startTime)
        {
            var e = new CompareHelperEventArgs()
            {
                CurrentRow = currentRow,
                StartTime = startTime,
                Status = "Running",
                Source = model.LeftSide,
                Current = Table.Rows.Count,
                ErrorCount = 0,
                RowCount = Table.Rows.Count
            };
            OnStatusEvent(this, e);
        }
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

        public CompareExecuter()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareExecuter"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="cancelToken">The cancel token.</param>
        public CompareExecuter(DataTable table,  CompareModel model, CancellationTokenSource cancelToken)
        {
            this.Table = table;
            
            this.CancelToken = cancelToken;
            this.Model = model;
        }
        public void InitalizeModel(DataTable table, CompareModel model, CancellationTokenSource cancelToken)
        {
            this.Table = table;
            this.Model = model;
            this.CancelToken = cancelToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareExecuter"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <param name="StatusEvent">The status event.</param>
        public CompareExecuter(DataTable table, CompareModel model, CancellationTokenSource cancelToken, EventHandler<CompareHelperEventArgs>StatusEvent) : this(table, model, cancelToken)
        {
            this.StatusEvent = StatusEvent;
        }

        /// <summary>
        /// Compares the columns.
        /// </summary>
        /// <param name="currentRow">The current row.</param>
        /// <param name="mapping">The mapping.</param>
        public void CompareColumns(int currentRow, CompareMappingModel mapping)
        {
            List<CompareResultModel> results = new List<CompareResultModel>();
            CompareParameters parms = new CompareParameters(mapping.Operator);
            DateTime startTime = DateTime.Now;

            parms.Add(
                Model.Source.TimeZoneOffset, 
                Model.Target.TimeZoneOffset, 
                mapping.ToDictionary(mapping.LeftLookupFile), 
                mapping.ToDictionary(mapping.RightLookupFile));

            OnStatusEvent(mapping, currentRow, startTime);

            for (int row = 0; row < Table.Rows.Count; row++)
            {
                var left = Convert.ToString(Table.Rows[row][mapping.LeftAlias]);
                var right = Convert.ToString(Table.Rows[row][mapping.RightAlias]);

                if (comparer.ContainsKey(mapping.CompareType))
                {
                    StatusMessage = "Completed";
                    if (!comparer[mapping.CompareType](left, right, parms.Operator, parms))
                    {
                        var primary_key = Table.Rows[row].Field<string>("primary_key");
                        results.Add(new CompareResultModel(primary_key, mapping.LeftSide, mapping.RightSide, left, right, row));
                        var detail = string.Format("{0} {1} {2} Values {3} {1} {4}", mapping.LeftAlias, mapping.Operator, mapping.RightAlias, left, right);

                        if (parms.Exception != null)
                            Model.ErrorMessages.Add(new ErrorMessageModel("Comparison", detail, parms.GetException()));

                        string key = mapping.LeftSide;
                        pairs.AddOrUpdate(key, 1, (existingKey, existingVal) => existingVal + 1);
                    }
                }
                else
                {
                    StatusMessage = "Skipped";
                    pairs.AddOrUpdate(mapping.LeftSide, 1, (existingKey, existingVal) => existingVal);
                }
            }

            OnStatusEvent(mapping, results, currentRow, StatusMessage, startTime);
        }

      
    }
   
   
}
