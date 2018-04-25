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
    internal partial class SyncDatabaseForm : Form
    {
        public SyncDatabaseForm()
        {
            InitializeComponent();
        }

        internal void Initialize(SyncDatabaseConfig config, DataSet dataSet)
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
                if (!SyncDatabaseConfig.DefaultSheetName.Equals(table.TableName, StringComparison.OrdinalIgnoreCase))
                {
                    builder.AppendLine($"DELETE FROM [{table.TableName}]");
                    builder.AppendLine($"IF EXISTS (SELECT 1 FROM sys.identity_columns WHERE object_id = object_id('{table.TableName}')) DBCC CHECKIDENT('{table.TableName}', RESEED, 0)");
                }
            }
            this.txtClearSQL.Text = builder.ToString();
            this.chkClear.Checked = config.ClearTableBeforeImport;
            // Model
            (this.panSyncDatabaseAction.Controls.Find($"rdoSyncDatabaseAction{config.Action}", false)[0] as RadioButton).Checked = true;
            (this.panSyncDatabaseMode.Controls.Find($"rdoSyncDatabaseMode{config.Mode}", false)[0] as RadioButton).Checked = true;                                       
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

        public SyncDatabaseAction GetSyncAction()
        {
            foreach (RadioButton button in this.panSyncDatabaseAction.Controls)
            {
                if (button.Checked)
                {
                    return (SyncDatabaseAction)Enum.Parse(typeof(SyncDatabaseAction), button.Name.Substring("rdoSyncDatabaseAction".Length));
                }
            }

            return SyncDatabaseAction.None;
        }

        public SyncDatabaseModel GetSyncModel()
        {
            foreach (RadioButton button in this.panSyncDatabaseMode.Controls)
            {
                if (button.Checked)
                {
                    return (SyncDatabaseModel)Enum.Parse(typeof(SyncDatabaseModel), button.Name.Substring("rdoSyncDatabaseMode".Length));
                }
            }

            return SyncDatabaseModel.None;
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
