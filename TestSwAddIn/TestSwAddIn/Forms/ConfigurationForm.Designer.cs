namespace TestSwAddIn.Forms
{
    partial class ConfigurationForm
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
            this.lblSheetTemplate = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TxtTemplatePath = new System.Windows.Forms.TextBox();
            this.btnSearchTemplate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDxfPath = new System.Windows.Forms.TextBox();
            this.btnSearchDxfFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSheetTemplate
            // 
            this.lblSheetTemplate.AutoSize = true;
            this.lblSheetTemplate.Location = new System.Drawing.Point(13, 13);
            this.lblSheetTemplate.Name = "lblSheetTemplate";
            this.lblSheetTemplate.Size = new System.Drawing.Size(184, 13);
            this.lblSheetTemplate.TabIndex = 0;
            this.lblSheetTemplate.Text = "Select the path to the sheet template:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // TxtTemplatePath
            // 
            this.TxtTemplatePath.Location = new System.Drawing.Point(12, 29);
            this.TxtTemplatePath.Name = "TxtTemplatePath";
            this.TxtTemplatePath.Size = new System.Drawing.Size(380, 20);
            this.TxtTemplatePath.TabIndex = 1;
            // 
            // btnSearchTemplate
            // 
            this.btnSearchTemplate.Location = new System.Drawing.Point(398, 29);
            this.btnSearchTemplate.Name = "btnSearchTemplate";
            this.btnSearchTemplate.Size = new System.Drawing.Size(94, 20);
            this.btnSearchTemplate.TabIndex = 2;
            this.btnSearchTemplate.Text = "Search Directory";
            this.btnSearchTemplate.UseVisualStyleBackColor = true;
            this.btnSearchTemplate.Click += new System.EventHandler(this.BtnSearchTemplateFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(149, 353);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 20);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtDxfPath
            // 
            this.txtDxfPath.Location = new System.Drawing.Point(12, 77);
            this.txtDxfPath.Name = "txtDxfPath";
            this.txtDxfPath.Size = new System.Drawing.Size(380, 20);
            this.txtDxfPath.TabIndex = 3;
            // 
            // btnSearchDxfFolder
            // 
            this.btnSearchDxfFolder.Location = new System.Drawing.Point(398, 77);
            this.btnSearchDxfFolder.Name = "btnSearchDxfFolder";
            this.btnSearchDxfFolder.Size = new System.Drawing.Size(94, 20);
            this.btnSearchDxfFolder.TabIndex = 4;
            this.btnSearchDxfFolder.Text = "Search Directory";
            this.btnSearchDxfFolder.UseVisualStyleBackColor = true;
            this.btnSearchDxfFolder.Click += new System.EventHandler(this.BtnSearchDxfFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the directory to save DXF and check for unusefull files(.prs)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input the standart color when a item is configured";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(95, 353);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(48, 20);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 385);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDxfPath);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSearchDxfFolder);
            this.Controls.Add(this.btnSearchTemplate);
            this.Controls.Add(this.TxtTemplatePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSheetTemplate);
            this.Name = "ConfigurationForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSheetTemplate;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox TxtTemplatePath;
        private System.Windows.Forms.Button btnSearchTemplate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDxfPath;
        private System.Windows.Forms.Button btnSearchDxfFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnCancel;
    }
}