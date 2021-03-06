﻿// ***********************************************************************
// Assembly         : Fme.Database.Verification
// Author           : mcarlucci
// Created          : 11-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 11-16-2017
// ***********************************************************************
// <copyright file="frmValidator.cs" company="">
//     Copyright ©  2013
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using Fme.Library.Repositories;
using Fme.Database.Verification.Extensions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.Filtering;
using Fme.Library;
using System.Threading;
using Fme.Library.Extensions;
using System.Diagnostics;
using DevExpress.XtraGrid;
using System.Collections.Concurrent;

namespace Fme.Database.Verification
{
    /// <summary>
    /// Class frmValidator.
    /// </summary>
    /// <seealso cref="DevExpress.XtraEditors.XtraForm" />
    public partial class frmValidator : DevExpress.XtraEditors.XtraForm
    {
        private DateTime executionStartTime;

        //List<ValidationEventArgs> eventItems = new List<ValidationEventArgs>();
        ConcurrentBag<ValidationEventArgs> eventItems = new ConcurrentBag<ValidationEventArgs>();
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        CompareModel model { get; set; }
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        DataSourceModel dataSource { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="frmValidator"/> class.
        /// </summary>
        public frmValidator()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="frmValidator"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="data">The data.</param>
        public frmValidator(CompareModel model, DataSourceModel data) : this()
        {
            this.model = model;
            this.dataSource = data;
            gridControl1.DataSource = data.SelectedSchema().Fields;
            gridView1.BestFitWidth(false, true);
            RefreshSummary();
            this.Text = data.DataSource.GetConnectionStringBuilder()["Data Source"]?.ToString();           
            gridControl3.DataSource = eventItems.ToList();
            
            QueryBuilder query = model.Source.DataSource.GetQueryBuilder();
                       
            if (string.IsNullOrEmpty(data.SelectedSchema().Query))
            {
                data.SelectedSchema().Query = query.BuildSql(model.Source,
                    data.SelectedSchema().Fields.Select(s => s.Name).ToArray(), null, null);
            }
            memoEdit1.Text = data.SelectedSchema().Query.Replace("\r\n", "\n").Replace("\n", "\r\n");
            memoEdit1.ReadOnly = true;
        }

        /// <summary>
        /// Handles the Load event of the frmValidator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmValidator_Load(object sender, EventArgs e)
        {
            //dataSource.SelectedSchema().Fields.Select(s=> s.Name)
        }
              

        /// <summary>
        /// Handles the MasterRowExpanded event of the gridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs"/> instance containing the event data.</param>
        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView view = sender as GridView;
            var X = view.Columns[1].Name;
            GridView detailView = view.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detailView.Columns[0].Width = 50;
            detailView.Columns[1].Width = 500;

        }

        /// <summary>
        /// Handles the Click event of the repositoryItemButtonEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            frmMacroSetup dlg = new frmMacroSetup(this.model.Source.SelectedSchema().Fields.First());
            dlg.ShowDialog();
            RefreshSummary();
        }

        /// <summary>
        /// Handles the Click event of the repositoryItemButtonEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
        private void repositoryItemButtonEdit1_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {            
            string key = gridView1.GetFocusedRowCellDisplayText("Name");
            var item = dataSource.SelectedSchema().Fields.Where(w => w.Name == key).SingleOrDefault();
            
            frmMacroSetup dlg = new frmMacroSetup(item);
            dlg.ShowDialog();
            RefreshSummary();
        }

        /// <summary>
        /// Handles the RowCellStyle event of the gridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            int row = e.RowHandle;

            string key = gridView1.GetRowCellValue(e.RowHandle, "Name").ToString();
            var item = dataSource.SelectedSchema().Fields.Where(w => w.Name == key).SingleOrDefault();

            if (string.IsNullOrEmpty(item.ValidationMacros) == false)
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.AliceBlue;
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshSummary();
        }
        /// <summary>
        /// Refreshes the summary.
        /// </summary>
        public void RefreshSummary()
        {
            gridControl2.DataSource = ValidatorRepository.GetFunctionSummary(dataSource.SelectedSchema().Fields, eventItems);
            gridControl3.DataSource = eventItems.ToList();
            gridControl2.RefreshDataSource();
            gridControl3.RefreshDataSource();            
        }
      
        /// <summary>
        /// Handles the RowCellStyle event of the gridView2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            DataTable table = gridControl2.DataSource as DataTable;
            if (table.Rows[e.RowHandle][0].ToString().Contains("\t") == false)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.AliceBlue;
            }
            else
                e.Appearance.Font = new Font("Consolas", 12.0F);
        }
            
        /// <summary>
        /// Handles the CheckedChanged event of the barCheckItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (((DevExpress.XtraBars.BarCheckItem)e.Item).Checked)
                this.gridView1.ActiveFilterString = "Not IsNullOrEmpty([ValidationMacros])";
            else
                this.gridView1.ActiveFilterString = null;
        }       

        /// <summary>
        /// Handles the ItemClick event of the btnExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void btnExecute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                //barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                ValidatorRepository repo = new ValidatorRepository();
                repo.Validate += Repo_Validate;
                CancellationTokenSource cancelToken = new CancellationTokenSource();
          
                eventItems = new ConcurrentBag<ValidationEventArgs>();
                gridControl3.DataSource = eventItems;

                QueryBuilder query = this.model.Source.DataSource.GetQueryBuilder();
                query.IncludeVersion = model.Source.IncludeVersions;
                var select1 = query.BuildSql(model.Source, dataSource.SelectedSchema().Fields.Select(s => s.Name).ToArray(), model.GetSourceIds(), model.GetSourceFilter());
                DateTime qStartTime = DateTime.Now;

                if (dataSource.SelectedSchema().IsCustom)
                    select1 = dataSource.SelectedSchema().Query;

                executionStartTime = DateTime.Now;

                var data1 = model.Source.DataSource.ExecuteQuery(select1, cancelToken.Token);
                this.gridSourceData.DataSource = data1.Table();

                lblQueryElapsed.Caption = "Query: " + new TimeSpan(DateTime.Now.Ticks - qStartTime.Ticks).Duration().ToString();

                repo.Execute(data1.Table(), dataSource.SelectedSchema().Fields);
                RefreshSummary();
                CreateReportSummary();
                lblElapsed.Caption = "Validation: "+ new TimeSpan(DateTime.Now.Ticks - executionStartTime.Ticks).Duration().ToString();
                MessageBox.Show("Validation Complete", "Validation Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// Creates the report summary.
        /// </summary>
        private void CreateReportSummary()
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            var root = eventItems.First().DataRow.Table;
            var clone = root.Clone();
            foreach(var item in eventItems) //.Select(s=> s.PrimaryKey).Distinct().ToList())
            {
                if (keys.ContainsKey(item.PrimaryKey))
                    continue;
                keys.Add(item.PrimaryKey, 0);
                var desRow = clone.NewRow();
                desRow.ItemArray = item.DataRow.ItemArray.Clone() as object[];
                
                clone.Rows.Add(desRow);
            }
            gridControl4.DataSource = clone;
        }
        /// <summary>
        /// Handles the Validate event of the Repo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ValidationEventArgs"/> instance containing the event data.</param>
        private void Repo_Validate(object sender, ValidationEventArgs e)
        {
            
            eventItems.Add(e);
           // Debug.Print(e.ToString());
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
        /// Handles the DataSourceChanged event of the gridControl2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void gridControl2_DataSourceChanged(object sender, EventArgs e)
        {
            var total = gridView2.Columns.Skip(1).Sum(s => s.Width);
            gridView2.Columns[0].Width = gridControl2.Width - total - 300;
        }
    }
}