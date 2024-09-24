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

namespace SampleAddIn
{
    [ComVisible(true)]
    [Guid("1EE37F60-45F7-4FFA-93E2-5ACCC371530F")]
    [Title("Sample AddIn")]

    public class TestSampleAddIn : SwAddInEx
    {
        [Title("Sample AddIn")]
        public enum Commands_e
        {
            [Title("Change color")]
            [Description("Change color for every selected item")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ChangeColor,
            /*[Title("Change color!")]
            [Description("Change color")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            ChangeColor*/
        }

        public override void OnConnect()
        {
            var cmdGrp = this.CommandManager.AddCommandGroup<Commands_e>();
            cmdGrp.CommandClick += CmGrp_CommandClick;
        }

        private void CmGrp_CommandClick(Commands_e spec)
        {
            ListComponents lc = new ListComponents();
            ChangeItemCollor cit = new ChangeItemCollor();

            switch (spec)
            {
                case Commands_e.ChangeColor:
                    List<Component2> children = new List<Component2>();
                    List<Component2> childrenShown = new List<Component2>();
                    children = lc.ListChildrenComponents();
                    childrenShown = lc.ListChildrenComponentsDisplayed();
                    SelectChildren sc = new SelectChildren(children, childrenShown);
                    sc.ShowDialog();
                    break;

                /*case Commands_e.ChangeColor:
                    cit.ChangeCollor();
                    break;*/
            }
        }
    }
}
