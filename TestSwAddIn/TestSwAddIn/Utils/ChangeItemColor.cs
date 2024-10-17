using SolidWorks.Interop.sldworks;
using System.Windows;
using System.Collections.Generic;

namespace TestSwAddIn.Utils
{
    class ChangeItemColor
    {
        public double[] GetRgbColor(ModelDoc2 swModel)
        {
            CustomPropertyManager cusPropMgr = swModel.Extension.CustomPropertyManager[""];
            string paintCode = "";
            string propName = "TRATAMENTO_SUPERFICIAL";
            string[] propertyNames = (string[])cusPropMgr.GetNames();
            double[] rgbColor = new double[] { };
            double[] errorDouble = new double[] {-1};
            if (propertyNames != null)
            {
                //If the item is configured, paint it grey
                if (IsConf(cusPropMgr) && GetProperty(cusPropMgr, "COR SECUNDARIA").ToUpper() == "COR SECUNDARIA")
                {
                    return GetColor("57459");
                } else
                {
                    paintCode = GetProperty(cusPropMgr, propName).Split('-')[0];
                    rgbColor = GetColor(paintCode);
                    if (rgbColor.Length > 1)
                    {
                        return rgbColor;
                    }
                    else
                    {
                        return errorDouble;
                    }
                }
            }
            else
            {
                MessageBox.Show("Properties are null!");
                return errorDouble;
            }
        }

        private bool IsConf(CustomPropertyManager cusPropMgr)
        {
            string[] propertyNames = (string[])cusPropMgr.GetNames();
            string propertyValue = "";
            string propertyResolvedValue = "";
            bool wasResolved = false;

            foreach (string propertyName in propertyNames)
            {
                cusPropMgr.Get5(propertyName, false, out propertyValue, out propertyResolvedValue, out wasResolved);
                string pName = propertyName;
                string pValue = propertyResolvedValue;
                if (propertyName.ToUpper() == "CONFIGURADO" && propertyResolvedValue != "" && propertyResolvedValue.ToUpper() == "SIM")
                {
                    return true;
                }
            }
            return false;
        }

        private string GetProperty(CustomPropertyManager cusPropMgr, string propName)
        {
            string[] propertyNames = (string[])cusPropMgr.GetNames();
            string propertyValue = "";
            string propertyResolvedValue = "";
            bool wasResolved = false;
            string propValue = "";

            //For each property write its value
            foreach (string propertyName in propertyNames)
            {
                cusPropMgr.Get5(propertyName, false, out propertyValue, out propertyResolvedValue, out wasResolved);
                if (propertyName.ToUpper() == propName && propertyResolvedValue != "")
                {
                    propValue = propertyResolvedValue;
                }
            }
            return propValue;
        }

        private double[] GetColor(string codColor)
        {
            double[] retColor = { };
            double[] retError = { 0 };

            Dictionary<string, double[]> colors = new Dictionary<string, double[]>{
                {"84351", new double[] {1, 1, 1 } },    //White powder
                {"57459", new double[] {0.333, 0.333, 0.333 } },    //Grey powder
                {"58628", new double[] { 0.333, 0.333, 0.333 } },    //Grey liquid
                {"98606", new double[] {1, 1, 0 } },        //Yellow powder
                {"2042", new double[] {0.278, 0.404, 1 } },     //Blue powder
                {"39236", new double[] { 0.333, 0.333, 0.333 } },    //Blue liquid
                {"2071", new double[] {0.216, 0.216, 0.216 } }      //Black powder
            };

            if (colors.ContainsKey(codColor))
            {
                return colors[codColor];
            }
            else
            {
                return retError;
            }
        }
    }
}
