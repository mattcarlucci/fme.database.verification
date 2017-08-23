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
using Fme.DqlProvider.Extensions;
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
        CompareModel model = new CompareModel();
        /// <summary>
        /// The cancel token source
        /// </summary>
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
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
        Dictionary<string, int> MisMatches = new Dictionary<string, int>();

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
            bindingNavigator1.BackColor = this.BackColor;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();

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
            for (int i = -12; i <= 12; i++)
            {
                cbSourceTZ.Properties.Items.Add(i.ToString());
                cbTargetTZ.Properties.Items.Add(i.ToString());
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

            var fallback = model;
            btnExecute.Enabled = false;
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;

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

                repo.Execute(cancelTokenSource);

            }
            catch (OperationCanceledException ex)
            {
                timerElapsed.Stop();
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
        /// Handles the ItemClick event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                cancelTokenSource.Cancel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemClick event of the btnAutoGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs" /> instance containing the event data.</param>
        private void btnAutoGenerate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            model.ColumnCompare = CompareMappingHelper.
                GetPairs(model.Source.SelectedSchema(), model.Target.SelectedSchema());

            bsMappings.DataSource = model.ColumnCompare;
            gridMappings.DataSource = bsMappings;
            gridFieldLookup.DataSource = bsMappings;
            gridCalcFields.DataSource = bsMappings;
            gridMappings.RefreshDataSource();
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
        /// Handles the ButtonClick event of the btnEditIdList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs" /> instance containing the event data.</param>
        private void btnEditIdList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Text File *.txt;*.csv|*.txt;*.csv"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            model.Source.IdListFile = dlg.FileName;
            btnEditIdList.Text = dlg.FileName;

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

                model.Source.SelectedTable = cbSourceTable.Text;

                cbSourceKey.Properties.Items.Clear();

                cbSourceKey.Properties.Items.AddRange(model.Source.SelectedSchema().
                    Fields.Select(s => s.Name).ToList());
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

                model.Target.SelectedTable = cbTargetTable.Text;

                cbTargetKey.Properties.Items.Clear();

                cbTargetKey.Properties.Items.AddRange(model.Target.SelectedSchema().
                    Fields.Select(s => s.Name).ToList());

                gridMappings.RefreshDataSource();
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
                //this is ugly
                GridView view = sender as GridView;
                if (view.IsRowVisible(e.RowHandle) == RowVisibleState.Hidden)
                    return;

                var isCalculated = model.ColumnCompare[e.RowHandle].IsCalculated;
                if (isCalculated)
                {
                    e.Appearance.ForeColor = Color.DarkRed;
                    e.Appearance.BackColor = Color.Ivory;

                    Debug.Print(e.Column.FieldName);
                    //  if (e.Column.FieldName == "LeftSide" || e.Column.Name == "RightSide")
                    //      e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

                    if (e.Column.FieldName.Contains("Query"))
                    {
                        e.Appearance.ForeColor = Color.DarkBlue;
                        e.Appearance.Font = new Font("Courier New", e.Appearance.Font.Size);
                    }
                }
                else
                {
                    if (view.Name == "gridView5" && e.Column.FieldName.Contains("Query"))
                    {
                        //e.Appearance.BackColor = this.BackColor;                        
                        e.Appearance.ForeColor = Color.DarkRed;
                        e.Appearance.BackColor = Color.Ivory;
                    }
                }
                if (view.Name == "gridView1" && model.ColumnCompare[e.RowHandle].CompareResults?.Count > 0)
                {
                    e.Appearance.BackColor = Color.AntiqueWhite;
                }

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
            if (e.Column.Name == "colLeftSide" && model.ColumnCompare[e.RowHandle].IsCalculated == false)
            {
                if (model.Source.SelectedSchema().Fields.Where(w => w.Name == model.ColumnCompare[e.RowHandle].LeftSide).Count() == 0)
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                    e.Appearance.ForeColor = Color.DimGray;
                else
                    e.Appearance.ForeColor = Color.DarkBlue;
            }
            else if (e.Column.Name == "colRightSide" && model.ColumnCompare[e.RowHandle].IsCalculated == false)
            {
                if (model.Target.SelectedSchema().Fields.Where(w => w.Name == model.ColumnCompare[e.RowHandle].RightSide).Count() == 0)
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                    e.Appearance.ForeColor = Color.DimGray;
                else
                    e.Appearance.ForeColor = Color.DarkBlue;
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
                bindingNavigator1.Refresh();
                gridMappings.RefreshDataSource();
                var items = bsMappings.DataSource as List<CompareMappingModel>;
                items.Last().IsCalculated = true;
                viewCalcFields.RefreshData();
            }
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

            GridView view = viewMappings;
            var temp = model.ColumnCompare[sourceRow];
            model.ColumnCompare.Remove(temp);
            model.ColumnCompare.Insert(targetRow, temp);
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
            timerElapsed.Stop();

            ShowError(sender as Exception);
            Animate(false);
            SetBestWidths();
            btnExecute.Enabled = true;
            SetDataSource(gridMessages, model.ErrorMessages);
            lblStatus.Caption = "Idle";
            LoadQueries();

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
            SetDataSource(gridResults, e.Table);
            SetDataSource(gridReport, model.ColumnCompare.SelectMany(many => many.CompareResults).ToList());

            timerElapsed.Stop();
            lblStatus.Caption = string.Format("Last Sucessful Compare {0}", DateTime.Now);
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
                txtSourceQuery.Text = File.ReadAllText(string.Format(@".\logs\{0}Query_{1}_{2}.sql", "Left", model.Source.SelectedTable, model.Source.Key));
                txtTargetQuery.Text = File.ReadAllText(string.Format(@".\logs\{0}Query_{1}_{2}.sql", "Right", model.Target.SelectedTable, model.Target.Key));
            }
            catch (Exception)
            {

            }
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

            if (e.CurrentRow == 0)
                gridMappings.BestFitWidth();

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

            if (MisMatches != null && MisMatches.ContainsKey(e.RowHandle + e.Column.FieldName.Replace("left_", "")))
                e.Appearance.BackColor = Color.Red;
            if (MisMatches != null && MisMatches.ContainsKey(e.RowHandle + e.Column.FieldName.Replace("right_", "")))
                e.Appearance.BackColor = Color.LightGreen;



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
                grid.ShowPrintPreview();

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
            
            foreach (var dataGrid in dataGrids)            
                dataGrid.BestFitWidth(false, true);

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

            btnEditIdList.Text = "";
            chkSourceRandom.Checked = false;
            txtSourceMaxRows.Text = "";

            foreach (var grid in grids)
                SetDataSource(grid, null);            
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
                chkSourceRandom.Checked = model.Source.IsRandom;
                txtSourceMaxRows.Text = model.Source.MaxRows;

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

        }
        /// <summary>
        /// Sets the target model.
        /// </summary>
        public void SetTargetModel()
        {
            btnTargetData.Text = model.Target.DataSource.GetConnectionStringBuilder()["Data Source"] as string;

            cbTargetTable.Properties.Items.Clear();

            cbTargetTable.Properties.Items.
                AddRange(model.Target.TableSchemas.Select(s => s.TableName).ToArray());

            cbTargetTable.Text = model.Target.SelectedTable;
            cbTargetKey.Text = model.Target.Key;
            cbTargetTZ.Text = model.Target.TimeZoneOffset.ToString();

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


            cbLeftSide.Items.AddRange(cbSourceKey.Properties.Items);
            cbRightSide.Items.AddRange(cbTargetKey.Properties.Items);

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
        /// Checks the alias lengths.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckAliasLengths()
        {
            List<string> invalid = new List<string>();

            foreach(var item in model.ColumnCompare)
            {
                if (item.LeftAlias.Length > 30) invalid.Add(item.LeftAlias + " | size = " + item.LeftAlias.Length);

                if (item.RightAlias.Length > 30) invalid.Add(item.RightAlias + "| Size = " + item.RightAlias.Length);
            }
            if (invalid.Count > 0)
            {
                string buffer = string.Join(Environment.NewLine, invalid);
                MessageBox.Show("The following fields exceed the maximum length\r\n\r\n" + buffer, "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            
            return true;

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
                GridControl gridControl = (GridControl)strip.SourceControl;
                var p = gridControl.PointToClient(Cursor.Position);
                GridView view = (GridView)gridControl.FocusedView;
                BaseHitInfo baseHI = view.CalcHitInfo(p);
                GridHitInfo gridHI = baseHI as GridHitInfo;
                Debug.Print(gridHI.RowHandle.ToString());
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

        private void txtSourceMaxRows_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(txtSourceMaxRows.Text, out int value))
                model.Source.MaxRows = value.ToString();

        }

        /// <summary>
        /// Handles the Click event of the bindingNavigatorDeleteAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void bindingNavigatorDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete all items. Are you sure?", "Delte All",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.Cancel) return;

            model.ColumnCompare.Clear();

            gridMappings.RefreshDataSource();
        }

        /// <summary>
        /// Handles the SystemColorsChanged event of the MainView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainView_SystemColorsChanged(object sender, EventArgs e)
        {
            bindingNavigator1.BackColor = this.BackColor;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbSourceTZ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbSourceTZ_SelectedIndexChanged(object sender, EventArgs e)
        {            
            model.Source.TimeZoneOffset = int.Parse(cbSourceTZ.Text);
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            GridView view = gridMappings.MainView as GridView;
            try
            {
                int lastRow = view.FocusedRowHandle;
                model.ColumnCompare.RemoveAt(view.FocusedRowHandle);
                // gridMappings.DataSource = model.ColumnCompare;
                gridMappings.RefreshDataSource();
                gridMappings.Refresh();
                if (lastRow > 1)
                    viewMappings.MakeRowVisible(lastRow - 1);
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
