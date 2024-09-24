using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TestSwAddIn.Utils;

namespace TestSwAddIn.Forms
{


    public partial class SelectChildren : Form
    {
        private List<Component2> listChildrenComponents;
        private List<Component2> listChildrenComponentsDisplayed;

        public SelectChildren(List<Component2> fstChildren, List<Component2> fstChildrenDisplayed)
        {

            OpenFile offs = new OpenFile();
            this.listChildrenComponents = fstChildren;
            this.listChildrenComponentsDisplayed = fstChildrenDisplayed;
            InitializeComponent();
            foreach (Component2 item in fstChildren)
            {
                clbChildren.Items.Add(System.IO.Path.GetFileNameWithoutExtension(item.GetPathName()));
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
            int lErrors = 0;
            int lWarnings = 0;
            List<string> errors = new List<string>();

            /*MessageBox.Show("fstChildren: " + string.Join(",", fstChildren.Select(c => c.Name2)) + "\nclsChildren.selectedItems: " + 
                String.Join(",", selectedObjects.Select(c => c.ToString())));*/

            foreach (Component2 obj in listChildrenComponents)
            {
                foreach (string item in selectedObjects)
                {
                    if (obj.Name2 == item)
                    {
                        offs.OpenFromObject(obj);
                        SldWorks swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                        ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                        cic.ChangeCollor();
                        swModel.Save3((int)swSaveAsOptions_e.swSaveAsOptions_Silent, ref lErrors, ref lWarnings);
                        if(lErrors != 0)
                        {
                            errors.Add(obj.Name2);
                        }
                        offs.CloseFileObject(obj);
                        /*int saveStatus = swModel.SaveAs3(filePath, (int)swSaveAsVersion_e.swSaveAsCurrentVersion, (int)swSaveAsOptions_e.swSaveAsOptions_Silent);

                        // Check if the save was successful
                        if (saveStatus == (int)swFileSaveError_e.swGenericSaveError)
                        {
                            Console.WriteLine("Failed to save the file.");
                        }*/
                        //MessageBox.Show("The item " + obj.Name2 + " was opened and closed");
                    }
                }
            }

            if(errors.Count > 0)
            {
                MessageBox.Show("The following items couldn't be saved\n:" + string.Join("\n", errors.Select(error => $"• {error}")));
            }
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

        private void btnSelectShown_Click(object sender, EventArgs e)
        {
            List<string> strItemName = new List<string>();
            btnUnselectAll_Click(null, null);
            foreach (Component2 item in listChildrenComponentsDisplayed) {
                strItemName.Add(System.IO.Path.GetFileNameWithoutExtension(item.GetPathName()));
            }
            
            for (int i = 0; i < clbChildren.Items.Count; i++)
            {
                if (strItemName.Contains(clbChildren.Items[i].ToString()))
                {
                    clbChildren.SetItemChecked(i, true);
                }
            }
        }
    }
}
