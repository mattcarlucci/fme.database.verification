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
    }
}
