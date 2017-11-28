using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Fme.Library.Models;
using Fme.Library.Extensions;

namespace Fme.Database.Verification
{
    /// <summary>
    /// Class frmSchemaEditor.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.XtraForm" />
    public partial class frmSchemaEditor : DevExpress.XtraEditors.XtraForm
    {
        
        bool IsNew { get; set; }
        public TableSchemaModel tableSchema = null;
        public TableSchemaModel compiledSchema = null;
        /// <summary>
        /// The model
        /// </summary>
        private DataSourceModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmSchemaEditor"/> class.
        /// </summary>
        public frmSchemaEditor()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="frmSchemaEditor"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public frmSchemaEditor(DataSourceModel model, string queryName, bool isNew) : this()
        {
            this.IsNew = isNew;
            this.model = model;
            if (isNew == false)
            {
                tableSchema = model.TableSchemas.Where(w => w.TableName == queryName).SingleOrDefault();               
                txtQuery.Text = tableSchema.Query.Replace("\r\n", "\n").Replace("\n", "\r\n");
                txtQueryName.EditValue = queryName;
                txtQueryName.Enabled = false;
            }
        }


        /// <summary>
        /// Handles the Load event of the frmSchemaEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmSchemaEditor_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemClick event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if (Compile())
            {
                UpdateModel();
                this.DialogResult = DialogResult.OK;
                Close();                
            }
        } 
        
        /// <summary>
        /// Handles the ItemClick event of the btnCompile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnCompile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Compile();
        }

        /// <summary>
        /// Updates the model.
        /// </summary>
        private void UpdateModel()
        {
            var schema = model.TableSchemas.Where(w => w.TableName == txtQueryName.EditValue.ToString()).SingleOrDefault();

            if (schema == null)
            {                
                model.TableSchemas.Add(compiledSchema);
                compiledSchema.TableName = txtQueryName.EditValue.ToString();
                compiledSchema.Query = txtQuery.EditValue.ToString();
            }
            else
            {
                foreach (var field in tableSchema.Fields.Where(w => string.IsNullOrEmpty(w.ValidationMacros) == false).ToList())
                {
                    var item = compiledSchema.Fields.Where(w => w.Name == field.Name).SingleOrDefault();
                    if (item != null)
                        item.ValidationMacros = field.ValidationMacros;
                }

                int row = model.TableSchemas.IndexOf(schema);
                model.TableSchemas[row] = compiledSchema;
                compiledSchema.TableName = txtQueryName.EditValue.ToString();
                compiledSchema.Query = txtQuery.EditValue.ToString();
            }            
        }
               
        /// <summary>
        /// Verifies this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Verify()
        {
            if (string.IsNullOrEmpty(txtQueryName?.EditValue?.ToString()))
            {
                MessageBox.Show("Enter a unique name for this Query", "Query Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return false;
            }
            return true;

        }
        /// <summary>
        /// Compiles this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Compile()
        {
            try
            {
                if (Verify() == false) return false;
                Cursor = Cursors.WaitCursor;
                var query = model.DataSource.ExecuteQuery(txtQuery.Text);

                compiledSchema = new TableSchemaModel(query.Table(), txtQueryName.EditValue.ToString(), txtQuery.Text);

                //TODO: Need to update this schema not to lose the validation paramters
                
                gridSourcehSchema.DataSource = compiledSchema.Fields.ToList();
                gridSourcehSchema.RefreshDataSource();
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
                return false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Handles the ItemClick event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}