using System;
using System.ComponentModel;
using System.Windows.Forms;
using TestSwAddIn.Models;
using System.IO;
using Newtonsoft.Json;
using System.Security.Policy;
using TestSwAddIn.Services;

namespace TestSwAddIn.Forms
{

    public partial class ConfigurationForm : Form
    {
        public const string settingPath = @"../../Settings/settings.json";
        public string dxfPath = "";
        public string sheetTemplatePath = "";
        Settings settings = new Settings();
        JsonImporter jsonImporter = new JsonImporter();


        public ConfigurationForm()
        {
            InitializeComponent();
            settings = (Settings)jsonImporter.LoadJsonSettings(settingPath);
            TxtTemplatePath.Text = settings.SheetTemplatePath;
            txtDxfPath.Text = settings.DxfPath;
        }

        /*public bool LoadJsonSettings()
        {
            if (File.Exists(settingPath))
            {
                string jsonString = File.ReadAllText(settingPath);
                settings = JsonConvert.DeserializeObject<Settings>(jsonString);
            } else
            {
                MessageBox.Show($"File {settingPath} could'n be found");
            }
            return true;
        }*/

        private void BtnSearchTemplateFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to only show .drwdot files
            openFileDialog.Filter = "SolidWorks Drawing Template (*.drwdot)|*.drwdot";

            // Set the title of the dialog
            openFileDialog.Title = "Select a SolidWorks Drawing Template";

            // Show the dialog and check if a file was selected
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Display the selected file path in the TextBox
                TxtTemplatePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnSearchDxfFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            // Show the dialog and get the result
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                txtDxfPath.Text = folderDialog.SelectedPath;
            }
            else
            {
                Console.WriteLine("No folder selected.");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            sheetTemplatePath = TxtTemplatePath.Text;
            dxfPath = txtDxfPath.Text;
            settings.SheetTemplatePath = sheetTemplatePath;
            settings.DxfPath = dxfPath;

            try
            {
                string jsonString = JsonConvert.SerializeObject(settings);
                File.WriteAllText(settingPath, jsonString);
                MessageBox.Show("Changes saved sucessifully");
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Changes were not applied");
            this.Close();
        }
    }
}
