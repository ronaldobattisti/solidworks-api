using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using TestSwAddIn.Models;

// Recieve the file path and the object return the object deserialized
namespace TestSwAddIn.Services
{
    class JsonImporter
    {
        public object LoadJson(string pathToJson)
        {
            string fullPath = Path.GetFullPath(pathToJson);
            if (File.Exists(fullPath))
            {
                string jsonString = File.ReadAllText(pathToJson);
                var obj = JsonConvert.DeserializeObject<Settings>(jsonString);
                return obj;
            }
            else
            {
                MessageBox.Show($"File {fullPath} could'n be found");
                return null;
            }
        }
    }
}
