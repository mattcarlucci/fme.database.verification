// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="DqlDataSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.DqlProvider;
using Fme.Library.Enums;
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections;
using Fme.Library.Extensions;

namespace Fme.Library
{
    /// <summary>
    /// Class DqlDataSource.
    /// </summary>
    /// <seealso cref="Fme.Library.DataSourceBase" />
    public class DqlDataSource : DataSourceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataSource" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DqlDataSource(string connectionString) : base(connectionString)
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataSource"/> class.
        /// </summary>
        public DqlDataSource()
        {
        }

        /// <summary>
        /// Sets the aliases.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="alias">The alias.</param>
        public override void SetAliases(DataTable table, string alias)
        {
            if (table == null || table.Columns.Count == 0) return;
            
            for (int i = 2; i < table.Columns.Count; i++)
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
        public override DataSet ExecuteQuery(string select, CancellationToken token)
        {
            DataSet dataSet = null;           
            
            using (var cn = new DqlConnection(ConnectionString))
            {
                cn.Open();
                using (DqlCommand cmd = new DqlCommand(select, cn))
                {
                    using (DqlDataAdapter adapter = new DqlDataAdapter(cmd))
                    {
                        dataSet = new DataSet();
                        adapter.Fill(dataSet, token);
                    }
                }
            }
            return dataSet;
        }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <returns>DataSet.</returns>
        public override DataSet ExecuteQuery(string select)
        {
            DataSet dataSet = null;

            using (var cn = new DqlConnection(ConnectionString))
            {
                cn.Open();
                using (DqlCommand cmd = new DqlCommand(select, cn))
                {
                    using (DqlDataAdapter adapter = new DqlDataAdapter(cmd))
                    {
                        dataSet = new DataSet();
                        adapter.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }

        /// <summary>
        /// Fills the schema model.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="item">The item.</param>
        private void FillSchemaModel(TableSchemaModel table,  IGrouping<string, DataRow> item)
        {
            DqlDataTypeMap map = new DqlDataTypeMap();

            table.TableName = item.Key;
            int ordinal = 0;
            table.Fields.AddRange(item.Select(s => new FieldSchemaModel()
            {
                Name = s.Field<string>("attr_name"),
                Ordinal = ordinal++,
                Type = map[(int)s["attr_type"]].ToString(),
                MaxLength = (int)s["attr_length"],
                IsRepeating = Convert.ToBoolean(s["attr_repeating"]),
                TableName = item.Key
            }).ToArray());
        }
        /// <summary>
        /// Gets the schema model.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>TableSchemaModel.</returns>
        public override TableSchemaModel GetSchemaModel(string tableName)
        {            
            TableSchemaModel table = new TableSchemaModel();
            using (DqlConnection cn = new DqlConnection(ConnectionString))
            {
                cn.Open();
                var schema = cn.GetSchema(tableName).AsEnumerable().GroupBy(g => g.Field<string>("name"));

                foreach (var item in schema)
                {
                    FillSchemaModel(table, item);
                }

            }
            return table;
        }
        /// <summary>
        /// Gets the schema model.
        /// </summary>
        /// <returns>List&lt;TableSchemaModel&gt;.</returns>
        public override List<TableSchemaModel> GetSchemaModel()
        {
            DqlDataTypeMap map = new DqlDataTypeMap();
            List<TableSchemaModel> tableSchemaModel = new List<TableSchemaModel>();

            using (DqlConnection cn = new DqlConnection(ConnectionString))
            {
                cn.Open();
                var schema = cn.GetSchema().AsEnumerable().GroupBy(g => g.Field<string>("name"));

                foreach (var item in schema)
                {                   
                    TableSchemaModel table = new TableSchemaModel();
                    tableSchemaModel.Add(table);
                    FillSchemaModel(table, item);                   
                }
            }
            return tableSchemaModel;        
        }
        /// <summary>
        /// Fills the schema.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>List&lt;TableSchemaModel&gt;.</returns>
        protected override List<TableSchemaModel> FillSchema(string select, string tableName)
        {
            var dataset = ExecuteQuery(select);

            List<TableSchemaModel> tableSchemaModel = new List<TableSchemaModel>();

            var schema = dataset.Tables[0];

            TableSchemaModel table = new TableSchemaModel();
            tableSchemaModel.Add(table);
            table.TableName = tableName;

            DqlDataTypeMap map = new DqlDataTypeMap();
            //attr_type, min_length, attr_length, attr_repeating, mandatory

            int ordinal = 0;
            var fields = schema.AsEnumerable().Select(s => new FieldSchemaModel
            {
                Name = (string)s["attr_name"],
                MaxLength = (int)s["attr_length"],
                Type = map[(int)s["attr_type"]].ToString(),
                IsRepeating = int.Parse(s["attr_repeating"].ToString()) == 0 ? false : true,
                Ordinal = ordinal++,
                 TableName = tableName
             } );
            table.Fields.AddRange(fields);
            return tableSchemaModel;
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
        public override string FormatSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows,string inField, string[] inValues)
        {
            DqlQueryBuilder builder = new DqlQueryBuilder();
            return builder.BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows, inField, inValues);
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
        public override string FormatSql(QueryBuilder builder, string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues)
        {
            return base.FormatSql(builder, primaryKey, fields, tableName, aliasPrefix, maxRows, inField, inValues);
        }
              
       

        /// <summary>
        /// Gets the query builder.
        /// </summary>
        /// <returns>QueryBuilder.</returns>
        public override QueryBuilder GetQueryBuilder()
        {
            return new DqlQueryBuilder();
        }

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>DqlConnectionStringBuilder.</returns>
        public override DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new DqlConnectionStringBuilder(this.ConnectionString);
        }

        public override DbConnection CreateConnection()
        {
            return new DqlConnection();
        }
    }
}
