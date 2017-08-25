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
        /// Gets or sets the error messages.
        /// </summary>
        /// <value>The error messages.</value>
        [XmlIgnore]
        public List<ErrorMessageModel> ErrorMessages {get;set;}
        

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareModel"/> class.
        /// </summary>
        public CompareModel()
        {
            Source = new DataSourceModel();
            Target = new DataSourceModel();
            ColumnCompare = new List<CompareMappingModel>();
            ErrorMessages = new List<ErrorMessageModel>();
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
        /// Gets the ids from file.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public string[] GetIdsFromFile()
        {
             if (IsValidIdFile() == false)
                return null;

            var list = File.ReadAllLines(Source.IdListFile).
                Where(w => !string.IsNullOrEmpty(w)).ToList();

            if (Source.IsRandom && !string.IsNullOrEmpty(Source.MaxRows))
                list = list.Distinct().ToList().PickRandom(int.Parse(Source.MaxRows)).ToList();

            return list.ToArray();
        }

        #region Calculated Fields. TODO refactor
        /// <summary>
        /// Merges the calculated data.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="side">The side.</param>
        /// <param name="field">The field.</param>
        /// <param name="query">The query.</param>
        /// <param name="in">The in.</param>
        /// <param name="dataSource">The data source.</param>
        private void MergeCalculatedData(DataTable table, string side, string field, string query, string[] @in, 
            DataSourceBase dataSource, CancellationTokenSource cancelToken)
        {
            //lblStatus.Caption = "Executing Calculated Queries: " + side + " " + field;

            QueryBuilder builder = new QueryBuilder();
            var having = builder.CreateInClause(query.Split(new char[] { ' ', ',' })[1], @in);
            var sql = string.Format(" {0} HAVING {1} ", query, having);

            var data = dataSource.ExecuteQuery(sql, cancelToken.Token).Tables[0];
            data.Columns[0].ColumnName = "primary_key";
            data.PrimaryKey = new[] { data.Columns[0] };
            table.InnerJoin<string>("primary_key", data);
            data.Columns[1].ColumnName = side + "_" + field;
            table.Merge(data, false, MissingSchemaAction.AddWithKey);
        }
        /// <summary>
        /// Executes the calculated fields.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public void ExecuteCalculatedFields(DataTable source, DataTable target, CancellationTokenSource cancelToken)
        {
            int index = 0;
            try
            {
                var calcs = ColumnCompare.Where(w => w.IsCalculated && w.Selected);
                foreach (var calc in calcs)
                {
                    if (cancelToken.IsCancellationRequested)
                        throw new OperationCanceledException("A cancellation token associated with this operation was canceled");
                    try
                    {
                        index = 1;
                        MergeCalculatedData(source, "left", calc.LeftSide, calc.LeftQuery, 
                            source.SelectKeys<string>("primary_key"), Source.DataSource, cancelToken);

                        index = 2;
                        MergeCalculatedData(target, "right", calc.RightSide, calc.RightQuery, 
                            target.SelectKeys<string>("primary_key"), Target.DataSource, cancelToken);
                    }
                    catch (Exception ex)
                    {
                        string query = index == 1 ? calc.LeftQuery : calc.RightQuery;
                        string field = index == 1 ? calc.LeftSide : calc.RightSide;

                        this.ErrorMessages.Add(new ErrorMessageModel("Calculated Query", field + " - " + query.Replace(Environment.NewLine, ""), ex.Message));
                        
                        //TODO: Log Calc error;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessages.Add(new ErrorMessageModel("Calculated Fields", ex.Message, ex?.InnerException?.Message));
                return;
            }

        }
        #endregion
        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>CompareModel.</returns>
        public static CompareModel Load(string path)
        {
            return Serializer.DeSerialize<CompareModel>(path);
        }
    }
}
