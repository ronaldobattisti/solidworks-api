using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TestSwAddIn.Models;

// Recieve the object and the Json path and return a boolstatus
namespace TestSwAddIn.Services
{
    class JsonExporter
    {
        public bool ExportJson(string pathToJson, object obj)
        {
            if (File.Exists(pathToJson))
            {

                string jsonString = JsonConvert.SerializeObject(obj);
                File.WriteAllText(pathToJson, jsonString);
                return true;
            }
            else
            {
                MessageBox.Show($"File {pathToJson} could'n be found");
                return false;
            }
        }
    }
}
