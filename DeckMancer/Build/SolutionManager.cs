using DeckMancer.Core;
using DeckMancer.Project;
using EnvDTE;
using EnvDTE80;
using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeckMancer.Build
{

    public class SolutionManager
    {
        public readonly bool IsDteAvailable = false;
        private readonly DTE2 _dte;

        const int maxVersion = 20;
        public SolutionManager()
        {
            string progIDPrefix = "VisualStudio.DTE.";
            Type type = null;
            for (int i = maxVersion; i >= 12; i--)
            {
                string progID = $"{progIDPrefix}{i}.0";
                type = Type.GetTypeFromProgID(progID);

                if (type != null)
                {
                    Debug.WriteLine($"Visual Studio DTE version: {i}");
                    break;
                }
            }
            if (type == null)
            {
                Debug.WriteLine($"Visual Studio not found", DebugIcon.Error);

            }
            else
            {
                _dte = (DTE2)Activator.CreateInstance(type);
                IsDteAvailable = true;
            }
        }
        public void SolutionClose() 
        {
            _dte.Quit();
        }
        public void SolutionOpen(string solutionFilePath) 
        {
            if (!_dte.Solution.IsOpen)
            {
                Exception _ex = null;
                int retries = 0;
                while (retries < 6)
                {
                    try
                    {
                        _dte.Solution.Open(solutionFilePath);
                        return;
                    }
                    catch (Exception ex)
                    {
                        _ex = ex;
                        retries++;
                        System.Threading.Thread.Sleep(1000); 
                    }
                }
                Debug.WriteLine($"Error opening solution: {_ex.Message}", DebugIcon.Error);
            }
        }

        public void CreateSolution(string solutionFolderPath, string solutionName)
        {
            string solutionFilePath = Path.Combine(solutionFolderPath, $"{solutionName}.sln");
            _dte.Solution.Create(solutionFolderPath, $"{solutionName}.sln");

            ProjectRootElement projectRootElement = ProjectRootElement.Create();
            projectRootElement.DefaultTargets = "Build";
            projectRootElement.ToolsVersion = "15.0";
            projectRootElement.Sdk = "Microsoft.NET.Sdk";
            ProjectPropertyGroupElement propertyGroupElement = projectRootElement.AddPropertyGroup();
            propertyGroupElement.AddProperty("Configuration", "Debug");
            propertyGroupElement.AddProperty("Platform", "AnyCPU");
            propertyGroupElement.AddProperty("TargetFramework", "netcoreapp3.1");
            ProjectItemGroupElement referenceElementFilter = projectRootElement.AddItemGroup();
            referenceElementFilter.AddItem("Content", @"Assets\**\*.cs", new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("Visible", "true") });
            referenceElementFilter.AddItem("None", @"*; Assets\**", new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("Visible", "false") });

            string[] systemReferences = new string[]
            {
                "OpenTK.Mathematics.dll", "Newtonsoft.Json.dll", "DeckMancer.dll","SkiaSharp.dll", "OpenTK.Windowing.Common.dll", "OpenTK.Windowing.Desktop.dll"
            };

            foreach (var reference in systemReferences)
            {
                ProjectItemElement referenceElement = projectRootElement.AddItem("Reference", reference);
                var path = Path.GetFullPath(reference);
                referenceElement.AddMetadata("HintPath", path);
            }


            string projectFilePath = Path.Combine(solutionFolderPath, $"{solutionName}.csproj");
            projectRootElement.Save(projectFilePath);



            _dte.Solution.AddFromTemplate(Path.Combine(solutionFolderPath, $"{solutionName}.csproj"), solutionFolderPath, $"{solutionName}", false);

            _dte.Solution.SaveAs(solutionFilePath);
            _dte.Solution.SolutionBuild.Build(true);
        }

        public string GetAssemblyPath(string solutionFilePath)
        {
            SolutionOpen(solutionFilePath);

            EnvDTE.Project project = _dte.Solution.Projects.Item(1);

            if (project != null)
            {
                string outputDirectory = Path.Combine(ProjectResources.ProjectFolder, project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString());
                string outputFileName = project.Properties.Item("OutputFileName").Value.ToString();

                return Path.Combine(outputDirectory, outputFileName);
            }
            else
            {
                Debug.WriteLine("Project not found in solution.", DebugIcon.Error);
                return null;
            }
        }

        public void AddCsFile(string csprojFilePath, string csFilePath)
        {
            if (!File.Exists(csprojFilePath))
            {
                Debug.WriteLine("The .csproj file was not found.", DebugIcon.Error);
                return;
            }

            ProjectRootElement projectRootElement = ProjectRootElement.Open(csprojFilePath);

            bool fileExistsInProject = projectRootElement.Items
                                            .Any(item => string.Equals(item.Include, csFilePath, StringComparison.OrdinalIgnoreCase));

            if (!fileExistsInProject)
            {
                projectRootElement.AddItem("Compile", csFilePath);
                projectRootElement.Save();
                Debug.WriteLine($"{Path.GetFileName(csFilePath)} successfully added to the project.", DebugIcon.Message);
            }
            else
            {
                Debug.WriteLine($"{Path.GetFileName(csFilePath)} already present in the project.", DebugIcon.Warning);
            }
                
        }

        public void RemoveCsFile(string csprojFilePath, string csFilePath)
        {
            ProjectRootElement projectRootElement = ProjectRootElement.Open(csprojFilePath);

            ProjectItemElement csFileItem = projectRootElement.Items
                .FirstOrDefault(item => item.Include.Equals(csFilePath, StringComparison.OrdinalIgnoreCase) &&
                                         item.ItemType.Equals("Compile", StringComparison.OrdinalIgnoreCase));

            if (csFileItem != null)
            {
                csFileItem.Parent.RemoveChild(csFileItem);
                projectRootElement.Save();
            }
        }
        public bool CompileProject(string solutionFilePath)
        {
            try
            {
                SolutionOpen(solutionFilePath);
                ErrorItems errors = null;
                _dte.Solution.SolutionBuild.Build(true);
                var buildInfo = _dte.Solution.SolutionBuild.LastBuildInfo;

                bool errorsShown = false;
                int maxRetries = 10;

                int retries = 0;
                while (!errorsShown && retries < maxRetries && buildInfo != 0)
                {
                    try
                    {
                        _dte.ToolWindows.ErrorList.ShowErrors = true;
                        var errorList = _dte.ToolWindows.ErrorList;
                        errors = errorList.ErrorItems;

                        if (errors.Count > 0)
                            errorsShown = true;

                        System.Threading.Thread.Sleep(1000);
                        retries++;
                    }
                    catch (Exception ex)
                    {
                        System.Threading.Thread.Sleep(1000);
                        retries++;
                    }
                }

                if (buildInfo == 0)
                {
                    Debug.WriteLine("Compilation succeeded");
                }
                else
                {
                    Debug.WriteLine($"Compilation error: {_dte.Solution.SolutionBuild.LastBuildInfo}", DebugIcon.Error);
                    if (errors != null && errors.Count > 0)
                    {

                        for (int i = 1; i <= errors.Count; i++)
                        {
                            var error = errors.Item(i);

                            Debug.WriteLine($"File: {error.FileName} | Line: {error.Line} | Error: {error.Description}", DebugIcon.Error);
                        }
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving compilation error report: {ex.Message}", DebugIcon.Error);
                return false;
            }
            return true;
        }
        public void OpenSolutionWithFocusOnCs(string solutionFilePath, string csFilePath)
        {
            SolutionOpen(solutionFilePath);

            ProjectItem csFileProjectItem = null;
            foreach (EnvDTE.Project project in _dte.Solution.Projects)
            {
                foreach (ProjectItem projectItem in GetProjectFiles(project))
                {
                    if (string.Equals(projectItem.Name, Path.GetFileName(csFilePath), StringComparison.OrdinalIgnoreCase))
                    {
                        csFileProjectItem  =  projectItem;
                        break;
                    }
                }
            }
           
            
            if (csFileProjectItem != null)
            {
                _dte.MainWindow.Activate();
                _dte.MainWindow.Visible = true;
                Window csFileWindow = _dte.ItemOperations.OpenFile(csFilePath);
                csFileWindow.Activate();
                csFileWindow.Visible = true; 

            }
        }

        private IEnumerable<ProjectItem> GetProjectFiles(EnvDTE.Project project)
        {
            if (project.ProjectItems != null)
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.Kind == Constants.vsProjectItemKindPhysicalFile)
                    {
                        yield return item;
                    }
                    else if (item.SubProject != null)
                    {
                        foreach (var subItem in GetProjectFiles(item.SubProject))
                        {
                            yield return subItem;
                        }
                    }
                    else if (item.ProjectItems != null)
                    {
                        foreach (ProjectItem subItem in GetProjectFiles(item))
                        {
                            yield return subItem;
                        }
                    }
                }
            }
        }
        private IEnumerable<ProjectItem> GetProjectFiles(ProjectItem projectItem)
        {
            if (projectItem.ProjectItems != null)
            {
                foreach (ProjectItem item in projectItem.ProjectItems)
                {
                    if (item.Kind == Constants.vsProjectItemKindPhysicalFile)
                    {
                        yield return item;
                    }
                    else if (item.SubProject != null)
                    {
                        foreach (var subItem in GetProjectFiles(item))
                        {
                            yield return subItem;
                        }
                    }
                    else if (item.ProjectItems != null)
                    {
                        foreach (ProjectItem subItem in GetProjectFiles(item))
                        {
                            yield return subItem;
                        }
                    }
                }
            }
        }
    }
}
