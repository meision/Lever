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

namespace Meision.VisualStudio.CustomCommands
{
    internal partial class DatabaseTransferForm : Form
    {
        public DatabaseTransferForm()
        {
            InitializeComponent();
        }

        internal void Initialize(DatabaseTransferConfig config, DataSet dataSet)
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
                if (!DatabaseTransferConfig.DefaultSheetName.Equals(table.TableName, StringComparison.OrdinalIgnoreCase))
                {
                    builder.AppendLine($"DELETE FROM [{table.TableName}]");
                }
            }
            this.txtClearSQL.Text = builder.ToString();
            this.chkClear.Checked = config.ClearTableBeforeImport;
            // Model
            switch (config.ImportMode)
            {
                case DatabaseTransferModel.InsertNotExists:
                    this.rdoImportModeIgnoreExists.Checked = true;
                    break;
                case DatabaseTransferModel.Merge:
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

        public DatabaseTransferModel GetImportModel()
        {
            if (this.rdoImportModeIgnoreExists.Checked)
            {
                return DatabaseTransferModel.InsertNotExists;
            }
            else if (this.rdoImportModeMerge.Checked)
            {
                return DatabaseTransferModel.Merge;
            }
            else
            {
                return DatabaseTransferModel.None;
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
