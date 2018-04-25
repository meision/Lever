namespace Meision.VisualStudio.CustomCommands
{
    partial class SyncDatabaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            this.panBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblClear = new System.Windows.Forms.Label();
            this.cboConnectionString = new System.Windows.Forms.ComboBox();
            this.chkClear = new System.Windows.Forms.CheckBox();
            this.txtClearSQL = new System.Windows.Forms.TextBox();
            this.lblSyncDatabaseMode = new System.Windows.Forms.Label();
            this.rdoSyncDatabaseModeMerge = new System.Windows.Forms.RadioButton();
            this.rdoSyncDatabaseModeInsertNotExists = new System.Windows.Forms.RadioButton();
            this.rdoSyncDatabaseActionGenerateScript = new System.Windows.Forms.RadioButton();
            this.rdoSyncDatabaseActionImportDatabase = new System.Windows.Forms.RadioButton();
            this.lblSyncDatabaseActioin = new System.Windows.Forms.Label();
            this.rdoSyncDatabaseModeInsert = new System.Windows.Forms.RadioButton();
            this.panSyncDatabaseAction = new System.Windows.Forms.Panel();
            this.panSyncDatabaseMode = new System.Windows.Forms.Panel();
            this.panBottom.SuspendLayout();
            this.panSyncDatabaseAction.SuspendLayout();
            this.panSyncDatabaseMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.btnCancel);
            this.panBottom.Controls.Add(this.lblSeparator);
            this.panBottom.Controls.Add(this.btnOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(0, 338);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(755, 63);
            this.panBottom.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(658, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSeparator
            // 
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeparator.Location = new System.Drawing.Point(0, 0);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(755, 2);
            this.lblSeparator.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(561, 21);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(21, 46);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(112, 13);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "Destination Database:";
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.Location = new System.Drawing.Point(64, 87);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(69, 13);
            this.lblClear.TabIndex = 2;
            this.lblClear.Text = "Clear Tables:";
            // 
            // cboConnectionString
            // 
            this.cboConnectionString.FormattingEnabled = true;
            this.cboConnectionString.Location = new System.Drawing.Point(153, 41);
            this.cboConnectionString.Name = "cboConnectionString";
            this.cboConnectionString.Size = new System.Drawing.Size(580, 21);
            this.cboConnectionString.TabIndex = 1;
            // 
            // chkClear
            // 
            this.chkClear.AutoSize = true;
            this.chkClear.Location = new System.Drawing.Point(153, 86);
            this.chkClear.Name = "chkClear";
            this.chkClear.Size = new System.Drawing.Size(248, 17);
            this.chkClear.TabIndex = 3;
            this.chkClear.Text = "Execute Command to clear tables before import";
            this.chkClear.UseVisualStyleBackColor = true;
            this.chkClear.CheckedChanged += new System.EventHandler(this.chkClear_CheckedChanged);
            // 
            // txtClearSQL
            // 
            this.txtClearSQL.Enabled = false;
            this.txtClearSQL.Location = new System.Drawing.Point(153, 106);
            this.txtClearSQL.Multiline = true;
            this.txtClearSQL.Name = "txtClearSQL";
            this.txtClearSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClearSQL.Size = new System.Drawing.Size(580, 141);
            this.txtClearSQL.TabIndex = 4;
            this.txtClearSQL.WordWrap = false;
            // 
            // lblSyncDatabaseMode
            // 
            this.lblSyncDatabaseMode.AutoSize = true;
            this.lblSyncDatabaseMode.Location = new System.Drawing.Point(69, 305);
            this.lblSyncDatabaseMode.Name = "lblSyncDatabaseMode";
            this.lblSyncDatabaseMode.Size = new System.Drawing.Size(64, 13);
            this.lblSyncDatabaseMode.TabIndex = 7;
            this.lblSyncDatabaseMode.Text = "Sync Mode:";
            // 
            // rdoSyncDatabaseModeMerge
            // 
            this.rdoSyncDatabaseModeMerge.AutoSize = true;
            this.rdoSyncDatabaseModeMerge.Location = new System.Drawing.Point(359, 8);
            this.rdoSyncDatabaseModeMerge.Name = "rdoSyncDatabaseModeMerge";
            this.rdoSyncDatabaseModeMerge.Size = new System.Drawing.Size(108, 17);
            this.rdoSyncDatabaseModeMerge.TabIndex = 2;
            this.rdoSyncDatabaseModeMerge.Text = "Merge for all rows";
            this.rdoSyncDatabaseModeMerge.UseVisualStyleBackColor = true;
            // 
            // rdoSyncDatabaseModeInsertNotExists
            // 
            this.rdoSyncDatabaseModeInsertNotExists.AutoSize = true;
            this.rdoSyncDatabaseModeInsertNotExists.Location = new System.Drawing.Point(175, 8);
            this.rdoSyncDatabaseModeInsertNotExists.Name = "rdoSyncDatabaseModeInsertNotExists";
            this.rdoSyncDatabaseModeInsertNotExists.Size = new System.Drawing.Size(131, 17);
            this.rdoSyncDatabaseModeInsertNotExists.TabIndex = 1;
            this.rdoSyncDatabaseModeInsertNotExists.Text = "Insert rows if not exists";
            this.rdoSyncDatabaseModeInsertNotExists.UseVisualStyleBackColor = true;
            // 
            // rdoSyncDatabaseActionGenerateScript
            // 
            this.rdoSyncDatabaseActionGenerateScript.AutoSize = true;
            this.rdoSyncDatabaseActionGenerateScript.Checked = true;
            this.rdoSyncDatabaseActionGenerateScript.Location = new System.Drawing.Point(3, 7);
            this.rdoSyncDatabaseActionGenerateScript.Name = "rdoSyncDatabaseActionGenerateScript";
            this.rdoSyncDatabaseActionGenerateScript.Size = new System.Drawing.Size(99, 17);
            this.rdoSyncDatabaseActionGenerateScript.TabIndex = 0;
            this.rdoSyncDatabaseActionGenerateScript.TabStop = true;
            this.rdoSyncDatabaseActionGenerateScript.Text = "Generate Script";
            this.rdoSyncDatabaseActionGenerateScript.UseVisualStyleBackColor = true;
            // 
            // rdoSyncDatabaseActionImportDatabase
            // 
            this.rdoSyncDatabaseActionImportDatabase.AutoSize = true;
            this.rdoSyncDatabaseActionImportDatabase.Location = new System.Drawing.Point(175, 7);
            this.rdoSyncDatabaseActionImportDatabase.Name = "rdoSyncDatabaseActionImportDatabase";
            this.rdoSyncDatabaseActionImportDatabase.Size = new System.Drawing.Size(119, 17);
            this.rdoSyncDatabaseActionImportDatabase.TabIndex = 1;
            this.rdoSyncDatabaseActionImportDatabase.Text = "Import To Database";
            this.rdoSyncDatabaseActionImportDatabase.UseVisualStyleBackColor = true;
            // 
            // lblSyncDatabaseActioin
            // 
            this.lblSyncDatabaseActioin.AutoSize = true;
            this.lblSyncDatabaseActioin.Location = new System.Drawing.Point(66, 264);
            this.lblSyncDatabaseActioin.Name = "lblSyncDatabaseActioin";
            this.lblSyncDatabaseActioin.Size = new System.Drawing.Size(67, 13);
            this.lblSyncDatabaseActioin.TabIndex = 5;
            this.lblSyncDatabaseActioin.Text = "Sync Action:";
            // 
            // rdoSyncDatabaseModeInsert
            // 
            this.rdoSyncDatabaseModeInsert.AutoSize = true;
            this.rdoSyncDatabaseModeInsert.Checked = true;
            this.rdoSyncDatabaseModeInsert.Location = new System.Drawing.Point(3, 8);
            this.rdoSyncDatabaseModeInsert.Name = "rdoSyncDatabaseModeInsert";
            this.rdoSyncDatabaseModeInsert.Size = new System.Drawing.Size(76, 17);
            this.rdoSyncDatabaseModeInsert.TabIndex = 0;
            this.rdoSyncDatabaseModeInsert.TabStop = true;
            this.rdoSyncDatabaseModeInsert.Text = "Insert rows";
            this.rdoSyncDatabaseModeInsert.UseVisualStyleBackColor = true;
            // 
            // panSyncDatabaseAction
            // 
            this.panSyncDatabaseAction.Controls.Add(this.rdoSyncDatabaseActionGenerateScript);
            this.panSyncDatabaseAction.Controls.Add(this.rdoSyncDatabaseActionImportDatabase);
            this.panSyncDatabaseAction.Location = new System.Drawing.Point(153, 255);
            this.panSyncDatabaseAction.Name = "panSyncDatabaseAction";
            this.panSyncDatabaseAction.Size = new System.Drawing.Size(580, 32);
            this.panSyncDatabaseAction.TabIndex = 6;
            // 
            // panSyncDatabaseMode
            // 
            this.panSyncDatabaseMode.Controls.Add(this.rdoSyncDatabaseModeInsert);
            this.panSyncDatabaseMode.Controls.Add(this.rdoSyncDatabaseModeMerge);
            this.panSyncDatabaseMode.Controls.Add(this.rdoSyncDatabaseModeInsertNotExists);
            this.panSyncDatabaseMode.Location = new System.Drawing.Point(153, 296);
            this.panSyncDatabaseMode.Name = "panSyncDatabaseMode";
            this.panSyncDatabaseMode.Size = new System.Drawing.Size(580, 33);
            this.panSyncDatabaseMode.TabIndex = 8;
            // 
            // SyncDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 401);
            this.Controls.Add(this.panSyncDatabaseMode);
            this.Controls.Add(this.panSyncDatabaseAction);
            this.Controls.Add(this.lblSyncDatabaseActioin);
            this.Controls.Add(this.lblSyncDatabaseMode);
            this.Controls.Add(this.txtClearSQL);
            this.Controls.Add(this.chkClear);
            this.Controls.Add(this.cboConnectionString);
            this.Controls.Add(this.lblClear);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.panBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SyncDatabaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sync Database";
            this.panBottom.ResumeLayout(false);
            this.panSyncDatabaseAction.ResumeLayout(false);
            this.panSyncDatabaseAction.PerformLayout();
            this.panSyncDatabaseMode.ResumeLayout(false);
            this.panSyncDatabaseMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Label lblClear;
        private System.Windows.Forms.ComboBox cboConnectionString;
        private System.Windows.Forms.CheckBox chkClear;
        private System.Windows.Forms.TextBox txtClearSQL;
        private System.Windows.Forms.Label lblSyncDatabaseMode;
        private System.Windows.Forms.RadioButton rdoSyncDatabaseModeMerge;
        private System.Windows.Forms.RadioButton rdoSyncDatabaseModeInsertNotExists;
        private System.Windows.Forms.RadioButton rdoSyncDatabaseActionGenerateScript;
        private System.Windows.Forms.RadioButton rdoSyncDatabaseActionImportDatabase;
        private System.Windows.Forms.Label lblSyncDatabaseActioin;
        private System.Windows.Forms.RadioButton rdoSyncDatabaseModeInsert;
        private System.Windows.Forms.Panel panSyncDatabaseAction;
        private System.Windows.Forms.Panel panSyncDatabaseMode;
    }
}