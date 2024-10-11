using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using View = SolidWorks.Interop.sldworks.View;

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
            double swSheetWidth = 0.21;
            double swSheetHeight = 0.3;
            string itemPath = swDoc.GetPathName();

            swDoc = ((ModelDoc2)(swApp.NewDocument("C:\\Users\\rbattisti\\Desktop\\A4 RETRATO.drwdot", (int)swDwgPaperSizes_e.swDwgPapersUserDefined, swSheetWidth, swSheetHeight)));

            if (swDoc != null)
            {
                swDrawing = (DrawingDoc)swDoc;
                object swSheet = null;
                swSheet = swDrawing.GetCurrentSheet();
                DrawingDoc swPart = ((DrawingDoc)(swDoc));
                //place a try catch here
                boolstatus = swPart.GenerateViewPaletteViews(itemPath);
                View frontView = ((View)(swDrawing.DropDrawingViewFromPalette2("*Frontal", 0.05, 0.25, 0)));
                View rightView = ((View)(swDrawing.CreateUnfoldedViewAt3(0.4, 0, 0, false)));
            }
            else
            {
                MessageBox.Show("The drawing could not be created");
            }
        }
        SldWorks swApp;
    }
}
