using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Database.Verification.Extensions
{
     /// <summary>
    /// Class GridViewExtensions.
    /// </summary>
    public static class GridViewExtensions
    {     
        /// <summary>
        /// Hides the columns.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="fields">The fields.</param>
        public static void HideEmptyColumn(this GridView view, IEnumerable<string> fields)
        {
            view.Columns.
                Where(s => fields.Contains(s.FieldName)).
                ToList().ForEach(item => item.Visible = false );            
        }
      
        /// <summary>
        /// Resets the data source.
        /// </summary>
        /// <param name="grid">The control.</param>
        /// <param name="dataSource">The data source.</param>
        public static void ResetDataSource(this GridControl grid, object dataSource)
        {
            grid.BeginUpdate();
            GridView view = grid.MainView as GridView;

            view?.Columns.Clear();
            grid.DataSource = null;
            grid.RefreshDataSource();
            grid.DataSource = dataSource;
            grid.RefreshDataSource();
            grid.EndUpdate();
        }
        /// <summary>
        /// Bests the width of the fit.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <param name="bestFitColumns">if set to <c>true</c> [best fit columns].</param>
        /// <param name="columnAutoWidth">if set to <c>true</c> [column automatic width].</param>
        public static void BestFitWidth(this GridControl grid, bool columnAutoWidth  = true, bool bestFitColumns  = false)
        {
            GridView view = grid.MainView as GridView;
            view.OptionsView.ColumnAutoWidth = columnAutoWidth;
            view.BestFitColumns(bestFitColumns);
        }

        /// <summary>
        /// Bests the width of the fit.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="bestFitColumns">if set to <c>true</c> [best fit columns].</param>
        /// <param name="columnAutoWidth">if set to <c>true</c> [column automatic width].</param>
        public static void BestFitWidth(this GridView view, bool columnAutoWidth = true, bool bestFitColumns = false)
        {   
            view.OptionsView.ColumnAutoWidth = columnAutoWidth;
            view.BestFitColumns(bestFitColumns);
        }


    }
}
