using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TestSwAddIn.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using System.Collections.Generic;
using Utils;
using TestSwAddIn.Utils;
using TestSwAddIn.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SampleAddIn
{
    [ComVisible(true)]
    [Guid("1EE37F60-45F7-4FFA-93E2-5ACCC371530F")]
    [Title("RAutomation")]

    public class TestSampleAddIn : SwAddInEx
    {
        [Title("Sample AddIn")]
        public enum Commands_e
        {
            [Title("Change color")]
            [Description("Change color for every selected item")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ChangeColor,
            
            [Title("Test!")]
            [Description("Test")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            Test
        }

        public override void OnConnect()
        {
            var cmdGrp = this.CommandManager.AddCommandGroup<Commands_e>();
            cmdGrp.CommandClick += CmGrp_CommandClick;
        }

        private void CmGrp_CommandClick(Commands_e spec)
        {
            ListComponents lc = new ListComponents();
            ChangeItemColor cit = new ChangeItemColor();

            switch (spec)
            {
                case Commands_e.ChangeColor:
                    List<Component2> children = new List<Component2>();
                    List<Component2> childrenShown = new List<Component2>();
                    children = lc.ListChildrenComponents();
                    childrenShown = lc.ListChildrenComponentsDisplayed();
                    SelectChildren sc = new SelectChildren(children, childrenShown);
                    sc.Show();
                    break;

                case Commands_e.Test:
                    // Connect to the running SolidWorks instance
                    SldWorks swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
                    ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

                    // Check if an assembly is active
                    /*if (swModel == null || swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        Console.WriteLine("No assembly document is open.");
                        return;
                    }*/

                    // RGB color for red (values between 0.0 and 1.0)
                    double[] redColor = { 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }; // Red color (R, G, B)

                    // Apply color to the top-level assembly
                    IModelDocExtension modelExtension = swModel.Extension;
                    modelExtension.SetMaterialPropertyValues(redColor, (int)swInConfigurationOpts_e.swAllConfiguration, null);
                    /*
                    if (status)
                    {
                        Console.WriteLine("Top-level assembly has been set to red.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to set color for the top-level assembly.");
                    }*/
                    break;
            }   
        }
        SldWorks swApp;
    }
}
