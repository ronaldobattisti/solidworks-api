using System.Collections.Generic;
using System.Windows;

namespace PaintModelUtilities{
    public static class Utilities{
        public static double[] getColor(string codColor, double[] materialProps){

            MessageBox.Show(codColor);

            Dictionary<string, double[]> colors = new Dictionary<string, double[]>{
                {"84351", new double[] {65025, 65025, 65025 } },//White powder
                {"57459", new double[] {21675, 21675, 21675 } },//Grey powder
                {"58628", new double[] {21675, 21675, 21675 } },//Grey liquid
                {"98606", new double[] {65025, 65025, 0 } },//Yellow powder
                {"2042", new double[] {18105, 26265, 65025 } },//Blue powder
                {"39236", new double[] {21675, 21675, 21675 } },//Blue liquid
                {"2071", new double[] {14025, 14025, 14025 } }//Black powder
            };

            if (codColor != ""){
                materialProps[0] = colors[codColor][0];
                materialProps[1] = colors[codColor][1];
                materialProps[2] = colors[codColor][2];
                return materialProps;
            }

            //MessageBox.Show("Color geted: " + codColor + " Color painted: " + colors[codColor]);

            //return the entire property changing just the collor
            return materialProps;
        }

        public static List<string> removeDuplicated(List<string> list)
        {
            List<string> listFiltered = new List<string>();
            foreach (string item in list)
            {
                if (listFiltered.Contains(item) == false)
                {
                    listFiltered.Add(item);
                }
            }
            return listFiltered;
        }
    }
}