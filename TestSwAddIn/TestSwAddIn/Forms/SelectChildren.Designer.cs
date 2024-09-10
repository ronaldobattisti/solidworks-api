namespace TestSwAddIn.Forms
{
    partial class SelectChildren
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
            this.clbChildren = new System.Windows.Forms.CheckedListBox();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbChildren
            // 
            this.clbChildren.FormattingEnabled = true;
            this.clbChildren.Location = new System.Drawing.Point(12, 12);
            this.clbChildren.Name = "clbChildren";
            this.clbChildren.Size = new System.Drawing.Size(340, 409);
            this.clbChildren.TabIndex = 0;
            this.clbChildren.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.Location = new System.Drawing.Point(277, 440);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(75, 23);
            this.btnSavePdf.TabIndex = 1;
            this.btnSavePdf.Text = "Save PDF";
            this.btnSavePdf.UseVisualStyleBackColor = true;
            // 
            // SelectChildren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 494);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.clbChildren);
            this.Name = "SelectChildren";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbChildren;
        private System.Windows.Forms.Button btnSavePdf;
    }
}