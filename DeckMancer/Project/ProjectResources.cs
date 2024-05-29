using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Project
{
    public class ProjectResources
    {
        public static string ProjectName { get; private set; }
        public static string ProjectFolder { get; private set; }
        public static string ProjectFile { get; private set; }
        public static string AssetsFolder { get; private set; }

        protected static void Initialize(string projectName, string projectsFolder, string projectFile, string assetsFolder)
        {
            ProjectName = projectName;
            ProjectFolder = projectsFolder;
            ProjectFile = projectFile;
            AssetsFolder = assetsFolder;
        }
    }
}
