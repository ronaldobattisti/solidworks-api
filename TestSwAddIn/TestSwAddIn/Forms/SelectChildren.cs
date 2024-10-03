﻿using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TestSwAddIn.Utils;
using Utils;

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

        private void BtnChangeColor_Click(object sender, EventArgs e)
        {
            OpenFile offs = new OpenFile();
            List<string> selectedObjects = clbChildren.CheckedItems.OfType<string>().ToList();
            ChangeItemColor cic = new ChangeItemColor();
            int lErrors = 0;
            int lWarnings = 0;
            List<string> errors = new List<string>();

            /*MessageBox.Show("fstChildren: " + string.Join(",", fstChildren.Select(c => c.Name2)) + "\nclsChildren.selectedItems: " + 
                String.Join(",", selectedObjects.Select(c => c.ToString())));*/

            foreach (Component2 obj in listChildrenComponents)
            {
                string objName = System.IO.Path.GetFileNameWithoutExtension(obj.GetPathName());
                foreach (string item in selectedObjects)
                {
                    if (objName == item)
                    {
                        offs.OpenFromObject(obj);
                        SldWorks swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                        ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                        cic.ChangeColor();
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

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < clbChildren.Items.Count; i++)
            {
                clbChildren.SetItemChecked(i, true);
            }
        }

        private void BtnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbChildren.Items.Count; i++)
            {
                clbChildren.SetItemChecked(i, false);
            }
        }

        private void BtnSelectShown_Click(object sender, EventArgs e)
        {
            ListComponents lc = new ListComponents();
            listChildrenComponentsDisplayed = lc.ListChildrenComponentsDisplayed();

            List<string> strItemName = new List<string>();
            BtnUnselectAll_Click(null, null);
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

        private void BtnChangeColorItem_Click(object sender, EventArgs e)
        {
            ChangeItemColor cic = new ChangeItemColor();
            cic.ChangeColor();
        }

        private void BtnSavePdf_Click(object sender, EventArgs e)
        {
            OpenFile offs = new OpenFile();
            List<string> selectedObjects = clbChildren.CheckedItems.OfType<string>().ToList();

            int lErrors = 0;
            int lWarnings = 0;
            List<string> filesWoDrawings = new List<string>();
            List<string> errors = new List<string>();

            foreach (Component2 obj in listChildrenComponents)
            {
                string objName = System.IO.Path.GetFileNameWithoutExtension(obj.GetPathName());
                string drawingPath = System.IO.Path.ChangeExtension(obj.GetPathName(), "SLDDRW");
                foreach (string item in selectedObjects)
                {
                    if (objName == item)
                    {
                        if (System.IO.File.Exists(obj.GetPathName()))
                        {
                            offs.OpenFromObject(obj);
                            SldWorks swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                            ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;

                            swModel.Save3((int)swSaveAsOptions_e.swSaveAsOptions_Silent, ref lErrors, ref lWarnings);
                            if (lErrors != 0)
                            {
                                errors.Add(obj.Name2);
                            }
                            offs.CloseFileObject(obj);
                        } else
                        {
                            filesWoDrawings.Add(objName);
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                MessageBox.Show("The following items couldn't be saved\n:" + string.Join("\n", errors.Select(error => $"• {error}")));
            }
        }

        //Button used to make some tests
        private void BtnTestAction_Click(object sender, EventArgs e)
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            AssemblyDoc assemblyDoc = (AssemblyDoc)swModel;

            string modelName = System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName()).ToUpper();
            MessageBox.Show("Before getComponent");
            IComponent2 rootComponent = assemblyDoc.GetComponentByName(modelName);

            //Component2[] components = (Component2[])assemblyDoc.GetComponents(true);
            object[] components = (object[])assemblyDoc.GetComponents(false);


            if (components != null)
            {
                string msg = "";
                foreach (Component2 item in components)
                {
                    msg += "\nItem: " + item.Name2;
                }
                MessageBox.Show(msg);
            }
            else
            {
                MessageBox.Show("No components found in the assembly.");
            }

            //MessageBox.Show("Root component: " + rootComponent.Name2);

            double[] color = new double[9];
            color[0] = 1.0; // Red
            color[1] = 0.0; // Green
            color[2] = 0.0; // Blue
            color[3] = 0.5; // Ambient
            color[4] = 0.5; // Diffuse
            color[5] = 0.5; // Specular
            color[6] = 0.5; // Shininess
            color[7] = 0.0; // Transparency
            color[8] = 0.0; // Emission


            //rootComponent.SetMaterialPropertyValues2(color, (int)swInConfigurationOpts_e.swThisConfiguration, Type.Missing);


            /*MessageBox.Show("Before foreach");

            foreach (Component2 item in listChildrenComponents)
            {
                string itemName = System.IO.Path.GetFileNameWithoutExtension(item.GetPathName()).ToUpper();
                string modelName = System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName()).ToUpper();
                MessageBox.Show("Item: " + itemName + " | Model: " + modelName);
                if (itemName == modelName)
                {
                    object confName = Type.Missing;
                    int confOpt = (int)swInConfigurationOpts_e.swAllConfiguration;
                    double[] materialProps = (double[])item.GetMaterialPropertyValues2(confOpt, confName);
                    MessageBox.Show("Colors: " + materialProps[0] + materialProps[1] + materialProps[2]);
                }
            }*/



        }
        SldWorks swApp;
    }
}
