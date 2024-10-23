using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSwAddIn.Services;

namespace TestSwAddIn.Models
{

    public class Settings
    {
        public const string settingPath = @"../../Settings/settings.json";
        public string SheetTemplatePath { get; set; }
        public string DxfPath { get; set; }

        // Singleton instance
        private static Settings _instance;

        // Lock object for thread safety (in case of multi-threading)
        private static readonly object _lock = new object();

        // Private constructor to prevent external instantiation
        //private Settings() { }

        // Public method to access the singleton instance
        public static Settings Instance
        {
            get
            {
                lock (_lock) // Ensure thread safety
                {
                    if (_instance == null) // Double-check locking
                    {
                        _instance = LoadSettings(); // Load the settings once
                    }
                }
                return _instance;
            }
        }

        // Method to load settings from JSON
        private static Settings LoadSettings()
        {
            JsonImporter ji = new JsonImporter();
            if (File.Exists(settingPath))
            {
                return (Settings)ji.LoadJson(settingPath);
            }
            else
            {
                // Return default settings if file doesn't exist
                return new Settings();
            }
        }

        // Method to save settings to JSON
        public void SaveSettings(Settings newSettings)
        {
            JsonExporter je = new JsonExporter();
            je.ExportJson(settingPath, newSettings);
        }
    }
}
