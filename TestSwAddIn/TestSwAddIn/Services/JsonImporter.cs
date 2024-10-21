using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TestSwAddIn.Forms;
using TestSwAddIn.Models;

// Recieve the file path and the object return the object deserialized
namespace TestSwAddIn.Services
{
    class JsonImporter
    {
        public object LoadJson(string pathToJson, object obj)
        {
            if (File.Exists(pathToJson))
            {
                string jsonString = File.ReadAllText(pathToJson);
                obj = JsonConvert.DeserializeObject<Settings>(jsonString);
                return obj;
            }
            else
            {
                MessageBox.Show($"File {pathToJson} could'n be found");
                return null;
            }
        }
    }
}
