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

namespace Fme.Database.Verification
{
    public partial class frmMacroSetup : DevExpress.XtraEditors.XtraForm
    {


      

        FieldSchemaModel model { get; set; }

        public frmMacroSetup()
        {
            InitializeComponent();
        }
        //public frmMacroSetup(CompareModel model, DataSourceModel data) : this()
        //{
        //    this.model = model;
        //    this.dataSource = data;
        //    this.listBoxControl2.DataSource = ValidatorRepository.GetFunctionList();
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMacroSetup"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public frmMacroSetup(FieldSchemaModel model): this()
        {
            this.model = model;
            this.Text = model.TableName + "\\" + model.Name;
            this.listBoxControl2.DataSource = ValidatorRepository.GetFunctionList();
            this.memoEdit1.Text = model.GetMacros(); //.ValidationMacros;
        }

        /// <summary>
        /// Handles the DoubleClick event of the listBoxControl2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void listBoxControl2_DoubleClick(object sender, EventArgs e)
        {
            var items = memoEdit1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (items.Count() == 0)
                memoEdit1.Text = listBoxControl2.Text + "()" + Environment.NewLine;
            else if (string.IsNullOrEmpty(items.Last().Trim()))
            {
                memoEdit1.Text += listBoxControl2.Text + "()" + Environment.NewLine;
            }
            else
                memoEdit1.Text += Environment.NewLine + listBoxControl2.Text + "()" + Environment.NewLine;
        }

        /// <summary>
        /// Handles the Click event of the simpleButton2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            model.ValidationMacros = memoEdit1.Text;
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}