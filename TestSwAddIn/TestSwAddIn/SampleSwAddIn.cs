using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TestSwAddIn.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;

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
            [Title("Hello, world!")]
            [Description("Show Hello, world!")]
            [Icon(typeof(Resources), nameof(Resources.Imagem1))]
            HelloWorld
        }

        public override void OnConnect()
        {
            var cmdGrp = this.CommandManager.AddCommandGroup<Commands_e>();
            cmdGrp.CommandClick += CmGrp_CommandClick;
        }

        private void CmGrp_CommandClick(Commands_e spec)
        {
            switch (spec)
            {
                case Commands_e.HelloWorld:
                    Application.ShowMessageBox("Hello, world!");
                    break;
            }
        }
    }
}
