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


        public void MapColumnsKeys(DataSet data, Func<List<CompareMappingModel>, int, string, string> selector)
        {
            MapColumnsKeys(data.Table(), selector);
        }

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
        /// Tries to convert to numbers 
        /// </summary>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.Int32[].</returns>
        private int[] TryConvert(string[] inValues)
        {
            try
            {
                return Array.ConvertAll(inValues, int.Parse);

            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Merges the calculated data.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="side">The side.</param>
        /// <param name="field">The field.</param>
        /// <param name="query">The query.</param>
        /// <param name="inValues">The in.</param>
        /// <param name="dataSource">The data source.</param>
        private DataTable MergeCalculatedData(DataTable table, string side, string field, string query, string[] inValues, 
            DataSourceBase dataSource, CancellationTokenSource cancelToken)
        {
            string having = string.Empty;
            int[] outValues = TryConvert(inValues);

            QueryBuilder builder = new QueryBuilder();
                
            if (outValues == null)
                having = builder.CreateInClause(query.Split(new char[] { ' ', ',' })[1],  inValues);
            else
               having = builder.CreateInClause(query.Split(new char[] { ' ', ',' })[1], outValues);

            var sql = string.Format(" {0} HAVING {1} ", query, having);

            int lastgroupBy = query.ToLower().LastIndexOf("group by");
            int lastEnable = query.ToLower().Substring(lastgroupBy).IndexOf("enable");

            if (lastEnable > 0)
            {
                var gb = query.Substring(lastgroupBy + lastEnable);
                var newQuery = query.Replace(gb, " ");
                sql = string.Format(" {0} HAVING {1} {2} ", newQuery, having, gb);
            }

            

            var results = dataSource.ExecuteQuery(sql, cancelToken.Token);
            if (results.Tables.Count == 0) return null;

            var data =  results.Tables[0];
            data = data.ListAggr();

            data.Columns[0].ColumnName = Alias.Primary_Key;
            data.PrimaryKey = new[] { data.Columns[0] };
            table.InnerJoin<string>(Alias.Primary_Key, data);
            data.Columns[1].ColumnName = side + "_" + field;
            return data;
            //table.Merge(data, false, MissingSchemaAction.AddWithKey);
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
                        if (string.IsNullOrEmpty(calc.GetLeftQuery()) == false)
                        {
                            var data1 = MergeCalculatedData(source, Alias.Left, calc.LeftSide, calc.GetLeftQuery(),
                                source.SelectKeys<string>(Alias.Primary_Key), Source.DataSource, cancelToken);

                            //only merge if the queries execute.
                            if (data1 != null)
                            {
                                source.Merge(data1, false, MissingSchemaAction.AddWithKey);
                                calc.LeftKey = data1.Columns[1].ColumnName;
                            }

                        }

                        index = 2;
                        if (string.IsNullOrEmpty(calc.GetRightQuery()) == false)
                        {
                            var data2 = MergeCalculatedData(target, Alias.Right, calc.RightSide, calc.GetRightQuery(),
                            target.SelectKeys<string>(Alias.Primary_Key), Target.DataSource, cancelToken);

                            if (data2 != null)
                            {
                                target.Merge(data2, false, MissingSchemaAction.AddWithKey);
                                calc.RightKey = data2.Columns[1].ColumnName;
                            }
                        }

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
