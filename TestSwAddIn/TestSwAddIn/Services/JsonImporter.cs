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

// Recieve the file path and return the object deserialized
namespace TestSwAddIn.Services
{
    class JsonImporter
    {

        string settingPath = ConfigurationForm.settingPath;
        Settings settings = new Settings();


        public object LoadJsonSettings(string pathToJson)
        {
            if (File.Exists(settingPath))
            {
                string jsonString = File.ReadAllText(settingPath);
                settings = JsonConvert.DeserializeObject<Settings>(jsonString);
                return settings;
            }
            else
            {
                MessageBox.Show($"File {settingPath} could'n be found");
                return null;
            }
        }
    }
}
