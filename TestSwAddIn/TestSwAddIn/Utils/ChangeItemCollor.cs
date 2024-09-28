using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using System;
using SolidWorks.Interop.swconst;

namespace TestSwAddIn.Utils
{
    class ChangeItemCollor
    {

        public void ChangeCollor()
        {
            SldWorks swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = ((ModelDoc2)(swApp.ActiveDoc));
            int doctype = swModel.GetType();

            if (doctype == (int)swDocumentTypes_e.swDocPART)
            {
                ApplyColorToModel(swModel);
            }
            else if (doctype == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                ApplyColorToAssembly(swModel);
            }

            
            MessageBox.Show("Change color concluded");
        }

        public void ApplyColorToModel(ModelDoc2 swModel)
        {
            CustomPropertyManager cusPropMgr = swModel.Extension.CustomPropertyManager[""];
            String paintCode = "";
            string properties = "";
            string propertyValue;
            string propertyResolvedValue;
            bool wasResolved;
            string[] propertyNames = (string[])cusPropMgr.GetNames();
            if (propertyNames != null)
            {
                //For each property write its value
                properties = ("File: " + System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName()) + "\n");
                foreach (string propertyName in propertyNames)
                {
                    cusPropMgr.Get5(propertyName, false, out propertyValue, out propertyResolvedValue, out wasResolved);
                    properties += ("Property: " + propertyName + " = " + propertyValue + "\n");
                    if (propertyName.ToUpper() == "TRATAMENTO_SUPERFICIAL")
                    {
                        paintCode = propertyResolvedValue.Split('-')[0];
                        MessageBox.Show("Color: " + paintCode);
                        //Console.WriteLine($"Cor: {paintCode}");
                    }
                }
                MessageBox.Show(properties);
            }
            else
            {
                MessageBox.Show("Properties are null!");
            }

            //Avoid solidworks crashing if none parameter is sent
            if (paintCode != "" && )
            {
                double[] materialProps = (double[])swModel.MaterialPropertyValues; //get the visual properties of the actual part in a variable
                materialProps = PaintModelUtilities.Utilities.GetColor(paintCode, materialProps); //Send the variable with the cod of the color to the function
                swModel.MaterialPropertyValues = materialProps; //The function return a double[] with all properties and color changed
                swModel.EditRebuild3();
            }
        }
        
        public void ApplyColorToAssembly(ModelDoc2 swModel)
        {
            /*String paintCode = "";

            // Access the custom properties of the assembly
            CustomPropertyManager cusPropMgr = swModel.Extension.CustomPropertyManager[""];
            string[] propertyNames = (string[])cusPropMgr.GetNames();

            if (propertyNames != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    cusPropMgr.Get5(propertyName, false, out string propertyValue, out string propertyResolvedValue, out bool wasResolved);

                    if (propertyName.ToUpper() == "TRATAMENTO_SUPERFICIAL")
                    {
                        paintCode = propertyResolvedValue.Split('-')[0];
                        MessageBox.Show("Color: " + paintCode);
                    }
                }
            }
            else
            {
                MessageBox.Show("Properties are null!");
                return;
            }

            // Apply the color if paintCode is not empty
            if (!string.IsNullOrEmpty(paintCode))
            {
                // Apply appearance to the top-level assembly
                double[] rgbColor = PaintModelUtilities.Utilities.GetRGBColorFromCode(paintCode); // Get RGB values from paint code
                ApplyAssemblyColor(swModel, rgbColor);
            }

            MessageBox.Show("Change assembly color concluded.");
        }

        private void ApplyAssemblyColor(ModelDoc2 swModel, double[] rgbColor)
        {
            // Ensure the color is a valid RGB array
            if (rgbColor == null || rgbColor.Length != 3)
            {
                MessageBox.Show("Invalid color.");
                return;
            }

            // Create an appearance for the entire assembly (top-level)
            ModelDocExtension swModelExtension = swModel.Extension;
            int appearanceId = swModelExtension.InsertAppearance(0, (int)swInsertAppearancesScope_e.swThisDisplayState);

            // Get the display state settings to change the color
            DisplayStateSetting swDisplayStateSetting = swModelExtension.CreateDisplayStateSetting((int)swDisplayStateOpts_e.swSpecifyDisplayState);
            swDisplayStateSetting.DisplayStateName = "Default"; // Ensure it applies to the active display state

            // Set the color for the entire assembly
            AppearanceSetting swAppearanceSetting = swDisplayStateSetting.GetAppearanceSetting();
            swAppearanceSetting.Color = (int)((rgbColor[0] * 255) << 16 | (rgbColor[1] * 255) << 8 | (rgbColor[2] * 255)); // Convert RGB to int
            swAppearanceSetting.SpecularAmount = 0.5; // Adjust shine (optional)
            swAppearanceSetting.SetColor((int)swColorAttribute_e.swFaceColor);

            // Apply and rebuild
            swModel.EditRebuild3();*/
        }
    }
}
