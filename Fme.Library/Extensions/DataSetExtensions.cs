using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Extensions
{
    public static class DataSetExtensions
    {
        /// <summary>
        /// Inners the join.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        public static void InnerJoin<T>(this DataTable source, string key, DataTable data)
        {
            var missing = data.AsEnumerable().
               Select(s => s.Field<T>(key)).
                   Except(source.AsEnumerable().Select(s2 => s2.Field<T>(key))).ToList();

            data.AsEnumerable().
              Where(w => missing.Contains(w.Field<T>(key)) == true).
                  ToList().ForEach(row => data.Rows.Remove(row));
        }
        /// <summary>
        /// Removes the duplicates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string RemoveDuplicates<T>(this DataTable dt, string key)
        {

            StringBuilder sb = new StringBuilder();
            var duplicates = dt.AsEnumerable()
              .Select(dr => dr.Field<T>(key))
              .GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(g => g.Key)
              .ToList();

            duplicates.ForEach(item =>
                sb.AppendLine(string.Format("Removing duplicate {0} = {1}", key, item)));

            dt.AsEnumerable().
             Where(w => duplicates.Contains(w.Field<T>(key)) == true).
                 ToList().ForEach(row => dt.Rows.Remove(row));
           
            return sb.ToString();
        }

        /// <summary>
        /// Removes the empty rows.
        /// </summary>
        /// <param name="table">The table.</param>
        public static DataTable RemoveEmptyRows(this DataTable table)
        {
            table.AsEnumerable().Where(w => w.ItemArray == null).ToList().ForEach(item => table.Rows.Remove(item));
            return table;
        }

        /// <summary>
        /// Sets the primary key.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="targets">The targets.</param>
        public static void SetPrimaryKey(this DataTable source, string columnName , params DataTable[] targets)
        {
            source.PrimaryKey = new[] { source.Columns[columnName] };
            foreach(DataTable target in targets)
                target.PrimaryKey = new[] { target.Columns[columnName] };
        }

        /// <summary>
        /// Selects the keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <param name="field">The field.</param>
        /// <returns>T[].</returns>
        public static T[] SelectKeys<T>(this DataTable table, string field)
        {
            return table.AsEnumerable().Select(s => s.Field<T>(field)).ToArray();
        }
        /// <summary>
        /// Removes the empty columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>DataColumn[].</returns>
        public static DataColumn[] RemoveEmptyColumns(this DataTable table)
        {
            List<DataColumn> remove = new List<DataColumn>();

            foreach (DataColumn col in table.Columns)
                if (table.AsEnumerable().
                        Select(row => Convert.ToString(row[col.ColumnName])).
                            Where(w => !string.IsNullOrEmpty(w)).Count() == 0)
                {
                    remove.Add(col);
                }

            remove.ForEach(item => table.Columns.Remove(item));
            return remove.ToArray();
        }

     


    }
}
