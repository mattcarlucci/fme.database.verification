
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Fme.Library.Models;
using Fme.Library;
using System.Data.Common;
using System.Data.SqlClient;
using Fme.DqlProvider;
using System.IO;
using System.Data.OleDb;

namespace Fme.Database.Verification
{
    /// <summary>
    /// Class frmConnection.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.XtraForm" />
    public partial class frmConnection : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        DataSourceModel Model { get; set; }
        /// <summary>
        /// The providers
        /// </summary>
        DbConnectionStringFactory providers = new DbConnectionStringFactory();
        /// <summary>
        /// The datasources
        /// </summary>
        DbDataSourceFactory datasources = new DbDataSourceFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="frmConnection" /> class.
        /// </summary>
        public frmConnection()
        {
            InitializeComponent();
            this.cbProviders.Properties.Items.AddRange(providers.Select(s => s.Key).ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="frmConnection" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public frmConnection(DataSourceModel model) : this()
        {
            this.Model = model;            
            var builder = model.DataSource?.GetConnectionStringBuilder();

            if (builder != null)
            {

                builder.TryGetValue("Provider", out object value);
                cbProviders.EditValue = Model.DataSource.ProviderName;

                builder.TryGetValue("User Id", out value);
                txtUserId.Text = value as string;

                builder.TryGetValue("Password", out value);
                txtPassword.Text = value as string;

                builder.TryGetValue("Data Source", out value);
                btnDataSource.Text = value as string;

                builder.TryGetValue("Extended Properties", out value);
                if (string.IsNullOrEmpty(value as string) == false)
                    txtExt.Text = string.Format("'{0}'", value);
                else
                    txtExt.Text = value as string;
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            pbar.Text = "Retrieving Schema...";
            pbar.Visible = true;
            //  this.Enabled = false;

            //  await Task.Run(() =>
            //  {
            if (CreateDataSource())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
          //  });
         //   this.Enabled = true;
            pbar.Visible = false;
            
        }

      
        /// <summary>
        /// Creates the data source.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CreateDataSource()
        {
          
            try
            {
                Cursor = Cursors.WaitCursor;
                if (cbProviders.Text == "Microsoft Excel")
                    CreateExcel();
                if (cbProviders.Text == "OpenText Documentum")
                    CreateDocumentum();
                if (cbProviders.Text == "Microsoft Access")
                    CreateAccess();
                if (cbProviders.Text == "Microsoft Sql Server")
                    CreateSqlServer();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Creates the SQL server.
        /// </summary>
        private void CreateSqlServer()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(btnDataSource.Text)
            {
                ["User ID"] = txtUserId.Text,
                ["Password"] = txtPassword.Text
            };
            Model.DataSource = new SqlDataSource(builder.ConnectionString);
            Model.TableSchemas = Model.DataSource.GetSchemaModel();
            Model.DataSource.ProviderName = "Microsoft Sql Server";
        }

        /// <summary>
        /// Creates the access.
        /// </summary>
        private void CreateAccess()
        {
            AccessDbConnectionStringBuilder builder = new AccessDbConnectionStringBuilder(btnDataSource.Text)
            {
                ["User ID"] = txtUserId.Text,
                ["Password"] = txtPassword.Text
            };
            Model.DataSource = new AccessDataSource(builder.ConnectionString);
            Model.TableSchemas = Model.DataSource.GetSchemaModel();
            Model.DataSource.ProviderName = "Microsoft Access";
        }

        /// <summary>
        /// Creates the documentum.
        /// </summary>
        private void CreateDocumentum()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder(txtUserId.Text, txtPassword.Text, btnDataSource.Text);
            builder["Extended Properties"] = txtExt.Text.Trim('\'', '\"');
            Model.DataSource = new DqlDataSource(builder.ConnectionString);
            Model.TableSchemas = Model.DataSource.GetSchemaModel();
            Model.DataSource.ProviderName = "OpenText Documentum";
        }

        /// <summary>
        /// Creates the excel.
        /// </summary>
        private void CreateExcel()
        {
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(btnDataSource.Text)
            {
                ["User ID"] = txtUserId.Text,
                ["Password"] = txtPassword.Text,
                ["Extended Properties"] = txtExt.Text
            };
            Model.DataSource = new ExcelDataSource(builder.ConnectionString);
            Model.TableSchemas = Model.DataSource.GetSchemaModel();
            Model.DataSource.ProviderName = "Microsoft Excel";
        }

        /// <summary>
        /// Handles the Load event of the frmConnection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmConnection_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnTestCn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private async void btnTestCn_Click(object sender, EventArgs e)
        {
            pbar.Text = "Testing connection...";
            pbar.Visible = true;
          
            await Task.Run(() =>
            {
                TestConnection();
            });
                        
            pbar.Visible = false;
            lblMessage.Text = DqlConnection.DefaultBrokerHost;
            return;           
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbProviders control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            var builder = providers[cbProviders.Text](string.Empty);
            object extended = txtExt.Text;
            builder.TryGetValue("Extended Properties", out extended);
            txtExt.Text = extended as string;
        }

        /// <summary>
        /// Handles the ButtonClick event of the btnDataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void btnDataSource_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (cbProviders.Text == "Microsoft Excel")
            {
                OpenFileDialog dlg = new OpenFileDialog()
                {
                    Filter = "Microsoft Excel (*.xlsx;*.xls)|*.xlsx;*.xls"
                };
                if (dlg.ShowDialog() == DialogResult.Cancel) return;
                btnDataSource.Text = dlg.FileName;

            }
            if (cbProviders.Text == "Microsoft Access")
            {
                OpenFileDialog dlg = new OpenFileDialog()
                {
                    Filter = "Microsoft Access (*.mdb;*.accdb)|*.mdb;*.accdb"
                };
                if (dlg.ShowDialog() == DialogResult.Cancel) return;
                btnDataSource.Text = dlg.FileName;
            }
            if (cbProviders.Text == "OpenText Documentum")
            {
                if (btnDataSource.Properties.Items.Count != 0) return;

                var builder = new DqlConnectionStringBuilder(txtUserId.Text, txtPassword.Text, btnDataSource.Text);
                DqlConnection cn = new DqlConnection(builder.ConnectionString);
                btnDataSource.Properties.Items.AddRange(cn.Catalogs);
                btnDataSource.Text = cn.Catalogs.First();
            }

        }

        /// <summary>
        /// Handles the DoubleClick event of the pictureEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void PictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            txtUserId.Text = "dmadmin";
            txtPassword.Text = "@vmware99";
            btnDataSource.Text = "ls_repos";
        }


        /// <summary>
        /// Tests the connection.
        /// </summary>
        private void TestConnection()
        {
            if (InvokeRequired)
            {
                Invoke((Action)TestConnection);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                if (cbProviders.Text == "Microsoft Excel")
                    TestExcel();
                if (cbProviders.Text == "OpenText Documentum")
                    TestDocumentum();
                if (cbProviders.Text == "Microsoft Access")
                    TestAccess();
                if (cbProviders.Text == "Microsoft Sql Server")
                    TestSqlServer();


                MessageBox.Show("Connection Successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Tests the SQL server.
        /// </summary>
        private void TestSqlServer()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(btnDataSource.Text)
            {
                ["User ID"] = txtExt.Text,
                ["Password"] = txtPassword.Text
            };
            using (SqlConnection cn = new SqlConnection(builder.ConnectionString))
            {
                cn.Open();
            }
        }

        /// <summary>
        /// Tests the access.
        /// </summary>
        private void TestAccess()
        {
            AccessDbConnectionStringBuilder builder = new AccessDbConnectionStringBuilder(btnDataSource.Text);
            //builder["Extended Properties"] = txtExt.Text;
            
            using (OleDbConnection cn = new OleDbConnection(builder.ConnectionString))
            {
                cn.Open();
            }
        }

        /// <summary>
        /// Texts the documentum.
        /// </summary>
        private void TestDocumentum()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder(txtUserId.Text, txtPassword.Text, btnDataSource.Text);
            builder["Extended Properties"] = txtExt.Text;

            DqlDataSource ds = new DqlDataSource(builder.ConnectionString);

            if (ds.UseExternalQueryEngine())
            {                
                ds.TestConnection();
                return;
            }

            using (DqlConnection cn = new DqlConnection(builder.ConnectionString))
            {
                cn.Open();
            }
        }

        /// <summary>
        /// Tests the excel.
        /// </summary>
        private void TestExcel()
        {
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(btnDataSource.Text)
            {
                ["Extended Properties"] = txtExt.Text
            };
            using (OleDbConnection cn = new OleDbConnection(builder.ConnectionString))
            {
                cn.Open();
            }

        }
    }
}