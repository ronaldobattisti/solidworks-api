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
    [Guid("591AF4D3-4109-4828-883B-717E9C672F22")]
    [Title("BSW")]

    public class TestSampleAddIn : SwAddInEx
    {
        [Title("BSW")]
        public enum Commands_e
        {
            [Title("Change color")]
            [Description("Change color for every selected item")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ChangeColor,
            
            [Title("Create 2D")]
            [Description("Create 2D")]
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
                    Create_2D c2d = new Create_2D();
                    c2d.Create2D();
                    break;
            }
        }
        SldWorks swApp;
    }
}
