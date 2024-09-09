using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using TestSwAddIn.Forms;
using System;

namespace Utils
{
    class ListComponents
    {

        public List<object> ListChildrenComponents()
        {
            //Create a list of objects where all components will be stored
            List<object> objChildrenList = new List<object>();

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            //Get the active document
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;

            // Check if the active document is an assembly
            if (swModelDoc != null && swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {

                AssemblyDoc swAssemblyDoc = (AssemblyDoc)swModelDoc;

                // Get the root components (top-level components)
                object[] components = (object[])swAssemblyDoc.GetComponents(false);

                if (components != null)
                {
                    foreach (Component2 component in components)
                    {
                        objChildrenList.Add(component);
                        // Display the component name
                        //MessageBox.Show(component.Name2);

                        // Optionally, recursively list all subcomponents
                        if (swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                        {
                            objChildrenList.AddRange(ListSubComponents(component));
                        }
                    }

                    SelectChildren sc = new SelectChildren(objChildrenList);
                    sc.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No components found in the assembly.");
                }
            }
            else
            {
                MessageBox.Show("The active document is not an assembly.");
            }
            return objChildrenList;
        }

        // Recursive method to list subcomponents
        private List<object> ListSubComponents(Component2 parentComponent)
        {
            List<object> list = new List<object>();

            object[] subComponents = (object[])parentComponent.GetChildren();

            if (subComponents != null)
            {
                foreach (Component2 subComponent in subComponents)
                {
                    list.Add(subComponent);
                    // Display the subcomponent name
                    //MessageBox.Show(subComponent.Name2);

                    // Recursively list subcomponents of this subcomponent
                    list.AddRange(ListSubComponents(subComponent));
                }
            }
            return list;
        }

        public void ChangeCollor()
        {
            String paintCode = "";

            SldWorks swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = ((ModelDoc2)(swApp.ActiveDoc));

            CustomPropertyManager cusPropMgr = swModel.Extension.CustomPropertyManager[""];

            string[] propertyNames = (string[])cusPropMgr.GetNames();
            if (propertyNames != null)
            {
                //For each property write its value
                foreach (string propertyName in propertyNames)
                {
                    string propertyValue;
                    string propertyResolvedValue;
                    bool wasResolved;
                    cusPropMgr.Get5(propertyName, false, out propertyValue, out propertyResolvedValue, out wasResolved);
                    Console.WriteLine($"Property: {propertyName}");
                    Console.WriteLine($"Value: {propertyValue}");
                    Console.WriteLine($"Resolved Value: {propertyResolvedValue}");
                    Console.WriteLine($"Resolved: {wasResolved}\n");
                    if (propertyName.ToUpper() == "TRATAMENTO_SUPERFICIAL")
                    {
                        paintCode = propertyResolvedValue.Split('-')[0];
                        MessageBox.Show("Color: " + paintCode);
                        Console.WriteLine($"Cor: {paintCode}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Properties are null!");
            }

            //Thread.Sleep(500); //This delay is used to avoid SolidWorks crashes

            double[] materialProps = (double[])swModel.MaterialPropertyValues; //get the visual properties of the actual part in a variable
            materialProps = PaintModelUtilities.Utilities.getColor(paintCode, materialProps); //Send the variable with the cod of the color to the function
            swModel.MaterialPropertyValues = materialProps; //The function return a double[] with all properties and color changed
            swModel.EditRebuild3();
        }

        SldWorks swApp;
    }
}
