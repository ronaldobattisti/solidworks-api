using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using PaintModelUtilities;
using TestSwAddIn.Forms;
using System.Linq;
using System;

namespace Utils
{
    class ListComponents
    {
        public List<Component2> ListChildrenComponents()
        {
            //Returns a List<Component2> with all children components of the assembly
            //The list will contain dupes objects because is contains the quantity of
            //each one inside the assembly - If you have two components "asd", one object
            //will be "asd - 1" and other "asd - 2" - so you have to filter de dupes after
            //Just add components that aren't supressed

            //Create a list of objects where all components will be stored
            List<Component2> objChildrenList = new List<Component2>();
            List<Component2> objChildrenListNoDupes = new List<Component2>();

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            //Get the active document
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;
            //objChildrenList.Add((component2)swApp.ActivateDoc);

            // Check if the active document is an assembly
            if (swModelDoc != null && swModelDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {

                AssemblyDoc swAssemblyDoc = (AssemblyDoc)swModelDoc;
                ModelDoc2 swModel = swApp.ActiveDoc as ModelDoc2;
                
                // Get the root components (top-level components)
                object[] components = (object[])swAssemblyDoc.GetComponents(true);

                if (components != null)
                {
                    foreach (Component2 component in components)
                    {
                        //Check if subcomponent is not supressed
                        if (component.GetSuppression2() != 0)
                        { 
                            objChildrenList.Add(component);
                            // Recursively list all subcomponents
                            if (Utilities.component2IsAssembly(component))
                            {
                                objChildrenList.AddRange(ListSubComponents(component));
                            }
                        }
                    }
                    
                    //Getting the filepath of each component with drawing
                    List<string> filePaths = new List<string>();
                    //List<string> filePathsNoDupes = new List<string>();

                    foreach (Component2 item in objChildrenList)
                    {                        
                        //Create a List with all objects that contains drawing and without dupes
                        if (filePaths.Contains(item.GetPathName()) == false)
                        {
                            filePaths.Add(item.GetPathName());
                            objChildrenListNoDupes.Add(item);
                        }
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
            //DocumentSpecification swDocSpecification = default(DocumentSpecification);
            //swDocSpecification = (DocumentSpecification)swApp.GetOpenDocSpec("C:/Users/rbattisti/Desktop/Valvula retencao/360550.SLDPRT");
            String message = "";
            foreach (Component2 item in objChildrenList)
            {
                message = message + "\n" + System.IO.Path.GetFileName(item.GetPathName());
            }
            //MessageBox.Show("The items in list are:\n" + message);
            return objChildrenListNoDupes;
        }

        // Recursive method to list subcomponents
        private List<Component2> ListSubComponents(Component2 parentComponent)
        {
            List<Component2> list = new List<Component2>();

            object[] subComponents = (object[])parentComponent.GetChildren();

            if (subComponents != null)
            {
                foreach (Component2 subComponent in subComponents)
                {
                    //Check if subcomponent is not supressed
                    if (subComponent.GetSuppression2() != 0)
                    {
                        list.Add(subComponent);
                        if (Utilities.component2IsAssembly(subComponent))
                        {
                            ListSubComponents(subComponent);
                            list.AddRange(ListSubComponents(subComponent));
                        }
                    } 
                }
            }
            return list;
        }
        SldWorks swApp;
    }
}
