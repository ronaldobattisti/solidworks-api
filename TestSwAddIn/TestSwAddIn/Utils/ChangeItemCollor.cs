using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using System;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using TestSwAddIn.Forms;

namespace TestSwAddIn.Utils
{
    class ChangeItemCollor
    {

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
                        //MessageBox.Show("Color: " + paintCode);
                        //Console.WriteLine($"Cor: {paintCode}");
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
