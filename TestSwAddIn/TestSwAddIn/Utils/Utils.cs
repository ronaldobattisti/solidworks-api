using System.Collections.Generic;

namespace PaintModelUtilities{
    public static class Utilities{
        public static double[] getColor(string codColor, double[] materialProps){

            Dictionary<string, double[]> colors = new Dictionary<string, double[]>{
                {"84351", new double[] {65025, 65025, 65025 } },//White powder
                {"57459", new double[] {21675, 21675, 21675 } },//Grey powder
                {"98606", new double[] {65025, 65025, 0 } },//Yellow powder
                {"2042", new double[] {18105, 26265, 65025 } },//Blue powder
                {"2071", new double[] {14025, 14025, 14025 } }//Black powder
            };

            if (codColor != ""){
                materialProps[0] = colors[codColor][0];
                materialProps[1] = colors[codColor][1];
                materialProps[2] = colors[codColor][2];
                return materialProps;
            }
            //Console.WriteLine($"Color getted: {codColor} = Color painted: {colors[codColor][0]}, {colors[codColor][1]}, {colors[codColor][2]}");
            //return the entire property changing just the collor
            return materialProps;
        }
    }
}