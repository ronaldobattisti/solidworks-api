using System;
using System.Windows.Forms;
using TestSwAddIn.Models;
using TestSwAddIn.Services;

namespace TestSwAddIn.Forms
{

    public partial class ConfigurationForm : Form
    {
        public string dxfPath = "";
        public string sheetTemplatePath = "";
        string settingPath = Settings.settingPath;
        JsonImporter jsonImporter = new JsonImporter();
        JsonExporter jsonExporter = new JsonExporter();
        object settings = Settings.Instance;

        public ConfigurationForm()
        {
            InitializeComponent();
        }

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
            Settings settingsObj = new Settings();
            sheetTemplatePath = TxtTemplatePath.Text;
            dxfPath = txtDxfPath.Text;
            settingsObj.SheetTemplatePath = sheetTemplatePath;
            settingsObj.DxfPath = dxfPath;

            settingsObj.SaveSettings(settingsObj);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Changes were not applied");
            this.Close();
        }
    }
}
