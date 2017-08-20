using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fme.Database.Verification.Extensions
{
    public static class WinFormsExtensions
    {
        public static T GetMenuContextOwner<T>(this ToolStripMenuItem sender) where T: class
        {
           // ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip owner = sender.Owner as ContextMenuStrip;            
            return owner?.SourceControl as T;
        }      
    }


    public static class GridViewExtensions
    {

        /// <summary>
        /// Hides the columns.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="fields">The fields.</param>
        public static void HideColumns(this GridView view, IEnumerable<string> fields)
        {
            view.Columns.
                Where(s => fields.Contains(s.FieldName)).
                ToList().ForEach(item => item.Visible = false);
        }
        /// <summary>
        /// Resets the data source.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        /// <param name="dataSource">The data source.</param>
        public static void ResetDataSource(this GridControl ctrl, object dataSource)
        {
            ctrl.BeginUpdate();
            GridView view = ctrl.MainView as GridView;
            view?.Columns.Clear();
            ctrl.DataSource = null;
            ctrl.RefreshDataSource();
            ctrl.DataSource = dataSource;
            ctrl.RefreshDataSource();
            ctrl.EndUpdate();
        }

      
    }
}
