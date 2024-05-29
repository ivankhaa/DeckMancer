using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using DeckMancer.Core;
using DeckMancer.Project;
namespace DeckMancer.WPF
{
    public class FileGridViewModel : INotifyPropertyChanged
    {
        private FileManager fileManager;
        private ObservableCollection<FileItem> _fileItems;
        private ObservableCollection<string> _folderItems;
        private int _lastIndex;
        private bool _isDragging = false;


        public ObservableCollection<FileItem> FileItems
        {
            get { return _fileItems; }
            set
            {
                if (_fileItems != value)
                {
                    _fileItems = value;
                    OnPropertyChanged(nameof(FileItems));
                }
            }
        }
        public ObservableCollection<string> FolderItems
        {
            get { return _folderItems; }
            set
            {
                if (_folderItems != value)
                {
                    _folderItems = value;
                    OnPropertyChanged(nameof(FolderItems));
                }
            }
        }

        public int LastIndex
        {
            get { return _lastIndex; }
            set
            {
                if (_lastIndex != value)
                {
                    _lastIndex = value;
                    OnPropertyChanged(nameof(LastIndex));
                }
            }
        }

        public bool IsDragging
        {
            get { return _isDragging; }
            set
            {
                if (_isDragging != value)
                {
                    _isDragging = value;
                   
                    OnPropertyChanged(nameof(IsDragging));
                }
            }
        }

        public FileGridViewModel()
        {
            fileManager = new FileManager(ProjectResources.AssetsFolder);
            FileItems = new ObservableCollection<FileItem>(fileManager.GetDirectoryContents());

            string currentDirectory = fileManager.GetCurrentDirectory();

            string root = ProjectResources.ProjectFolder;

            string relativePath = Path.GetRelativePath(root, currentDirectory);

            string[] folderItems = relativePath.Split(Path.DirectorySeparatorChar);

            _folderItems = new ObservableCollection<string>(folderItems);
            _lastIndex = FolderItems.Count - 1;
        }
        public void AddItem(string path) 
        {
            fileManager.AddFileOrFolder(fileManager.GetCurrentDirectory(), path);
            FileItems = new ObservableCollection<FileItem>(fileManager.GetDirectoryContents());
        }
        public void OpenFolder(string NameFolder) 
        {
            fileManager.SetCurrentDirectory(Path.Combine(fileManager.GetCurrentDirectory(), NameFolder));
            FileItems = new ObservableCollection<FileItem>(fileManager.GetDirectoryContents());
            FolderItems.Add(NameFolder);
            LastIndex = FolderItems.Count - 1;
        }
        public void DirectoryChange(int indexFolder) 
        {
            string currentDirectory = fileManager.GetCurrentDirectory();
            string root = ProjectResources.ProjectFolder;
            string relativePath = Path.GetRelativePath(root, currentDirectory);
            string[] folderItems = relativePath.Split(Path.DirectorySeparatorChar);
            string[] newFolderItems = new string[indexFolder+1];
            Array.Copy(folderItems, 0, newFolderItems, 0, indexFolder+1);
            string targetFolderPath = Path.Combine(root, Path.Combine(newFolderItems));

            if (!folderItems.SequenceEqual(newFolderItems) || newFolderItems.Length != folderItems.Length)
            {
                fileManager.SetCurrentDirectory(targetFolderPath);
                FileItems = new ObservableCollection<FileItem>(fileManager.GetDirectoryContents());
                FolderItems = new ObservableCollection<string>(newFolderItems);
                LastIndex = FolderItems.Count - 1;
            }
        }
        public string AddNewItem(string path) 
        {
            var Error = fileManager.CreateFileOrFolder(path);
            if (Error.Code > 0)
            {
                Debug.WriteLine($"Code {Error.Code} | {Error.Message}", DebugIcon.Error);
                return null;
            }
            FileItems = new ObservableCollection<FileItem>(fileManager.GetDirectoryContents());
            return Error.Message;

        }
        public void RemoveFileOrFolder(FileItem fileItem) 
        {
            var Error = fileManager.DeleteFileOrFolder(fileItem.FullPath);
            if (Error.Code != 0) 
            {
                Debug.WriteLine($"Code {Error.Code} | {Error.Message}", DebugIcon.Error);
                return;
            }
            FileItems.Remove(fileItem);

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
