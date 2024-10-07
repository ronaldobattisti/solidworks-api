using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.Windows;
using System;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;

namespace TestSwAddIn.Utils
{
    class ChangeItemColor
    {
        public double[] GetRgbColor(ModelDoc2 swModel)
        {
            CustomPropertyManager cusPropMgr = swModel.Extension.CustomPropertyManager[""];
            String paintCode = "";
            string properties = "";
            string propertyValue = "";
            string propertyResolvedValue = "";
            bool wasResolved = false;
            string[] propertyNames = (string[])cusPropMgr.GetNames();
            double[] rgbColor = new double[] { };
            double[] errorDouble = new double[] {-1};
            if (propertyNames != null)
            {
                //For each property write its value
                properties = ("File: " + System.IO.Path.GetFileNameWithoutExtension(swModel.GetPathName()) + "\n");
                foreach (string propertyName in propertyNames)
                {
                    cusPropMgr.Get5(propertyName, false, out propertyValue, out propertyResolvedValue, out wasResolved);
                    properties += ("Property: " + propertyName + " = " + propertyValue + "\n");
                    if (propertyName.ToUpper() == "TRATAMENTO_SUPERFICIAL")
                    {
                        if (propertyResolvedValue != "")
                        {
                            paintCode = propertyResolvedValue.Split('-')[0];
                            rgbColor = GetColor(paintCode);
                            return rgbColor;
                        }
                        else
                        {
                            return errorDouble;
                        }
                    }
                }
                return errorDouble;
            }
            else
            {
                MessageBox.Show("Properties are null!");
                return errorDouble;
            }
        }

        public static double[] GetColor(string codColor)
        {
            /// Summary:
            ///     Writes the specified string value, followed by the current line terminator, to
            ///     the standard output stream.
            ///
            /// Parameters:
            ///   value:
            ///     The value to write.
            ///
            /// Exceptions:
            ///   T:System.IO.IOException:
            ///     An I/O error occurred.

            Dictionary<string, double[]> colors = new Dictionary<string, double[]>{
                {"84351", new double[] {1, 1, 1 } },    //White powder
                {"57459", new double[] {0.333, 0.333, 0.333 } },    //Grey powder
                {"58628", new double[] { 0.333, 0.333, 0.333 } },    //Grey liquid
                {"98606", new double[] {1, 1, 0 } },        //Yellow powder
                {"2042", new double[] {0.278, 0.404, 1 } },     //Blue powder
                {"39236", new double[] { 0.333, 0.333, 0.333 } },    //Blue liquid
                {"2071", new double[] {0.216, 0.216, 0.216 } }      //Black powder
            };

            //MessageBox.Show("Color geted: " + codColor + " Color painted: " + colors[codColor]);

            //return the entire property changing just the collor
            
            return colors[codColor];
        }
    }
}
