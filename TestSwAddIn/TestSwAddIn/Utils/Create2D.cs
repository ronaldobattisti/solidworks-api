using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using View = SolidWorks.Interop.sldworks.View;
using System.Linq;
using System;
using TestSwAddIn.Forms;
using TestSwAddIn.Models;
using TestSwAddIn.Services;

namespace TestSwAddIn.Utils
{
    class Create_2D
    {
        public void Create2D()
        {
            Settings settings = new Settings();
            JsonImporter jsonImporter = new JsonImporter();
            //get the path where the settings are saved - it is a const string placed in ConfigurationForm
            string settingPath = ConfigurationForm.settingPath;
            settings = (Settings)jsonImporter.LoadJson(settingPath, settings);

            ModelDoc2 swDoc = null;
            DrawingDoc swDrawing = null;
            //AssemblyDoc swAssembly = null;
            bool boolstatus = false;

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            swDoc = ((ModelDoc2)(swApp.ActiveDoc));
            double[] size = GetBoundingSize(swDoc);
            double swSheetWidth = 0.21;
            double swSheetHeight = 0.3;
            string itemPath = swDoc.GetPathName();
            double sheetWidth = 0.0;
            double sheetHeight = 0.0;
            double scale = 0.0;


            if (swDoc != null && swDoc.GetType() != (int)swDocumentTypes_e.swDocDRAWING)
            {
                //Reference the model .drwdot that will be caught
                swDoc = ((ModelDoc2)(swApp.NewDocument(settings.SheetTemplatePath, (int)swDwgPaperSizes_e.swDwgPapersUserDefined, swSheetWidth, swSheetHeight)));
                swDrawing = (DrawingDoc)swDoc;
                Sheet swSheet = (Sheet)swDrawing.GetCurrentSheet();
                //Get the size of the sheet - I want to extract only the size
                double[] sheetProperties = (double[])swSheet.GetProperties2();
                //*10 to cast from centimeter to milimeter
                sheetWidth = (double)sheetProperties[5] * 1000;
                sheetHeight = (double)sheetProperties[6] * 1000;

                scale = GetScale(sheetWidth, sheetHeight, size[0], size[1], size[2], HaveFlatPattern(swDoc));

                DrawingDoc swPart = ((DrawingDoc)(swDoc));
                //place a try catch here
                boolstatus = swPart.GenerateViewPaletteViews(itemPath);
                if (boolstatus == true)
                {
                    swSheet.SetProperties2(12, 12, 1, scale, false, swSheetWidth, swSheetHeight, true);
                    string[] palleteViewNames = (string[])swDrawing.GetDrawingPaletteViewNames();
                    //here are setted the absolute and relative positions to insert the view
                    double xPosFront = 0.05;
                    double yPosFront = 0.22;
                    double xPosOffset = 0.1;
                    double yPosOffset = 0.07;
                    double xPosFlat = 0.1;
                    double yPosFlat = 0.1;
                    if (palleteViewNames.Contains("*Frontal"))
                    {
                        View frontView = (View)(swDrawing.DropDrawingViewFromPalette2("*Frontal", xPosFront, yPosFront, 0));
                        boolstatus = swDoc.Extension.SelectByID2("Vista de desenho1", "DRAWINGVIEW"/*swSelectType_e.swSelDRAWINGVIEWS.ToString()*/, 0, 0, 0, false, 0, null, 0);
                        View rightView = swDrawing.CreateUnfoldedViewAt3(xPosFront + xPosOffset, yPosFront, 0, false);
                        boolstatus = swDoc.Extension.SelectByID2("Vista de desenho1", "DRAWINGVIEW"/*swSelectType_e.swSelDRAWINGVIEWS.ToString()*/, 0, 0, 0, false, 0, null, 0);
                        View topView = swDrawing.CreateUnfoldedViewAt3(xPosFront, yPosFront - yPosOffset, 0, false);
                    }
                    else
                    {
                        MessageBox.Show("View '*Front' could not be found");
                    }

                    if (HaveFlatPattern(swDoc))
                    {
                        //Run over each name possibility bcs the name in no standardized
                        string[] flatPatternNames = { "Padrão plano", "Flat pattern", "Padrão-plano", "Flat-pattern", "*Padrão plano" };
                        foreach (string flatPatternName in flatPatternNames)
                        {
                            View flatPatternView = (View)(swDrawing.DropDrawingViewFromPalette2("Padrão plano", xPosFlat, yPosFlat, 0));
                            if (flatPatternView != null)
                            {
                                break;
                            }
                        }
                        //View flatPatternView = (View)(swDrawing.DropDrawingViewFromPalette2("Padrão plano", xPosFlat, yPosFlat, 0));
                    }
                    /*else
                    {
                        MessageBox.Show("View 'Padrão plano' couldn't be found");
                    }*/

                    if (palleteViewNames.Contains("*Isométrica"))
                    {
                        View flatPatternView = (View)(swDrawing.DropDrawingViewFromPalette2("*Isométrica", xPosFront + xPosOffset, yPosFront - yPosOffset, 0));
                    }
                    else
                    {
                        MessageBox.Show("View '*Isométrica' couldn't be found");
                    }
                }
                else
                {
                    MessageBox.Show("Couldn't generate pallette view. Check if the file is saved.");
                }

            }
            else
            {
                MessageBox.Show("The drawing could not be created");
            }
        }

        public double[] GetBoundingSize(ModelDoc2 item)
        {
            double xSize = 0.0;
            double ySize = 0.0;
            double zSize = 0.0;
            double[] boundingBox = new double[] { };
            bool boolstatus;
            if (item.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                if (HaveFlatPattern(item))
                {
                    //MessageBox.Show($"item {System.IO.Path.GetFileNameWithoutExtension(item.GetPathName())} have flat pattern");
                    boolstatus = item.Extension.SelectByID2("Padrão-Plano", "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
                    PartDoc swPart = (PartDoc)item;
                    boundingBox = (double[])swPart.GetPartBox(false);
                    xSize = boundingBox[3] - boundingBox[0];
                    ySize = boundingBox[4] - boundingBox[1];
                    zSize = boundingBox[5] - boundingBox[2];
                }
                else
                {
                    //MessageBox.Show($"item {System.IO.Path.GetFileNameWithoutExtension(item.GetPathName())} don't have flat pattern");
                    PartDoc swPart = (PartDoc)item;
                    boundingBox = (double[])swPart.GetPartBox(false);
                    xSize = boundingBox[3] - boundingBox[0];
                    ySize = boundingBox[4] - boundingBox[1];
                    zSize = boundingBox[5] - boundingBox[2];
                }

            }
            else if (item.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                AssemblyDoc swAsm = (AssemblyDoc)item;
                boundingBox = (Double[])swAsm.GetBox((int)swBoundingBoxOptions_e.swBoundingBoxIncludeRefPlanes);
                //In assembly returns in meter, so we have to *1000 to get in milimeters
                xSize = (boundingBox[3] - boundingBox[0]) * 1000;
                ySize = (boundingBox[4] - boundingBox[1]) * 1000;
                zSize = (boundingBox[5] - boundingBox[2]) * 1000;
            }
            return new double[] {xSize, ySize, zSize};
        }

        public bool HaveFlatPattern(ModelDoc2 item)
        {
            //SolidWorks could have many names for the standart flat pattern configuration depending of
            //the version or templates, so I created a String with tha main ones to scan them
            string[] flatNames = { "Padrão-Plano", "DefaultSM-FLAT-PATTERN", "Valor predeterminadoSM-FLAT-PATTERN" };
            bool boolstatus = false;
            foreach (string flatName in flatNames)
            {
                boolstatus = item.Extension.SelectByID2(flatName, "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
                if (boolstatus == true)
                {
                    return boolstatus;
                }
            }
            return boolstatus;
        }

        private double GetScale(double paperWidth, double paperHeight, double itemX, double itemY, double itemZ, bool haveFlatPattern)
        {
            double xScale = 1/((paperWidth / itemX) / 4);
            double yScale = 1/((paperWidth / itemY) / 4);
            double zScale = 1/((paperHeight / itemZ) / 5);
            double[] scales = { xScale, yScale, zScale };

            double lower = xScale;

            foreach (double item in scales)
            {
                if (item > lower)
                {
                    lower = item;
                }
            }

            return lower;
        }
        SldWorks swApp;
    }
}
