using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using TestSwAddIn.Forms;
using System.Linq;
using System;

namespace Utils
{
    class ListComponents
    {

        public List<object> ListChildrenComponents()
        {
            //Returns a List<Component2> with all children components of the assembly
            //The list will contain dupes objects because is contains the quantity of
            //each one inside the assembly - If you have twi components "asd", one object
            //will be "asd - 1" and other "asd - 2" - so you have to filter de dupes after
            //Just add components that aren't supressed

            //Create a list of objects where all components will be stored
            List<object> objChildrenList = new List<object>();
            List<object> objChildrenListNoDupes = new List<object>();

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            //Get the active document
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            ModelDoc2 modelDoc = null;
            string fileExtension;
            int intFileExtension = 0;
            int errors = 0;
            int warnings = 0;

            // Check if the active document is an assembly
            if (swModelDoc != null && swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {

                AssemblyDoc swAssemblyDoc = (AssemblyDoc)swModelDoc;

                // Get the root components (top-level components)
                object[] components = (object[])swAssemblyDoc.GetComponents(false);

                if (components != null)
                {
                    foreach (Component2 component in components)
                    {
                        //Check if subcomponent is not supressed
                        if (component.GetSuppression2() != 0)
                        { 
                            objChildrenList.Add(component);
                            // Recursively list all subcomponents
                            if (swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                            {
                                objChildrenList.AddRange(ListSubComponents(component));
                            }
                        }
                    }
                    
                    //Getting the filepath of each component with drawing
                    string drawingPath = "";
                    List<string> filePaths = new List<string>();
                    //List<string> filePathsNoDupes = new List<string>();

                    foreach (Component2 item in objChildrenList)
                    {
                        //DocumentSpecification swDocSpecification = default(DocumentSpecification);
                        //swDocSpecification = (DocumentSpecification)swApp.GetOpenDocSpec(item.GetPathName());
                        drawingPath = System.IO.Path.ChangeExtension(item.GetPathName(), "slddrw");
                        
                        //Create a List with all objects that contains drawing and without dupes
                        if (filePaths.Contains(item.GetPathName()) == false)
                        {
                            filePaths.Add(item.GetPathName());
                            objChildrenListNoDupes.Add(item);
                        }
                    }

                    MessageBox.Show("filePaths contains the following items: \n" + string.Join("\n", filePaths));

                    foreach (Component2 filePath in objChildrenListNoDupes)
                    {
                        //****Still couldn't open each file...*********//
                        fileExtension = filePath.GetPathName().Split('.')[filePath.GetPathName().Split('.').Length-1].ToUpper();
                        MessageBox.Show("Path: " + filePath.GetPathName() + 
                            "\nExtension: " + fileExtension + 
                            "\nIts supression state is: " + filePath.GetSuppression2());
                        //To open the file, I need to send what type of file it is:
                        //1 - Part
                        //2 - Assembly
                        //3 - Drawing
                        switch (fileExtension){
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
                        //The second argument could be a 3 to open just the drawings
                        /*modelDoc = */swApp.OpenDoc6(filePath.GetPathName(), intFileExtension, (int)swOpenDocOptions_e.swOpenDocOptions_LoadLightweight, "", ref errors, ref warnings);
                        swApp.CloseDoc(filePath.GetPathName());
                    }
                }
                else
                {
                    MessageBox.Show("No components found in the assembly.");
                }
            }
            else
            {
                MessageBox.Show("The active document is not an assembly.");
            }
            return objChildrenListNoDupes;
        }

        // Recursive method to list subcomponents
        private List<object> ListSubComponents(Component2 parentComponent)
        {
            List<object> list = new List<object>();

            object[] subComponents = (object[])parentComponent.GetChildren();

            if (subComponents != null)
            {
                foreach (Component2 subComponent in subComponents)
                {
                    //Check if subcomponent is not supressed
                    if (subComponent.GetSuppression2() != 0)
                    {
                        list.Add(subComponent);
                        list.AddRange(ListSubComponents(subComponent));
                    } 
                }
            }
            return list;
        }
        SldWorks swApp;
    }
}
