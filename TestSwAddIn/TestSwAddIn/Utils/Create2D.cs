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
            AssemblyDoc swAssembly = null;
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
                View myView = null;
                myView = ((View)(swDrawing.DropDrawingViewFromPalette2("Vista de desenho1", 1.1024722863741338E+18, 9.078048498845268E+18, 0)));
            }
            else
            {
                MessageBox.Show("The drawing could not be created");
            }
        }
        SldWorks swApp;
    }
}
