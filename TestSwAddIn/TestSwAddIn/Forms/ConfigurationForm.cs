using System;
using System.ComponentModel;
using System.Windows.Forms;
using TestSwAddIn.Models;
using System.IO;
using System.Text.Json;

namespace TestSwAddIn.Forms
{

    public partial class ConfigurationForm : Form
    {
        public const string settingPath = @"../../Settings/settings.json";
        Settings settings = new Settings();

        public ConfigurationForm()
        {
            InitializeComponent();
            LoadJsonSettings();
            TxtTemplatePath.Text = settings.SheetTemplatePath;
        }

        public bool LoadJsonSettings()
        {
            if (File.Exists(settingPath))
            {
                string jsonString = File.ReadAllText(settingPath);
                settings = JsonSerializer.Deserialize<Settings>(jsonString);
            } else
            {
                MessageBox.Show($"File {settingPath} could'n be found");

            }
            return true;
        }

        private void BtnSearchFile_Click(object sender, EventArgs e)
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            string sheetTemplatePath = TxtTemplatePath.ToString();
            string dxfPath = txtDxfPath.ToString();
            settings.SheetTemplatePath = sheetTemplatePath;
            settings.DxfPath = dxfPath;

            string jsonString = JsonSerializer.Serialize(settings);
            try
            {
                File.WriteAllText(settingPath, jsonString);
                MessageBox.Show("Changes saved sucessifully");
                this.Close();

            } catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            

        }

        private void BtnSearchDxfFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            // Show the dialog and get the result
            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                // Display the selected folder path
                Console.WriteLine("Selected folder: " + folderDialog.SelectedPath);
            }
            else
            {
                Console.WriteLine("No folder selected.");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Changes were not applied");
            this.Close();
        }
    }
}
