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
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnSelectShown = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.clbChildren = new System.Windows.Forms.CheckedListBox();
            this.btnChangeColorItem = new System.Windows.Forms.Button();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(12, 469);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(85, 23);
            this.btnChangeColor.TabIndex = 4;
            this.btnChangeColor.Text = "Change Color";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.BtnChangeColor_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 440);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Location = new System.Drawing.Point(93, 440);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnselectAll.TabIndex = 2;
            this.btnUnselectAll.Text = "Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.BtnUnselectAll_Click);
            // 
            // btnSelectShown
            // 
            this.btnSelectShown.Location = new System.Drawing.Point(174, 440);
            this.btnSelectShown.Name = "btnSelectShown";
            this.btnSelectShown.Size = new System.Drawing.Size(87, 23);
            this.btnSelectShown.TabIndex = 3;
            this.btnSelectShown.Text = "Select Shown";
            this.btnSelectShown.UseVisualStyleBackColor = true;
            this.btnSelectShown.Click += new System.EventHandler(this.BtnSelectShown_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clbChildren);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 422);
            this.panel1.TabIndex = 5;
            // 
            // clbChildren
            // 
            this.clbChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbChildren.FormattingEnabled = true;
            this.clbChildren.Location = new System.Drawing.Point(0, 0);
            this.clbChildren.Name = "clbChildren";
            this.clbChildren.Size = new System.Drawing.Size(340, 422);
            this.clbChildren.TabIndex = 1;
            // 
            // btnChangeColorItem
            // 
            this.btnChangeColorItem.Location = new System.Drawing.Point(103, 469);
            this.btnChangeColorItem.Name = "btnChangeColorItem";
            this.btnChangeColorItem.Size = new System.Drawing.Size(102, 23);
            this.btnChangeColorItem.TabIndex = 5;
            this.btnChangeColorItem.Text = "Change Color Item";
            this.btnChangeColorItem.UseVisualStyleBackColor = true;
            this.btnChangeColorItem.Click += new System.EventHandler(this.BtnChangeColorItem_Click);
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.Location = new System.Drawing.Point(211, 469);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(65, 23);
            this.btnSavePdf.TabIndex = 6;
            this.btnSavePdf.Text = "Save PDF";
            this.btnSavePdf.UseVisualStyleBackColor = true;
            this.btnSavePdf.Click += new System.EventHandler(this.BtnSavePdf_Click);
            // 
            // SelectChildren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 525);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.btnChangeColorItem);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSelectShown);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnChangeColor);
            this.Name = "SelectChildren";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSelectShown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox clbChildren;
        private System.Windows.Forms.Button btnChangeColorItem;
        private System.Windows.Forms.Button btnSavePdf;
    }
}