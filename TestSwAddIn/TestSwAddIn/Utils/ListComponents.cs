using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using PaintModelUtilities;
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

            swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");

            //Get the active document
            ModelDoc2 swModelDoc = (ModelDoc2)swApp.ActiveDoc;

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
                        //False: is supr && is shown: False || is supr && is hidden: True
                        if (component.GetSuppression2() != 0)
                        { 
                            objChildrenList.Add(component);
                            // Recursively list all subcomponents
                            if (Utilities.Component2IsAssembly(component))
                            {
                                objChildrenList.AddRange(ListSubComponents(component));
                            }
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
            objChildrenList = RemoveDupes(objChildrenList);
            return objChildrenList;
        }

        public List<Component2> ListChildrenComponentsDisplayed()
        {
            List<Component2> objChildrenListDisplayed = new List<Component2>();

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
                        //True: is supr && is shown: True || is supr && is hidden: True
                        if (component.GetSuppression2() != 0 && !component.IsHidden(false))
                        {
                            objChildrenListDisplayed.Add(component);
                            // Recursively list all subcomponents
                            if (Utilities.Component2IsAssembly(component))
                            {
                                objChildrenListDisplayed.AddRange(ListSubComponentsDisplayed(component));
                            }
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
            //MessageBox.Show(string.Join(System.Environment.NewLine, objChildrenListDisplayed));
            objChildrenListDisplayed = RemoveDupes(objChildrenListDisplayed);
            return objChildrenListDisplayed;
        }

        private List<Component2> RemoveDupes(List<Component2> list)
        {
            //List<string> filePathsNoDupes = new List<string>();
            List<Component2> listNoDupes = new List<Component2>();
            List<string> filePaths = new List<string>();
            foreach (Component2 item in list)
            {
                //Create a List with all objects that contains drawing and without dupes
                if (!filePaths.Contains(item.GetPathName()))
                {
                    filePaths.Add(item.GetPathName());
                    listNoDupes.Add(item);
                }
            }
            return listNoDupes;
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
                        if (Utilities.Component2IsAssembly(subComponent))
                        {
                            ListSubComponents(subComponent);
                            list.AddRange(ListSubComponents(subComponent));
                        }
                    } 
                }
            }
            return list;
        }

        private List<Component2> ListSubComponentsDisplayed(Component2 parentComponent)
        {
            List<Component2> list = new List<Component2>();

            object[] subComponents = (object[])parentComponent.GetChildren();

            if (subComponents != null)
            {
                foreach (Component2 subComponent in subComponents)
                {
                    //Check if subcomponent is not supressed
                    if (subComponent.GetSuppression2() != 0 && (!subComponent.IsHidden(false)))
                    {
                        list.Add(subComponent);
                        if (Utilities.Component2IsAssembly(subComponent))
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
