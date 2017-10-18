using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Fme.Database.Verification
{
    partial class MainView
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            this.compareMappingModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnExecute = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.skinBarSubItem2 = new DevExpress.XtraBars.SkinBarSubItem();
            this.btnAutoGenerate = new DevExpress.XtraBars.BarButtonItem();
            this.tnExportPackage = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.lblStatus = new DevExpress.XtraBars.BarStaticItem();
            this.barEditStatus = new DevExpress.XtraBars.BarEditItem();
            this.mbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.lblElapsed = new DevExpress.XtraBars.BarStaticItem();
            this.lblVersion = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.skinBarSubItem1 = new DevExpress.XtraBars.SkinBarSubItem();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.pbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.commandBarGalleryDropDown2 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown3 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown4 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown5 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown1 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.cbComparType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabConfiguration = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabControl4 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage8 = new DevExpress.XtraTab.XtraTabPage();
            this.chkTargetVersions = new System.Windows.Forms.CheckBox();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.cbTargetTZ = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cbTargetKey = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbTargetTable = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnTargetData = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl3 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.chkSourceVersions = new System.Windows.Forms.CheckBox();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.cbSourceTZ = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnDCTM = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetSchema = new DevExpress.XtraEditors.SimpleButton();
            this.cbSourceKey = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbSourceTable = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSourceData = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.chkSourceRandom = new System.Windows.Forms.CheckBox();
            this.txtSourceMaxRows = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnEditIdList = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabFieldMappings = new DevExpress.XtraTab.XtraTabPage();
            this.gridMappings = new DevExpress.XtraGrid.GridControl();
            this.ctxGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.hideEmptyColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHiddenColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMappings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repo_chkSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLeftSide = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbLeftSide = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colRightSide = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbRightSide = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colCompareType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbCompareType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colIgnoreChars = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colErrors = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbOperator = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colSelection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabLookup = new DevExpress.XtraTab.XtraTabPage();
            this.gridFieldLookup = new DevExpress.XtraGrid.GridControl();
            this.viewFieldLookup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colLeftLookupFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colRightLookupFile1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox5 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemMRUEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit();
            this.xtraTabCalcFields = new DevExpress.XtraTab.XtraTabPage();
            this.gridCalcFields = new DevExpress.XtraGrid.GridControl();
            this.viewCalcFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCalcSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCalculated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox7 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemComboBox8 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox9 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemMRUEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.gridSourcehSchema = new DevExpress.XtraGrid.GridControl();
            this.viewSourceSchema = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.gridTargetSchema = new DevExpress.XtraGrid.GridControl();
            this.viewTargetSchema = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabComparison = new DevExpress.XtraTab.XtraTabPage();
            this.gridComparison = new DevExpress.XtraGrid.GridControl();
            this.viewComparison = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dataNavigator1 = new DevExpress.XtraEditors.DataNavigator();
            this.bsMappings = new System.Windows.Forms.BindingSource(this.components);
            this.navGridMappings = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorDeleteAll = new System.Windows.Forms.ToolStripButton();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.listBoxControl2 = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.lbTargetFields = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.lbSourceFields = new DevExpress.XtraEditors.ListBoxControl();
            this.tabSourceData = new DevExpress.XtraTab.XtraTabPage();
            this.gridSourceData = new DevExpress.XtraGrid.GridControl();
            this.viewSourceData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabTargetData = new DevExpress.XtraTab.XtraTabPage();
            this.gridTargetData = new DevExpress.XtraGrid.GridControl();
            this.viewTargetData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabGridResults = new DevExpress.XtraTab.XtraTabPage();
            this.gridResults = new DevExpress.XtraGrid.GridControl();
            this.viewResults = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabCompareResults = new DevExpress.XtraTab.XtraTabPage();
            this.gridReport = new DevExpress.XtraGrid.GridControl();
            this.viewReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabQueries = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtSourceQuery = new DevExpress.XtraEditors.MemoEdit();
            this.txtTargetQuery = new DevExpress.XtraEditors.MemoEdit();
            this.tabMessages = new DevExpress.XtraTab.XtraTabPage();
            this.gridMessages = new DevExpress.XtraGrid.GridControl();
            this.cardViewMessages = new DevExpress.XtraGrid.Views.Card.CardView();
            this.repoItemMemo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repoItemMessage = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.viewMessages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.fieldSchemaModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableSchemaModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.timerElapsed = new System.Windows.Forms.Timer(this.components);
            this.ctxLookupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewErrorDetailReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colRightLookupFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupMenu3 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.tmrMonitor = new System.Windows.Forms.Timer(this.components);
            this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.compareMappingModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbComparType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl4)).BeginInit();
            this.xtraTabControl4.SuspendLayout();
            this.xtraTabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetTZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTargetData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl3)).BeginInit();
            this.xtraTabControl3.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceTZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSourceData.Properties)).BeginInit();
            this.xtraTabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceMaxRows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditIdList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabFieldMappings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).BeginInit();
            this.ctxGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_chkSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLeftSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRightSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCompareType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator)).BeginInit();
            this.xtraTabLookup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFieldLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMRUEdit1)).BeginInit();
            this.xtraTabCalcFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCalcFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCalcFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMRUEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSourcehSchema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSourceSchema)).BeginInit();
            this.xtraTabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTargetSchema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTargetSchema)).BeginInit();
            this.xtraTabComparison.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridComparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewComparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navGridMappings)).BeginInit();
            this.navGridMappings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbTargetFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbSourceFields)).BeginInit();
            this.tabSourceData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSourceData)).BeginInit();
            this.tabTargetData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTargetData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTargetData)).BeginInit();
            this.tabGridResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewResults)).BeginInit();
            this.tabCompareResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReport)).BeginInit();
            this.tabQueries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuery.Properties)).BeginInit();
            this.tabMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardViewMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldSchemaModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableSchemaModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.ctxLookupMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // compareMappingModelBindingSource
            // 
            this.compareMappingModelBindingSource.DataSource = typeof(Fme.Library.Models.CompareMappingModel);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnOpen,
            this.btnSave,
            this.skinBarSubItem1,
            this.barDockingMenuItem1,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.btnExecute,
            this.btnCancel,
            this.barButtonItem10,
            this.barButtonItem11,
            this.btnNew,
            this.barEditItem1,
            this.barEditStatus,
            this.lblStatus,
            this.skinBarSubItem2,
            this.btnRefresh,
            this.lblElapsed,
            this.btnAutoGenerate,
            this.lblVersion,
            this.btnExport,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem7,
            this.barButtonItem8,
            this.tnExportPackage});
            this.barManager1.MaxItemId = 201;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.pbStatus,
            this.mbStatus});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOpen, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExecute, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.skinBarSubItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAutoGenerate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.tnExportPackage, true)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnNew
            // 
            this.btnNew.Caption = "New";
            this.btnNew.Id = 13;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.LargeImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.Caption = "Open";
            this.btnOpen.Id = 0;
            this.btnOpen.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.ImageOptions.Image")));
            this.btnOpen.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.ImageOptions.LargeImage")));
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpen_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.LargeImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Id = 5;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // btnExecute
            // 
            this.btnExecute.Caption = "Execute";
            this.btnExecute.Id = 9;
            this.btnExecute.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.ImageOptions.Image")));
            this.btnExecute.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExecute.ImageOptions.LargeImage")));
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem1.Text = "Execute a Compare";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.btnExecute.SuperTip = superToolTip1;
            this.btnExecute.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExecute_ItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Caption = "Cancel";
            this.btnCancel.Id = 10;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.LargeImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem2.Text = "Cancel Execution";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.btnCancel.SuperTip = superToolTip2;
            this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Refresh";
            this.btnRefresh.Id = 18;
            this.btnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.Image")));
            this.btnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.LargeImage")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // skinBarSubItem2
            // 
            this.skinBarSubItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.skinBarSubItem2.Caption = "Skin";
            this.skinBarSubItem2.Id = 17;
            this.skinBarSubItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("skinBarSubItem2.ImageOptions.Image")));
            this.skinBarSubItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("skinBarSubItem2.ImageOptions.LargeImage")));
            this.skinBarSubItem2.Name = "skinBarSubItem2";
            this.skinBarSubItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnAutoGenerate
            // 
            this.btnAutoGenerate.Caption = " Generate Mappings";
            this.btnAutoGenerate.Id = 193;
            this.btnAutoGenerate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoGenerate.ImageOptions.Image")));
            this.btnAutoGenerate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAutoGenerate.ImageOptions.LargeImage")));
            this.btnAutoGenerate.Name = "btnAutoGenerate";
            this.btnAutoGenerate.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAutoGenerate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAutoGenerate_ItemClick);
            // 
            // tnExportPackage
            // 
            this.tnExportPackage.Caption = "Export Package";
            this.tnExportPackage.Id = 200;
            this.tnExportPackage.Name = "tnExportPackage";
            this.tnExportPackage.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.tnExportPackage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tnExportPackage_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lblStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.lblElapsed),
            new DevExpress.XtraBars.LinkPersistInfo(this.lblVersion)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // lblStatus
            // 
            this.lblStatus.Caption = "Status";
            this.lblStatus.Id = 16;
            this.lblStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lblStatus.ImageOptions.Image")));
            this.lblStatus.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("lblStatus.ImageOptions.LargeImage")));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.lblStatus.Size = new System.Drawing.Size(500, 0);
            this.lblStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblStatus.Width = 500;
            // 
            // barEditStatus
            // 
            this.barEditStatus.AutoFillWidth = true;
            this.barEditStatus.Edit = this.mbStatus;
            this.barEditStatus.Id = 15;
            this.barEditStatus.ImageOptions.Image = global::Fme.Database.Verification.Properties.Resources.deletesheetrows_16x16;
            this.barEditStatus.ImageOptions.LargeImage = global::Fme.Database.Verification.Properties.Resources.deletesheetrows_32x32;
            this.barEditStatus.Name = "barEditStatus";
            this.barEditStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // mbStatus
            // 
            this.mbStatus.Name = "mbStatus";
            // 
            // lblElapsed
            // 
            this.lblElapsed.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.lblElapsed.Caption = "Idle";
            this.lblElapsed.Id = 192;
            this.lblElapsed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("lblElapsed.ImageOptions.Image")));
            this.lblElapsed.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("lblElapsed.ImageOptions.LargeImage")));
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.lblElapsed.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblVersion
            // 
            this.lblVersion.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.lblVersion.Caption = "v1.2.7";
            this.lblVersion.Id = 194;
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(862, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 575);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(862, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 544);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(862, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 544);
            // 
            // skinBarSubItem1
            // 
            this.skinBarSubItem1.Caption = "skinBarSubItem1";
            this.skinBarSubItem1.Id = 3;
            this.skinBarSubItem1.Name = "skinBarSubItem1";
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "barDockingMenuItem1";
            this.barDockingMenuItem1.Id = 4;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.ActAsDropDown = true;
            this.barButtonItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem4.Caption = "Source";
            this.barButtonItem4.DropDownControl = this.popupMenu1;
            this.barButtonItem4.Id = 6;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem3.Text = "Select the Source Data";
            superToolTip3.Items.Add(toolTipTitleItem3);
            this.barButtonItem4.SuperTip = superToolTip3;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "Excel";
            this.barButtonItem10.Id = 11;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "Oraclel";
            this.barButtonItem11.Id = 12;
            this.barButtonItem11.Name = "barButtonItem11";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.ActAsDropDown = true;
            this.barButtonItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem5.Caption = "Target";
            this.barButtonItem5.Description = "Select Source Data";
            this.barButtonItem5.DropDownControl = this.popupMenu2;
            this.barButtonItem5.Id = 7;
            this.barButtonItem5.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.barButtonItem5.ImageOptions.AllowStubGlyph = DevExpress.Utils.DefaultBoolean.False;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem4.Text = "Select the Target Data";
            superToolTip4.Items.Add(toolTipTitleItem4);
            this.barButtonItem5.SuperTip = superToolTip4;
            // 
            // popupMenu2
            // 
            this.popupMenu2.Manager = this.barManager1;
            this.popupMenu2.Name = "popupMenu2";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 8;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barEditItem1
            // 
            this.barEditItem1.AutoFillWidthInMenu = DevExpress.Utils.DefaultBoolean.False;
            this.barEditItem1.Caption = "Progress Bar";
            this.barEditItem1.Edit = this.pbStatus;
            this.barEditItem1.Id = 14;
            this.barEditItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEditItem1.ImageOptions.Image")));
            this.barEditItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barEditItem1.ImageOptions.LargeImage")));
            this.barEditItem1.Name = "barEditItem1";
            // 
            // pbStatus
            // 
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.ShowTitle = true;
            // 
            // btnExport
            // 
            this.btnExport.Caption = "Export";
            this.btnExport.Id = 195;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.LargeImage")));
            this.btnExport.Name = "btnExport";
            this.btnExport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Print/Export Data";
            this.barButtonItem1.Id = 196;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Hide Empty Columns";
            this.barButtonItem2.Id = 197;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Delete Empty Columns";
            this.barButtonItem7.Id = 198;
            this.barButtonItem7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.Image")));
            this.barButtonItem7.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.LargeImage")));
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "Show Hidden Columns";
            this.barButtonItem8.Id = 199;
            this.barButtonItem8.ImageOptions.Image = global::Fme.Database.Verification.Properties.Resources.show_16x16;
            this.barButtonItem8.ImageOptions.LargeImage = global::Fme.Database.Verification.Properties.Resources.show_32x32;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // commandBarGalleryDropDown2
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown2.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown2.Gallery.ImageSize = new System.Drawing.Size(32, 32);
            this.commandBarGalleryDropDown2.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.commandBarGalleryDropDown2.Manager = this.barManager1;
            this.commandBarGalleryDropDown2.Name = "commandBarGalleryDropDown2";
            // 
            // commandBarGalleryDropDown3
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown3.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown3.Gallery.ImageSize = new System.Drawing.Size(32, 32);
            this.commandBarGalleryDropDown3.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.commandBarGalleryDropDown3.Manager = this.barManager1;
            this.commandBarGalleryDropDown3.Name = "commandBarGalleryDropDown3";
            // 
            // commandBarGalleryDropDown4
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown4.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown4.Gallery.ImageSize = new System.Drawing.Size(32, 32);
            this.commandBarGalleryDropDown4.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.commandBarGalleryDropDown4.Manager = this.barManager1;
            this.commandBarGalleryDropDown4.Name = "commandBarGalleryDropDown4";
            // 
            // commandBarGalleryDropDown5
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown5.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown5.Gallery.ColumnCount = 7;
            this.commandBarGalleryDropDown5.Gallery.DrawImageBackground = false;
            this.commandBarGalleryDropDown5.Gallery.ItemAutoSizeMode = DevExpress.XtraBars.Ribbon.Gallery.GalleryItemAutoSizeMode.None;
            this.commandBarGalleryDropDown5.Gallery.ItemSize = new System.Drawing.Size(73, 58);
            this.commandBarGalleryDropDown5.Gallery.RowCount = 10;
            this.commandBarGalleryDropDown5.Manager = this.barManager1;
            this.commandBarGalleryDropDown5.Name = "commandBarGalleryDropDown5";
            // 
            // commandBarGalleryDropDown1
            // 
            // 
            // 
            // 
            this.commandBarGalleryDropDown1.Gallery.AllowFilter = false;
            this.commandBarGalleryDropDown1.Gallery.ColumnCount = 1;
            this.commandBarGalleryDropDown1.Gallery.DrawImageBackground = false;
            this.commandBarGalleryDropDown1.Gallery.ImageSize = new System.Drawing.Size(65, 46);
            this.commandBarGalleryDropDown1.Gallery.ItemAutoSizeMode = DevExpress.XtraBars.Ribbon.Gallery.GalleryItemAutoSizeMode.None;
            this.commandBarGalleryDropDown1.Gallery.ItemSize = new System.Drawing.Size(136, 26);
            this.commandBarGalleryDropDown1.Gallery.RowCount = 14;
            this.commandBarGalleryDropDown1.Gallery.ShowGroupCaption = false;
            this.commandBarGalleryDropDown1.Gallery.ShowItemText = true;
            this.commandBarGalleryDropDown1.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.commandBarGalleryDropDown1.Manager = this.barManager1;
            this.commandBarGalleryDropDown1.Name = "commandBarGalleryDropDown1";
            // 
            // cbComparType
            // 
            this.cbComparType.AutoHeight = false;
            this.cbComparType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbComparType.Name = "cbComparType";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 31);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabConfiguration;
            this.xtraTabControl1.Size = new System.Drawing.Size(862, 544);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabConfiguration,
            this.tabSourceData,
            this.tabTargetData,
            this.tabGridResults,
            this.tabCompareResults,
            this.tabQueries,
            this.tabMessages});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabControl1.Click += new System.EventHandler(this.xtraTabControl1_Click);
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Controls.Add(this.xtraTabControl4);
            this.tabConfiguration.Controls.Add(this.xtraTabControl3);
            this.tabConfiguration.Controls.Add(this.xtraTabControl2);
            this.tabConfiguration.Controls.Add(this.dataNavigator1);
            this.tabConfiguration.Controls.Add(this.navGridMappings);
            this.tabConfiguration.Controls.Add(this.groupControl6);
            this.tabConfiguration.Controls.Add(this.groupControl4);
            this.tabConfiguration.Controls.Add(this.groupControl3);
            this.tabConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("tabConfiguration.Image")));
            this.tabConfiguration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Size = new System.Drawing.Size(856, 513);
            this.tabConfiguration.Text = "Configuration";
            // 
            // xtraTabControl4
            // 
            this.xtraTabControl4.Location = new System.Drawing.Point(439, 15);
            this.xtraTabControl4.Name = "xtraTabControl4";
            this.xtraTabControl4.SelectedTabPage = this.xtraTabPage8;
            this.xtraTabControl4.Size = new System.Drawing.Size(388, 181);
            this.xtraTabControl4.TabIndex = 28;
            this.xtraTabControl4.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage8});
            // 
            // xtraTabPage8
            // 
            this.xtraTabPage8.Controls.Add(this.chkTargetVersions);
            this.xtraTabPage8.Controls.Add(this.labelControl12);
            this.xtraTabPage8.Controls.Add(this.cbTargetTZ);
            this.xtraTabPage8.Controls.Add(this.simpleButton2);
            this.xtraTabPage8.Controls.Add(this.simpleButton1);
            this.xtraTabPage8.Controls.Add(this.cbTargetKey);
            this.xtraTabPage8.Controls.Add(this.cbTargetTable);
            this.xtraTabPage8.Controls.Add(this.labelControl6);
            this.xtraTabPage8.Controls.Add(this.labelControl4);
            this.xtraTabPage8.Controls.Add(this.btnTargetData);
            this.xtraTabPage8.Controls.Add(this.labelControl2);
            this.xtraTabPage8.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage8.Image")));
            this.xtraTabPage8.Name = "xtraTabPage8";
            this.xtraTabPage8.Size = new System.Drawing.Size(382, 150);
            this.xtraTabPage8.Text = "Source";
            // 
            // chkTargetVersions
            // 
            this.chkTargetVersions.AutoSize = true;
            this.chkTargetVersions.Location = new System.Drawing.Point(24, 125);
            this.chkTargetVersions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTargetVersions.Name = "chkTargetVersions";
            this.chkTargetVersions.Size = new System.Drawing.Size(113, 17);
            this.chkTargetVersions.TabIndex = 57;
            this.chkTargetVersions.Text = "Include All Version";
            this.chkTargetVersions.UseVisualStyleBackColor = true;
            this.chkTargetVersions.Visible = false;
            this.chkTargetVersions.CheckedChanged += new System.EventHandler(this.chkTargetVersions_CheckedChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(204, 125);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(79, 13);
            this.labelControl12.TabIndex = 56;
            this.labelControl12.Text = "Timezone Offset";
            // 
            // cbTargetTZ
            // 
            this.cbTargetTZ.EditValue = "0";
            this.cbTargetTZ.Location = new System.Drawing.Point(289, 122);
            this.cbTargetTZ.MenuManager = this.barManager1;
            this.cbTargetTZ.Name = "cbTargetTZ";
            this.cbTargetTZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTargetTZ.Size = new System.Drawing.Size(57, 20);
            this.cbTargetTZ.TabIndex = 55;
            this.cbTargetTZ.SelectedIndexChanged += new System.EventHandler(this.cbTargetTZ_SelectedIndexChanged);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(347, 36);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(17, 18);
            this.simpleButton2.TabIndex = 53;
            this.simpleButton2.Tag = "2";
            this.simpleButton2.Text = "...";
            this.simpleButton2.ToolTip = "Documentum";
            this.simpleButton2.Visible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(347, 70);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(17, 18);
            this.simpleButton1.TabIndex = 39;
            this.simpleButton1.Text = "...";
            this.simpleButton1.Visible = false;
            // 
            // cbTargetKey
            // 
            this.cbTargetKey.Location = new System.Drawing.Point(120, 97);
            this.cbTargetKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTargetKey.MenuManager = this.barManager1;
            this.cbTargetKey.Name = "cbTargetKey";
            this.cbTargetKey.Properties.AllowMouseWheel = false;
            this.cbTargetKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTargetKey.Properties.Sorted = true;
            this.cbTargetKey.Size = new System.Drawing.Size(226, 20);
            this.cbTargetKey.TabIndex = 38;
            this.cbTargetKey.SelectedIndexChanged += new System.EventHandler(this.cbTargetKey_SelectedIndexChanged);
            this.cbTargetKey.SelectedValueChanged += new System.EventHandler(this.cbTargetKey_SelectedIndexChanged);
            this.cbTargetKey.Leave += new System.EventHandler(this.cbTargetKey_Leave);
            // 
            // cbTargetTable
            // 
            this.cbTargetTable.Location = new System.Drawing.Point(120, 70);
            this.cbTargetTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTargetTable.MenuManager = this.barManager1;
            this.cbTargetTable.Name = "cbTargetTable";
            this.cbTargetTable.Properties.AllowMouseWheel = false;
            this.cbTargetTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbTargetTable.Properties.Sorted = true;
            this.cbTargetTable.Size = new System.Drawing.Size(226, 20);
            this.cbTargetTable.TabIndex = 37;
            this.cbTargetTable.SelectedIndexChanged += new System.EventHandler(this.cbTargetTable_SelectedIndexChanged);
            this.cbTargetTable.Leave += new System.EventHandler(this.cbTargetTable_Leave);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(24, 100);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(79, 13);
            this.labelControl6.TabIndex = 36;
            this.labelControl6.Text = "Source Key Field";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 72);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(61, 13);
            this.labelControl4.TabIndex = 35;
            this.labelControl4.Text = "Target Table";
            // 
            // btnTargetData
            // 
            this.btnTargetData.Location = new System.Drawing.Point(24, 36);
            this.btnTargetData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTargetData.MenuManager = this.barManager1;
            this.btnTargetData.Name = "btnTargetData";
            this.btnTargetData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnTargetData.Properties.ReadOnly = true;
            this.btnTargetData.Size = new System.Drawing.Size(322, 20);
            this.btnTargetData.TabIndex = 34;
            this.btnTargetData.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnTargetData_ButtonClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 18);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 33;
            this.labelControl2.Text = "Data Source";
            // 
            // xtraTabControl3
            // 
            this.xtraTabControl3.Location = new System.Drawing.Point(29, 15);
            this.xtraTabControl3.Name = "xtraTabControl3";
            this.xtraTabControl3.SelectedTabPage = this.xtraTabPage6;
            this.xtraTabControl3.Size = new System.Drawing.Size(398, 181);
            this.xtraTabControl3.TabIndex = 27;
            this.xtraTabControl3.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage6,
            this.xtraTabPage7});
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.chkSourceVersions);
            this.xtraTabPage6.Controls.Add(this.labelControl11);
            this.xtraTabPage6.Controls.Add(this.cbSourceTZ);
            this.xtraTabPage6.Controls.Add(this.btnDCTM);
            this.xtraTabPage6.Controls.Add(this.btnGetSchema);
            this.xtraTabPage6.Controls.Add(this.cbSourceKey);
            this.xtraTabPage6.Controls.Add(this.cbSourceTable);
            this.xtraTabPage6.Controls.Add(this.labelControl5);
            this.xtraTabPage6.Controls.Add(this.labelControl3);
            this.xtraTabPage6.Controls.Add(this.btnSourceData);
            this.xtraTabPage6.Controls.Add(this.labelControl1);
            this.xtraTabPage6.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage6.Image")));
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(392, 150);
            this.xtraTabPage6.Text = "Target";
            // 
            // chkSourceVersions
            // 
            this.chkSourceVersions.AutoSize = true;
            this.chkSourceVersions.Location = new System.Drawing.Point(24, 122);
            this.chkSourceVersions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSourceVersions.Name = "chkSourceVersions";
            this.chkSourceVersions.Size = new System.Drawing.Size(113, 17);
            this.chkSourceVersions.TabIndex = 55;
            this.chkSourceVersions.Text = "Include All Version";
            this.chkSourceVersions.UseVisualStyleBackColor = true;
            this.chkSourceVersions.Visible = false;
            this.chkSourceVersions.CheckedChanged += new System.EventHandler(this.chkSourceVersions_CheckedChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(206, 121);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(79, 13);
            this.labelControl11.TabIndex = 54;
            this.labelControl11.Text = "Timezone Offset";
            // 
            // cbSourceTZ
            // 
            this.cbSourceTZ.EditValue = "0";
            this.cbSourceTZ.Location = new System.Drawing.Point(292, 118);
            this.cbSourceTZ.MenuManager = this.barManager1;
            this.cbSourceTZ.Name = "cbSourceTZ";
            this.cbSourceTZ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSourceTZ.Size = new System.Drawing.Size(57, 20);
            this.cbSourceTZ.TabIndex = 53;
            this.cbSourceTZ.ToolTip = "Timezone Offset for DateTime Comparisons";
            this.cbSourceTZ.SelectedIndexChanged += new System.EventHandler(this.cbSourceTZ_SelectedIndexChanged);
            // 
            // btnDCTM
            // 
            this.btnDCTM.Location = new System.Drawing.Point(351, 36);
            this.btnDCTM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDCTM.Name = "btnDCTM";
            this.btnDCTM.Size = new System.Drawing.Size(17, 18);
            this.btnDCTM.TabIndex = 52;
            this.btnDCTM.Tag = "1";
            this.btnDCTM.Text = "...";
            this.btnDCTM.ToolTip = "Documentum";
            this.btnDCTM.Visible = false;
            // 
            // btnGetSchema
            // 
            this.btnGetSchema.Location = new System.Drawing.Point(351, 65);
            this.btnGetSchema.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetSchema.Name = "btnGetSchema";
            this.btnGetSchema.Size = new System.Drawing.Size(17, 18);
            this.btnGetSchema.TabIndex = 51;
            this.btnGetSchema.Text = "...";
            this.btnGetSchema.Visible = false;
            // 
            // cbSourceKey
            // 
            this.cbSourceKey.Location = new System.Drawing.Point(120, 93);
            this.cbSourceKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSourceKey.MenuManager = this.barManager1;
            this.cbSourceKey.Name = "cbSourceKey";
            this.cbSourceKey.Properties.AllowMouseWheel = false;
            this.cbSourceKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSourceKey.Properties.Sorted = true;
            this.cbSourceKey.Size = new System.Drawing.Size(229, 20);
            this.cbSourceKey.TabIndex = 47;
            this.cbSourceKey.SelectedIndexChanged += new System.EventHandler(this.cbSourceKey_SelectedIndexChanged);
            this.cbSourceKey.SelectedValueChanged += new System.EventHandler(this.cbSourceKey_SelectedIndexChanged);
            this.cbSourceKey.Leave += new System.EventHandler(this.cbSourceKey_Leave);
            // 
            // cbSourceTable
            // 
            this.cbSourceTable.Location = new System.Drawing.Point(120, 65);
            this.cbSourceTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSourceTable.MenuManager = this.barManager1;
            this.cbSourceTable.Name = "cbSourceTable";
            this.cbSourceTable.Properties.AllowMouseWheel = false;
            this.cbSourceTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSourceTable.Properties.Sorted = true;
            this.cbSourceTable.Size = new System.Drawing.Size(229, 20);
            this.cbSourceTable.TabIndex = 46;
            this.cbSourceTable.SelectedIndexChanged += new System.EventHandler(this.cbSourceTable_SelectedIndexChanged);
            this.cbSourceTable.Leave += new System.EventHandler(this.cbSourceTable_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 67);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 45;
            this.labelControl5.Text = "Source Table";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 95);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 44;
            this.labelControl3.Text = "Target Key Field";
            // 
            // btnSourceData
            // 
            this.btnSourceData.Location = new System.Drawing.Point(24, 36);
            this.btnSourceData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSourceData.MenuManager = this.barManager1;
            this.btnSourceData.Name = "btnSourceData";
            this.btnSourceData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnSourceData.Properties.ReadOnly = true;
            this.btnSourceData.Size = new System.Drawing.Size(325, 20);
            this.btnSourceData.TabIndex = 43;
            this.btnSourceData.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnSourceData_ButtonClick);
            this.btnSourceData.DoubleClick += new System.EventHandler(this.btnSourceData_DoubleClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 19);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 42;
            this.labelControl1.Text = "Data Source";
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.chkSourceRandom);
            this.xtraTabPage7.Controls.Add(this.txtSourceMaxRows);
            this.xtraTabPage7.Controls.Add(this.labelControl9);
            this.xtraTabPage7.Controls.Add(this.labelControl10);
            this.xtraTabPage7.Controls.Add(this.btnEditIdList);
            this.xtraTabPage7.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage7.Image")));
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(392, 150);
            this.xtraTabPage7.Text = "Id List";
            // 
            // chkSourceRandom
            // 
            this.chkSourceRandom.AutoSize = true;
            this.chkSourceRandom.Location = new System.Drawing.Point(199, 66);
            this.chkSourceRandom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSourceRandom.Name = "chkSourceRandom";
            this.chkSourceRandom.Size = new System.Drawing.Size(100, 17);
            this.chkSourceRandom.TabIndex = 53;
            this.chkSourceRandom.Text = "Select Random ";
            this.chkSourceRandom.UseVisualStyleBackColor = true;
            this.chkSourceRandom.CheckedChanged += new System.EventHandler(this.chkSourceRandom_CheckedChanged);
            // 
            // txtSourceMaxRows
            // 
            this.txtSourceMaxRows.EditValue = "100";
            this.txtSourceMaxRows.Location = new System.Drawing.Point(95, 64);
            this.txtSourceMaxRows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSourceMaxRows.MenuManager = this.barManager1;
            this.txtSourceMaxRows.Name = "txtSourceMaxRows";
            this.txtSourceMaxRows.Size = new System.Drawing.Size(86, 20);
            this.txtSourceMaxRows.TabIndex = 52;
            this.txtSourceMaxRows.EditValueChanged += new System.EventHandler(this.txtSourceMaxRows_EditValueChanged);
            this.txtSourceMaxRows.Validating += new System.ComponentModel.CancelEventHandler(this.txtSourceMaxRows_Validating);
            this.txtSourceMaxRows.Validated += new System.EventHandler(this.txtSourceMaxRows_Validated);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(27, 68);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(62, 13);
            this.labelControl9.TabIndex = 51;
            this.labelControl9.Text = "Max Records";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(28, 18);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(49, 13);
            this.labelControl10.TabIndex = 16;
            this.labelControl10.Text = "ID File List";
            // 
            // btnEditIdList
            // 
            this.btnEditIdList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditIdList.Location = new System.Drawing.Point(27, 36);
            this.btnEditIdList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditIdList.MenuManager = this.barManager1;
            this.btnEditIdList.Name = "btnEditIdList";
            this.btnEditIdList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditIdList.Size = new System.Drawing.Size(340, 20);
            this.btnEditIdList.TabIndex = 15;
            this.btnEditIdList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditIdList_ButtonClick);
            this.btnEditIdList.EditValueChanged += new System.EventHandler(this.btnEditIdList_Leave);
            this.btnEditIdList.Leave += new System.EventHandler(this.btnEditIdList_Leave);
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl2.Location = new System.Drawing.Point(27, 206);
            this.xtraTabControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabFieldMappings;
            this.xtraTabControl2.Size = new System.Drawing.Size(804, 262);
            this.xtraTabControl2.TabIndex = 26;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabFieldMappings,
            this.xtraTabLookup,
            this.xtraTabCalcFields,
            this.xtraTabPage4,
            this.xtraTabPage5,
            this.xtraTabComparison});
            this.xtraTabControl2.Click += new System.EventHandler(this.xtraTabControl2_Click);
            // 
            // xtraTabFieldMappings
            // 
            this.xtraTabFieldMappings.Controls.Add(this.gridMappings);
            this.xtraTabFieldMappings.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabFieldMappings.Image")));
            this.xtraTabFieldMappings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabFieldMappings.Name = "xtraTabFieldMappings";
            this.xtraTabFieldMappings.Size = new System.Drawing.Size(798, 231);
            this.xtraTabFieldMappings.Text = "Field Mappings";
            // 
            // gridMappings
            // 
            this.gridMappings.AllowDrop = true;
            this.gridMappings.ContextMenuStrip = this.ctxGrid;
            this.gridMappings.DataSource = this.compareMappingModelBindingSource;
            this.gridMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMappings.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridMappings.Location = new System.Drawing.Point(0, 0);
            this.gridMappings.MainView = this.viewMappings;
            this.gridMappings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridMappings.MenuManager = this.barManager1;
            this.gridMappings.Name = "gridMappings";
            this.gridMappings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbLeftSide,
            this.cbRightSide,
            this.cbCompareType,
            this.cbOperator,
            this.repo_chkSelected,
            this.repoSpinEdit1});
            this.gridMappings.Size = new System.Drawing.Size(798, 231);
            this.gridMappings.TabIndex = 0;
            this.gridMappings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMappings});
            this.gridMappings.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gridControl_ProcessGridKey);
            this.gridMappings.Enter += new System.EventHandler(this.GridMappings_Enter);
            // 
            // ctxGrid
            // 
            this.ctxGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.hideEmptyColumnToolStripMenuItem,
            this.showHiddenColumnsToolStripMenuItem});
            this.ctxGrid.Name = "ctxGrid";
            this.ctxGrid.Size = new System.Drawing.Size(197, 76);
            this.ctxGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ctxGrid_Opening);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exportToolStripMenuItem.Text = "Print/Export Data";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(193, 6);
            // 
            // hideEmptyColumnToolStripMenuItem
            // 
            this.hideEmptyColumnToolStripMenuItem.Name = "hideEmptyColumnToolStripMenuItem";
            this.hideEmptyColumnToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.hideEmptyColumnToolStripMenuItem.Text = "Hide Empty Columns";
            this.hideEmptyColumnToolStripMenuItem.Click += new System.EventHandler(this.HideEmptyColumnToolStripMenuItem_Click);
            // 
            // showHiddenColumnsToolStripMenuItem
            // 
            this.showHiddenColumnsToolStripMenuItem.Name = "showHiddenColumnsToolStripMenuItem";
            this.showHiddenColumnsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showHiddenColumnsToolStripMenuItem.Text = "Show Hidden Columns";
            this.showHiddenColumnsToolStripMenuItem.Click += new System.EventHandler(this.showHiddenColumnsToolStripMenuItem_Click);
            // 
            // viewMappings
            // 
            this.viewMappings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colLeftSide,
            this.colRightSide,
            this.colCompareType,
            this.colIgnoreChars,
            this.colErrors,
            this.colStatus,
            this.colOperator,
            this.colSelection,
            this.colStartTime});
            this.viewMappings.CustomizationFormBounds = new System.Drawing.Rectangle(924, 426, 212, 212);
            this.viewMappings.GridControl = this.gridMappings;
            this.viewMappings.Name = "viewMappings";
            this.viewMappings.OptionsCustomization.AllowRowSizing = true;
            this.viewMappings.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.viewMappings.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewMappings.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewMapping_RowCellStyle);
            this.viewMappings.LostFocus += new System.EventHandler(this.viewMappings_LostFocus);
            // 
            // colSelected
            // 
            this.colSelected.ColumnEdit = this.repo_chkSelected;
            this.colSelected.FieldName = "Selected";
            this.colSelected.Name = "colSelected";
            this.colSelected.ToolTip = "Selected";
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            this.colSelected.Width = 48;
            // 
            // repo_chkSelected
            // 
            this.repo_chkSelected.AutoHeight = false;
            this.repo_chkSelected.Name = "repo_chkSelected";
            // 
            // colLeftSide
            // 
            this.colLeftSide.ColumnEdit = this.cbLeftSide;
            this.colLeftSide.FieldName = "LeftSide";
            this.colLeftSide.Name = "colLeftSide";
            this.colLeftSide.Visible = true;
            this.colLeftSide.VisibleIndex = 1;
            this.colLeftSide.Width = 127;
            // 
            // cbLeftSide
            // 
            this.cbLeftSide.AllowMouseWheel = false;
            this.cbLeftSide.AutoHeight = false;
            this.cbLeftSide.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLeftSide.Name = "cbLeftSide";
            this.cbLeftSide.Sorted = true;
            // 
            // colRightSide
            // 
            this.colRightSide.ColumnEdit = this.cbRightSide;
            this.colRightSide.FieldName = "RightSide";
            this.colRightSide.Name = "colRightSide";
            this.colRightSide.Visible = true;
            this.colRightSide.VisibleIndex = 2;
            this.colRightSide.Width = 139;
            // 
            // cbRightSide
            // 
            this.cbRightSide.AllowMouseWheel = false;
            this.cbRightSide.AutoHeight = false;
            this.cbRightSide.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbRightSide.Name = "cbRightSide";
            this.cbRightSide.Sorted = true;
            // 
            // colCompareType
            // 
            this.colCompareType.ColumnEdit = this.cbCompareType;
            this.colCompareType.FieldName = "CompareType";
            this.colCompareType.Name = "colCompareType";
            this.colCompareType.Visible = true;
            this.colCompareType.VisibleIndex = 3;
            this.colCompareType.Width = 86;
            // 
            // cbCompareType
            // 
            this.cbCompareType.AllowMouseWheel = false;
            this.cbCompareType.AutoHeight = false;
            this.cbCompareType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCompareType.Name = "cbCompareType";
            this.cbCompareType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colIgnoreChars
            // 
            this.colIgnoreChars.FieldName = "IgnoreChars";
            this.colIgnoreChars.Name = "colIgnoreChars";
            this.colIgnoreChars.Width = 102;
            // 
            // colErrors
            // 
            this.colErrors.AppearanceCell.BackColor = System.Drawing.Color.Ivory;
            this.colErrors.AppearanceCell.Options.UseBackColor = true;
            this.colErrors.AppearanceCell.Options.UseTextOptions = true;
            this.colErrors.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colErrors.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colErrors.ColumnEdit = this.repoSpinEdit1;
            this.colErrors.FieldName = "Errors";
            this.colErrors.Name = "colErrors";
            this.colErrors.OptionsColumn.AllowEdit = false;
            this.colErrors.OptionsColumn.ReadOnly = true;
            this.colErrors.Visible = true;
            this.colErrors.VisibleIndex = 5;
            this.colErrors.Width = 44;
            // 
            // repoSpinEdit1
            // 
            this.repoSpinEdit1.AutoHeight = false;
            this.repoSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSpinEdit1.Name = "repoSpinEdit1";
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.BackColor = System.Drawing.Color.Ivory;
            this.colStatus.AppearanceCell.Options.UseBackColor = true;
            this.colStatus.AppearanceHeader.BackColor = System.Drawing.Color.Ivory;
            this.colStatus.AppearanceHeader.Options.UseBackColor = true;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 6;
            this.colStatus.Width = 100;
            // 
            // colOperator
            // 
            this.colOperator.ColumnEdit = this.cbOperator;
            this.colOperator.FieldName = "Operator";
            this.colOperator.Name = "colOperator";
            this.colOperator.Visible = true;
            this.colOperator.VisibleIndex = 4;
            this.colOperator.Width = 67;
            // 
            // cbOperator
            // 
            this.cbOperator.AllowMouseWheel = false;
            this.cbOperator.AutoHeight = false;
            this.cbOperator.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colSelection
            // 
            this.colSelection.FieldName = "Selection";
            this.colSelection.Name = "colSelection";
            this.colSelection.OptionsColumn.AllowEdit = false;
            this.colSelection.OptionsColumn.ReadOnly = true;
            this.colSelection.Width = 62;
            // 
            // colStartTime
            // 
            this.colStartTime.AppearanceCell.BackColor = System.Drawing.Color.Ivory;
            this.colStartTime.AppearanceCell.Options.UseBackColor = true;
            this.colStartTime.DisplayFormat.FormatString = "MM/dd/yy hh:mm:ss";
            this.colStartTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartTime.FieldName = "StartTime";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.OptionsColumn.AllowEdit = false;
            this.colStartTime.OptionsColumn.ReadOnly = true;
            this.colStartTime.Visible = true;
            this.colStartTime.VisibleIndex = 7;
            this.colStartTime.Width = 169;
            // 
            // xtraTabLookup
            // 
            this.xtraTabLookup.Controls.Add(this.gridFieldLookup);
            this.xtraTabLookup.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabLookup.Image")));
            this.xtraTabLookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabLookup.Name = "xtraTabLookup";
            this.xtraTabLookup.Size = new System.Drawing.Size(798, 231);
            this.xtraTabLookup.Text = "Field Lookups";
            // 
            // gridFieldLookup
            // 
            this.gridFieldLookup.AllowDrop = true;
            this.gridFieldLookup.ContextMenuStrip = this.ctxGrid;
            this.gridFieldLookup.DataSource = this.compareMappingModelBindingSource;
            this.gridFieldLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFieldLookup.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridFieldLookup.Location = new System.Drawing.Point(0, 0);
            this.gridFieldLookup.MainView = this.viewFieldLookup;
            this.gridFieldLookup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridFieldLookup.MenuManager = this.barManager1;
            this.gridFieldLookup.Name = "gridFieldLookup";
            this.gridFieldLookup.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox2,
            this.repositoryItemComboBox3,
            this.repositoryItemComboBox4,
            this.repositoryItemComboBox5,
            this.repositoryItemMRUEdit1,
            this.repositoryItemButtonEdit1});
            this.gridFieldLookup.Size = new System.Drawing.Size(798, 231);
            this.gridFieldLookup.TabIndex = 1;
            this.gridFieldLookup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFieldLookup});
            this.gridFieldLookup.DragDrop += new System.Windows.Forms.DragEventHandler(this.GridMappings_DragDrop);
            this.gridFieldLookup.DragOver += new System.Windows.Forms.DragEventHandler(this.GridMappings_DragOver);
            // 
            // viewFieldLookup
            // 
            this.viewFieldLookup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.colLeftLookupFile,
            this.gridColumn3,
            this.colRightLookupFile1});
            this.viewFieldLookup.CustomizationFormBounds = new System.Drawing.Rectangle(924, 426, 212, 212);
            this.viewFieldLookup.GridControl = this.gridFieldLookup;
            this.viewFieldLookup.Name = "viewFieldLookup";
            this.viewFieldLookup.OptionsCustomization.AllowSort = false;
            this.viewFieldLookup.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewMapping_RowCellStyle);
            // 
            // gridColumn2
            // 
            this.gridColumn2.ColumnEdit = this.repositoryItemComboBox2;
            this.gridColumn2.FieldName = "LeftSide";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 121;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // colLeftLookupFile
            // 
            this.colLeftLookupFile.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colLeftLookupFile.FieldName = "LeftLookupFile";
            this.colLeftLookupFile.Name = "colLeftLookupFile";
            this.colLeftLookupFile.Visible = true;
            this.colLeftLookupFile.VisibleIndex = 1;
            this.colLeftLookupFile.Width = 258;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepositoryItemButtonEdit1_ButtonClick);
            // 
            // gridColumn3
            // 
            this.gridColumn3.ColumnEdit = this.repositoryItemComboBox3;
            this.gridColumn3.FieldName = "RightSide";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 108;
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            // 
            // colRightLookupFile1
            // 
            this.colRightLookupFile1.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colRightLookupFile1.FieldName = "RightLookupFile";
            this.colRightLookupFile1.Name = "colRightLookupFile1";
            this.colRightLookupFile1.Visible = true;
            this.colRightLookupFile1.VisibleIndex = 3;
            this.colRightLookupFile1.Width = 424;
            // 
            // repositoryItemComboBox4
            // 
            this.repositoryItemComboBox4.AutoHeight = false;
            this.repositoryItemComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
            // 
            // repositoryItemComboBox5
            // 
            this.repositoryItemComboBox5.AutoHeight = false;
            this.repositoryItemComboBox5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
            // 
            // repositoryItemMRUEdit1
            // 
            this.repositoryItemMRUEdit1.AutoHeight = false;
            this.repositoryItemMRUEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMRUEdit1.Name = "repositoryItemMRUEdit1";
            // 
            // xtraTabCalcFields
            // 
            this.xtraTabCalcFields.Controls.Add(this.gridCalcFields);
            this.xtraTabCalcFields.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabCalcFields.Image")));
            this.xtraTabCalcFields.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabCalcFields.Name = "xtraTabCalcFields";
            this.xtraTabCalcFields.Size = new System.Drawing.Size(798, 231);
            this.xtraTabCalcFields.Text = "Calculated Fields";
            // 
            // gridCalcFields
            // 
            this.gridCalcFields.AllowDrop = true;
            this.gridCalcFields.ContextMenuStrip = this.ctxGrid;
            this.gridCalcFields.DataSource = this.compareMappingModelBindingSource;
            this.gridCalcFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCalcFields.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridCalcFields.Location = new System.Drawing.Point(0, 0);
            this.gridCalcFields.MainView = this.viewCalcFields;
            this.gridCalcFields.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridCalcFields.MenuManager = this.barManager1;
            this.gridCalcFields.Name = "gridCalcFields";
            this.gridCalcFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox6,
            this.repositoryItemComboBox7,
            this.repositoryItemComboBox8,
            this.repositoryItemComboBox9,
            this.repositoryItemMRUEdit2,
            this.repositoryItemButtonEdit2,
            this.repositoryItemCheckEdit2,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemButtonEdit3});
            this.gridCalcFields.Size = new System.Drawing.Size(798, 231);
            this.gridCalcFields.TabIndex = 2;
            this.gridCalcFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCalcFields});
            // 
            // viewCalcFields
            // 
            this.viewCalcFields.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCalcSelected,
            this.colIsCalculated,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.viewCalcFields.CustomizationFormBounds = new System.Drawing.Rectangle(924, 426, 212, 212);
            this.viewCalcFields.GridControl = this.gridCalcFields;
            this.viewCalcFields.Name = "viewCalcFields";
            this.viewCalcFields.OptionsCustomization.AllowRowSizing = true;
            this.viewCalcFields.OptionsCustomization.AllowSort = false;
            this.viewCalcFields.OptionsDetail.EnableDetailToolTip = true;
            this.viewCalcFields.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewCalcFields.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.viewCalcFields_RowCellClick);
            this.viewCalcFields.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewMapping_RowCellStyle);
            this.viewCalcFields.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.viewCalcFields_FocusedColumnChanged);
            // 
            // colCalcSelected
            // 
            this.colCalcSelected.FieldName = "Selected";
            this.colCalcSelected.Name = "colCalcSelected";
            this.colCalcSelected.Visible = true;
            this.colCalcSelected.VisibleIndex = 0;
            this.colCalcSelected.Width = 40;
            // 
            // colIsCalculated
            // 
            this.colIsCalculated.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colIsCalculated.FieldName = "IsCalculated";
            this.colIsCalculated.Name = "colIsCalculated";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.repositoryItemComboBox6;
            this.gridColumn1.FieldName = "LeftSide";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 95;
            // 
            // repositoryItemComboBox6
            // 
            this.repositoryItemComboBox6.AutoHeight = false;
            this.repositoryItemComboBox6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox6.Name = "repositoryItemComboBox6";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.ColumnEdit = this.repositoryItemButtonEdit3;
            this.gridColumn4.FieldName = "LeftQuery";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 268;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.AcceptsReturn = false;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.ColumnEdit = this.repositoryItemComboBox7;
            this.gridColumn5.FieldName = "RightSide";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 76;
            // 
            // repositoryItemComboBox7
            // 
            this.repositoryItemComboBox7.AutoHeight = false;
            this.repositoryItemComboBox7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox7.Name = "repositoryItemComboBox7";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn6.ColumnEdit = this.repositoryItemButtonEdit3;
            this.gridColumn6.FieldName = "RightQuery";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 280;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.AcceptsReturn = false;
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // repositoryItemComboBox8
            // 
            this.repositoryItemComboBox8.AutoHeight = false;
            this.repositoryItemComboBox8.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox8.Name = "repositoryItemComboBox8";
            // 
            // repositoryItemComboBox9
            // 
            this.repositoryItemComboBox9.AutoHeight = false;
            this.repositoryItemComboBox9.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox9.Name = "repositoryItemComboBox9";
            // 
            // repositoryItemMRUEdit2
            // 
            this.repositoryItemMRUEdit2.AutoHeight = false;
            this.repositoryItemMRUEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMRUEdit2.Name = "repositoryItemMRUEdit2";
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.gridSourcehSchema);
            this.xtraTabPage4.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage4.Image")));
            this.xtraTabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(798, 231);
            this.xtraTabPage4.Text = "Source Schema";
            // 
            // gridSourcehSchema
            // 
            this.gridSourcehSchema.ContextMenuStrip = this.ctxGrid;
            this.gridSourcehSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSourcehSchema.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridSourcehSchema.Location = new System.Drawing.Point(0, 0);
            this.gridSourcehSchema.MainView = this.viewSourceSchema;
            this.gridSourcehSchema.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridSourcehSchema.MenuManager = this.barManager1;
            this.gridSourcehSchema.Name = "gridSourcehSchema";
            this.gridSourcehSchema.Size = new System.Drawing.Size(798, 231);
            this.gridSourcehSchema.TabIndex = 0;
            this.gridSourcehSchema.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewSourceSchema});
            // 
            // viewSourceSchema
            // 
            this.viewSourceSchema.GridControl = this.gridSourcehSchema;
            this.viewSourceSchema.Name = "viewSourceSchema";
            this.viewSourceSchema.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewSourceSchema.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewSourceSchema.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewSourceSchema.OptionsBehavior.Editable = false;
            this.viewSourceSchema.OptionsView.EnableAppearanceEvenRow = true;
            this.viewSourceSchema.OptionsView.EnableAppearanceOddRow = true;
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.gridTargetSchema);
            this.xtraTabPage5.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage5.Image")));
            this.xtraTabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(798, 231);
            this.xtraTabPage5.Text = "Target Schema";
            // 
            // gridTargetSchema
            // 
            this.gridTargetSchema.ContextMenuStrip = this.ctxGrid;
            this.gridTargetSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTargetSchema.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTargetSchema.Location = new System.Drawing.Point(0, 0);
            this.gridTargetSchema.MainView = this.viewTargetSchema;
            this.gridTargetSchema.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTargetSchema.MenuManager = this.barManager1;
            this.gridTargetSchema.Name = "gridTargetSchema";
            this.gridTargetSchema.Size = new System.Drawing.Size(798, 231);
            this.gridTargetSchema.TabIndex = 1;
            this.gridTargetSchema.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTargetSchema});
            // 
            // viewTargetSchema
            // 
            this.viewTargetSchema.GridControl = this.gridTargetSchema;
            this.viewTargetSchema.Name = "viewTargetSchema";
            this.viewTargetSchema.OptionsBehavior.Editable = false;
            this.viewTargetSchema.OptionsView.EnableAppearanceEvenRow = true;
            this.viewTargetSchema.OptionsView.EnableAppearanceOddRow = true;
            // 
            // xtraTabComparison
            // 
            this.xtraTabComparison.Controls.Add(this.gridComparison);
            this.xtraTabComparison.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabComparison.Image")));
            this.xtraTabComparison.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraTabComparison.Name = "xtraTabComparison";
            this.xtraTabComparison.PageVisible = false;
            this.xtraTabComparison.Size = new System.Drawing.Size(798, 231);
            this.xtraTabComparison.Text = "Comparison Report";
            // 
            // gridComparison
            // 
            this.gridComparison.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridComparison.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridComparison.Location = new System.Drawing.Point(0, 0);
            this.gridComparison.MainView = this.viewComparison;
            this.gridComparison.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridComparison.MenuManager = this.barManager1;
            this.gridComparison.Name = "gridComparison";
            this.gridComparison.Size = new System.Drawing.Size(798, 231);
            this.gridComparison.TabIndex = 1;
            this.gridComparison.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewComparison});
            // 
            // viewComparison
            // 
            this.viewComparison.GridControl = this.gridComparison;
            this.viewComparison.Name = "viewComparison";
            this.viewComparison.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewComparison.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewComparison.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewComparison.OptionsBehavior.Editable = false;
            this.viewComparison.OptionsMenu.ShowConditionalFormattingItem = true;
            this.viewComparison.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewComparison.OptionsView.EnableAppearanceEvenRow = true;
            this.viewComparison.OptionsView.EnableAppearanceOddRow = true;
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataNavigator1.DataSource = this.bsMappings;
            this.dataNavigator1.Location = new System.Drawing.Point(581, 496);
            this.dataNavigator1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.ShowToolTips = true;
            this.dataNavigator1.Size = new System.Drawing.Size(253, 22);
            this.dataNavigator1.TabIndex = 2;
            this.dataNavigator1.Text = "dataNavigator1";
            this.dataNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.dataNavigator1.Visible = false;
            // 
            // bsMappings
            // 
            this.bsMappings.DataSource = this.compareMappingModelBindingSource;
            // 
            // navGridMappings
            // 
            this.navGridMappings.AddNewItem = this.bindingNavigatorAddNewItem;
            this.navGridMappings.BindingSource = this.bsMappings;
            this.navGridMappings.CountItem = this.bindingNavigatorCountItem;
            this.navGridMappings.DeleteItem = null;
            this.navGridMappings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navGridMappings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.navGridMappings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.toolStripSeparator1,
            this.bindingNavigatorDeleteAll});
            this.navGridMappings.Location = new System.Drawing.Point(0, 486);
            this.navGridMappings.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.navGridMappings.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.navGridMappings.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.navGridMappings.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.navGridMappings.Name = "navGridMappings";
            this.navGridMappings.PositionItem = this.bindingNavigatorPositionItem;
            this.navGridMappings.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.navGridMappings.Size = new System.Drawing.Size(856, 27);
            this.navGridMappings.TabIndex = 25;
            this.navGridMappings.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.BindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(38, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorDeleteAll
            // 
            this.bindingNavigatorDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteAll.Image = global::Fme.Database.Verification.Properties.Resources.deletesheetrows_16x16;
            this.bindingNavigatorDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorDeleteAll.Name = "bindingNavigatorDeleteAll";
            this.bindingNavigatorDeleteAll.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorDeleteAll.ToolTipText = "Delete All";
            this.bindingNavigatorDeleteAll.Click += new System.EventHandler(this.bindingNavigatorDeleteAll_Click);
            // 
            // groupControl6
            // 
            this.groupControl6.Controls.Add(this.labelControl8);
            this.groupControl6.Controls.Add(this.labelControl7);
            this.groupControl6.Controls.Add(this.listBoxControl2);
            this.groupControl6.Controls.Add(this.listBoxControl1);
            this.groupControl6.Controls.Add(this.textEdit1);
            this.groupControl6.Controls.Add(this.textEdit2);
            this.groupControl6.Controls.Add(this.groupControl5);
            this.groupControl6.Location = new System.Drawing.Point(910, 209);
            this.groupControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(274, 202);
            this.groupControl6.TabIndex = 22;
            this.groupControl6.Text = "Field Mappings";
            this.groupControl6.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(27, 97);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 28;
            this.labelControl8.Text = "Target Field";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(27, 47);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(58, 13);
            this.labelControl7.TabIndex = 27;
            this.labelControl7.Text = "Source Field";
            // 
            // listBoxControl2
            // 
            this.listBoxControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxControl2.Location = new System.Drawing.Point(125, 115);
            this.listBoxControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxControl2.Name = "listBoxControl2";
            this.listBoxControl2.Size = new System.Drawing.Size(90, 51);
            this.listBoxControl2.TabIndex = 26;
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxControl1.Location = new System.Drawing.Point(125, 52);
            this.listBoxControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(90, 48);
            this.listBoxControl1.TabIndex = 25;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(27, 65);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(75, 20);
            this.textEdit1.TabIndex = 22;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(27, 115);
            this.textEdit2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textEdit2.MenuManager = this.barManager1;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(75, 20);
            this.textEdit2.TabIndex = 23;
            // 
            // groupControl5
            // 
            this.groupControl5.Location = new System.Drawing.Point(125, 47);
            this.groupControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(274, 202);
            this.groupControl5.TabIndex = 21;
            this.groupControl5.Text = "Field Mappings";
            this.groupControl5.Visible = false;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.lbTargetFields);
            this.groupControl4.Location = new System.Drawing.Point(912, 457);
            this.groupControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(157, 16);
            this.groupControl4.TabIndex = 20;
            this.groupControl4.Text = "Target Fields";
            this.groupControl4.Visible = false;
            // 
            // lbTargetFields
            // 
            this.lbTargetFields.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbTargetFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTargetFields.Location = new System.Drawing.Point(2, 20);
            this.lbTargetFields.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbTargetFields.Name = "lbTargetFields";
            this.lbTargetFields.Size = new System.Drawing.Size(153, 0);
            this.lbTargetFields.TabIndex = 27;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.lbSourceFields);
            this.groupControl3.Location = new System.Drawing.Point(910, 426);
            this.groupControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(157, 26);
            this.groupControl3.TabIndex = 19;
            this.groupControl3.Text = "Source Fields";
            this.groupControl3.Visible = false;
            // 
            // lbSourceFields
            // 
            this.lbSourceFields.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbSourceFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSourceFields.Location = new System.Drawing.Point(2, 20);
            this.lbSourceFields.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbSourceFields.Name = "lbSourceFields";
            this.lbSourceFields.Size = new System.Drawing.Size(153, 4);
            this.lbSourceFields.TabIndex = 26;
            // 
            // tabSourceData
            // 
            this.tabSourceData.Controls.Add(this.gridSourceData);
            this.tabSourceData.Image = ((System.Drawing.Image)(resources.GetObject("tabSourceData.Image")));
            this.tabSourceData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabSourceData.Name = "tabSourceData";
            this.tabSourceData.Size = new System.Drawing.Size(856, 513);
            this.tabSourceData.Text = "Source Data";
            // 
            // gridSourceData
            // 
            this.gridSourceData.ContextMenuStrip = this.ctxGrid;
            this.gridSourceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSourceData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridSourceData.Location = new System.Drawing.Point(0, 0);
            this.gridSourceData.MainView = this.viewSourceData;
            this.gridSourceData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridSourceData.MenuManager = this.barManager1;
            this.gridSourceData.Name = "gridSourceData";
            this.gridSourceData.Size = new System.Drawing.Size(856, 513);
            this.gridSourceData.TabIndex = 2;
            this.gridSourceData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewSourceData});
            // 
            // viewSourceData
            // 
            this.viewSourceData.GridControl = this.gridSourceData;
            this.viewSourceData.Name = "viewSourceData";
            this.viewSourceData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewSourceData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewSourceData.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewSourceData.OptionsBehavior.Editable = false;
            this.viewSourceData.OptionsMenu.ShowConditionalFormattingItem = true;
            this.viewSourceData.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewSourceData.OptionsView.ColumnAutoWidth = false;
            this.viewSourceData.OptionsView.EnableAppearanceEvenRow = true;
            this.viewSourceData.OptionsView.EnableAppearanceOddRow = true;
            // 
            // tabTargetData
            // 
            this.tabTargetData.Controls.Add(this.gridTargetData);
            this.tabTargetData.Image = ((System.Drawing.Image)(resources.GetObject("tabTargetData.Image")));
            this.tabTargetData.Name = "tabTargetData";
            this.tabTargetData.Size = new System.Drawing.Size(856, 513);
            this.tabTargetData.Text = "Target Data";
            // 
            // gridTargetData
            // 
            this.gridTargetData.ContextMenuStrip = this.ctxGrid;
            this.gridTargetData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTargetData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTargetData.Location = new System.Drawing.Point(0, 0);
            this.gridTargetData.MainView = this.viewTargetData;
            this.gridTargetData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTargetData.MenuManager = this.barManager1;
            this.gridTargetData.Name = "gridTargetData";
            this.gridTargetData.Size = new System.Drawing.Size(856, 513);
            this.gridTargetData.TabIndex = 3;
            this.gridTargetData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTargetData});
            // 
            // viewTargetData
            // 
            this.viewTargetData.GridControl = this.gridTargetData;
            this.viewTargetData.Name = "viewTargetData";
            this.viewTargetData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewTargetData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewTargetData.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewTargetData.OptionsBehavior.Editable = false;
            this.viewTargetData.OptionsMenu.ShowConditionalFormattingItem = true;
            this.viewTargetData.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewTargetData.OptionsView.ColumnAutoWidth = false;
            this.viewTargetData.OptionsView.EnableAppearanceEvenRow = true;
            this.viewTargetData.OptionsView.EnableAppearanceOddRow = true;
            // 
            // tabGridResults
            // 
            this.tabGridResults.Controls.Add(this.gridResults);
            this.tabGridResults.Image = ((System.Drawing.Image)(resources.GetObject("tabGridResults.Image")));
            this.tabGridResults.Name = "tabGridResults";
            this.tabGridResults.Size = new System.Drawing.Size(856, 513);
            this.tabGridResults.Text = "Column Comparison";
            // 
            // gridResults
            // 
            this.gridResults.ContextMenuStrip = this.ctxGrid;
            this.gridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResults.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridResults.Location = new System.Drawing.Point(0, 0);
            this.gridResults.MainView = this.viewResults;
            this.gridResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridResults.MenuManager = this.barManager1;
            this.gridResults.Name = "gridResults";
            this.gridResults.Size = new System.Drawing.Size(856, 513);
            this.gridResults.TabIndex = 3;
            this.gridResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewResults});
            // 
            // viewResults
            // 
            this.viewResults.GridControl = this.gridResults;
            this.viewResults.Name = "viewResults";
            this.viewResults.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewResults.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewResults.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewResults.OptionsBehavior.Editable = false;
            this.viewResults.OptionsMenu.ShowConditionalFormattingItem = true;
            this.viewResults.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewResults.OptionsView.ColumnAutoWidth = false;
            this.viewResults.OptionsView.EnableAppearanceEvenRow = true;
            this.viewResults.OptionsView.EnableAppearanceOddRow = true;
            this.viewResults.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.ViewResults_RowCellStyle);
            // 
            // tabCompareResults
            // 
            this.tabCompareResults.Controls.Add(this.gridReport);
            this.tabCompareResults.Image = ((System.Drawing.Image)(resources.GetObject("tabCompareResults.Image")));
            this.tabCompareResults.Name = "tabCompareResults";
            this.tabCompareResults.Size = new System.Drawing.Size(856, 513);
            this.tabCompareResults.Text = "Comparison Report";
            // 
            // gridReport
            // 
            this.gridReport.ContextMenuStrip = this.ctxGrid;
            this.gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReport.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridReport.Location = new System.Drawing.Point(0, 0);
            this.gridReport.MainView = this.viewReport;
            this.gridReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridReport.MenuManager = this.barManager1;
            this.gridReport.Name = "gridReport";
            this.gridReport.Size = new System.Drawing.Size(856, 513);
            this.gridReport.TabIndex = 4;
            this.gridReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewReport});
            // 
            // viewReport
            // 
            this.viewReport.GridControl = this.gridReport;
            this.viewReport.Name = "viewReport";
            this.viewReport.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewReport.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewReport.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.viewReport.OptionsBehavior.Editable = false;
            this.viewReport.OptionsMenu.ShowConditionalFormattingItem = true;
            this.viewReport.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.viewReport.OptionsPrint.PrintPreview = true;
            this.viewReport.OptionsView.EnableAppearanceEvenRow = true;
            this.viewReport.OptionsView.EnableAppearanceOddRow = true;
            // 
            // tabQueries
            // 
            this.tabQueries.Controls.Add(this.splitContainerControl1);
            this.tabQueries.Image = ((System.Drawing.Image)(resources.GetObject("tabQueries.Image")));
            this.tabQueries.Name = "tabQueries";
            this.tabQueries.Size = new System.Drawing.Size(856, 513);
            this.tabQueries.Text = "Queries";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.txtSourceQuery);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.txtTargetQuery);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(856, 513);
            this.splitContainerControl1.SplitterPosition = 438;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // txtSourceQuery
            // 
            this.txtSourceQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceQuery.Location = new System.Drawing.Point(0, 0);
            this.txtSourceQuery.MenuManager = this.barManager1;
            this.txtSourceQuery.Name = "txtSourceQuery";
            this.txtSourceQuery.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSourceQuery.Properties.Appearance.Options.UseFont = true;
            this.txtSourceQuery.Size = new System.Drawing.Size(438, 513);
            this.txtSourceQuery.TabIndex = 0;
            // 
            // txtTargetQuery
            // 
            this.txtTargetQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTargetQuery.EditValue = "";
            this.txtTargetQuery.Location = new System.Drawing.Point(0, 0);
            this.txtTargetQuery.MenuManager = this.barManager1;
            this.txtTargetQuery.Name = "txtTargetQuery";
            this.txtTargetQuery.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetQuery.Properties.Appearance.Options.UseFont = true;
            this.txtTargetQuery.Size = new System.Drawing.Size(413, 513);
            this.txtTargetQuery.TabIndex = 0;
            // 
            // tabMessages
            // 
            this.tabMessages.Appearance.PageClient.BackColor = System.Drawing.Color.DimGray;
            this.tabMessages.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMessages.Controls.Add(this.gridMessages);
            this.tabMessages.Image = ((System.Drawing.Image)(resources.GetObject("tabMessages.Image")));
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Size = new System.Drawing.Size(856, 513);
            this.tabMessages.Text = "System Messages";
            // 
            // gridMessages
            // 
            this.gridMessages.ContextMenuStrip = this.ctxGrid;
            this.gridMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMessages.Location = new System.Drawing.Point(0, 0);
            this.gridMessages.MainView = this.cardViewMessages;
            this.gridMessages.MenuManager = this.barManager1;
            this.gridMessages.Name = "gridMessages";
            this.gridMessages.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoItemMemo,
            this.repoItemMessage});
            this.gridMessages.Size = new System.Drawing.Size(856, 513);
            this.gridMessages.TabIndex = 0;
            this.gridMessages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardViewMessages,
            this.viewMessages});
            this.gridMessages.DataSourceChanged += new System.EventHandler(this.gridMessages_DataSourceChanged);
            // 
            // cardViewMessages
            // 
            this.cardViewMessages.FocusedCardTopFieldIndex = 0;
            this.cardViewMessages.GridControl = this.gridMessages;
            this.cardViewMessages.Name = "cardViewMessages";
            this.cardViewMessages.CustomDrawCardField += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.cardViewMessages_CustomDrawCardField);
            this.cardViewMessages.CustomDrawCardFieldValue += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.cardViewMessages_CustomDrawCardFieldValue);
            // 
            // repoItemMemo
            // 
            this.repoItemMemo.Name = "repoItemMemo";
            // 
            // repoItemMessage
            // 
            this.repoItemMessage.Name = "repoItemMessage";
            // 
            // viewMessages
            // 
            this.viewMessages.GridControl = this.gridMessages;
            this.viewMessages.Name = "viewMessages";
            this.viewMessages.OptionsBehavior.Editable = false;
            this.viewMessages.OptionsView.RowAutoHeight = true;
            this.viewMessages.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.viewMessages_RowCellStyle);
            this.viewMessages.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.viewMessages_RowStyle);
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // barManager2
            // 
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.MaxItemId = 0;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControl1.Size = new System.Drawing.Size(862, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 602);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControl2.Size = new System.Drawing.Size(862, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControl3.Size = new System.Drawing.Size(0, 602);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(862, 0);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControl4.Size = new System.Drawing.Size(0, 602);
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // timerElapsed
            // 
            this.timerElapsed.Interval = 1000;
            this.timerElapsed.Tick += new System.EventHandler(this.TimerElapsed_Tick);
            // 
            // ctxLookupMenu
            // 
            this.ctxLookupMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxLookupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.viewErrorDetailReportToolStripMenuItem});
            this.ctxLookupMenu.Name = "ctxLookupMenu";
            this.ctxLookupMenu.Size = new System.Drawing.Size(220, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem1.Text = "View Error Summary Report";
            // 
            // viewErrorDetailReportToolStripMenuItem
            // 
            this.viewErrorDetailReportToolStripMenuItem.Name = "viewErrorDetailReportToolStripMenuItem";
            this.viewErrorDetailReportToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.viewErrorDetailReportToolStripMenuItem.Text = "View Error Detail Report";
            // 
            // colRightLookupFile
            // 
            this.colRightLookupFile.FieldName = "RightLookupFile";
            this.colRightLookupFile.Name = "colRightLookupFile";
            // 
            // popupMenu3
            // 
            this.popupMenu3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8)});
            this.popupMenu3.Manager = this.barManager1;
            this.popupMenu3.Name = "popupMenu3";
            // 
            // tmrMonitor
            // 
            this.tmrMonitor.Enabled = true;
            this.tmrMonitor.Interval = 1000;
            this.tmrMonitor.Tick += new System.EventHandler(this.tmrMonitor_Tick);
            // 
            // repositoryItemButtonEdit3
            // 
            this.repositoryItemButtonEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemButtonEdit3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemButtonEdit3.AutoHeight = false;
            this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            this.repositoryItemButtonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.CalculatedFields_OnShowFileDialog);
            this.repositoryItemButtonEdit3.MouseEnter += new System.EventHandler(this.repositoryItemButtonEdit3_MouseEnter);
            this.repositoryItemButtonEdit3.MouseHover += new System.EventHandler(this.repositoryItemButtonEdit3_MouseHover);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 602);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Migration Verification Management";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.SystemColorsChanged += new System.EventHandler(this.MainView_SystemColorsChanged);
            ((System.ComponentModel.ISupportInitialize)(this.compareMappingModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbComparType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
            this.tabConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl4)).EndInit();
            this.xtraTabControl4.ResumeLayout(false);
            this.xtraTabPage8.ResumeLayout(false);
            this.xtraTabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetTZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTargetTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTargetData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl3)).EndInit();
            this.xtraTabControl3.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceTZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSourceTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSourceData.Properties)).EndInit();
            this.xtraTabPage7.ResumeLayout(false);
            this.xtraTabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceMaxRows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditIdList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabFieldMappings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).EndInit();
            this.ctxGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repo_chkSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLeftSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRightSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCompareType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator)).EndInit();
            this.xtraTabLookup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFieldLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFieldLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMRUEdit1)).EndInit();
            this.xtraTabCalcFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCalcFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCalcFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMRUEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSourcehSchema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSourceSchema)).EndInit();
            this.xtraTabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTargetSchema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTargetSchema)).EndInit();
            this.xtraTabComparison.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridComparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewComparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navGridMappings)).EndInit();
            this.navGridMappings.ResumeLayout(false);
            this.navGridMappings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            this.groupControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbTargetFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbSourceFields)).EndInit();
            this.tabSourceData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSourceData)).EndInit();
            this.tabTargetData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTargetData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTargetData)).EndInit();
            this.tabGridResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewResults)).EndInit();
            this.tabCompareResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewReport)).EndInit();
            this.tabQueries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetQuery.Properties)).EndInit();
            this.tabMessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardViewMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldSchemaModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableSchemaModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ctxLookupMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabConfiguration;
        private DevExpress.XtraTab.XtraTabPage tabSourceData;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnOpen;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.BarButtonItem btnExecute;
        private DevExpress.XtraBars.BarButtonItem btnCancel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbComparType;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl2;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.ListBoxControl lbTargetFields;
        private DevExpress.XtraEditors.ListBoxControl lbSourceFields;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.BindingSource compareMappingModelBindingSource;
        private System.Windows.Forms.BindingSource tableSchemaModelBindingSource;
        private System.Windows.Forms.BindingSource fieldSchemaModelBindingSource;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraGrid.GridControl gridMappings;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMappings;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.PopupMenu popupMenu2;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colLeftSide;
        private DevExpress.XtraGrid.Columns.GridColumn colRightSide;
        private DevExpress.XtraGrid.Columns.GridColumn colCompareType;
        private DevExpress.XtraGrid.Columns.GridColumn colIgnoreChars;
        private DevExpress.XtraGrid.Columns.GridColumn colErrors;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colOperator;
        private DevExpress.XtraGrid.Columns.GridColumn colSelection;
        private DevExpress.XtraGrid.Columns.GridColumn colStartTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbLeftSide;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbRightSide;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbCompareType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbOperator;
        private System.Windows.Forms.BindingNavigator navGridMappings;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.BindingSource bsMappings;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private DevExpress.XtraBars.BarEditItem barEditStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar mbStatus;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar pbStatus;
        private DevExpress.XtraBars.BarStaticItem lblStatus;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem2;
        private DevExpress.XtraEditors.DataNavigator dataNavigator1;

        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabFieldMappings;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraGrid.GridControl gridSourcehSchema;
        private DevExpress.XtraGrid.Views.Grid.GridView viewSourceSchema;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraGrid.GridControl gridTargetSchema;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTargetSchema;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown1;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown2;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown3;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown4;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown5;

        private DevExpress.XtraBars.BarStaticItem lblElapsed;
        private System.Windows.Forms.Timer timerElapsed;
        private DevExpress.XtraBars.BarButtonItem btnAutoGenerate;
        private System.Windows.Forms.ContextMenuStrip ctxLookupMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevExpress.XtraTab.XtraTabPage xtraTabLookup;
        private DevExpress.XtraGrid.GridControl gridFieldLookup;
        private DevExpress.XtraGrid.Views.Grid.GridView viewFieldLookup;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn colLeftLookupFile;
        private DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit repositoryItemMRUEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox5;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colRightLookupFile;
        private DevExpress.XtraGrid.Columns.GridColumn colRightLookupFile1;
        private DevExpress.XtraTab.XtraTabPage xtraTabComparison;
        private DevExpress.XtraGrid.GridControl gridComparison;
        private DevExpress.XtraGrid.Views.Grid.GridView viewComparison;
        private System.Windows.Forms.ToolStripMenuItem viewErrorDetailReportToolStripMenuItem;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage8;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.ButtonEdit btnEditIdList;
        private DevExpress.XtraEditors.SimpleButton btnGetSchema;
        private DevExpress.XtraEditors.ComboBoxEdit cbSourceKey;
        private DevExpress.XtraEditors.ComboBoxEdit cbSourceTable;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ButtonEdit btnSourceData;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.ComboBoxEdit cbTargetKey;
        private DevExpress.XtraEditors.ComboBoxEdit cbTargetTable;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ButtonEdit btnTargetData;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDCTM;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraTab.XtraTabPage xtraTabCalcFields;
        private DevExpress.XtraGrid.GridControl gridCalcFields;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCalcFields;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox8;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox9;
        private DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit repositoryItemMRUEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCalculated;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraBars.BarStaticItem lblVersion;
        private DevExpress.XtraGrid.GridControl gridSourceData;
        private GridView viewSourceData;
        private DevExpress.XtraTab.XtraTabPage tabTargetData;
        private DevExpress.XtraTab.XtraTabPage tabGridResults;
        private DevExpress.XtraGrid.GridControl gridTargetData;
        private GridView viewTargetData;
        private DevExpress.XtraGrid.GridControl gridResults;
        private GridView viewResults;
        private DevExpress.XtraTab.XtraTabPage tabQueries;
        private DevExpress.XtraTab.XtraTabPage tabCompareResults;
        private DevExpress.XtraGrid.GridControl gridReport;
        private GridView viewReport;
        private DevExpress.XtraTab.XtraTabPage tabMessages;
        private DevExpress.XtraBars.BarButtonItem btnExport;
        private ContextMenuStrip ctxGrid;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem hideEmptyColumnToolStripMenuItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.PopupMenu popupMenu3;
        private DevExpress.XtraGrid.GridControl gridMessages;
        private GridView viewMessages;
        private ToolStripSeparator toolStripMenuItem2;
        private CheckBox chkSourceRandom;
        private DevExpress.XtraEditors.TextEdit txtSourceMaxRows;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.MemoEdit txtSourceQuery;
        private DevExpress.XtraEditors.MemoEdit txtTargetQuery;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repo_chkSelected;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton bindingNavigatorDeleteAll;
        private DevExpress.XtraGrid.Columns.GridColumn colCalcSelected;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.ComboBoxEdit cbTargetTZ;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.ComboBoxEdit cbSourceTZ;
        private ToolStripMenuItem showHiddenColumnsToolStripMenuItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private CheckBox chkTargetVersions;
        private CheckBox chkSourceVersions;
        private DevExpress.XtraGrid.Views.Card.CardView cardViewMessages;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repoItemMemo;
        private Timer tmrMonitor;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repoItemMessage;
        private DevExpress.XtraBars.BarButtonItem tnExportPackage;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit3;
    }

}