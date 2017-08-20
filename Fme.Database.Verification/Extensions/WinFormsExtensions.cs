using System.Windows.Forms;

namespace Fme.Database.Verification.Extensions
{
    /// <summary>
    /// Class WinFormsExtensions.
    /// </summary>
    public static class WinFormsExtensions
    {
        public static T GetMenuContextOwner<T>(this ToolStripMenuItem sender) where T: class
        {
           // ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip owner = sender.Owner as ContextMenuStrip;            
            return owner?.SourceControl as T;
        }      
    }
}
