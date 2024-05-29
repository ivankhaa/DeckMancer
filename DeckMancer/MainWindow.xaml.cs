using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using OpenTK.Wpf;
using OpenTK;
using OpenTK.Graphics;

using OpenTK.Mathematics;
using DeckMancer.Core;
using DeckMancer.WPF;
using DeckMancer.Engine;
using DeckMancer.Serialization;
using System.IO;
using System.Windows.Threading;
using DeckMancer.Project;
using DeckMancer.Build;
using System.Reflection;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System.Threading;

namespace DeckMancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        double WidthEditor => OpenTkControl.ActualWidth;
        double HeightEditor => OpenTkControl.ActualHeight;

        private DispatcherTimer focusTimer;
        private RenderEditor renderEditor;
        public MainWindow()
        {
            System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            ProjectManagerWindow projectManagerWindow = new ProjectManagerWindow();
            bool? dialogResult = projectManagerWindow.ShowDialog();
            if (dialogResult != true)
            {
                this.Close();
                return;
            }
            InitializeComponent();



            InitializeTimer();
            InitializeUI();


            var settings = new GLWpfControlSettings
            {
                MajorVersion = 4,
                MinorVersion = 6,
                GraphicsProfile = OpenTK.Windowing.Common.ContextProfile.Core


            };
            OpenTkControl.Start(settings);
            renderEditor = new RenderEditor(OpenTkControl);
            base.Closing += ClosingMainWindow;

        }
        private void InitializeUI()
        {
            ListBoxDebugOutput.DataContext = new ListBoxDebugViewModel();
            GridFileManager.DataContext = new FileGridViewModel();
            new SceneManager(ProjectManager.LoadScenes());

            GridSceneObjects.DataContext = new TreeViewScenes(SceneManager.Scenes);
            GridInspector.DataContext = new ComponentsViewModel();
        }
        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();

            var result = folderBrowserDialog.ShowDialog();

            if (result == true)
            {
                string selectedFolder = folderBrowserDialog.SelectedPath;

                InputProjectNameWindow inputProjectNameWindow = new InputProjectNameWindow();

                bool? dialogResult = inputProjectNameWindow.ShowDialog();

                if (dialogResult == true)
                {
                    string projectName = inputProjectNameWindow.ProjectName;
                    ProjectManager.CreateProject(selectedFolder, projectName);
                    InitializeUI();
                }
            }
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Deck Project Files (*.deckproject)|*.deckproject";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedFileName = openFileDialog.FileName;
                ProjectManager.LoadProject(selectedFileName);
                InitializeUI();
            }
        }
        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectManager.SaveProject();

        }
        private void ClosingMainWindow(object sender, EventArgs e)
        {
            ProjectManager.Solution.SolutionClose();
        }
        private void AddMenuTreeView_Click(object sender, RoutedEventArgs e)
        {
            var Header = (sender as MenuItem).Header;
            int i = 0;
            switch (Header)
            {
                case "GameObject":
                    var svm = (TreeViewScenes.CurrentSceneViewSelected as SceneViewModel);
                    i = svm.Scene.GameObjects.Count + 1;
                    svm.AddGameObject(new GameObject($"GameObject{i}"));
                    RenderEditorManager.AddGameObjectRender(SceneManager.CurrentScene.GameObjects.Last());
                    break;
                case "Scene":
                    var tvs = (GridSceneObjects.DataContext as TreeViewScenes);
                    i = tvs.Scenes.Count + 1;
                    var scene = new Scene($"Scene{i}");
                    tvs.AddScene(scene);
                    SceneManager.Scenes.Add(scene);
                    break;
            }
        }

        private void RenameMenuTreeView_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItemViewModel selectedItem = TreeViewSceneObject.SelectedItem as TreeViewItemViewModel;

            if (selectedItem != null)
            {

                selectedItem.IsEditing = true;
                selectedItem.IsEditing = true;
                selectedItem.IsFocused = true;
            }

        }

        private void DeleteMenuTreeView_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItemViewModel DeleteObject = SceneViewModel.CurrentGameObjectSelected;
            MessageBoxResult result;
            if (DeleteObject == null)
            {
                DeleteObject = TreeViewScenes.CurrentSceneViewSelected;

                if (DeleteObject == null)
                    return;

                result = MessageBox.Show($"Are you sure you want to delete the {(DeleteObject as SceneViewModel).Name}?", "Deletion confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    delete(DeleteObject as SceneViewModel, false);
            }
            else
            {
                result = MessageBox.Show($"Are you sure you want to delete the {(DeleteObject as GameObjectViewModel).Name}?", "Deletion confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    delete(DeleteObject as GameObjectViewModel);
            }


            void delete(TreeViewItemViewModel deleteObj, bool isGameObject = true)
            {
                if (isGameObject)
                {
                    var obj = deleteObj as GameObjectViewModel;
                    if (!(obj.GameObject is MainCamera))
                    {

                        SceneManager.CurrentScene.RemoveGameObject(obj.GameObject);
                        RenderEditorManager.RmoveGameObjectRender(obj.GameObject);
                        obj.IsSelected = false;
                        TreeViewScenes.CurrentSceneViewSelected.Children.Remove(deleteObj);

                    }
                }
                else
                {
                    var obj = deleteObj as SceneViewModel;

                    var viewScenes = (TreeViewSceneObject.DataContext as TreeViewScenes);
                    if (viewScenes.Scenes.Count > 1)
                    {
                        viewScenes.Scenes.Remove(obj);
                        SceneManager.LoadScene(viewScenes.Scenes[0].Scene);
                    }

                }
            }
        }
        private void CreateMenuFileManager_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var FileManagerView = GridFileManager.DataContext as FileGridViewModel;
            string[] folders = FileManagerView.FolderItems.ToArray();
            string url = Path.Combine(ProjectResources.ProjectFolder, Path.Combine(folders)) + "\\";
            switch (menuItem.Header)
            {
                case "Folder":
                    FileManagerView.AddNewItem(Path.Combine(url, "NewFolder"));
                    break;
                case "Script":
                    FileManagerView.AddNewItem(Path.Combine(url, "NewScript.cs"));
                    break;
            }

        }
        private void MenuFileManagerItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            switch (menuItem.Header)
            {
                case "Delete":

                    var contextMenu = (ContextMenu)menuItem.Parent;
                    var clickedItem = (Grid)contextMenu.PlacementTarget;
                    var fileItem = (FileItem)clickedItem.DataContext;

                    if (fileItem.Extension == ".cs")
                    {
                        (GridFileManager.DataContext as FileGridViewModel).RemoveFileOrFolder(fileItem);
                        ProjectManager.Solution.RemoveCsFile(Path.Combine(ProjectResources.ProjectFolder, ProjectResources.ProjectName + ".csproj"), fileItem.FullPath);
                    }
                    else
                    {
                        (GridFileManager.DataContext as FileGridViewModel).RemoveFileOrFolder(fileItem);
                    }
                    break;
            }
        }



        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            BuildGame.CancelRunGame();
            var path = Path.Combine(ProjectResources.ProjectFolder, ProjectResources.ProjectName + ".sln");

            var stat = ProjectManager.Solution.CompileProject(path);

            if (stat)
            {

                ProjectManager.SaveProject();
                string dllPath = ProjectManager.Solution.GetAssemblyPath(path);
                string gamePath = Path.Combine(ProjectResources.ProjectFolder, ProjectResources.ProjectName);
                string gameExePath = Path.Combine(gamePath, ProjectResources.ProjectName + ".exe");
                BuildGame.CreateSample(gamePath, dllPath, SceneManager.CurrentScene.Name);

                AutoResetEvent processStartedEvent = new AutoResetEvent(false);

                BuildGame.RunGameAsync(gameExePath, processStartedEvent);
                processStartedEvent.WaitOne(2000);
            }

        }
        private void RemoveComponentButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Tag)
                {
                    case nameof(Mesh):
                        RenderEditorManager.RmoveGameObjectRender(SceneManager.CurrentGameObjectSelected);
                        SceneManager.CurrentGameObjectSelected.RemoveComponent<Mesh>();
                        break;
                    case nameof(Script):
                        SceneManager.CurrentGameObjectSelected.RemoveComponent<Script>();
                        break;
                }

                (GridInspector.DataContext as ComponentsViewModel).Update();
            }
        }
        private void SetMeshButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Wavefront OBJ Files (*.obj)|*.obj";
            openFileDialog.InitialDirectory = ProjectResources.AssetsFolder;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                LoadObjToMesh(filePath);

            }
        }
        private void LoadObjToMesh(string filePath)
        {
            var mesh = SceneManager.CurrentGameObjectSelected.GetComponent<Mesh>();
            uint[] ind;
            Vector2[] texC;
            Vector3[] vert;
            WavefrontObjLoader.ImportFromObj(filePath, out vert, out ind, out texC);
            mesh.Vertices = vert;
            mesh.Indices = ind;
            mesh.UVs = texC;

            var curGm = SceneManager.CurrentGameObjectSelected;
            SceneManager.LoadScene(SceneManager.CurrentScene);
            SceneManager.CurrentGameObjectSelected = curGm;
        }
        private void Texture_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (file.Length < 2)
                {
                    if (IsImageFile(file[0]))
                    {
                        e.Effects = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void Texture_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                var components = (GridInspector.DataContext as ComponentsViewModel).Components;
                var Bitmap = ImageLoader.LoadBitmap(files[0]);
                foreach (var c in components)
                {
                    if (c is MeshViewModel mesh)
                    {
                        mesh.SetTexture(Bitmap);

                    }
                }
            }
        }
        private void Script_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (file.Length < 2)
                {
                    string extension = Path.GetExtension(file[0]);
                    if (extension == ".cs")
                    {
                        e.Effects = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }
        private void Script_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                var components = (GridInspector.DataContext as ComponentsViewModel).Components;
                foreach (var c in components)
                {
                    if (c is ScriptViewModel scriptView)
                    {
                        scriptView.Name = Path.GetFileNameWithoutExtension(files[0]);
                        ProjectManager.AddBaseTextToCs(files[0]);
                    }
                }
            }
        }
        private void Mesh_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                LoadObjToMesh(files[0]);
            }
        }
        private void Mesh_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (file.Length < 2)
                {
                    string extension = Path.GetExtension(file[0]);
                    if (extension == ".obj")
                    {
                        e.Effects = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }


        private bool IsImageFile(string filePath)
        {
            try
            {
                string extension = System.IO.Path.GetExtension(filePath);
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

                return imageExtensions.Contains(extension.ToLower());
            }
            catch
            {
                return false;
            }
        }
        private void ItemContainerComponents_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string name = "DeckMancer.Core." + (sender as ListBoxItem).Content.ToString();
            Type componentType = Type.GetType(name);
            GameObject gameObject = SceneManager.CurrentGameObjectSelected;
            gameObject.AddComponent(componentType);
            ComponentMapping.AddComponentMapping(componentType, gameObject);
            RenderEditorManager.AddGameObjectRender(gameObject);
            (GridInspector.DataContext as ComponentsViewModel).Update();
            PopupAddComponents.IsOpen = false;
        }
        private DataObject dataFileItem;

        private void ListboxItemFile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] folders = (GridFileManager.DataContext as FileGridViewModel).FolderItems.ToArray();
            string url = Path.Combine(ProjectResources.ProjectFolder, Path.Combine(folders)) + "\\";
            var listBoxItem = ((DependencyObject)sender);
            var textbox = FindVisualChild<TextBox>(listBoxItem);
            if (textbox != null)
            {
                dataFileItem = new DataObject(DataFormats.FileDrop, new string[] { url + textbox.Text });
            }
        }
        private void DebugCopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var clickedItem = (Grid)contextMenu.PlacementTarget;
            var debugItem = (DebugContent)clickedItem.DataContext;
            Clipboard.SetText(debugItem.Message);
        }


        private void ListboxItemFile_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ListboxItemFile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        bool fileManagerDoDragDrop = false;
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && dataFileItem != null)
            {
                fileManagerDoDragDrop = true;
                DragDrop.DoDragDrop(GridFileManager, dataFileItem, DragDropEffects.Move);
            }
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {

            }
        }

        private void InitializeTimer()
        {
            focusTimer = new DispatcherTimer();
            focusTimer.Interval = TimeSpan.FromSeconds(1);
            focusTimer.Tick += FocusTimer_Tick;
        }

        private void FocusTimer_Tick(object sender, EventArgs e)
        {
            TimerFocuseStart = false;
            buttonNameFilePressed = false;
            textBoxItem.Focusable = true;
            focusTimer.Stop();
            textBoxItem.Focus();
        }
        private bool buttonNameFilePressed = false;
        private bool TimerFocuseStart = false;
        private TextBox textBoxItem = null;
        private void TextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {

            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && !(obj is ListBoxItem))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is ListBoxItem listBoxItem && listBoxItem.IsSelected)
            {
                if (!TimerFocuseStart)
                {
                    focusTimer.Start();
                    TimerFocuseStart = true;
                    (sender as TextBox).Focusable = false;
                }
                else
                {
                    e.Handled = true;
                }

                if (TimerFocuseStart && buttonNameFilePressed)
                {
                    ListBoxFileItem_MouseDoubleClick(obj, e);
                    buttonNameFilePressed = false;
                    e.Handled = true;
                    return;
                }
                buttonNameFilePressed = true;

                if (sender is TextBox textBox)
                {
                    textBoxItem = textBox;
                    string text = textBox.Text;
                    int lastDotIndex = text.LastIndexOf('.');

                    if (lastDotIndex >= 0)
                    {
                        textBox.Select(0, lastDotIndex);
                    }
                    else
                    {
                        textBox.SelectAll();
                    }
                }
            }

        }
        private void ListBoxFileManager_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TimerFocuseStart = false;
            buttonNameFilePressed = false;
            focusTimer.Stop();
        }
        private void ListBoxFileItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = sender as ListBoxItem;

            if (listBoxItem != null)
            {
                FileItem item = listBoxItem.DataContext as FileItem;

                if (item != null)
                {
                    if (item.IsFolder)
                    {
                        (ListBoxFileManager.DataContext as FileGridViewModel).OpenFolder(item.Name);
                    }
                    else
                    {
                        switch (item.Extension)
                        {
                            case ".cs":
                                ProjectManager.AddBaseTextToCs(item.FullPath);
                                ProjectManager.Solution.OpenSolutionWithFocusOnCs(Path.Combine(ProjectResources.ProjectFolder, ProjectResources.ProjectName + ".sln"), item.FullPath);
                                break;

                        }
                    }
                }
            }
        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;
            if (e.ChangedButton == MouseButton.Right)
            {
                if (!(obj is ListBoxItem))
                    e.Handled = true;
            }


            while (obj != null)
            {
                if (obj is ListBoxItem)
                    break;
                if (!(obj is Run))
                    obj = VisualTreeHelper.GetParent(obj);
                else
                    break;
            }

            if (obj == null)
            {
                ListBoxDebugOutput.SelectedItem = null;
                ListBoxFileManager.SelectedItem = null;
                if (textBoxItem != null)
                    textBoxItem.Focusable = false;

            }

        }
        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dataFileItem = null;
        }

        private void ListBoxListBoxUrlFile_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                var item = listBox.SelectedItem;


                (ListBoxFileManager.DataContext as FileGridViewModel)?.DirectoryChange(listBox.Items.IndexOf(item));
                /*               var viewer = FindVisualChild<ScrollViewer>(listBox);
                               if (viewer != null)
                                    viewer.ScrollToRightEnd();*/

            }

        }
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;

                T childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        private void FileDebugTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                int selectedIndex = tabControl.SelectedIndex;

                switch (selectedIndex)
                {
                    case 0:
                        ButtonsDebug.IsEnabled = false;
                        ButtonsDebug.Visibility = Visibility.Collapsed;
                        break;

                    case 1:
                        ButtonsDebug.IsEnabled = true;
                        ButtonsDebug.Visibility = Visibility.Visible;

                        break;

                }
            }

        }
        private void ListBoxFileManager_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void ListBoxFileManager_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !fileManagerDoDragDrop)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    (ListBoxFileManager.DataContext as FileGridViewModel).AddItem(file);
                }
            }
            fileManagerDoDragDrop = false;
        }
        private void ClearDebug_Click(object sender, RoutedEventArgs e) 
        {
            Debug.Clear();
            
        }






        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
        
        }

        private void FileGrid_DragLeave(object sender, DragEventArgs e)
        {
            dataFileItem = null;
        }
    }

}
