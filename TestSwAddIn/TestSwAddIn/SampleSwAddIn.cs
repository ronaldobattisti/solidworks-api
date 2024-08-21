using SolidWorks.Interop.sldworks;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using TestSwAddIn.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using PaintModelUtilities;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using TestSwAddIn.Forms;

namespace SampleAddIn
{
    [ComVisible(true)]
    [Guid("1EE37F60-45F7-4FFA-93E2-5ACCC371530F")]
    [Title("Sample AddIn")]

    public class TestSampleAddIn : SwAddInEx
    {
        [Title("Sample AddIn")]
        public enum Commands_e
        {
            [Title("List Components")]
            [Description("List components")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ListComponents,
            [Title("Change collor!")]
            [Description("Color changed!")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ChangeColor
        }

        public override void OnConnect()
        {
            var cmdGrp = this.CommandManager.AddCommandGroup<Commands_e>();
            cmdGrp.CommandClick += CmGrp_CommandClick;
        }

        private void CmGrp_CommandClick(Commands_e spec)
        {
            

            switch (spec)
            {
                
                case Commands_e.ListComponents:
                    ListComponents();
                    break;

                case Commands_e.ChangeColor:
                    ChangeCollor();
                    break;
            }
            
        }

        #region List Components
        /*private void ListComponents()
        {
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            AssemblyDoc swAssemblyDoc = (AssemblyDoc)swModelDoc;

            String[] str = (String[])swAssemblyDoc.GetComponents(false);
            foreach (string item in str)
            {
                MessageBox.Show(item);
            }
        }*/

        private void ListComponents()
        {
            List<string> list = new List<string>();

            string msg = "";

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            // Get the active document
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
                        list.Add(component.Name2.Split('-')[0]);
                        // Display the component name
                        //MessageBox.Show(component.Name2);

                        // Optionally, recursively list all subcomponents
                        list.AddRange(ListSubComponents(component));
                        
                    }
                    list = Utilities.removeDuplicated(list);
                    string s = String.Join(",", list);
                    MessageBox.Show(s);
                    SelectChildren sc = new SelectChildren();
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
            
        }

        // Recursive method to list subcomponents
        private List<string> ListSubComponents(Component2 parentComponent)
        {
            List<string> list = new List<string>();

            object[] subComponents = (object[])parentComponent.GetChildren();

            if (subComponents != null)
            {
                foreach (Component2 subComponent in subComponents)
                {
                    list.Add(subComponent.Name2);
                    // Display the subcomponent name
                    //MessageBox.Show(subComponent.Name2);

                    // Recursively list subcomponents of this subcomponent
                    list.AddRange(ListSubComponents(subComponent));
                }
            }
            return list;
        }




        #endregion

        #region Change Collor
        private void ChangeCollor()
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
            materialProps = Utilities.getColor(paintCode, materialProps); //Send the variable with the cod of the color to the function
            swModel.MaterialPropertyValues = materialProps; //The function return a double[] with all properties and color changed
            swModel.EditRebuild3();
        }
        #endregion


        SldWorks swApp;
    }
}
