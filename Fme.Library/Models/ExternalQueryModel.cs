// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 11-06-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 11-06-2017
// ***********************************************************************
// <copyright file="ExternalQueryModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Models
{

    /// <summary>
    /// Class ExternFile.
    /// </summary>
    public class ExternalQueryModel
    {
        private string _session = string.Empty;

        /// <summary>
        /// The user
        /// </summary>
        private string sessionId = string.Empty; // System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
       
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public string Connection
        {
            get { return sessionId + "-cn.dat"; }
        }
               
        /// <summary>
        /// Gets the select.
        /// </summary>
        /// <value>The select.</value>
        public string Select
        {
            get { return sessionId + "-select.dat"; }
        }
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error.</value>
        public string Error
        {
            get { return sessionId + "-error.dat"; }
        }

        /// <summary>
        /// Writes the query.
        /// </summary>
        public void WriteQuery()
        {
            DqlDataSource dql = new DqlDataSource(GetConnectionString());
            var ds = dql.ExecuteQuery(GetQueryString());

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                ds.WriteXmlSchema(Schema);
                ds.Tables[0].WriteXml(Dataset);
            }
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void WriteError(Exception ex)
        {
            File.WriteAllText(Error, ex.Message);
            File.WriteAllText(StackTrace, ex.StackTrace);
        }

        /// <summary>
        /// Gets the stack.
        /// </summary>
        /// <value>The stack.</value>
        public string StackTrace
        {
            get { return sessionId + "-stacktrace.dat"; }
        }
        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string Schema
        {
            get { return sessionId + "-schema.dat"; }
        }
        /// <summary>
        /// Gets the dataset.
        /// </summary>
        /// <value>The dataset.</value>
        public string Dataset
        {
            get { return sessionId + "-dataset.dat"; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalQueryModel"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public ExternalQueryModel(string sessionId)
        {
            this.sessionId = sessionId;
        }
        /// <summary>
        /// Performs the cleanup.
        /// </summary>
        public void PerformCleanup()
        {
            if (File.Exists(Dataset)) File.Delete(Dataset);
            if (File.Exists(Schema)) File.Delete(Schema);
            if (File.Exists(StackTrace)) File.Delete(StackTrace);
            if (File.Exists(Error)) File.Delete(Error);
            if (File.Exists(Select)) File.Delete(Select);
            if (File.Exists(Connection)) File.Delete(Connection);
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetConnectionString()
        {
            return File.ReadAllText(this.Connection);
        }
        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetQueryString()
        {
            return File.ReadAllText(this.Select);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="select">The select.</param>
        /// <returns>DataSet.</returns>
        public DataSet ExecuteQuery(string connectionString, string select)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ".\\Extern.Dql.Executor.exe";
            File.WriteAllText(this.Connection, connectionString);
            File.WriteAllText(this.Select, select);
            info.UseShellExecute = false;
            info.Arguments = sessionId;
            info.CreateNoWindow = ShowExternalWindow();
            process.StartInfo = info;
            process.Start();

            process.WaitForExit();
            return LoadQueryResults();
        }

       
        /// <summary>
        /// Loads the query.
        /// </summary>
        /// <returns>DataSet.</returns>
        private DataSet LoadQueryResults()
        {
            if (File.Exists(Error))
            {
                var exception = new ExternalQueryException(File.ReadAllText(Error), File.ReadAllText(StackTrace));
                PerformCleanup();
                throw exception;
            }

            DataSet ds = new DataSet();

            if (File.Exists(this.Schema))
            {
                string data = File.ReadAllText(this.Schema);
                if (data.Contains("Table1"))
                    ds.ReadXmlSchema(Schema);
            }

            if (File.Exists(Dataset))
            {
                string data = File.ReadAllText(Dataset);
                if (data.Contains("Table1"))
                    ds.ReadXml(Dataset);
            }
            return ds;
        }

        /// <summary>
        /// Uses the extern query engine.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool UseExternQueryEngine()
        {
            return File.Exists(".\\UseExternalQuery.prop");            
        }

        /// <summary>
        /// Shows the external window.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShowExternalWindow()
        {
            if (UseExternQueryEngine())
            {
                var text = File.ReadAllLines("UseExternalQuery.prop");
                return text.Count() > 0 && text[0] == "1";
            }
            return false;
        }
    }
   
}
