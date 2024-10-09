using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;

namespace TestSwAddIn.Utils
{
    class OpenFile
    {
        public ModelDoc2 OpenFromObject(ModelDoc2 item)
        {

            //swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            //ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;

            string filePath = item.GetPathName();
            string fileNameExtension = System.IO.Path.GetFileName(filePath);
            int type = (int)item.GetType();
            int options = (int)swOpenDocOptions_e.swOpenDocOptions_LoadLightweight;
            //int options = (int)swOpenDocOptions_e.swOpenDocOptions_OverrideDefaultLoadLightweight;
            string configurations = "";
            int errors = 0;

            MessageBox.Show($"fileName: {filePath}\ntype: {type}\noptions: {options}\nconfigurations: {configurations}");

            //item.SetSuppression2((int)swComponentSuppressionState_e.swComponentLightweight);
            //The second argument could be a 3 to open just the drawings
            //chegando string nula, tenho que rever a comparação no SelectChildren
            //swApp.OpenDoc6(fileName, type, options, configurations, ref errors, ref warnings);
            swApp.ActivateDoc3(fileNameExtension, true, (int)swRebuildOnActivation_e.swDontRebuildActiveDoc, ref errors);
            return item;
        }

        public void CloseFileObject(Component2 item)
        {
            swApp.CloseDoc(item.GetPathName());
        }

        SldWorks swApp;
    }
}
