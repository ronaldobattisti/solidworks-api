using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using TestSwAddIn.Forms;
using TestSwAddIn.Models;
using TestSwAddIn.Services;
using Xarial.XCad;

namespace TestSwAddIn.Utils
{
    class Save_Dxf
    {
        public void SaveDxf()
        {
            Settings settings = new Settings();
            JsonImporter jsonImporter = new JsonImporter();
            //get the path where the settings are saved - it is a const string placed in ConfigurationForm
            string settingPath = ConfigurationForm.settingPath;
            settings = (Settings)jsonImporter.LoadJson(settingPath, settings);

            PartDoc swPart = null;
            string sModelName;
            string sPathName;
            object varAlignment;
            double[] dataAlignment = new double[12];
            object varViews;
            string[] dataViews = new string[2];
            int options = 0;


            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            sModelName = System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName());
            sPathName = settings.DxfPath + @"\\" + sModelName + ".DXF";

            swPart = (PartDoc)swModel;

            dataAlignment[0] = 0.0;
            dataAlignment[1] = 0.0;
            dataAlignment[2] = 0.0;
            dataAlignment[3] = 1.0;
            dataAlignment[4] = 0.0;
            dataAlignment[5] = 0.0;
            dataAlignment[6] = 0.0;
            dataAlignment[7] = 1.0;
            dataAlignment[8] = 0.0;
            dataAlignment[9] = 0.0;
            dataAlignment[10] = 0.0;
            dataAlignment[11] = 1.0;

            varAlignment = dataAlignment;

            dataViews[0] = "*Current";
            dataViews[1] = "*Front";

            varViews = dataViews;

            //Export each annotation view to a separate drawing file
            swPart.ExportToDWG2(sPathName, sModelName, (int)swExportToDWG_e.swExportToDWG_ExportAnnotationViews, false, varAlignment, false, false, 0, varViews);

            //Export sheet metal to a single drawing file
            options = 1;  //include flat-pattern geometry
            swPart.ExportToDWG2(sPathName, sModelName, (int)swExportToDWG_e.swExportToDWG_ExportSheetMetal, true, varAlignment, false, false, options, null);

            /*Settings settings = new Settings();
            JsonImporter jsonImporter = new JsonImporter();
            //get the path where the settings are saved - it is a const string placed in ConfigurationForm
            string settingPath = ConfigurationForm.settingPath;

            settings = (Settings)jsonImporter.LoadJson(settingPath, settings);
            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            PartDoc swPart = null;
            string dxfPath = settings.DxfPath;



            MessageBox.Show($"DXF path: {dxfPath}");



            try
            {
                if (swModelDoc == null)
                {
                    Console.WriteLine("Failed to open part: ");
                    return;
                }

                // Cast the model to a part document
                swPart = (PartDoc)swModelDoc;

                // Check if the part has a flat pattern (for sheet metal)
                Feature flatPatternFeature = (Feature)swPart.FeatureByName("Flat-Pattern1");
                if (flatPatternFeature != null)
                {
                    // Create the flat pattern if it is not already created
                    swPart.CreateFlatPatternView();
                    Console.WriteLine("Flat pattern created.");
                }

                // Export the flat pattern as a DXF file
                bool exportSuccess = swModelDoc.Extension.SaveAs(dxfPath,
                    (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                    (int)swSaveAsOptions_e.swSaveAsOptions_Copy,
                    null, ref intErrors, ref intWarnings);

                if (exportSuccess)
                {
                    Console.WriteLine("DXF saved successfully at: " + dxfPath);
                }
                else
                {
                    Console.WriteLine("Failed to save DXF.");
                }
            }
            catch (COMException comEx)
            {
                Console.WriteLine("COM Exception: " + comEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                // Close the part if it was opened
                if (swModelDoc != null)
                {
                    swApp.CloseDoc(swModelDoc.GetTitle());
                }

                // Clean up the COM object
                if (swApp != null)
                {
                    Marshal.ReleaseComObject(swApp);
                    swApp = null;
                }
            }*/

        }
        SldWorks swApp;
    }
}
