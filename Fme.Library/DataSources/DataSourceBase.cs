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

        /// <summary>
        /// Sets the aliases.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="alias">The alias.</param>
        public virtual void SetAliases(DataSet dataSet, string alias)
        {
            foreach(DataTable table in dataSet.Tables)
            {
                foreach(DataColumn column in table.Columns.Cast<IEnumerable>().Skip(1).ToList())
                {
                    column.ColumnName = alias + "_" + column.ColumnName;
                }
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
        /// Formats the SQL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <returns>System.String.</returns>
        public virtual string FormatSql(QueryBuilder builder, string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues)
        {
            return builder.BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows, inField, inValues); 
        }

        /// <summary>
        /// Formats the SQL.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <returns>System.String.</returns>
        public virtual string FormatSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues )
        {
            QueryBuilder builder = new QueryBuilder();
            return builder.BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows, inField, inValues);
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

        public abstract DbConnection CreateConnection();




    }
}
