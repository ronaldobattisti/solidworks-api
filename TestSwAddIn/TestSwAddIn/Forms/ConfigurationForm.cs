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
        public const string settingPath = @"Settings/settings.json";

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnSearchFile_Click(object sender, EventArgs e)
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
                txtTemplatePath.Text = openFileDialog.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            string sheetTemplatePath = txtTemplatePath.ToString();
            string dxfPath = txtDxfPath.ToString();
            settings.sheetTemplatePath = sheetTemplatePath;
            settings.dxfPath = dxfPath;

            string jsonString = JsonSerializer.Serialize(settings);
            File.WriteAllText(settingPath, jsonString);
        }

        private void btnSearchDxfFolder_Click(object sender, EventArgs e)
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
    }
}
