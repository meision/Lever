namespace Meision.VisualStudio.CustomCommands
{
    partial class DatabaseTransferForm
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
            this.lblImportMode = new System.Windows.Forms.Label();
            this.rdoImportModeMerge = new System.Windows.Forms.RadioButton();
            this.rdoImportModeIgnoreExists = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panBottom.SuspendLayout();
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
            this.panBottom.TabIndex = 2;
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
            this.lblConnectionString.Location = new System.Drawing.Point(16, 46);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(112, 13);
            this.lblConnectionString.TabIndex = 3;
            this.lblConnectionString.Text = "Destination Database:";
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.Location = new System.Drawing.Point(64, 87);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(69, 13);
            this.lblClear.TabIndex = 5;
            this.lblClear.Text = "Clear Tables:";
            // 
            // cboConnectionString
            // 
            this.cboConnectionString.FormattingEnabled = true;
            this.cboConnectionString.Location = new System.Drawing.Point(153, 41);
            this.cboConnectionString.Name = "cboConnectionString";
            this.cboConnectionString.Size = new System.Drawing.Size(580, 21);
            this.cboConnectionString.TabIndex = 6;
            // 
            // chkClear
            // 
            this.chkClear.AutoSize = true;
            this.chkClear.Location = new System.Drawing.Point(153, 86);
            this.chkClear.Name = "chkClear";
            this.chkClear.Size = new System.Drawing.Size(248, 17);
            this.chkClear.TabIndex = 7;
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
            this.txtClearSQL.TabIndex = 8;
            // 
            // lblImportMode
            // 
            this.lblImportMode.AutoSize = true;
            this.lblImportMode.Location = new System.Drawing.Point(65, 304);
            this.lblImportMode.Name = "lblImportMode";
            this.lblImportMode.Size = new System.Drawing.Size(73, 13);
            this.lblImportMode.TabIndex = 9;
            this.lblImportMode.Text = "Trasfer Mode:";
            // 
            // rdoImportModeMerge
            // 
            this.rdoImportModeMerge.AutoSize = true;
            this.rdoImportModeMerge.Location = new System.Drawing.Point(512, 302);
            this.rdoImportModeMerge.Name = "rdoImportModeMerge";
            this.rdoImportModeMerge.Size = new System.Drawing.Size(108, 17);
            this.rdoImportModeMerge.TabIndex = 10;
            this.rdoImportModeMerge.Text = "Merge for all rows";
            this.rdoImportModeMerge.UseVisualStyleBackColor = true;
            // 
            // rdoImportModeIgnoreExists
            // 
            this.rdoImportModeIgnoreExists.AutoSize = true;
            this.rdoImportModeIgnoreExists.Checked = true;
            this.rdoImportModeIgnoreExists.Location = new System.Drawing.Point(357, 302);
            this.rdoImportModeIgnoreExists.Name = "rdoImportModeIgnoreExists";
            this.rdoImportModeIgnoreExists.Size = new System.Drawing.Size(131, 17);
            this.rdoImportModeIgnoreExists.TabIndex = 11;
            this.rdoImportModeIgnoreExists.TabStop = true;
            this.rdoImportModeIgnoreExists.Text = "Insert rows if not exists";
            this.rdoImportModeIgnoreExists.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(156, 271);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(134, 17);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Import rows if not exists";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(357, 269);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(108, 17);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = "Merge for all rows";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Operation Action:";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(156, 302);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(76, 17);
            this.radioButton3.TabIndex = 15;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Insert rows";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // DatabaseTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 401);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoImportModeIgnoreExists);
            this.Controls.Add(this.rdoImportModeMerge);
            this.Controls.Add(this.lblImportMode);
            this.Controls.Add(this.txtClearSQL);
            this.Controls.Add(this.chkClear);
            this.Controls.Add(this.cboConnectionString);
            this.Controls.Add(this.lblClear);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.panBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DatabaseTransferForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Transfer";
            this.panBottom.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblImportMode;
        private System.Windows.Forms.RadioButton rdoImportModeMerge;
        private System.Windows.Forms.RadioButton rdoImportModeIgnoreExists;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}