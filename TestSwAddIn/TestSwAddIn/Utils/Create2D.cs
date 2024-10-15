using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using View = SolidWorks.Interop.sldworks.View;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using System.Linq;
using System;
using Xarial.XCad.Geometry.Evaluation;

namespace TestSwAddIn.Utils
{
    class Create_2D
    {
        public void Create2D()
        {
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
                swDoc = ((ModelDoc2)(swApp.NewDocument(@"\\fileserver.sazi.com.br\sistemas$\SolidWorks\Templates\rbattisti\A4 RETRATO.drwdot", (int)swDwgPaperSizes_e.swDwgPapersUserDefined, swSheetWidth, swSheetHeight)));
                //swDoc = ((ModelDoc2)(swApp.NewDocument("C:\\SOLIDWORKS Data\\rbattisti\\A4 RETRATO.drwdot", (int)swDwgPaperSizes_e.swDwgPapersUserDefined, swSheetWidth, swSheetHeight)));
                swDrawing = (DrawingDoc)swDoc;
                Sheet swSheet = (Sheet)swDrawing.GetCurrentSheet();
                //Get the size of the sheet - I want to extract only the size
                double[] sheetProperties = (double[])swSheet.GetProperties2();
                //*100 to cast from meter to milimeter
                sheetWidth = sheetProperties[5] * 100;
                sheetHeight = sheetProperties[6] * 100;

                scale = GetScale(sheetWidth, sheetHeight, size[0], size[1], size[2], HaveFlatPattern(swDoc));

                DrawingDoc swPart = ((DrawingDoc)(swDoc));
                //place a try catch here
                boolstatus = swPart.GenerateViewPaletteViews(itemPath);
                if (boolstatus == true)
                {
                    swSheet.SetProperties2(12, 12, 1, 5, false, swSheetWidth, swSheetHeight, true);
                    string[] palleteViewNames = (string[])swDrawing.GetDrawingPaletteViewNames();
                    double xPosFront = 0.05;
                    double yPosFront = 0.25;
                    double xPosOffset = 0.1;
                    double yPosOffset = 0.1;
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

                    if (palleteViewNames.Contains("Padrão plano")){
                        View flatPatternView = (View)(swDrawing.DropDrawingViewFromPalette2("Padrão plano", xPosFlat, yPosFlat, 0));
                    }
                    else
                    {
                        MessageBox.Show("View 'Padrão plano' couldn't be found");
                    }

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
                xSize = boundingBox[3] - boundingBox[0];
                ySize = boundingBox[4] - boundingBox[1];
                zSize = boundingBox[5] - boundingBox[2];
            }
            return new double[] {xSize, ySize, zSize};
        }

        public bool HaveFlatPattern(ModelDoc2 item)
        {
            bool boolstatus = false;
            boolstatus = item.Extension.SelectByID2("Padrão-Plano", "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
            return boolstatus;
        }

        private double GetScale(double paperWidth, double paperHeight, double itemWidth, double itemHeight, double itemLength, bool haveFlatPattern)
        {
            double xScale = ((paperWidth / itemWidth) / 3);
            double yScale = ((paperWidth / itemLength) / 3);
            double zScale = ((paperHeight / itemHeight) / 5);
            //return Math.Min(xScale, yScale, zScale);
            return 0.0;
        }
        SldWorks swApp;
    }
}
