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
            this.SuspendLayout();
            // 
            // clbChildren
            // 
            this.clbChildren.FormattingEnabled = true;
            this.clbChildren.Location = new System.Drawing.Point(12, 12);
            this.clbChildren.Name = "clbChildren";
            this.clbChildren.Size = new System.Drawing.Size(514, 409);
            this.clbChildren.TabIndex = 0;
            this.clbChildren.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // SelectChildren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clbChildren);
            this.Name = "SelectChildren";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbChildren;
    }
}