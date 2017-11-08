// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 10-31-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 10-31-2017
// ***********************************************************************
// <copyright file="QueryMessageModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class QueryMessageModel.
    /// </summary>
    public class QueryMessageModel
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public string Duration
        {
            get
            {
                if (EndTime == null)
                    return "Running...";

                return new TimeSpan(EndTime.Value.Ticks - StartTime.Ticks).
                    Duration().ToString();
            }
        }
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets the SQL.
        /// </summary>
        /// <value>The SQL.</value>
        public string Sql { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageModel"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="count">The count.</param>
        /// <param name="sql">The SQL.</param>
        public QueryMessageModel(DateTime startTime,  string sql, string name)
        {
            this.StartTime = startTime;           
            this.Sql = sql;
            this.Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageModel"/> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        public QueryMessageModel(string sql, string name)
        {
            this.StartTime = DateTime.Now;
            this.Sql = sql;
            this.Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageModel"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="count">The count.</param>
        /// <param name="sql">The SQL.</param>
        public QueryMessageModel(DateTime startTime, DateTime endTime, int count, string sql)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Count = count;
            this.Sql = sql;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMessageModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="count">The count.</param>
        public QueryMessageModel(string name, DateTime startTime, DateTime endTime, string sql, int count)
        {
            this.Name = name;
            this.StartTime = startTime;
            this.EndTime = endTime;            
            this.Sql = sql;
            this.Count = count;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>System.Int32.</returns>
        public int SetCount(DataSet results)
        {
            EndTime = DateTime.Now;
            Count = results.Tables.Count > 0 ? results.Tables[0].Rows.Count : 0;
            return Count;
        }

        /// <summary>
        /// Gets the SQL.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetSql()
        {
            if (EndTime == null)
                return string.Format("\r\n// Start Time: {0}\r\n// Duration: Running\r\n{1}", StartTime, Sql);
            
            return   string.Format("\r\n// Start Time: {0}\r\n// Duration: {1}\r\n// Count: {2}\r\n{3}", 
                    StartTime, new TimeSpan(EndTime.Value.Ticks - StartTime.Ticks).Duration(), Count, Sql);
        }

        /// <summary>
        /// Logs the query.
        /// </summary>
        /// <param name="file">The file.</param>
        public void LogQuery(string file)
        {
            try
            {             

                if (Directory.Exists(Alias.Debug))
                    File.WriteAllText(file, GetSql());
            }
            catch (Exception)
            {
                return;
            }

        }

    }
}
