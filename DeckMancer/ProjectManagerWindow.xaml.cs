using DeckMancer.Project;
using System;
using System.Windows;
using System.Windows.Forms;

namespace DeckMancer
{
    /// <summary>
    /// Логика взаимодействия для ProjectManager.xaml
    /// </summary>
    public partial class ProjectManagerWindow : Window
    {
        public ProjectManagerWindow()
        {
            InitializeComponent();
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
             
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFolder = folderBrowserDialog.SelectedPath;

                InputProjectNameWindow inputProjectNameWindow = new InputProjectNameWindow();

                bool? dialogResult = inputProjectNameWindow.ShowDialog();

                if (dialogResult == true)
                {
                    string projectName = inputProjectNameWindow.ProjectName;
                    ProjectManager.CreateProject(selectedFolder, projectName);
                    DialogResult = true;
                }
            }
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Deck Project Files (*.deckproject)|*.deckproject";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                ProjectManager.LoadProject(selectedFileName);
                DialogResult = true;
            }
        }
    }
}
