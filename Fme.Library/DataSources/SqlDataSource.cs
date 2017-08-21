// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="SqlDataSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Fme.Library.Enums;
using System.Threading;
using System.Data.Common;

namespace Fme.Library
{

    /// <summary>
    /// Class SqlDataSource.
    /// </summary>
    /// <seealso cref="Fme.Library.DataSourceBase" />
    public class SqlDataSource : DataSourceBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataSource"/> class.
        /// </summary>
        public SqlDataSource()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataSource" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlDataSource(string connectionString) : base(connectionString)
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
            using (var cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(select, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
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
            table.TableName = item.Key;
            table.Fields.AddRange(item.Select(s => new FieldSchemaModel()
            {
                Name = s.Field<string>("COLUMN_NAME"),
                Ordinal = s.Field<Int64>("ORDINAL_POSITION"),
                Type = Enum.GetName(typeof(DataTypeEnums), s.Field<object>("DATA_TYPE")),
                //  Type = s.Field<object>("DATA_TYPE").ToString(),
                MaxLength = s.Field<Int64?>("CHARACTER_MAXIMUM_LENGTH"),
                TableName = item.Key
            }).ToArray());
        }
        /// <summary>
        /// Gets the schema model.
        /// </summary>
        /// <returns>List&lt;TableSchemaModel&gt;.</returns>
        public override List<TableSchemaModel> GetSchemaModel()
        {
            List<TableSchemaModel> tableSchemaModel = new List<TableSchemaModel>();

            using (SqlConnection cn = new SqlConnection(ConnectionString))
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
        /// Gets the schema model.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>TableSchemaModel.</returns>
        public override TableSchemaModel GetSchemaModel(string tableName)
        {
            TableSchemaModel table = new TableSchemaModel();
            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                cn.GetSchema("Tables", new string[] { tableName });

                var schema = cn.GetSchema("Columns", new string[] { tableName }).AsEnumerable().GroupBy(g => g.Field<string>("TABLE_NAME"));
                foreach (var item in schema)
                {
                    table.TableName = item.Key;
                    table.Fields.AddRange(item.Select(s => new FieldSchemaModel()
                    {
                        Name = s.Field<string>("COLUMN_NAME"),
                        Ordinal = s.Field<Int64>("ORDINAL_POSITION"),
                        Type = Enum.GetName(typeof(DataTypeEnums), s.Field<object>("DATA_TYPE")),
                        MaxLength = s.Field<Int64?>("CHARACTER_MAXIMUM_LENGTH"),
                        TableName = item.Key
                    }).ToArray());
                }
            }
            return table;
        }

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>DqlConnectionStringBuilder.</returns>
        public override DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder(this.ConnectionString);
            
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
        /// Creates the connection.
        /// </summary>
        /// <returns>DbConnection.</returns>
        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }
    }
}
