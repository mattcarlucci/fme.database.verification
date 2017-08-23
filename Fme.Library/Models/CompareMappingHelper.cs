// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-18-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-18-2017
// ***********************************************************************
// <copyright file="CompareMappingHelper.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Fme.Library.Enums;
using Fme.Library.Comparison;
using System.Threading;
using System.IO;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class CompareMappingHelper.
    /// </summary>
    public class CompareMappingHelper
    {
        public static List<string> IgnoreList()
        {
            if (File.Exists(".\\IgnoreAutoMappings.txt"))
                return File.ReadAllLines(".\\IgnoreAutoMappings.txt").ToList();

            return new List<string>();

        }
        /// <summary>
        /// Gets the pairs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>List&lt;CompareMappingModel&gt;.</returns>
        public static List<CompareMappingModel> GetPairs(TableSchemaModel source, TableSchemaModel target)
        {
            var exceptions = IgnoreList();

            return source.Fields.Where(w=> exceptions.Contains(w.Name) == false).
             Join(target.Fields.Where(w => exceptions.Contains(w.Name) == false),
                 s => new { s.Name },
                 t => new { t.Name },
                 (s, t) => new CompareMappingModel(s.Name, t.Name)
             ).ToList();
        }

        /// <summary>
        /// Orders the columns.
        /// </summary>
        /// <param name="master">The master.</param>
        /// <param name="ColumnCompare">The column compare.</param>
        public static void OrderColumns(DataTable master, List<CompareMappingModel> ColumnCompare)
        {
            int ordinal = 0;
            foreach (var item in ColumnCompare)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.LeftSide))
                        master.Columns[item.LeftAlias].SetOrdinal(ordinal++);
                    if (!string.IsNullOrEmpty(item.RightSide))
                        master.Columns[item.RightAlias].SetOrdinal(ordinal++);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// Compares the columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ColumnCompare">The column compare.</param>
        /// <param name="token">The token.</param>
        public static void CompareColumns(CompareRows comparer, DataTable table, CompareModel model, CancellationTokenSource token)
        {
            var items = model.ColumnCompare.Where(w => w.IsPair() && w.Selected);
            foreach (var item in items)
            {
                if (table.Columns.Contains(item.LeftAlias) && table.Columns.Contains(item.RightAlias))
                {
                    var currentRow = model.ColumnCompare.IndexOf(item);
                    var results = comparer.CompareColumns(currentRow, table, item, model, token);
                }               
            }
        }

        /// <summary>
        /// Handles the <see cref="E:StatusEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CompareHelperEventArgs"/> instance containing the event data.</param>
        private static void OnStatusEvent(object sender, CompareHelperEventArgs e)
        {
           
        }
       
    }
}
