using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestSwAddIn.Forms;
using TestSwAddIn.Models;

namespace TestSwAddIn.Utils
{
    class Save_Dxf
    {
        public void SaveDxf()
        {
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            ConfigurationForm cf = new ConfigurationForm();
            Settings settings = new Settings();
            string dxfPath = settings.DxfPath;


            MessageBox.Show($"DXF path: {dxfPath}");

        }
        SldWorks swApp;
    }
}
