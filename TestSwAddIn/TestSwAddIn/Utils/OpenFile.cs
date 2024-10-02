using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;

namespace TestSwAddIn.Utils
{
    class OpenFile
    {
        public void OpenFromObject(Component2 item)
        {
            //List<object> componentList = new List<object>();
            string fileExtension;
            int intFileExtension = 0;
            int errors = 0;
            int warnings = 0;

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;

            //Get the file extension to throw as parameter when opening with OpenDoc6
            fileExtension = item.GetPathName().Split('.')[item.GetPathName().Split('.').Length - 1].ToUpper();
            /*MessageBox.Show("Path: " + item.GetPathName().Replace("\\", "/") +
                "\nExtension: " + fileExtension +
                "\nIts supression state is: " + item.GetSuppression2());*/
            //To open the file, I need to send what type of file it is:
            //1 - Part
            //2 - Assembly
            //3 - Drawing
            switch (fileExtension)
            {
                case "SLDPRT":
                    intFileExtension = 1;
                    break;
                case "SLDASM":
                    intFileExtension = 2;
                    break;
                case "SLDDRW":
                    intFileExtension = 3;
                    break;
            }
            item.SetSuppression2((int)swComponentSuppressionState_e.swComponentLightweight);
            //The second argument could be a 3 to open just the drawings
            //chegando string nula, tenho que rever a comparação no SelectChildren
            swApp.OpenDoc6(item.GetPathName().Replace("\\", "/"), 1, (int)swOpenDocOptions_e.swOpenDocOptions_LoadLightweight, "", ref errors, ref warnings);
        }

        public void CloseFileObject(Component2 item)
        {
            swApp.CloseDoc(item.GetPathName());
        }

        SldWorks swApp;
    }
}
