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
    public class CalcFieldModel
    {
        public int ChunkSize = 0;
        public List<CompareMappingModel> ColumnCompare { get; set; }
        public DataSourceModel Target { get; private set; }
        public DataSourceModel Source { get; private set; }
        public List<ErrorMessageModel> ErrorMessages { get; set; }

        CompareModel Model { get; set; }

        public EventHandler<CompareModelStatusEventArgs> CompareModelStatus;
        protected virtual void OnCompareModelStatus(object sender, CompareModelStatusEventArgs e)
        {
            CompareModelStatus?.Invoke(sender, e);
        }

        public CalcFieldModel(CompareModel model)
        {
            this.Model = model;
            ColumnCompare = model.ColumnCompare;
            Source = model.Source;
            Target = model.Target;
            ErrorMessages = model.ErrorMessages;
           
        }

        public CalcFieldModel(CompareModel model, int chunkSize) : this(model)
        {
            ChunkSize = chunkSize;
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
        /// Logs the debug.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="sql">The SQL.</param>
        private void LogDebug(string field, string sql)
        {
            try
            {
                string file = Alias.Debug + this.Model.GetName() + "." + field + ".sql";

                if (Directory.Exists(Alias.Debug))
                    File.WriteAllText(file, sql);
            }
            catch(Exception)
            {
                return;
            }

        }
        /// <summary>
        /// Merges the calculated data.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="side">The side.</param>
        /// <param name="field">The field.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>System.Data.DataTable.</returns>
        private DataTable MergeCalculatedData(DataTable table, string side, string field, string sql,
                    DataSourceBase dataSource, CancellationTokenSource cancelToken)
        {
            string name = side + "_" + field;

            string file = Alias.Debug + this.Model.GetName() + "." + name + ".sql";

            var query = new QueryMessageModel(sql, name);                   
            query.LogQuery(file);
            
            var results = dataSource.ExecuteQuery(sql, cancelToken.Token);

            query.SetCount(results);
            query.LogQuery(file);

            this.Model.Queries.Add(query);
                    
            if (results.Tables.Count == 0) return null;
            var data = results.MergeAll();
            //  var data = results.Tables[0];
            data = data.ListAggr();

            data.Columns[0].ColumnName = Alias.Primary_Key;
            data.PrimaryKey = new[] { data.Columns[0] };
            table.InnerJoin<string>(Alias.Primary_Key, data);
            data.Columns[1].ColumnName = side + "_" + field;
            return data;
        }

        /// <summary>
        /// Builds the calculated SQL.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        private string BuildCalculatedSql(string query, string[] inValues)
        {
            string having = string.Empty;
            int[] outValues = TryConvert(inValues);

            QueryBuilder builder = new QueryBuilder();

            if (outValues == null)
                having = builder.CreateInClause(query.Split(new char[] { ' ', ',' })[1], inValues);
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
            return sql;
        }


        /// <summary>
        /// Merges the multi calculated data.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="side">The side.</param>
        /// <param name="field">The field.</param>
        /// <param name="query">The query.</param>
        /// <param name="inValues">The in values.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="cancelToken">The cancel token.</param>
        /// <returns>DataTable.</returns>
        public DataTable MergeMultiCalculatedData(DataTable table, string side, string field, string query, string[] inValues,
            DataSourceBase dataSource, CancellationTokenSource cancelToken)
        {
            List<string> sqls = new List<string>();
            inValues.Split(ChunkSize).ToList().ForEach(block =>
            {
                sqls.Add(BuildCalculatedSql(query, block.ToArray()));
            });

            string sql = string.Join(";\r\n", sqls);
            return MergeCalculatedData(table, side, field, sql, dataSource, cancelToken);
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
                this.Model.Queries.Clear();

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

                            OnCompareModelStatus(this, new CompareModelStatusEventArgs(null, "Calculated Field", "Executing " + calc.LeftSide));
                            var data1 = MergeMultiCalculatedData(source, Alias.Left, calc.LeftSide, calc.GetLeftQuery(),
                                source.SelectKeys<string>(Alias.Primary_Key), Source.DataSource, cancelToken);

                            //only merge if the queries execute.
                            if (data1 != null)
                            {
                                source.Merge(data1, false, MissingSchemaAction.AddWithKey);
                                calc.LeftKey = data1.Columns[1].ColumnName;
                            }
                            else if (source.Columns.Contains(Alias.Left_ + calc.LeftSide) == false)
                            {
                                source.Columns.Add(Alias.Left_ + calc.LeftSide, typeof(string));
                                calc.LeftKey = Alias.Left_ + calc.LeftSide;
                            }

                        }

                        index = 2;
                        if (string.IsNullOrEmpty(calc.GetRightQuery()) == false)
                        {
                            OnCompareModelStatus(this, new CompareModelStatusEventArgs(null, "Calculated Field", "Executing " + calc.RightAlias));
                            var data2 = MergeMultiCalculatedData(target, Alias.Right, calc.RightSide, calc.GetRightQuery(),
                            target.SelectKeys<string>(Alias.Primary_Key), Target.DataSource, cancelToken);

                            if (data2 != null)
                            {
                                target.Merge(data2, false, MissingSchemaAction.AddWithKey);
                                calc.RightKey = data2.Columns[1].ColumnName;
                            }
                            else if (target.Columns.Contains(Alias.Right_ + calc.RightSide) == false)
                            {
                                
                                target.Columns.Add(Alias.Right_ + calc.RightSide, typeof(string));
                                calc.RightKey = Alias.Right_ + calc.RightSide;
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
    }
}
