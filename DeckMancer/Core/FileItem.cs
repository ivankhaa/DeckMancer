using System.IO;

namespace DeckMancer.Core
{
    public class FileItem
    {
        public bool IsFolder { get; }
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public long Size { get; }
        public string FullPath { get; private set; }
        public byte[] ImageBuffer { get; }
        public string GetFullName
        {
            get { return Name + Extension; }
            set 
            {
                if (value != Name + Extension) 
                {
                    int lastDotIndex = value.LastIndexOf('.');
                    if (lastDotIndex != -1)
                    {
                        Name = value.Substring(0, lastDotIndex);
                        Extension = value.Substring(lastDotIndex);
                    }
                    else if (IsFolder)
                    {
                        Name = value;
                    }
                    else
                    {
                        Extension = null;
                        Name = value;
                    }
                    string newPath = Path.Combine(Path.GetDirectoryName(FullPath), GetFullName);
                    if (IsFolder)
                        Directory.Move(FullPath, newPath);
                    else
                        File.Move(FullPath, newPath);
                    FullPath = newPath;
                }
            }
        }

        public FileItem(bool isFolder, string name, string extension, long size, string fullPath)
        {
            IsFolder = isFolder;
            Name = name;
            Extension = extension;
            Size = size;
            FullPath = fullPath;
            ImageBuffer = GetImage(fullPath, isFolder, extension);
        }
        private byte[] GetImage(string path, bool isFolder, string extension) 
        {
            byte[] buffer;

            if (isFolder)
            {
                buffer = ImageLoader.LoadImage(System.IO.Path.GetFullPath(@"Resource\Icon\Folder.png"));
            }
            else
            {
                switch (extension)
                {
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                        buffer = ImageLoader.LoadImage(path);
                        break;
                    case ".cs":
                        buffer = ImageLoader.LoadImage(System.IO.Path.GetFullPath(@"Resource\Icon\csFile.png"));
                        break;
                    default:
                        buffer = ImageLoader.LoadImage(System.IO.Path.GetFullPath(@"Resource\Icon\File.png"));
                        break;
                }
            }  
            return buffer;
        }
    }
}
