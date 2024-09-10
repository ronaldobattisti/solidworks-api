using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using TestSwAddIn.Forms;
using System.Linq;

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

            //Create a list of objects where all components will be stored
            List<object> objChildrenList = new List<object>();
            List<object> objChildrenListNoDupes = new List<object>();

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            //Get the active document
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            ModelDoc2 modelDoc = null;
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
                        objChildrenList.Add(component);

                        // Recursively list all subcomponents
                        if (swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                        {
                            objChildrenList.AddRange(ListSubComponents(component));
                            objChildrenListNoDupes.Add(component);
                        }
                    }

                    SelectChildren sc = new SelectChildren(objChildrenList);
                    
                    //Getting the filepath of each component with drawing
                    string drawingPath = "";
                    List<string> filePaths = new List<string>();
                    //List<string> filePathsNoDupes = new List<string>();

                    foreach (Component2 item in objChildrenList)
                    {
                        DocumentSpecification swDocSpecification = default(DocumentSpecification);
                        swDocSpecification = (DocumentSpecification)swApp.GetOpenDocSpec(item.GetPathName());
                        drawingPath = System.IO.Path.ChangeExtension(item.GetPathName(), "slddrw");
                        if (System.IO.File.Exists(drawingPath) && (objChildrenListNoDupes.Contains(item) == false))
                        {
                            //filePaths.Add(drawingPath);
                            objChildrenListNoDupes.Add(item);
                        }
                        //filePathsNoDupes = filePaths.Distinct().ToList();  
                    }
                    foreach (Component2 filePath in objChildrenListNoDupes)
                    {
                        //Is still returing dupes I think I'll compare each object file to solve it
                        MessageBox.Show("Path: " + filePath.GetPathName());
                        //The second argument could be a 3 to open just the drawings
                        //modelDoc = swApp.OpenDoc6(filePath, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_ReadOnly, "", ref errors, ref warnings);
                        //swApp.CloseDoc(filePath.Split('\\')[filePath.Split('\\').Length-1].Split('.')[0]);
                        //MessageBox.Show()
                        //string filename = 
                    }

                    sc.ShowDialog();
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
            return objChildrenList;
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
                    list.Add(subComponent);
                    // Display the subcomponent name
                    //MessageBox.Show(subComponent.Name2);

                    // Recursively list subcomponents of this subcomponent
                    list.AddRange(ListSubComponents(subComponent));
                }
            }
            return list;
        }

        

        SldWorks swApp;
    }
}
