using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSwAddIn.Forms
{
    public partial class SelectChildren : Form
    {
        public SelectChildren(List<object> lstChildren)
        {
            InitializeComponent();
            //List<string> lista = new List<string>();
            //children.
            //string[] str = { "1", "2", "3", "4", "5" };
            //clbChildren.Items.AddRange(children);
            foreach (Component2 item in lstChildren)
            {
                clbChildren.Items.Add(item.Name2);
                clbChildren.Items.Add(item.GetPathName());
            }
            clbChildren.Update();
            clbChildren.CheckOnClick = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
