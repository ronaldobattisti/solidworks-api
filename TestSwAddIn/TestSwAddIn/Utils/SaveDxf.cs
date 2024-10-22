using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using TestSwAddIn.Forms;
using TestSwAddIn.Models;
using TestSwAddIn.Services;
using MessageBox = System.Windows.Forms.MessageBox;
using View = SolidWorks.Interop.sldworks.View;

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
            int bitMask = 0;
            bool boolStatus = false;
            string[] extensionsToDelete = {".DXF", ".DWG", ".PRS", ".ODT", ".TXT" };

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            sModelName = System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName());
            sPathName = settings.DxfPath + "\\" + sModelName + ".DXF";

            boolStatus = DeleteFileFromDirectory(settings.DxfPath, sModelName, extensionsToDelete);

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

            CreateDxfVisualization(swModel);
            //Export each annotation view to a separate drawing file
            swPart.ExportToDWG2(sPathName, sModelName, /*(int)swExportToDWG_e.swExportToDWG_ExportSheetMetal*/(int)swExportToDWG_e.swExportToDWG_ExportAnnotationViews, false, varAlignment, false, false, bitMask, varViews);
            //BitMask is used to select what will be exported in the drawing, see 
            //help.solidworks.com/2022/english/api/sldworksapi/solidworks.interop.sldworks~solidworks.interop.sldworks.ipartdoc~exporttodwg2.html

            MessageBox.Show("DXF saved Sucessifully");
        }

        public bool DeleteFileFromDirectory(string folderPath, string fileName, string[] extensions)
        {
            bool boolStatus = false;
            string filePath = "";
            string deletedItems = null;
            string notDeletedItems = null;
            string upperExtension = "";
            foreach (string extension in extensions)
            {
                upperExtension = extension.ToUpper();
                filePath = folderPath + @"\" + fileName + upperExtension;
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        deletedItems += filePath + ";\n";
                    }
                    catch (Exception ex)
                    {
                        notDeletedItems += notDeletedItems + ";\n";
                    }
                }
            }
            MessageBox.Show($"The following item were deleted: " +
                            $"{"\n" + deletedItems}" + 
                            $"The following items could not be deleted:" +
                            $"{"\n" + notDeletedItems}");

            return boolStatus;
        }

        public void CreateDxfVisualization(ModelDoc2 swDoc)
        {
            string tempPath = $"../../TempFiles/" + swDoc.GetTitle() + ".SLDDRW";
            tempPath = Path.GetFullPath(tempPath);
            Create_2D c2d = new Create_2D();
            DrawingDoc swDrawing = null;
            double[] paperSize = c2d.GetBoundingSize(swDoc);
            double sheetWidth = (double)paperSize[0];
            double sheetHeight = (double)paperSize[1];
            bool boolstatus = false;

            //swDoc = ((ModelDoc2)(swApp.NewDocument(tempPath, (int)swDwgPaperSizes_e.swDwgPapersUserDefined, sheetWidth, sheetHeight)));
            swDoc = ((ModelDoc2)swApp.NewDocument("C:\\SOLIDWORKS Data\\rbattisti\\DXF.drwdot", (int)swDwgPaperSizes_e.swDwgPapersUserDefined, sheetWidth, sheetHeight));

            swDrawing = (DrawingDoc)swDoc;
            Sheet swSheet = (Sheet)swDrawing.GetCurrentSheet();
            //Get the size of the sheet - I want to extract only the size
            double[] sheetProperties = (double[])swSheet.GetProperties2();
            
            DrawingDoc swPart = ((DrawingDoc)(swDoc));
            boolstatus = swPart.GenerateViewPaletteViews(swDoc.GetPathName());
            if (boolstatus == true)
            {
                swSheet.SetProperties2(12, 12, 1, 1, false, sheetWidth, sheetHeight, true);
                string[] palleteViewNames = (string[])swDrawing.GetDrawingPaletteViewNames();
                if (c2d.HaveFlatPattern(swDoc))
                {
                    //Run over each name possibility bcs the name in no standardized
                    string[] flatPatternNames = { "Padrão plano", "Flat pattern", "Padrão-plano", "Flat-pattern", "*Padrão plano" };
                    foreach (string flatPatternName in flatPatternNames)
                    {
                        View flatPatternView = (View)swDrawing.DropDrawingViewFromPalette2(flatPatternName, 0, 0, 0);
                    }
                }
            }
        }

        SldWorks swApp;
    }
}
