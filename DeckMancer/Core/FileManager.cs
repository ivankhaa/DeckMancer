using System;
using System.Collections.Generic;
using System.IO;

namespace DeckMancer.Core
{
    public struct FileManagerError
    {
        public int Code { get; }
        public string Message { get; }

        public FileManagerError(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
    public class FileManager
    {
        private string currentDirectory;

        public FileManager(string initialDirectory)
        {
            SetCurrentDirectory(initialDirectory);
        }

        public void SetCurrentDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    currentDirectory = path;
                }
                else
                {
                    throw new DirectoryNotFoundException($"Directory '{path}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetCurrentDirectory()
        {
            return currentDirectory;
        }

        public List<FileItem> GetDirectoryContents()
        {
            try
            {
                if (Directory.Exists(currentDirectory))
                {
                    List<FileItem> items = new List<FileItem>();

                    string[] files = Directory.GetFiles(currentDirectory);
                    string[] directories = Directory.GetDirectories(currentDirectory);

                    foreach (var dir in directories)
                    {
                        items.Add(new FileItem(true, Path.GetFileName(dir), null, 0, dir));
                    }

                    foreach (var file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        items.Add(new FileItem(false, Path.GetFileNameWithoutExtension(file), fileInfo.Extension, fileInfo.Length, file));
                    }

                    return items;
                }
                else
                {
                    throw new DirectoryNotFoundException($"Directory '{currentDirectory}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void AddFileOrFolder(string targetDirectory, string path)
        {
            if (File.Exists(path))
            {
                AddFile(targetDirectory, path);
            }
            else if (Directory.Exists(path))
            {
                AddFolder(targetDirectory, path);
            }
        }

        private void AddFile(string targetDirectory, string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine(targetDirectory, fileName);

            if (!File.Exists(destinationPath))
            {
                File.Copy(filePath, destinationPath);
            }
            else
            {
                // Обработка ситуации, когда файл с таким именем уже существует
            }
        }

        private void AddFolder(string targetDirectory, string folderPath)
        {
            string folderName = Path.GetFileName(folderPath);
            string destinationPath = Path.Combine(targetDirectory, folderName);

            if (!Directory.Exists(destinationPath))
            {
                CopyDirectory(folderPath, destinationPath, true);
            }
            else
            {
                // Обработка ситуации, когда папка с таким именем уже существует
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            var dir = new DirectoryInfo(sourceDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);

                AddFile(destinationDir, file.FullName);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
        public FileManagerError CreateFileOrFolder(string path)
        {
            try
            {
                if (Path.HasExtension(path))
                {
                    if (File.Exists(path))
                    {
                        string directory = Path.GetDirectoryName(path);
                        string extension = Path.GetExtension(path);

                        string fileName = Path.GetFileNameWithoutExtension(path);
                        string newFileName = Path.Combine(directory, fileName + "1" + extension);
                        int count = 2;

                        while (File.Exists(newFileName))
                        {
                            newFileName = Path.Combine(directory, fileName + $"{count}" + extension);
                            count++;
                        }

                        File.Create(newFileName).Close();
                        return new FileManagerError(-1, newFileName);
                    }
                    else
                    {
                        File.Create(path).Close();
                    }
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        string newDirectoryName = path + " (1)";
                        int count = 2;

                        while (Directory.Exists(newDirectoryName))
                        {
                            newDirectoryName = path + $" ({count})";
                            count++;
                        }

                        Directory.CreateDirectory(newDirectoryName);
                        return new FileManagerError(-1, newDirectoryName);
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                return new FileManagerError(-1, path);
            }
            catch (Exception ex)
            {
                return new FileManagerError(4, $"Error creating file or folder '{path}': {ex.Message}");
            }
        }
        public FileManagerError DeleteFileOrFolder(string path)
        {
            if (File.Exists(path))
            {
                return DeleteFile(path);
            }
            else if (Directory.Exists(path))
            {
                return DeleteFolder(path);
            }
            return new FileManagerError(56, $"Delete Error: non - existent path");

        }

        private FileManagerError DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return new FileManagerError(0, null);
            }
            catch (Exception ex)
            {
                return new FileManagerError(5, $"Error deleting file '{filePath}': {ex.Message}");
            }
        }

        private FileManagerError DeleteFolder(string folderPath)
        {
            try
            {
                Directory.Delete(folderPath, true);
                return new FileManagerError(0, null);
            }
            catch (Exception ex)
            {
                return new FileManagerError(6, $"Error deleting folder '{folderPath}': {ex.Message}");
            }
        }
    }
}
