using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meision.VisualStudio
{
    internal partial class ImportDatabaseForm : Form
    {
        public ImportDatabaseForm()
        {
            InitializeComponent();
        }

        internal void Initialize(ImportDatabaseConfig config, DataSet dataSet)
        {
            // Connection String
            this.cboConnectionString.Items.Clear();
            foreach (string connectionString in config.ConnectionStrings)
            {
                this.cboConnectionString.Items.Add(connectionString);
            }
            this.cboConnectionString.SelectedIndex = 0;
            // DataSet
            StringBuilder builder = new StringBuilder();
            for (int i = dataSet.Tables.Count - 1; i >= 0; i--)
            {
                DataTable table = dataSet.Tables[i];
                if (!ImportDatabaseConfig.DefaultSheetName.Equals(table.TableName, StringComparison.OrdinalIgnoreCase))
                {
                    builder.AppendLine($"DELETE FROM [{table.TableName}]");
                }
            }
            this.txtClearSQL.Text = builder.ToString();
            this.chkClear.Checked = config.ClearTableBeforeImport;
            // Model
            switch (config.ImportMode)
            {
                case ImportDatabaseModel.IgnoreExists:
                    this.rdoImportModeIgnoreExists.Checked = true;
                    break;
                case ImportDatabaseModel.Merge:
                    this.rdoImportModeMerge.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void chkClear_CheckedChanged(object sender, EventArgs e)
        {
            this.txtClearSQL.Enabled = this.chkClear.Checked;
        }

        public string GetConnectionString()
        {
            return this.cboConnectionString.Text;
        }

        public string GetClearSQL()
        {
            if (!this.chkClear.Checked)
            {
                return null;
            }

            return this.txtClearSQL.Text;
        }

        public ImportDatabaseModel GetImportModel()
        {
            if (this.rdoImportModeIgnoreExists.Checked)
            {
                return ImportDatabaseModel.IgnoreExists;
            }
            else if (this.rdoImportModeMerge.Checked)
            {
                return ImportDatabaseModel.Merge;
            }
            else
            {
                return ImportDatabaseModel.None;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.cboConnectionString.Text.Length == 0)
            {
                MessageBox.Show("ConnectionString could not empty.");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
