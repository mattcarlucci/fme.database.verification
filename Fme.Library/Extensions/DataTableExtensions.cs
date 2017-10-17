using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Copies to data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>DataTable.</returns>
        public static DataTable CopyToDataTable(this DataTable table)
        {
            var target = table.AsEnumerable().CopyToDataTable();
            for (int index = 0; index < table.Columns.Count; index++)
                target.Columns[index].Caption = table.Columns[index].Caption;

            return target;
        }
        /// <summary>
        /// Copies to data table.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns>DataTable.</returns>
        public static DataTable CopyToDataTable(this DataSet ds)
        {
            var table = ds.Tables[0];
            var target = table.AsEnumerable().CopyToDataTable();
            for (int index = 0; index < table.Columns.Count; index++)
                target.Columns[index].Caption = table.Columns[index].Caption;

            return target;
        }

        /// <summary>
        /// Does a ListAggr for calculated fields only. 
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ListAggr(this DataTable table)
        {
            var copy = table.Clone();

            var q = from row in table.AsEnumerable()
                        //group row by (string)row[0] into idGroup
                    group row by Convert.ChangeType(row[0], typeof(string)) into idGroup
                    select new
                    {
                        idGroup.Key,
                        Value = string.Join("|", idGroup.Select(s => s[1]))
                    };

            foreach (var item in q)
            {
                var d = copy.NewRow();
                d[0] = item.Key;
                d[1] = item.Value;
                copy.Rows.Add(d);
            }
            return copy;
        }
        /// <summary>
        /// Groups the merge.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <param name="key">The key.</param>
        /// <returns>DataTable.</returns>
        public static DataTable CalculatedMerge<T>(this DataTable table)
        {
            var result = table.Clone();

            var duplicates = table.AsEnumerable()
             .Select(dr => (T)Convert.ChangeType(dr[0], typeof(T)))
             .GroupBy(x =>
             x)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key)
             .ToList();

            foreach(var item in duplicates)
            {                
                //TODO: do the merge. replaced with ListAggr for now. Simple convention only
                throw new NotImplementedException();
            }
            return result;
        }
    }
}
