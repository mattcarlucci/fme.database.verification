// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="DataSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Xml.Serialization;
using System.Linq;
using System.Collections;
using Fme.Library.Extensions;

namespace Fme.Library
{
    /// <summary>
    /// Class DataSource.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(AccessDataSource))]
    [XmlInclude(typeof(ExcelDataSource))]
    [XmlInclude(typeof(DqlDataSource))]
    [XmlInclude(typeof(SqlDataSource))]    
    public abstract class DataSourceBase
    {        
        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName { get; set; }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <returns>DataSet.</returns>
        public abstract DataSet ExecuteQuery(string select);
                
        public string[] Map(string[] fields)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<string> @new = new List<string>();
            foreach (var field in fields)
            {
                if (map.ContainsKey(field) == false)
                {
                    map.Add(field, 0);
                    @new.Add(field);
                }
                else
                {
                    map[field] += map[field];
                    @new.Add(string.Format("{0} as {0}{1}", field, map[field]));
                }

            }
            return @new.ToArray();
        }
        /// <summary>
        /// Sets the aliases.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="alias">The alias.</param>
        public virtual void SetAliases(DataSet dataSet, string alias)
        {
            if (dataSet.Tables.Count > 0)
                SetAliases(dataSet.Tables[0], alias);
        }
        /// <summary>
        /// Sets the aliases.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="alias">The alias.</param>
        public virtual void SetAliases(DataTable table, string alias)
        {
            if (table == null || table.Columns.Count == 0) return;

            for(int i = 1; i < table.Columns.Count; i++)            
            {
                DataColumn column = table.Columns[i];
                if (column.ColumnName.StartsWith(alias + "_"))
                    return;

                column.ColumnName = alias + "_" + column.ColumnName;
            }
            
        }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="token">The token.</param>
        /// <returns>DataSet.</returns>
        public abstract DataSet ExecuteQuery(string select, CancellationToken token);

        /// <summary>
        /// Gets the schema model.
        /// </summary>
        /// <returns>List&lt;TableSchemaModel&gt;.</returns>
        public abstract List<TableSchemaModel> GetSchemaModel();
        /// <summary>
        /// Gets the schema model.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>TableSchemaModel.</returns>
        public abstract TableSchemaModel GetSchemaModel(string tableName);

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceBase"/> class.
        /// </summary>
        public DataSourceBase()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceBase" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DataSourceBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// Fills the schema.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>List&lt;TableSchemaModel&gt;.</returns>
        protected virtual List<TableSchemaModel> FillSchema(string select, string tableName)
        {            
            var schema = ExecuteQuery(select);

            List<TableSchemaModel> tableSchemaModel = new List<TableSchemaModel>();

            TableSchemaModel table = new TableSchemaModel();
            tableSchemaModel.Add(table);
            table.TableName = tableName;

            foreach (DataColumn col in schema.Tables[0].Columns)
            {
                var field = new FieldSchemaModel(col, tableName);               
                table.Fields.Add(field);
            }
            return tableSchemaModel;
        }
        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>DqlConnectionStringBuilder.</returns>
        public abstract DbConnectionStringBuilder GetConnectionStringBuilder();
     
        /// <summary>
        /// Gets the query builder.
        /// </summary>
        /// <returns>QueryBuilder.</returns>
        public abstract QueryBuilder GetQueryBuilder();

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns>DbConnection.</returns>
        public abstract DbConnection CreateConnection();

        /// <summary>
        /// Adds the update schema.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="query">The query.</param>
        public virtual List<TableSchemaModel> CustomSchema(string name, string query)
        {
           return FillSchema(query, name);                       
        }
    }
}
