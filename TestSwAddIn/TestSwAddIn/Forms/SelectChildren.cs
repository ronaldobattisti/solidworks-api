using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestSwAddIn.Utils;

namespace TestSwAddIn.Forms
{
    public partial class SelectChildren : Form
    {
        private List<Component2> fstChildren;

        public SelectChildren(List<Component2> fstChildren)
        {
            OpenFile offs = new OpenFile();
            this.fstChildren = fstChildren;
            InitializeComponent();
            foreach (Component2 item in fstChildren)
            {
                clbChildren.Items.Add(item.Name2);
                //clbChildren.Items.Add(item.GetPathName());
            }
            //clbChildren.SelectionMode = SelectionMode.MultiExtended;
            clbChildren.Update();
            clbChildren.CheckOnClick = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            OpenFile offs = new OpenFile();
            List<string> selectedObjects = clbChildren.CheckedItems.OfType<string>().ToList();
            ChangeItemCollor cic = new ChangeItemCollor();
            string fileExtension;
            int intFileExtension = 0;
            string pdfPath = "C:/Users/rbattisti/Desktop/Teste Macro Cs/SavePath";

            /*MessageBox.Show("fstChildren: " + string.Join(",", fstChildren.Select(c => c.Name2)) + "\nclsChildren.selectedItems: " + 
                String.Join(",", selectedObjects.Select(c => c.ToString())));*/

            foreach (Component2 obj in fstChildren)
            {
                foreach (string item in selectedObjects)
                {
                    if (obj.Name2 == item)
                    {
                        offs.OpenFromObject(obj);
                        cic.ChangeCollor();
                        offs.CloseFileObject(obj);
                        //MessageBox.Show("The item " + obj.Name2 + " was opened and closed");
                    }
                }
            }

            ;// offs.OpenFromObject(selectedObjects);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < clbChildren.Items.Count; i++)
            {
                clbChildren.SetItemChecked(i, true);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbChildren.Items.Count; i++)
            {
                clbChildren.SetItemChecked(i, false);
            }
        }
    }
}
