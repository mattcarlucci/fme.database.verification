// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="OleDbDataSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Enums;
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading;
using System.Data.Common;

namespace Fme.Library
{
    /// <summary>
    /// Class OleDbDataSource.
    /// </summary>
    /// <seealso cref="Fme.Library.DataSourceBase" />
    [Serializable]
    public class OleDbDataSource : DataSourceBase
    {       
        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbDataSource" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public OleDbDataSource(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OleDbDataSource"/> class.
        /// </summary>
        public OleDbDataSource()
        {
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="token">The token.</param>
        /// <returns>DataSet.</returns>
        public override DataSet ExecuteQuery(string select, CancellationToken token)
        {
            DataSet dataset = new DataSet();
            using (var cn = new OleDbConnection(ConnectionString))
            {
                cn.Open();
                using (OleDbCommand cmd = new OleDbCommand(select, cn))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dataset);                     
                    }
                }
            }
            return dataset;
        }
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <returns>DataSet.</returns>
        public override DataSet ExecuteQuery(string select)
        {
            return ExecuteQuery(select, new CancellationToken());
        }
      
       
        /// <summary>
        /// Fills the schema model.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="item">The item.</param>
        private void FillSchemaModel(TableSchemaModel table, IGrouping<string, DataRow> item)
        {
            OleDbDataTypeMapping map = OleDbDataTypeMapping.Instance;

            table.TableName = item.Key;
            table.Fields.AddRange(item.Select(s => new FieldSchemaModel()
            {
                Name = s.Field<string>("COLUMN_NAME"),
                Ordinal = s.Field<Int64>("ORDINAL_POSITION"),
                Type = map.GetDataType(s.Field<object>("DATA_TYPE")), // Enum.GetName(typeof(DataTypeEnums), s.Field<object>("DATA_TYPE")),
                MaxLength = s.Field<Int64?>("CHARACTER_MAXIMUM_LENGTH"),
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
            using (OleDbConnection cn = new OleDbConnection(ConnectionString))
            {
                cn.Open();
                cn.GetSchema("Tables");

                var schema = cn.GetSchema("Columns").AsEnumerable().GroupBy(g => g.Field<string>("TABLE_NAME"));
                foreach (var item in schema.Where(w=> w.Key == tableName))
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
            List<TableSchemaModel> tableSchemaModel = new List<TableSchemaModel>();

            using(OleDbConnection cn = new OleDbConnection(ConnectionString))
            {
                cn.Open();
                cn.GetSchema("Tables");

                var schema = cn.GetSchema("Columns").AsEnumerable().GroupBy(g => g.Field<string>("TABLE_NAME"));
                foreach (var item in schema)
                {
                    // var type = item.Select(s => s.Table.Columns[index++].DataType.ToString()).Single();
                    TableSchemaModel table = new TableSchemaModel();
                    tableSchemaModel.Add(table);
                    FillSchemaModel(table, item);
                }
            }
            return tableSchemaModel;
        }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>DataTable.</returns>
        private DataTable GetSchema(string table)
        {
           // try
           // {
                using (var cn = new OleDbConnection(ConnectionString))
                {
                    cn.Open();
                    cn.GetSchema("Tables");
                    return cn.GetSchema("Columns");
                }
          //  }
          //  catch (Exception ex)
          //  {                
          //      return null;
          //  }
        }

       
        /// <summary>
        /// Gets the query builder.
        /// </summary>
        /// <returns>QueryBuilder.</returns>
        public override QueryBuilder GetQueryBuilder()
        {
            return new QueryBuilder();
        }

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>DqlConnectionStringBuilder.</returns>
        public override DbConnectionStringBuilder GetConnectionStringBuilder()
        {

            var builder = new DbConnectionStringBuilder()
            {
                ConnectionString = this.ConnectionString
            };
            return builder;
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns>DbConnection.</returns>
        public override DbConnection CreateConnection()
        {
            return new OleDbConnection();
        }
    }
}
