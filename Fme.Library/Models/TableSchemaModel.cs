using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class TableSchemaModel.
    /// </summary>
    [Serializable]
    public class TableSchemaModel
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get;set;}
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public List<FieldSchemaModel> Fields { get; set; }
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the is custom.
        /// </summary>
        /// <value>The is custom.</value>
        public bool IsCustom { get; set; }

        /// <summary>
        /// Gets the SQL fields.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns>System.String.</returns>
        public string GetSqlFields(string prefix)
        {
            return string.Join(",", Fields.Select(s => s.GetSqlFields(prefix)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSchemaModel"/> class.
        /// </summary>
        public TableSchemaModel()
        {
            Fields = new List<FieldSchemaModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSchemaModel"/> class.
        /// </summary>
        /// <param name="queryName">Name of the query.</param>
        public TableSchemaModel(string queryName)
        {
            this.TableName = queryName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSchemaModel"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public TableSchemaModel(DataTable source, string tableName, string query): this()
        {
            this.TableName = TableName;
            this.Query = query;
            this.IsCustom = true;

            foreach(DataColumn col in source.Columns)
            {
                Fields.Add(new FieldSchemaModel(col, tableName));
            }
        }
       
    }
}
