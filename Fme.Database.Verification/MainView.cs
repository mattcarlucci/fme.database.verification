// ***********************************************************************
// Assembly         : Fme.Database.Verification
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="MainView.cs" company="">
//     Copyright ©  2013
// </copyright>
// <summary></summary>
// ***********************************************************************
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Fme.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fme.Library.Extensions;
using Fme.Library.Models;
using System.Threading.Tasks;
using System.Collections;
using DevExpress.XtraGrid;
using Fme.DqlProvider;
using Fme.Library.Enums;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Fme.Library.Repositories;
using System.Threading;
using Fme.Library.Comparison;
using System.Reflection;
using Fme.Database.Verification.Extensions;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System.IO;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraTab;
using DevExpress.XtraPrinting;
using DevExpress.XtraBars;

namespace Fme.Database.Verification
{
    /// <summary>
    /// Class MainView.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.XtraForm" />
    public partial class MainView : DevExpress.XtraEditors.XtraForm
    {
        PublicOptions Options = new PublicOptions();

        /// <summary>
        /// The model
        /// </summary>
        CompareModel model = new CompareModel(Properties.Settings.Default.ChunkSize);
        /// <summary>
        /// The cancel token source
        /// </summary>
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        Task currentTask = null;
        /// <summary>
        /// The cancel token
        /// </summary>
        CancellationToken cancelToken;
        /// <summary>
        /// The execution start time
        /// </summary>
        DateTime ExecutionStartTime = DateTime.Now;
        /// <summary>
        /// The mis matches
        /// </summary>
        Dictionary<string, string> MisMatches = new Dictionary<string, string>();

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MainViewModel>();
            UserLookAndFeel.Default.StyleChanged += new EventHandler(Default_StyleChanged);
        }

        /// <summary>
        /// Handles the StyleChanged event of the Default control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void Default_StyleChanged(object sender, EventArgs e)
        {
            navGridMappings.BackColor = this.BackColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();

            AppDomain.CurrentDomain.UnhandledException += new 
                UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            OnCreateNewModel();
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                string error = string.Format(e.ExceptionObject.ToString());
                if (model != null && model.ErrorMessages != null)
                    model.ErrorMessages.Add(new ErrorMessageModel("Global Exception Handler", "Current Domain", error));
            }
            catch(Exception)
            {
                return;
            }

        }

        /// <summary>
        /// Handles the Load event of the MainView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainView_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            lblVersion.Caption = "v" + version;
            exportToolStripMenuItem.Image = barButtonItem1.ImageOptions.Image;
            hideEmptyColumnToolStripMenuItem.Image = barButtonItem2.ImageOptions.Image;
            showHiddenColumnsToolStripMenuItem.Image = barButtonItem8.ImageOptions.Image;
            for (int i = -24; i <= 24; i++)
            {
                cbSourceTZ.Properties.Items.Add(i.ToString());
                cbTargetTZ.Properties.Items.Add(i.ToString());
            }           
            LoadMru();
           
        }
        /// <summary>
        /// Saves the MRU.
        /// </summary>
        /// <param name="mru">The MRU.</param>
        private void SaveMru(string mru)
        {
            var items = GetMruItems();
            items.Remove(mru);
            items.Insert(0, mru);
            File.WriteAllLines(".\\mru.txt", items);
            LoadMru();
        }
        /// <summary>
        /// Gets the MRU items.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        private List<string> GetMruItems()
        {
            if (File.Exists(".\\mru.txt") == false) return new List<string>();
            return File.ReadAllLines(".\\mru.txt").ToList();
        }
        /// <summary>
        /// Loads the MRU.
        /// </summary>
        private void LoadMru()
        {
            var items = GetMruItems();
            btnOpen1.ClearLinks();
            for (int i = 0; i < items.Count(); i++)
            {
                BarItem btn = new BarButtonItem();
                btn.Caption = items[i];
                btn.ItemClick += new ItemClickEventHandler(OnOpenModel);
                btnOpen1.AddItem(btn);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCreateNewModel();
        }

        /// <summary>
        /// Handles the <see cref="E:OpenModel" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void OnOpenModel(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            OnOpenModel(e.Item.Caption);
            SaveMru(e.Item.Caption);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the ItemClick event of the btnOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog()
                {
                    Filter = "Comparison Model *.xml|*.xml"
                };
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

                
                OnOpenModel(dlg.FileName);
                SaveMru(dlg.FileName);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  if (string.IsNullOrEmpty(model.Name))
            {
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    FileName = model.Name,
                    Filter = "Comparison Model *.xml|*.xml"
                };
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                model.Name = dlg.FileName;
                model.Source.Key = cbSourceKey.Text;
                model.Target.Key = cbTargetKey.Text;
                model.Source.IdListFile = btnEditIdList.Text;

                Serializer.Serialize<CompareModel>(dlg.FileName, model);
            }
            //else
            //    Serializer.Serialize<CompareModel>(model.Name, model);

            this.Text = model.Name;

        }

        /// <summary>
        /// Handles the ItemClick event of the btnExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidateRequiredFields() == false)
                return;
                      
            tmrMonitor.Start();
            model.ErrorMessages.Clear();
            LockControls(true);

            var fallback = model;
            btnExecute.Enabled = false;
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            //model.Source.Side = Alias.Left;

            try
            {
                ExecutionStartTime = DateTime.Now;
                timerElapsed.Enabled = true;
                timerElapsed.Start();

                Animate(true);

                CompareModelRepository repo = new CompareModelRepository(this.model);
                repo.CompareStart += Compare_CompareStart;
                repo.SourceLoadComplete += Compare_SourceComplete;
                repo.TargetLoadComplete += Compare_TargetComplete;
                repo.CompareComplete += Compare_Complete;
                repo.StatusEvent += OnEventStatus;
                repo.CompareModelStatus += Compare_CompareStatus;
                repo.Error += Compare_OnError;

                currentTask = repo.Execute(cancelTokenSource);

            }
            catch (OperationCanceledException ex)
            {
                timerElapsed.Stop();
                LockControls(false);
                MessageBox.Show(ex.Message, "Cancellation Requested", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                timerElapsed.Stop();
                ShowError(ex);
                model = fallback;
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void LockControls(bool value)
        {
            cbTargetKey.Enabled = !value;
            cbTargetTable.Enabled = !value;
            cbSourceKey.Enabled = !value;
            cbSourceTable.Enabled = !value;
            cbSourceTZ.Enabled = !value;
            cbTargetTZ.Enabled = !value;
            btnSourceData.Enabled = !value;
            btnTargetData.Enabled = !value;
            btnEditIdList.Enabled = !value;
            txtSourceMaxRows.Enabled = !value;
            chkSourceRandom.Enabled = !value;
            chkSourceVersions.Enabled = !value;
            chkTargetVersions.Enabled = !value;
            navGridMappings.Enabled = !value;

            GridControl[] configGrids = { gridMappings ,gridCalcFields, gridFieldLookup };

            GridView[] views = { viewMappings, viewCalcFields, viewFieldLookup };
            foreach (GridView view in views)
                view.OptionsBehavior.ReadOnly = value;


            btnOpen.Enabled = !value;
            btnAutoGenerate.Enabled = !value;
            btnNew.Enabled = !value;
            btnRefresh.Enabled = !value;
            btnSave.Enabled = !value;
        }

        /// <summary>
        /// Handles the ItemClick event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                cancelTokenSource.Cancel();
                currentTask.Dispose();
                
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.model.Source.RefreshSchemaModel();

            if (string.IsNullOrEmpty(model.Source.SelectedTable) == false)
            {
                gridSourcehSchema.DataSource = model.Source.SelectedSchema().Fields;

                cbSourceTable.Properties.Items.Clear();
                cbSourceTable.Properties.Items.
                    AddRange(model.Source.TableSchemas.Select(s => s.TableName).ToArray());

                cbSourceKey.Properties.Items.Clear();
                cbSourceKey.Properties.Items.
                    AddRange(model.Source.SelectedSchema().Fields.Select(s => s.Name).ToList());

                cbLeftSide.Items.Clear();
                cbLeftSide.Items.AddRange(cbSourceKey.Properties.Items);
            }

            this.model.Target.RefreshSchemaModel();
            if (string.IsNullOrEmpty(model.Target.SelectedTable) == false)
            {
                gridTargetSchema.DataSource = model.Target.SelectedSchema().Fields;

                cbTargetTable.Properties.Items.Clear();
                cbTargetTable.Properties.Items.
                    AddRange(model.Target.TableSchemas.Select(s => s.TableName).ToArray());

                cbTargetKey.Properties.Items.Clear();
                cbTargetKey.Properties.Items.
                    AddRange(model.Target.SelectedSchema().Fields.Select(s => s.Name).ToList());
                               
                cbRightSide.Items.Clear();
                cbRightSide.Items.AddRange(cbTargetKey.Properties.Items);
            }
            gridMappings.RefreshDataSource();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the ItemClick event of the btnAutoGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnAutoGenerate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (model.Source == null || model.Target == null)
                {
                    MessageBox.Show("Can't generate mapping until both source and target tables have been selected.", "Error", 
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }

              //  if (model.ColumnCompare.Count > 0)
              //      return;

                var temp = CompareMappingHelper.
                    GetPairs(model.Source.SelectedSchema(), model.Target.SelectedSchema());

                List<CompareMappingModel> items = new List<CompareMappingModel>();
                foreach (var item in temp)
                {
                    if (model.ColumnCompare.Where(w => w.LeftSide == item.LeftSide && w.RightSide == item.RightSide).SingleOrDefault() == null)
                        model.ColumnCompare.Add(item);

                }
               //foreach (var item in model.ColumnCompare)
               // {
               //     var addnew = temp.Where(w => w.LeftSide == item.LeftSide && w.RightSide == item.RightSide).SingleOrDefault();
               //     if (addnew == null)
               //         items.Add(item);
               // }                
               // model.ColumnCompare.AddRange(items.Distinct(new MappingModelComparer()));

               // //var x =  model.ColumnCompare.Distinct(new MappingModelComparer()).ToList();
                //model.ColumnCompare = x;

                bsMappings.DataSource = model.ColumnCompare;
                gridMappings.DataSource = bsMappings;
                gridFieldLookup.DataSource = bsMappings;
                gridCalcFields.DataSource = bsMappings;
                model.SetCompareOrdinal();
                gridMappings.RefreshDataSource();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch(Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }

        /// <summary>
        /// Handles the ButtonClick event of the btnTargetData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void btnTargetData_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            frmConnection cn = new frmConnection(model.Target);
            if (cn.ShowDialog() == DialogResult.OK)
            {
                btnTargetData.Text = model.Target.DataSource.GetConnectionStringBuilder()["Data Source"] as string;

                cbTargetTable.Properties.Items.Clear();

                cbTargetTable.Properties.Items.
                    AddRange(model.Target.TableSchemas.Select(s => s.TableName).ToArray());

                chkTargetVersions.Visible = model.Target.DataSource is DqlDataSource;
            }
        }

        /// <summary>
        /// Handles the ButtonClick event of the btnSourceData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void btnSourceData_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            frmConnection cn = new frmConnection(model.Source);
            if (cn.ShowDialog() == DialogResult.OK)
            {
                btnSourceData.Text = model.Source.DataSource.GetConnectionStringBuilder()["Data Source"] as string;

                cbSourceTable.Properties.Items.Clear();

                cbSourceTable.Properties.Items.
                    AddRange(model.Source.TableSchemas.Select(s => s.TableName).ToArray());

                chkSourceVersions.Visible = model.Source.DataSource is DqlDataSource;
                chkTargetVersions.Visible = model.Target.DataSource is DqlDataSource;
                


            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the Leave event of the btnEditIdList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEditIdList_Leave(object sender, EventArgs e)
        {
            model.Source.IdListFile = btnEditIdList.Text;
        }

        /// <summary>
        /// Handles the Leave event of the btnFilterFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFilterFile_Leave(object sender, EventArgs e)
        {
          //  model.Source.FilterFile = btnFilterFile.Text;
        }
        /// <summary>
        /// Handles the ButtonClick event of the btnEditIdList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void btnEditIdList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //"Supported Files (*.txt;*.csv;*.sql)|*.txt;*.csv;*.sql|Text File *.txt;*.csv|*.txt;*.csv|SQL Filter *.sql|*.sql"
            ButtonEdit edit = sender as ButtonEdit;
            string initFolder = string.IsNullOrEmpty(edit?.Text) ? Directory.GetCurrentDirectory() : edit.Text;

            OpenFileDialog dlg = new OpenFileDialog()
            {
                //Filter = "Supported Files (*.txt;*.csv)|*.txt;*.csv"
                Filter = "Supported Files (*.txt;*.csv;*.sql)|*.txt;*.csv;*.sql|Text File *.txt;*.csv|*.txt;*.csv|SQL Filter *.sql|*.sql"
            //    , InitialDirectory = Path.GetDirectoryName(initFolder)
              //  , FileName = Path.GetFileName(edit?.Text ?? Directory.GetCurrentDirectory())
            };
            dlg.InitialDirectory = initFolder;
            dlg.FileName = Path.GetFileName(initFolder);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            model.Source.IdListFile = dlg.FileName;
            btnEditIdList.Text = dlg.FileName;

        }
        /// <summary>
        /// Handles the ButtonClick event of the btnFilterFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs"/> instance containing the event data.</param>
        private void btnFilterFile_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Supported Files (*.sql)|*.sql"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            model.Source.IdListFile = dlg.FileName;
            btnFilterFile.Text = dlg.FileName;

        }

        private void ShowHideSourceSchemaEdit()
        {
            try
            {
                btnSourceEditSchema.Visible = model.Source.SelectedSchema().IsCustom;                
            }
            catch (Exception)
            {
                btnSourceEditSchema.Visible = false;                
                return;
            }
        }
        /// <summary>
        /// Shows the hide schema edit.
        /// </summary>
        private void ShowHideTargetSchemaEdit()
        {
            try
            {                
                btnTargetEditSchema.Visible = model.Target.SelectedSchema().IsCustom;
            }
            catch (Exception)
            {             
                btnTargetEditSchema.Visible = false;
                return;
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbSourceTable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cbSourceTable_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //   if (string.IsNullOrEmpty(cbSourceTable.Text)) return;

                
                if (cbSourceTable.Text == DataSourceModel.ADD_NEW_SCHEMA)
                {
                    frmSchemaEditor editor = new frmSchemaEditor(this.model.Source, "", true);
                    var result = editor.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        cbSourceTable.Text = model.Source.SelectedTable;
                        return;
                    }
                    else
                    {
                        cbSourceTable.Text = editor.compiledSchema.TableName;
                        ShowHideSourceSchemaEdit();
                    }
                    
                }
                model.Source.SelectedTable = cbSourceTable.Text;
                ShowHideSourceSchemaEdit();

                cbSourceKey.Properties.Items.Clear();

                cbSourceKey.Properties.Items.AddRange(model.Source.SelectedSchema().
                    Fields.Select(s => s.Name).ToList());

                cbLeftSide.Items.Clear();
                cbLeftSide.Items.AddRange(cbSourceKey.Properties.Items);

                gridSourcehSchema.DataSource = model.Source.SelectedSchema().Fields;
                
                gridMappings.RefreshDataSource();
                
            }
            catch(Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbTargetTable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cbTargetTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // if (string.IsNullOrEmpty(cbTargetTable.Text)) return;

                if (cbTargetTable.Text == DataSourceModel.ADD_NEW_SCHEMA)
                {
                    frmSchemaEditor editor = new frmSchemaEditor(this.model.Target, "", true);
                    var result = editor.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        cbTargetTable.Text = model.Target.SelectedTable;
                        return;
                    }
                    else
                    {
                        cbTargetTable.Text = editor.compiledSchema.TableName;
                        ShowHideTargetSchemaEdit();
                    }

                }
                model.Target.SelectedTable = cbTargetTable.Text;
                ShowHideTargetSchemaEdit();

                cbTargetKey.Properties.Items.Clear();

                cbTargetKey.Properties.Items.AddRange(model.Target.SelectedSchema().
                    Fields.Select(s => s.Name).ToList());

                gridMappings.RefreshDataSource();
                cbRightSide.Items.Clear();
                cbRightSide.Items.AddRange(cbTargetKey.Properties.Items);
                gridTargetSchema.DataSource = model.Target.SelectedSchema().Fields;
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbSourceKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cbSourceKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.Source.Key = cbSourceKey.Text;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbTargetKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cbTargetKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.Target.Key = cbTargetKey.Text;
        }
        
        /// <summary>
        /// Handles the RowCellStyle event of the GridViewMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs" /> instance containing the event data.</param>
        private void GridViewMapping_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridViewMapping_MarkMissingFields(sender, e);
                GridViewMapping_MarkCalculatedFields(sender, e);
                GridViewCalculated_StyleQueries(sender, e);
                GridLookupStyleFields(sender, e);

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the MarkMissingFields event of the GridViewMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void GridViewMapping_MarkMissingFields(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            var RowHandle = (int)Convert.ChangeType(view.GetRowCellValue(e.RowHandle, "Ordinal"), typeof(int));

            if (e.Column.Name == "colLeftSide" && model.ColumnCompare[RowHandle].IsCalculated == false)
            {
                if (model.Source.SelectedSchema().Fields.Where(w => w.Name == model.ColumnCompare[RowHandle].LeftSide).Count() == 0)
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                    e.Appearance.ForeColor = Color.DimGray;
                else
                    e.Appearance.ForeColor = Color.DarkBlue;
            }
            else if (e.Column.Name == "colRightSide" && model.ColumnCompare[RowHandle].IsCalculated == false)
            {
                if (model.Target.SelectedSchema().Fields.Where(w => w.Name == model.ColumnCompare[RowHandle].RightSide).Count() == 0)
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                    e.Appearance.ForeColor = Color.DimGray;
                else
                    e.Appearance.ForeColor = Color.DarkBlue;
            }
        }
        /// <summary>
        /// Handles the MarkCalculatedFields event of the GridViewMapping control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void GridViewMapping_MarkCalculatedFields(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != viewMappings) return;

            var RowHandle = (int)Convert.ChangeType(view.GetRowCellValue(e.RowHandle, "Ordinal"), typeof(int));
            if (model.ColumnCompare[RowHandle].IsCalculated)            
            {
                e.Appearance.ForeColor = Color.DarkRed;
                e.Appearance.BackColor = Color.Ivory;
            }
           
        }
        /// <summary>
        /// Handles the RowCellStyle event of the GridViewCalculated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void GridViewCalculated_StyleQueries(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != viewCalcFields) return;
            var RowHandle = (int)Convert.ChangeType(view.GetRowCellValue(e.RowHandle, "Ordinal"), typeof(int));
            if (model.ColumnCompare[RowHandle].IsCalculated == false) return;

            e.Appearance.ForeColor = Color.DarkRed;
            e.Appearance.BackColor = Color.Ivory;           

            if (e.Column.FieldName.Contains("Query"))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
             //   e.Appearance.Font = new Font("Courier New", e.Appearance.Font.Size);                
            }
        }
        /// <summary>
        /// Grids the lookup style fields.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void GridLookupStyleFields(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            var RowHandle = (int)Convert.ChangeType(view.GetRowCellValue(e.RowHandle, "Ordinal"), typeof(int));
           // if (view == this.viewCalcFields) return;
            
            if (!string.IsNullOrEmpty(model.ColumnCompare[RowHandle].LeftLookupFile) && (e.Column.FieldName == "LeftSide"))
            {
                e.Appearance.ForeColor = Color.DarkGreen;
            }
            if (!string.IsNullOrEmpty(model.ColumnCompare[RowHandle].RightLookupFile) && (e.Column.FieldName == "RightSide"))
            {
                e.Appearance.ForeColor = Color.DarkGreen;
            }
            if (!string.IsNullOrEmpty(model.ColumnCompare[RowHandle].RightLookupFile) && (e.Column.FieldName == "LeftLookupFile"))
            {
                if (File.Exists(model.ColumnCompare[e.RowHandle].LeftLookupFile)) return;
              //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.DarkRed;
            }
            if (!string.IsNullOrEmpty(model.ColumnCompare[RowHandle].RightLookupFile) && (e.Column.FieldName == "RightLookupFile"))
            {
                if (File.Exists(model.ColumnCompare[RowHandle].RightLookupFile)) return;
           //     e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor = Color.DarkRed;
            }
        }
        /// <summary>
        /// Handles the Click event of the bindingNavigatorAddNewItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void BindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == xtraTabCalcFields)
            {
                var last = model.ColumnCompare.Last();
                last.IsCalculated = true;
                navGridMappings.Refresh();
                gridMappings.RefreshDataSource();
                var items = bsMappings.DataSource as List<CompareMappingModel>;
                items.Last().IsCalculated = true;
                viewCalcFields.RefreshData();
            }
            model.SetCompareOrdinal();
        }

        /// <summary>
        /// Handles the ButtonClick event of the repositoryItemButtonEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void RepositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Lookup files (*.txt, *.csv)|*.txt;*.csv|All files (*.*)|*.*"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            ButtonEdit edit = sender as ButtonEdit;
            edit.Text = dlg.FileName;
        }

        #region Need to refactor some of this. Just for dragging columns
        /// <summary>
        /// Down hit information
        /// </summary>
        GridHitInfo downHitInfo = null;
        /// <summary>
        /// The order field name
        /// </summary>
        const string OrderFieldName = "Ordinal";

        /// <summary>
        /// Handles the MouseDown event of the gridMappings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void GridMappings_MouseDown(object sender, MouseEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;
            downHitInfo = null;

            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None)
                return;
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.RowHandle != GridControl.NewItemRowHandle)
                downHitInfo = hitInfo;
        }

        /// <summary>
        /// Handles the MouseMove event of the gridMappings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void GridMappings_MouseMove(object sender, MouseEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;

            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All);
                    downHitInfo = null;
                }
            }
        }

        /// <summary>
        /// Handles the DragOver event of the gridMappings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private void GridMappings_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GridHitInfo)))
            {
                GridHitInfo downHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
                if (downHitInfo == null)
                    return;

                GridControl grid = sender as GridControl;
                GridView view = grid.MainView as GridView;
                GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
                if (hitInfo.InRow && hitInfo.RowHandle != downHitInfo.RowHandle && hitInfo.RowHandle != GridControl.NewItemRowHandle)
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the DragDrop event of the gridMappings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        private void GridMappings_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;
            GridHitInfo srcHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
            GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
            int sourceRow = srcHitInfo.RowHandle;
            int targetRow = hitInfo.RowHandle;
            MoveRow(sourceRow, targetRow);
        }
        /// <summary>
        /// Moves the row.
        /// </summary>
        /// <param name="sourceRow">The source row.</param>
        /// <param name="targetRow">The target row.</param>
        private void MoveRow(int sourceRow, int targetRow)
        {
            if (sourceRow == targetRow)
                return;

            model.ColumnCompare.Swap(sourceRow, targetRow);           
            gridMappings.RefreshDataSource();
        }
        #endregion


        #region Execution Events
        /// <summary>
        /// Handles the CompareStart event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CompareStartEventArgs" /> instance containing the event data.</param>
        public void Compare_CompareStart(object sender, CompareStartEventArgs e)
        {
            EventHandler<CompareStartEventArgs> handler = Compare_CompareStart;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            lblStatus.Caption = "Executing...";
            gridMappings.RefreshDataSource();
        }
        /// <summary>
        /// Handles the CompareStatus event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CompareModelStatusEventArgs"/> instance containing the event data.</param>
        public void Compare_CompareStatus(object sender, CompareModelStatusEventArgs e)
        {
            EventHandler<CompareModelStatusEventArgs> handler = Compare_CompareStatus;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            lblStatus.Caption = e.StatusMessage;            
            Application.DoEvents();
            SetDataSource(gridCalcQueries, model.Queries);
        }


        /// <summary>
        /// Handles the SourceComplete event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataTableEventArgs" /> instance containing the event data.</param>
        public void Compare_SourceComplete(object sender, DataTableEventArgs e)
        {
            EventHandler<DataTableEventArgs> handler = Compare_SourceComplete;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            SetDataSource(gridSourceData, e.Table);
            tabSourceData.Tooltip = string.Format("{0} record(s)", e.Table.Rows.Count);

        }

        /// <summary>
        /// Handles the TargetComplete event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataTableEventArgs" /> instance containing the event data.</param>
        public void Compare_TargetComplete(object sender, DataTableEventArgs e)
        {
            EventHandler<DataTableEventArgs> handler = Compare_TargetComplete;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            SetDataSource(gridTargetData, e.Table);
            SetDataSource(gridMessages, model.ErrorMessages);
            SetDataSource(gridCalcQueries, model.Queries);
            tabTargetData.Tooltip = string.Format("{0} record(s)", e.Table.Rows.Count);

        }

        /// <summary>
        /// Handles the OnError event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void Compare_OnError(object sender, EventArgs e)
        {
            EventHandler<DataTableEventArgs> handler = Compare_TargetComplete;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            LockControls(false);
            timerElapsed.Stop();

            ShowError(sender as Exception);
            Animate(false);
            SetBestWidths();
            btnExecute.Enabled = true;
            SetDataSource(gridMessages, model.ErrorMessages);
            
            lblStatus.Caption = "Idle";
            LoadQueries();
            model.ErrorMessages.Add(new 
                ErrorMessageModel("Compare.Execute", ((Exception)sender).Message, sender.ToString()));
            gridMessages.ResetDataSource(model.ErrorMessages);
        }

        /// <summary>
        /// Handles the Complete event of the Compare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataTableEventArgs" /> instance containing the event data.</param>
        public void Compare_Complete(object sender, DataTableEventArgs e)
        {
            EventHandler<DataTableEventArgs> handler = Compare_Complete;
            if (InvokeRequired)
            {
                Invoke(handler, sender, e);
                return;
            }
            LockControls(false);
            SetDataSource(gridResults, e.Table);
            SetDataSource(gridReport, model.ColumnCompare.SelectMany(many => many.CompareResults).ToList());

            timerElapsed.Stop();
            lblStatus.Caption = string.Format("Last Successful Compare {0}", DateTime.Now);
            MessageBox.Show("Comparison Completed", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Animate(false);

            if (Options.HideEmptyColumns)
                HideEmptyColumns();

            SetBestWidths();
            btnExecute.Enabled = true;
            MisMatches = CompareModelRepository.GetCompareResults(model);
            LoadQueries();            
        }
        /// <summary>
        /// Loads the queries.
        /// </summary>
        private void LoadQueries()
        {
            try
            {
                string logFile = @".\logs\{0}Query_{1}_{2}.sql";

                Cursor = Cursors.WaitCursor;
                txtSourceQuery.Text = File.ReadAllText(string.Format(logFile, Alias.Left, 
                    model.Source.SelectedTable, model.Source.Key));

                txtTargetQuery.Text = File.ReadAllText(string.Format(logFile, Alias.Right, 
                    model.Target.SelectedTable, model.Target.Key));
            }
            catch (Exception)
            {

            }
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// Handles the <see cref="E:EventStatus" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CompareHelperEventArgs" /> instance containing the event data.</param>
        private void OnEventStatus(object sender, CompareHelperEventArgs e)
        {
            EventHandler<CompareHelperEventArgs> handler = OnEventStatus;

            if (InvokeRequired)
            {
                this.Invoke(handler, sender, e);
                return;
            }

            lblStatus.Caption = "Processing " + e.Source;
            viewMappings.MakeRowVisible(e.CurrentRow);
            // bsMappings.DataSource = model.ColumnCompare;
            gridMappings.RefreshDataSource();

           // if (e.CurrentRow == 0)
           //     gridMappings.BestFitWidth();

            Application.DoEvents();
        }
        #endregion

        /// <summary>
        /// Handles the RowCellStyle event of the viewResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void ViewResults_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
           

            if (MisMatches != null && MisMatches.ContainsKey(e.RowHandle + e.Column.FieldName))
                e.Appearance.BackColor = Color.FromName(MisMatches[e.RowHandle + e.Column.FieldName]); //red
            else if (MisMatches != null && MisMatches.ContainsKey(e.RowHandle + e.Column.FieldName))
                e.Appearance.BackColor = Color.FromName(MisMatches[e.RowHandle + e.Column.FieldName]); //light green

        }

        /// <summary>
        /// Handles the Tick event of the timerElapsed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerElapsed_Tick(object sender, EventArgs e)
        {
            lblElapsed.Caption = new TimeSpan(DateTime.Now.Ticks - ExecutionStartTime.Ticks).
                Duration().ToString();
        }

        /// <summary>
        /// Handles the Enter event of the gridMappings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GridMappings_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the exportToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip owner = item.Owner as ContextMenuStrip;
            if (owner.SourceControl is GridControl grid)
            {
                grid.ShowPrintPreview();
            }
        }

        /// <summary>
        /// Handles the Click event of the removeEmptyColumnToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void HideEmptyColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gridControl = ((ToolStripMenuItem)sender).GetMenuContextOwner<GridControl>();
            if (gridControl != null)
            {
                GridView view = (GridView)gridControl.FocusedView;
                var table = gridControl.DataSource as DataTable;
                if (table == null) return;
                var cols = table.RemoveEmptyColumns(false).Select(s => s.ColumnName).ToList();
                view.HideEmptyColumn(cols);
                view.RefreshData();
            }
        }


        #region Helper Functions
        /// <summary>
        /// Animates the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        private void Animate(bool state)
        {
            Cursor = state == true ? Cursors.WaitCursor : Cursors.Default;

            if (state)
                lblStatus.Caption = "Running";

            barEditStatus.Visibility = state == true ?
                DevExpress.XtraBars.BarItemVisibility.Always :
                DevExpress.XtraBars.BarItemVisibility.Never;
        }
        /// <summary>
        /// Sets the data source.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        /// <param name="dataSource">The data source.</param>
        private void SetDataSource(BindingSource ctrl, object dataSource)
        {
            ctrl.DataSource = dataSource;
        }
        /// <summary>
        /// Sets the data source.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        /// <param name="dataSource">The data source.</param>
        private void SetDataSource(GridControl ctrl, object dataSource)
        {
            ctrl.ResetDataSource(dataSource);
        }
        /// <summary>
        /// Hides the empty columns.
        /// </summary>
        private void HideEmptyColumns()
        {
            GridControl[] dataGrids = { gridSourceData, gridTargetData, gridReport, gridResults };
            foreach (var dataGrid in dataGrids)
            {
                GridView view = (GridView)dataGrid.FocusedView;
                var table = dataGrid.DataSource as DataTable;
                if (table != null)
                {
                    var cols = table.RemoveEmptyColumns(false).Select(s => s.ColumnName).ToList();
                    view.HideEmptyColumn(cols);
                }
            }
        }
        /// <summary>
        /// Sets the best widths.
        /// </summary>
        private void SetBestWidths()
        {
            GridControl[] dataGrids = { gridSourceData, gridTargetData, gridReport, gridResults,gridMessages };
            GridControl[] configGrids = { gridMappings /*,gridCalcFields, gridFieldLookup */ };

            DataTable table = gridSourceData.DataSource as DataTable;
            if (table != null && table.Rows.Count < 1000)
            {
                foreach (var dataGrid in dataGrids)
                    dataGrid.BestFitWidth(false, true);
            }
            foreach (var configGrid in configGrids)
                configGrid.BestFitWidth(true, true);
           

            #region no longer needed?
            //foreach (GridControl grid in set1)
            //{
            //    GridView view = grid.MainView as GridView;
            //    view.OptionsView.ColumnAutoWidth = false;
            //    view.BestFitColumns(true);
            //}

            //viewMappings.OptionsView.ColumnAutoWidth = true;
            //viewMappings.BestFitColumns(true);
            //viewCalcFields.OptionsView.ColumnAutoWidth = true;
            //viewCalcFields.BestFitColumns(true);
            //viewFieldLookup.OptionsView.ColumnAutoWidth = true;
            //viewFieldLookup.BestFitColumns(true);
            #endregion

        }
        /// <summary>
        /// Called when [new model].
        /// </summary>
        public void OnCreateNewModel()
        {
            GridControl[] grids = { gridSourceData, gridTargetData, gridResults, gridReport, gridMessages };

            BaseEdit[] editors = { cbSourceTable, cbTargetTable, cbSourceKey, cbTargetKey, btnSourceData, btnTargetData };
            model = new CompareModel();

            editors.ToList().ForEach(editor => editor.Text = "");

            
            this.Text = "Untitled";

            bsMappings.DataSource = model.ColumnCompare;
            gridMappings.DataSource = bsMappings;
            gridFieldLookup.DataSource = bsMappings;
            gridCalcFields.DataSource = bsMappings;

            cbLeftSide.Items.Clear();
            cbRightSide.Items.Clear();
            cbSourceTable.Properties.Items.Clear();
            cbTargetTable.Properties.Items.Clear();
            cbSourceKey.Properties.Items.Clear();
            cbTargetKey.Properties.Items.Clear();
            cbSourceTZ.Text = "0";
            cbTargetTZ.Text = "0";

            this.chkSourceVersions.Checked = false;
            this.chkTargetVersions.Checked = false;


            chkSourceVersions.Visible = false;
            chkTargetVersions.Visible = false;

            btnEditIdList.Text = "";
            chkSourceRandom.Checked = false;
            txtSourceMaxRows.Text = "0";

            btnFilterFile.Text = "";

            foreach (var grid in grids)
                SetDataSource(grid, null);

            tmrMonitor.Stop();
            tabMessages.Text = "System Messages";
            gridSourcehSchema.ResetDataSource(null);
            gridTargetSchema.ResetDataSource(null);
            SetupGridMappings();
            SetupMapping();
        }

        /// <summary>
        /// Called when [open model].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void OnOpenModel(string fileName)
        {
            try
            {
                var temp = Serializer.DeSerialize<CompareModel>(fileName);
                OnCreateNewModel();
                model = temp;
                SetSourceModel();
                SetTargetModel();
                SetupMapping();

                btnEditIdList.Text = model.Source.IdListFile;
               // btnFilterFile.Text = model.Source.FilterFile;
                chkSourceRandom.Checked = model.Source.IsRandom;
                txtSourceMaxRows.Text = string.IsNullOrEmpty(model.Source.MaxRows) ? "0" :model.Source.MaxRows;

                //BUG: Storing file name doesn't make sense. User can rename file outside application. 
                //     We update the model to reflect the correct name
                if (model.Name != fileName)
                {
                    model.Name = fileName; // can't use saved name because someone can make copy of file.
                    Serializer.Serialize<CompareModel>(fileName, model);
                }
                
                this.Text = model.Name;
                SetBestWidths();
            }
            catch(Exception ex)
            {
                ShowError(ex);
            }
        }

        /// <summary>
        /// Called when [rollback].
        /// </summary>
        /// <param name="fallback">The fallback.</param>
        public void OnRollback(CompareModel fallback)
        {
            model.ColumnCompare = fallback.ColumnCompare;
            gridMappings.RefreshDataSource();
        }
        /// <summary>
        /// Sets the source model.
        /// </summary>
        public void SetSourceModel()
        {
            btnSourceData.Text = model.Source.DataSource.GetConnectionStringBuilder()["Data Source"] as string;

            cbSourceTable.Properties.Items.Clear();

            cbSourceTable.Properties.Items.
                AddRange(model.Source.TableSchemas.Select(s => s.TableName).ToArray());

            cbSourceTable.Text = model.Source.SelectedTable;
            cbSourceKey.Text = model.Source.Key;
            cbSourceTZ.Text = model.Source.TimeZoneOffset.ToString();

            chkSourceVersions.Checked = model.Source.IncludeVersions;            
            chkSourceVersions.Visible = model.Source.DataSource is DqlDataSource;            

        }
        /// <summary>
        /// Sets the target model.
        /// </summary>
        public void SetTargetModel()
        {
          
            if (model.Target.DataSource != null)
                btnTargetData.Text = model.Target.DataSource.GetConnectionStringBuilder()["Data Source"] as string;

            cbTargetTable.Properties.Items.Clear();

            cbTargetTable.Properties.Items.
                AddRange(model.Target.TableSchemas.Select(s => s.TableName).ToArray());

            cbTargetTable.Text = model.Target.SelectedTable;
            cbTargetKey.Text = model.Target.Key;
            cbTargetTZ.Text = model.Target.TimeZoneOffset.ToString();

            chkTargetVersions.Checked = model.Target.IncludeVersions;
            chkTargetVersions.Visible = model.Target.DataSource is DqlDataSource;        
        }
        /// <summary>
        /// Setups the mapping.
        /// </summary>
        public void SetupMapping()
        {
            bsMappings.DataSource = model.ColumnCompare;
            gridMappings.DataSource = bsMappings;
            gridFieldLookup.DataSource = bsMappings;
            gridCalcFields.DataSource = bsMappings;
            gridMappings.RefreshDataSource();

            ColumnView view = viewCalcFields;
            ViewColumnFilterInfo viewFilterInfo = new ViewColumnFilterInfo(view.Columns["CategoryName"],
              new ColumnFilterInfo("[IsCalculated] = True", ""));
            view.ActiveFilter.Add(viewFilterInfo);

            viewCalcFields.ActiveFilterCriteria = new DevExpress.Data.Filtering.BinaryOperator("IsCalculated", true);

            gridMappings.RefreshDataSource();

            ComboBoxItemCollection[] gridBoxes = { cbLeftSide.Items, cbRightSide.Items, cbCompareType.Items, cbOperator.Items };
            gridBoxes.ToList().ForEach(item => item.Clear());
            SetupGridMappings();

           
        }
        public void SetupGridMappings()
        {
           
            cbLeftSide.Items.AddRange(cbSourceKey.Properties.Items);
            cbRightSide.Items.AddRange(cbTargetKey.Properties.Items);

            cbCompareType.Items.Clear();
            cbOperator.Items.Clear();
            cbCompareType.Items.AddRange(Enum.GetNames(typeof(ComparisonTypeEnum)));
            cbOperator.Items.AddRange(Enum.GetNames(typeof(OperatorEnums)));

        }
        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Determines whether [is session valid].
        /// </summary>
        /// <returns><c>true</c> if [is session valid]; otherwise, <c>false</c>.</returns>
        private bool ValidateRequiredFields()
        {
           
            if (string.IsNullOrEmpty(cbSourceTable.Text))
            {
                MessageBox.Show("Please select the Table");
                cbSourceTable.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbTargetTable.Text))
            {
                MessageBox.Show("Please select the Table");
                cbTargetTable.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cbSourceKey.Text))
            {
                MessageBox.Show("Please select the Primary Key");
                cbSourceKey.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbTargetKey.Text))
            {
                MessageBox.Show("Please select the  Primary Key");
                cbTargetKey.Focus();
                return false;
            }

            if (model.ColumnCompare.Count == 0)
            {
                MessageBox.Show("Please select the field mappings before you continue.", "No Mappings Defined",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (model.Source.MaxRows == "0" || model.Source.MaxRows == "")
                model.Source.MaxRows = null;

            Dictionary<string, int> testDuplicates = new Dictionary<string, int>();
            foreach(var key in model.ColumnCompare.
                Where(w=> w.Selected == true && w.LeftSide != w.RightSide).Select(s => s.LeftSide + " <=> " + s.RightSide))
            {
                if (testDuplicates.ContainsKey(key))
                {
                    MessageBox.Show("Duplicate mappings are not allow. Please correct the error and try again.\r\n" + key);                    
                    return false;
                }
                testDuplicates.Add(key, 0);
            }
            foreach (var key in model.ColumnCompare.
                Where(w => w.Selected == true).Select(s => s.RightSide + " <=> " + s.LeftSide))
            {
                if (testDuplicates.ContainsKey(key))
                {
                    MessageBox.Show("Duplicate mappings are not allow. Please correct the error and try again.\r\n" + key);
                    return false;
                }
                testDuplicates.Add(key, 0);
            }

            //if (GetIdList() == null || GetIdList().Count == 0)
            //{                
            //    MessageBox.Show("Please enter a valid ID List File.");
            //    cbSourceKey.Focus();
            //    return false;
            //}

            return true;
        }
        #endregion

        /// <summary>
        /// Handles the Opening event of the ctxGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void ctxGrid_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip strip = sender as ContextMenuStrip;
            if (strip.SourceControl is GridControl)            
            {
                BaseView view = null;

                GridControl gridControl = (GridControl)strip.SourceControl;
                var p = gridControl.PointToClient(Cursor.Position);
                view = gridControl.FocusedView as GridView;
                if (view == null)
                    return;

                //    view = gridControl.FocusedView as CardView;

                if (view == null)
                    return;

                BaseHitInfo baseHI = view.CalcHitInfo(p);
                GridHitInfo gridHI = baseHI as GridHitInfo;
                //Debug.Print(gridHI.RowHandle.ToString());
                if (gridHI.RowHandle < 0)
                    e.Cancel = true;
               
            }        
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkSourceRandom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkSourceRandom_CheckedChanged(object sender, EventArgs e)
        {
            model.Source.IsRandom = chkSourceRandom.Checked;
        }

        private void txtSourceMaxRows_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (int.TryParse(txtSourceMaxRows.Text, out int value))
            //        model.Source.MaxRows = value.ToString();
            //}
            //catch(Exception)
            //{
            //    return;
            //}
        }

        /// <summary>
        /// Handles the Validating event of the txtSourceMaxRows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void txtSourceMaxRows_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !int.TryParse(txtSourceMaxRows.Text, out int value);
        }

        /// <summary>
        /// Handles the Validated event of the txtSourceMaxRows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtSourceMaxRows_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(txtSourceMaxRows.Text, out int value))
                model.Source.MaxRows = value.ToString();

        }

      
        /// <summary>
        /// Handles the SystemColorsChanged event of the MainView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainView_SystemColorsChanged(object sender, EventArgs e)
        {
            navGridMappings.BackColor = this.BackColor;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbSourceTZ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbSourceTZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                model.Source.TimeZoneOffset = int.Parse(cbSourceTZ.Text);
            }
            catch(Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbTargetTZ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbTargetTZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.Target.TimeZoneOffset = int.Parse(cbTargetTZ.Text);
        }

        /// <summary>
        /// Handles the Click event of the showHiddenColumnsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void showHiddenColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gridControl = ((ToolStripMenuItem)sender).GetMenuContextOwner<GridControl>();
            if (gridControl != null)
            {                
                var table = gridControl.DataSource as DataTable;
                if (table == null) return;

                gridControl.ResetDataSource(gridControl.DataSource);
                gridControl.BestFitWidth(false, true);              
            }
        }

        /// <summary>
        /// Handles the ProcessGridKey event of the gridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void gridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {

            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView; 
                        
            if (grid != gridMappings) return;
            if (view.FocusedColumn.OptionsColumn.AllowEdit == true) return;

            
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;

            if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control ) 
            {
                Debug.Print("Move Up");               
               
                if (index <= 0) return;
                model.ColumnCompare.Swap(index - 1, index);
                view.FocusedRowHandle = index;               

            }
            if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
            {
                Debug.Print("Move Down");
               
                if (index >= view.DataRowCount - 1) return;
                model.ColumnCompare.Swap(index + 1, index);                
                view.FocusedRowHandle = index ;               
            }
            model.SetCompareOrdinal();
           
            //grid.RefreshDataSource();

            //var grid = sender as GridControl;
            //var view = grid.FocusedView as GridView;
            //if (e.KeyData == Keys.Delete)
            //{
            //    view.DeleteSelectedRows();
            //    e.Handled = true;
            //}
        }

        /// <summary>
        /// Handles the Click event of the bindingNavigatorDeleteAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void bindingNavigatorDeleteAll_Click(object sender, EventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == xtraTabLookup) return;

            if (MessageBox.Show("Delete all items. Are you sure?", "Delete All",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.Cancel) return;
            bool isCalcTab = false;

            GridView view = gridMappings.MainView as GridView;
            if (xtraTabControl2.SelectedTabPage == xtraTabCalcFields)
            {
                view = gridCalcFields.MainView as GridView;
                isCalcTab = true;
            }

            var items = model.ColumnCompare.Where(w => w.IsCalculated == isCalcTab).ToList();

            foreach (var item in items)
                model.ColumnCompare.Remove(item);

            view.GridControl.RefreshDataSource();


        }

        /// <summary>
        /// Handles the Click event of the bindingNavigatorDeleteItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == xtraTabLookup) return;

            GridView view = gridMappings.MainView as GridView;
            if (xtraTabControl2.SelectedTabPage == xtraTabCalcFields)
                view = gridCalcFields.MainView as GridView;


            try
            {
                int lastRow = view.FocusedRowHandle;
                if (lastRow < 0) return;

                view.DeleteSelectedRows();               
                if (lastRow > 1)
                    view.MakeRowVisible(lastRow - 1);

                model.SetCompareOrdinal();
            }
            catch(Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkSourceVersions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkSourceVersions_CheckedChanged(object sender, EventArgs e)
        {
            model.Source.IncludeVersions = chkSourceVersions.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkTargetVersions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkTargetVersions_CheckedChanged(object sender, EventArgs e)
        {
            model.Target.IncludeVersions = chkTargetVersions.Checked;
        }

        private void gridCalcQueries_DataSourceChanged(object sender, EventArgs e)
        {
            this.cardViewQueries.OptionsBehavior.FieldAutoHeight = true;
            //this.cardViewQueries.Columns["StartTime"].DisplayFormat = "mm/dd/yy hh:mm:ss";
        }
        /// <summary>
        /// Handles the DataSourceChanged event of the gridMessages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void gridMessages_DataSourceChanged(object sender, EventArgs e)
        {
            cardViewMessages.CardWidth = (cardViewMessages.GridControl.Width - 75) / 2;
          //cardViewMessages.OptionsBehavior.AutoHorzWidth = true;
            cardViewMessages.OptionsBehavior.FieldAutoHeight = true;
            cardViewMessages.Columns["StackTrace"].ColumnEdit = repoItemMemo;
            cardViewMessages.Columns["Message"].ColumnEdit = repoItemMessage;


            cardViewMessages.RefreshData();

                        try
            {
                if (model.ErrorMessages.Count() > 0)
                    tabMessages.Text = string.Format("System Messages - {0} Error(s)", model.ErrorMessages.Count());
            }
            catch(Exception)
            {
                return;
            }

        }

        private void viewCalcFields_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            
            //int row = viewCalcFields.FocusedRowHandle;
            //var value = viewCalcFields.GetRowCellValue(row, e.PrevFocusedColumn);
            //var name = e.PrevFocusedColumn.FieldName;
            //var left = viewCalcFields.GetRowCellValue(row, "LeftSide");

            //if (name == "RightQuery" && string.IsNullOrEmpty(value?.ToString()))
            //{
            //    var query = viewCalcFields.GetRowCellValue(row, "LeftQuery");
            //    viewCalcFields.SetRowCellValue(row, "RightQuery", query);
            //}
            //if (name == "RightSide" && string.IsNullOrEmpty(value?.ToString()))
            //{
            //    var query = viewCalcFields.GetRowCellValue(row, "LeftSide");
            //    viewCalcFields.SetRowCellValue(row, "RightSide", query);
            //}

            //   if (name == "RigthQuery" && value == null )
            ////viewCalcFields.FocusedValue = viewCalcFields.GetFocusedRow()
            //            if (e.FocusedColumn.FieldName == "RightSide" & viewCalcFields.FocusedValue)
        }

        /// <summary>
        /// Handles the RowCellClick event of the viewCalcFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellClickEventArgs"/> instance containing the event data.</param>
        private void viewCalcFields_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != viewCalcFields) return;
            var RowHandle = (int)Convert.ChangeType(view.GetRowCellValue(e.RowHandle, "Ordinal"), typeof(int));

            if (e.Column.FieldName == "RightQuery" && string.IsNullOrEmpty(e.CellValue?.ToString()))
            {
                var query = viewCalcFields.GetRowCellValue(RowHandle, "LeftQuery");
                viewCalcFields.SetRowCellValue(RowHandle, "RightQuery", query);
            }
            if (e.Column.FieldName == "RightSide" && string.IsNullOrEmpty(e.CellValue?.ToString()))
            {
                var query = viewCalcFields.GetRowCellValue(RowHandle, "LeftSide");
                viewCalcFields.SetRowCellValue(RowHandle, "RightSide", query);
            }

        }

        private void cbSourceTable_Leave(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Leave event of the cbSourceKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbSourceKey_Leave(object sender, EventArgs e)
        {
            model.Source.Key = cbSourceKey.Text;
        }

        private void cbTargetTable_Leave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Leave event of the cbTargetKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbTargetKey_Leave(object sender, EventArgs e)
        {
            model.Target.Key = cbTargetKey.Text;
            //probelm is we don't always have the field in teh schema so how can we tell the datatype.
            //model.Target.SelectedSchema().Fields.Where(w=> w.Name == cbTargetKey.Text)
        }

       
        /// <summary>
        /// Handles the Tick event of the tmrMonitor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrMonitor_Tick(object sender, EventArgs e)
        {
            try
            {
                if (model.ErrorMessages.Count > 0)
                {
                    tabMessages.Text = string.Format("System Messages - {0} Error(s)", model.ErrorMessages.Count());
                    if (tabMessages.Appearance.Header.BackColor == Color.LightPink)
                        tabMessages.Appearance.Header.BackColor = (Color)tabMessages.Tag;
                    else
                    {
                        tabMessages.Tag = tabMessages.Appearance.Header.BackColor;
                        tabMessages.Appearance.Header.BackColor = Color.LightPink;
                    }
                }
                else
                {
                    tabMessages.Appearance.Header.BackColor = Color.Transparent;
                    tabMessages.Text = "System Messages";
                }
            }
            catch(Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the SelectedPageChanged event of the xtraTabControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TabPageChangedEventArgs"/> instance containing the event data.</param>
        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == tabMessages)
                tmrMonitor.Stop();

            tabMessages.Appearance.Header.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Handles the Click event of the xtraTabControl2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void xtraTabControl2_Click(object sender, EventArgs e)
        {
            gridCalcFields.RefreshDataSource();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (xtraTabControl1.SelectedTabPage == tabMessages)
                gridMessages.RefreshDataSource();
            Cursor = Cursors.Default;
        }

        private void viewMessages_RowStyle(object sender, RowStyleEventArgs e)
        {
           
        }

        private void viewMessages_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            if (e.Column.FieldName.Contains("Message"))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                //e.Appearance.Font = new Font("Courier New", e.Appearance.Font.Size);
            }

        }

        private void cardViewMessages_CustomDrawCardField(object sender, RowCellCustomDrawEventArgs e)
        {
            

        }

        private void cardViewMessages_CustomDrawCardFieldValue(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName.Contains("Message"))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                //e.Appearance.Font = new Font("Courier New", e.Appearance.Font.Size);
            }

            if (e.Column.FieldName.Contains("StackTrace"))
            {
                e.Appearance.ForeColor = Color.DarkRed;
                //e.Appearance.Font = new Font("Courier New", e.Appearance.Font.Size);
            }
        }

        private void viewMappings_LostFocus(object sender, EventArgs e)
        {           
            viewMappings.PostEditor();
            viewMappings.UpdateCurrentRow();            
        }

        /// <summary>
        /// Handles the ItemClick event of the tnExportPackage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void tnExportPackage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tnExportPackage.Enabled = false;
                var name = Path.GetFileNameWithoutExtension(this.Text);
                var path = Path.GetDirectoryName(this.Text);
                var folder = string.Format("{0}\\{1}", path, name);
                GridControl[] grids = { gridSourceData, gridTargetData, gridResults , gridComparison , gridMessages, gridMappings, gridFieldLookup,
                                    gridCalcFields,gridSourcehSchema, gridTargetSchema  };

                foreach (var grid in grids)
                {
                    ExportGrids(grid);
                }

                model.Queries.ForEach(item =>
                {
                    string file = folder + "\\" + item.Name + ".sql";
                    item.LogQuery(file);
                });
  
                Serializer.Serialize<CompareModel>(folder + "\\" + name + ".exported.xml", this.model);
                string queries = txtSourceQuery.Text + ";+\r\n" + txtTargetQuery.Text;
                File.WriteAllText(folder + "\\" + name + ".queries.sql", queries);
                ShowMessage("Export Complete");
                Process.Start(folder);
                
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            tnExportPackage.Enabled = true;
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// Exports the grids.
        /// </summary>
        /// <param name="grid">The grid.</param>
        private void ExportGrids(GridControl grid)
        {
            var name = Path.GetFileNameWithoutExtension(this.Text);
            var path = Path.GetDirectoryName(this.Text);

            var folder = string.Format("{0}\\{1}", path, name);
            Directory.CreateDirectory(folder);

            //var target = @"{0}\{1}_{2}.{3}";
            var target = @"{0}\{1}.{2}";
            string xls = string.Format(target, folder, grid.Name, ".xlsx");
            
            XlsxExportOptionsEx options = new XlsxExportOptionsEx(TextExportMode.Value);
            options.ShowGridLines = false;

           // view.Export(ExportTarget.Xlsx, xls);
            grid.ExportToXlsx(xls, options);
            

            var table = grid.DataSource as DataTable;
            if (table == null) return;

            string xml = string.Format(target, folder, grid.Name, ".xml");

            if (table.DataSet == null)
            {
                DataSet dataset = new DataSet(name);
                dataset.Tables.Add(table);
            }
            table.DataSet.WriteXml(xml, XmlWriteMode.WriteSchema);
          
        }

        /// <summary>
        /// Handles the DoubleClick event of the btnSourceData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSourceData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = btnSourceData.Text
                };
                Process.Start(info);
            }
            catch(Exception)
            {

            }
        }

        /// <summary>
        /// Handles the OnShowFileDialog event of the CalculatedFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs"/> instance containing the event data.</param>
        private void CalculatedFields_OnShowFileDialog(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit edit = sender as ButtonEdit;
            string initFolder = string.IsNullOrEmpty(edit?.Text) ? Directory.GetCurrentDirectory() : edit.Text;
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "DQL/SQL files (*.dql, *.sql)|*.dql;*.sql|All files (*.*)|*.*",
               // InitialDirectory = Path.GetDirectoryName(initFolder),
              //  FileName = Path.GetFileName(edit.Text)
               
            };
            dlg.InitialDirectory = Path.GetDirectoryName(initFolder);
            dlg.FileName = Path.GetFileName(initFolder);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

          
            edit.Text = dlg.FileName;
        }

        private void repositoryItemButtonEdit3_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ButtonEdit)
            {
                try
                {
                    //ButtonEdit be = sender as ButtonEdit;
                    //be.ShowToolTips = true;
                    
                    //be.ToolTip = File.ReadAllText(be.Text);
                    //be.ToolTipTitle = be.Text;
                    
                }
                catch(Exception)
                {
                    //ignore
                }
            }
        }

        private void repositoryItemButtonEdit3_MouseHover(object sender, EventArgs e)
        {
            Debug.Print("Hovering");
        }

        /// <summary>
        /// Handles the Click event of the btnValidateSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnValidateSource_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.model.Source.SelectedTable))
            {
                MessageBox.Show("Please select the source Table", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmValidator frm = new frmValidator(this.model, this.model.Source);
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the btnNewSchema control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNewSchema_Click(object sender, EventArgs e)
        {            
            frmSchemaEditor editor = new frmSchemaEditor(this.model.Source, model.Source.SelectedTable, false);
            var result = editor.ShowDialog();
            cbSourceTable_SelectedIndexChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event of the btnTargetEditSchema control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnTargetEditSchema_Click(object sender, EventArgs e)
        {
            frmSchemaEditor editor = new frmSchemaEditor(this.model.Target, model.Target.SelectedTable, false);
            var result = editor.ShowDialog();
            cbTargetTable_SelectedIndexChanged(this, EventArgs.Empty);
        }
    }
}
