// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-23-2017
// ***********************************************************************
// <copyright file="DataSourceModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fme.Library.Enums;
using System.Reflection;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class DataSourceModel.
    /// </summary>
    [Serializable]
    public class DataSourceModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
                
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
       
        /// <summary>
        /// Gets or sets the table schemas.
        /// </summary>
        /// <value>The table schemas.</value>
        public List<TableSchemaModel> TableSchemas {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether this instance is random.
        /// </summary>
        /// <value><c>true</c> if this instance is random; otherwise, <c>false</c>.</value>
        public bool IsRandom { get; set; }
        /// <summary>
        /// Gets or sets the maximum rows.
        /// </summary>
        /// <value>The maximum rows.</value>
        public string MaxRows { get; set; }
        /// <summary>
        /// Gets or sets the identifier list.
        /// </summary>
        /// <value>The identifier list.</value>
        public string IdList { get; set; }
        /// <summary>
        /// Gets or sets the identifier list file.
        /// </summary>
        /// <value>The identifier list file.</value>
        public string IdListFile { get; set; }              
        /// <summary>
        /// Gets or sets the selected table.
        /// </summary>
        /// <value>The selected table.</value>
        public string SelectedTable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include versions].
        /// </summary>
        /// <value><c>true</c> if [include versions]; otherwise, <c>false</c>.</value>
        public bool IncludeVersions { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [include deleted versions].
        /// </summary>
        /// <value><c>true</c> if [include deleted versions]; otherwise, <c>false</c>.</value>
        public bool IncludeDeletedVersions { get; set; }
        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        public int TimeZoneOffset { get; set; }
               

        /// <summary>
        /// Determines whether [is key string].
        /// </summary>
        /// <returns>System.String.</returns>
        public Type KeyType
        {
            get
            {
                var field = SelectedSchema().Fields.Where(w => w.Name == Key).SingleOrDefault();
                var datatype = field == null ? "System.String" : field.Type;
                Type type = Type.GetType(datatype); 
                return type ?? typeof(string);              
            }
        }
        
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public DataSourceBase DataSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceModel"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public DataSourceModel(DataSourceBase dataSource)
        {
            DataSource = DataSource;            
            TableSchemas = dataSource.GetSchemaModel();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceModel"/> class.
        /// </summary>
        public DataSourceModel()
        {
            TableSchemas = new List<TableSchemaModel>();
        }
        /// <summary>
        /// Loads the schema.
        /// </summary>
        public void LoadSchema()
        {
            TableSchemas = DataSource.GetSchemaModel();
        }
        /// <summary>
        /// Selecteds the schema.
        /// </summary>
        /// <returns>TableSchemaModel.</returns>
        public TableSchemaModel SelectedSchema()
        {
            return TableSchemas.Where(w => w.TableName == SelectedTable).FirstOrDefault();
        }
        

      
    }
}
