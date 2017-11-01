// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-17-2017
// ***********************************************************************
// <copyright file="CompareModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Fme.Library.Enums;
using Fme.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class CompareModel.
    /// </summary>
    [Serializable]
    public class CompareModel
    {
        /// <summary>
        /// The chunk size
        /// </summary>
        private int chunkSize;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public DataSourceModel Source { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public DataSourceModel Target { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public DataSourceModel Result { get; set; }
        /// <summary>
        /// Gets or sets the column compare.
        /// </summary>
        /// <value>The column compare.</value>
        public List<CompareMappingModel> ColumnCompare { get; set; }

        /// <summary>
        /// Calculateds the fields.
        /// </summary>
        /// <returns>List&lt;CompareMappingModel&gt;.</returns>
        public List<CompareMappingModel> CalculatedFields()
        {
            return ColumnCompare.Where(w => w.IsCalculated).ToList();
        }

        /// <summary>
        /// Gets or sets the error messages.
        /// </summary>
        /// <value>The error messages.</value>
        [XmlIgnore]
        public List<ErrorMessageModel> ErrorMessages {get;set;}

        /// <summary>
        /// Gets or sets the queries.
        /// </summary>
        /// <value>The queries.</value>
        [XmlIgnore]
        public List<QueryMessageModel> Queries { get; set; }

        [XmlIgnore]
        public EventHandler<CompareModelStatusEventArgs> CompareModelStatus;
        protected virtual void OnCompareModelStatus(object sender, CompareModelStatusEventArgs e)
        {
            CompareModelStatus?.Invoke(sender, e);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareModel"/> class.
        /// </summary>
        public CompareModel()
        {
            Source = new DataSourceModel();
            Target = new DataSourceModel();
            ColumnCompare = new List<CompareMappingModel>();
            ErrorMessages = new List<ErrorMessageModel>();
            Queries = new List<QueryMessageModel>();
            chunkSize = 1500;
        }

        public CompareModel(int chunkSize) : this()
        {
            this.chunkSize = chunkSize;
        }
        

        /// <summary>
        /// Maps the table columns.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="selector">The selector.</param>
        public void MapColumnCaptions(DataSet data, Func<List<CompareMappingModel>, int, string> selector)
        {
            MapColumnCaptions(data.Tables[0], selector);
        }
        

        /// <summary>
        /// Maps the table columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="selector">The selector.</param>
        public void MapColumnCaptions(DataTable table, Func<List<CompareMappingModel>, int , string> selector)
        {
            var f1 = this.ColumnCompare.Where(w => w.IsCalculated == false && w.Selected == true).
                OrderBy(o => o.Ordinal).ToList();

            int i = 0;
            for (i = 0; i < table.Columns.Count; i++)
                if (table.Columns[i].ColumnName == Alias.Primary_Key)
                    break;
            int y = ++i;

            for (; i < table.Columns.Count; i++)
            {
                table.Columns[i].SetHeading(selector(f1, i - y));                
            }
        }


        /// <summary>
        /// Maps the columns keys.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="selector">The selector.</param>
        public void MapColumnsKeys(DataSet data, Func<List<CompareMappingModel>, int, string, string> selector)
        {
            MapColumnsKeys(data.Table(), selector);
        }

        /// <summary>
        /// Maps the columns keys.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="selector">The selector.</param>
        public void MapColumnsKeys(DataTable table, Func<List<CompareMappingModel>, int, string, string> selector)
        {
            var f1 = this.ColumnCompare.Where(w => w.IsCalculated == false && w.Selected == true).
                OrderBy(o => o.Ordinal).ToList();

            int i = 0;
            for (i = 0; i < table.Columns.Count; i++)
                if (table.Columns[i].ColumnName == Alias.Primary_Key)
                    break;
            int y = ++i;

            for (; i < table.Columns.Count; i++)
            {
                selector(f1, i - y, table.Columns[i].ColumnName);                
            }
        }

        
        /// <summary>
        /// Sets the compare ordinal.
        /// </summary>
        public void SetCompareOrdinal()
        {
            int index = 0;
            foreach(var col in ColumnCompare)
            {
                col.Ordinal = index++;
            }
        }

        /// <summary>
        /// Determines whether [is valid identifier file].
        /// </summary>
        /// <returns><c>true</c> if [is valid identifier file]; otherwise, <c>false</c>.</returns>
        public bool IsValidIdFile()
        {
            if (File.Exists(Source?.IdListFile) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Determines whether [has identifier file].
        /// </summary>
        /// <returns><c>true</c> if [has identifier file]; otherwise, <c>false</c>.</returns>
        public bool HasIdFile()
        {
            return (string.IsNullOrEmpty(Source.IdListFile));
        }

        /// <summary>
        /// Gets the source filter.
        /// </summary>
        /// <returns>System.String[].</returns>
        public string GetSourceFilter()
        {
            if (this.Source.IdListFile.ToLower().EndsWith(".sql"))
            {
                if (File.Exists(this.Source.IdListFile))
                {
                    return File.ReadAllText(this.Source.IdListFile);                   
                }
               
            }
            return null;
        }
        /// <summary>
        /// Gets the ids from file.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public string[] GetSourceIds()
        {
             if (IsValidIdFile() == false)
                return null;

            if (GetSourceFilter() != null)
                return null;

            var list = File.ReadAllLines(Source.IdListFile).
                Where(w => !string.IsNullOrEmpty(w)).ToList();

            if (Source.IsRandom && !string.IsNullOrEmpty(Source.MaxRows))
                list = list.Distinct().ToList().PickRandom(int.Parse(Source.MaxRows)).ToList();

            return list.ToArray();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetName()
        {
            return Path.GetFileNameWithoutExtension(Name);            
        }
        
        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>CompareModel.</returns>
        public static CompareModel Load(string path)
        {
            return Serializer.DeSerialize<CompareModel>(path);
        }

        internal void ExecuteCalculatedFields(DataTable table1, DataTable table2, CancellationTokenSource cancelToken)
        {
            CalcFieldModel calc = new CalcFieldModel(this, chunkSize);
            calc.CompareModelStatus += CompareModelStatus;
            calc.ExecuteCalculatedFields(table1, table2, cancelToken);
            
        }
    }
}
