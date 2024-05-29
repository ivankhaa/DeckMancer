using DeckMancer.Core;
using DeckMancer.Build;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DeckMancer.Serialization;

namespace DeckMancer.Project
{
    public class ProjectManager : ProjectResources
    {
        public static readonly SolutionManager Solution = new SolutionManager();
        public static void CreateProject(string projectsFolder, string projectName)
        {
            string projectFolderPath = Path.Combine(projectsFolder, projectName);
            Directory.CreateDirectory(projectFolderPath);

            string projectFilePath = Path.Combine(projectFolderPath, projectName + ".deckproject");
            using (File.Create(projectFilePath)) { }
            string assetsFolderPath = Path.Combine(projectFolderPath, "Assets");
            Directory.CreateDirectory(assetsFolderPath);

            using (StreamWriter writer = new StreamWriter(projectFilePath))
            {
                writer.WriteLine("ProjectName=" + projectName);
                writer.WriteLine("ProjectFolder=" + projectFolderPath);
                writer.WriteLine("AssetsFolder=" + assetsFolderPath);
            }
            ProjectResources.Initialize(projectName, projectFolderPath, projectFilePath, assetsFolderPath);
            Solution.CreateSolution(projectFolderPath, projectName);
            Solution.SolutionOpen(Path.Combine(projectFolderPath, projectName+".sln"));
        }
        public static void LoadProject(string projectFilePath)
        {
            try
            {
                if (!File.Exists(projectFilePath))
                {
                    Debug.WriteLine("Файл .deckproject не найден.", DebugIcon.Warning);
                    return;
                }

                using (StreamReader reader = new StreamReader(projectFilePath))
                {
                    string projectName = null;
                    string projectFolderPath = null;
                    string assetsFolderPath = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0];
                            string value = parts[1];

                            switch (key)
                            {
                                case "ProjectName":
                                    projectName = value;
                                    break;
                                case "ProjectFolder":
                                    projectFolderPath = value;
                                    break;
                                case "AssetsFolder":
                                    assetsFolderPath = value;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    ProjectResources.Initialize(projectName, projectFolderPath, projectFilePath, assetsFolderPath);
                    Solution.SolutionOpen(Path.Combine(projectFolderPath, projectName + ".sln"));
                }
                Debug.WriteLine("Проект успешно загружен.");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Ошибка при загрузке проекта: " + e.Message, DebugIcon.Error);
            }
        }
        public static List<Scene> LoadScenes() 
        {
            var scenes = new List<Scene>();
            string directoryPath = Path.Combine(AssetsFolder, "Scenes");

            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath, "*.scene");
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        Scene scene = BinarySerialization.Deserialize<Scene>(file);
                        if (scene != null)
                        {
                            scenes.Add(scene);
                        }
                        else if (BinarySerialization.GetLastError() != null) 
                        {
                            Debug.WriteLine(BinarySerialization.GetLastError(), DebugIcon.Error);
                        }
                    }
                }
                else
                {
                    scenes.Add(new Scene("NewScene"));
                }
            }
            else 
            {
                scenes.Add(new Scene("NewScene"));
            }

            return scenes;
        }
        public static void SaveProject() 
        {
            string directoryPath = Path.Combine(AssetsFolder, "Scenes");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            foreach (var scene in SceneManager.Scenes)
            {
                BinarySerialization.Serialize(scene, Path.Combine(AssetsFolder, "Scenes", scene.Name + ".scene"));
            }
        }

        public static void AddBaseTextToCs(string pathCs) 
        {
            string Name = Path.GetFileNameWithoutExtension(pathCs);
            string baseText = $@"using DeckMancer.Core;
using OpenTK.Windowing.Common;

class {Name} : Behaviour
{{
    public override void Start()
    {{

    }}
    
    public override void Update()
    {{

    }}
}}";

            if (File.Exists(pathCs) && new FileInfo(pathCs).Length < 5)
            {
                Solution.AddCsFile(Path.Combine(ProjectResources.ProjectFolder, ProjectResources.ProjectName + ".csproj"), pathCs);
                File.WriteAllText(pathCs, baseText);
            }

        }
    }
}
