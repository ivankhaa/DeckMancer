using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DeckMancer.Core;
using DeckMancer.Project;
using DeckMancer.Serialization;

namespace DeckMancer.Build
{
    public class BuildGame
    {
        public static void CreateSample(string gameFolderPath, string scriptsDllPath, string startSceneName)
        {
            try
            {
                string resourceFolderPath = Path.Combine(gameFolderPath, "Resource");
                string openGLFolderPath = Path.Combine(resourceFolderPath, "OpenGL");
                string fontFolderPath = Path.Combine(resourceFolderPath, "Font");
                string scenesFolderPath = Path.Combine(resourceFolderPath, "Scenes");
                string assetsScenesPath = Path.Combine(ProjectResources.AssetsFolder, "Scenes");
                string startScenePath = Path.Combine(assetsScenesPath, $"{startSceneName}.scene");
                string exeFilePath = Path.Combine(gameFolderPath, "Game.exe");

                if (!string.IsNullOrEmpty(startScenePath) && File.Exists(startScenePath))
                {
                    Directory.CreateDirectory(gameFolderPath);
                    Directory.CreateDirectory(resourceFolderPath);
                    Directory.CreateDirectory(openGLFolderPath);
                    Directory.CreateDirectory(fontFolderPath);

                    if (Directory.Exists(scenesFolderPath))
                    {
                        Directory.Delete(scenesFolderPath, true);
                    }
                    Directory.CreateDirectory(scenesFolderPath);

                    CopyDirectory(Path.GetFullPath("Resource\\OpenGL"), openGLFolderPath);
                    CopyDirectory(Path.GetFullPath("Resource\\Font"), fontFolderPath);
              

                    if (!string.IsNullOrEmpty(scriptsDllPath) && File.Exists(scriptsDllPath))
                    {
                        string newScriptsDllPath = Path.Combine(gameFolderPath, Path.GetFileName(scriptsDllPath));
                        File.Copy(scriptsDllPath, newScriptsDllPath, true);
                    }

                    List<string> sceneNames = new List<string>();
                    sceneNames.Add(startSceneName);
                    string sceneFileName = Path.GetFileName(startScenePath);
                    string destinationPath = Path.Combine(scenesFolderPath, sceneFileName);
                    File.Copy(startScenePath, destinationPath, true);

                    var sceneFiles = new List<string>(Directory.GetFiles(assetsScenesPath, "*.scene"));

                    sceneFiles.Remove(startScenePath);

                    foreach (string sceneFile in sceneFiles)
                    {
                        sceneFileName = Path.GetFileName(sceneFile);
                        destinationPath = Path.Combine(scenesFolderPath, sceneFileName);
                        File.Copy(sceneFile, destinationPath, true);
                        sceneNames.Add(Path.GetFileNameWithoutExtension(sceneFile));
                    }

                    GlobalGameManager manager = GlobalGameManager.SetInstance("Resource\\Scenes", sceneNames.ToArray());
                    BinarySerialization.Serialize(manager, Path.Combine(resourceFolderPath, "Globalgamemanager"));
                
                    string newExeFilePath = Path.Combine(gameFolderPath, $"{ProjectResources.ProjectName}.exe");
                    if (!File.Exists(newExeFilePath)) 
                    {
                        CopyDirectory(Path.GetFullPath("Resource\\Sample"), gameFolderPath);
                        if (File.Exists(exeFilePath))
                        {
                            File.Move(exeFilePath, newExeFilePath);
                        } 
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating sample: {ex.Message}", DebugIcon.Error);
            }
        }

        private static void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string dest = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string dest = Path.Combine(targetDir, Path.GetFileName(dir));
                CopyDirectory(dir, dest);
            }
        }
        private static CancellationTokenSource cancellationTokenSource;
        private static System.Diagnostics.Process process;
        private static TaskCompletionSource<bool> processStartedTask;
        public static async Task RunGameAsync(string gameOutputFilePath, AutoResetEvent processStartedEvent = null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            processStartedTask = new TaskCompletionSource<bool>();

            try
            {
                if (File.Exists(gameOutputFilePath))
                {
                    process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = gameOutputFilePath;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(gameOutputFilePath);

                    process.StartInfo.UseShellExecute = false;

                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.StartInfo.CreateNoWindow = false;

                    process.Start();
                    
                    Task readOutputTask = ReadOutputAsync(process.StandardOutput);
                    Task readErrorTask = ReadOutputAsync(process.StandardError, DebugIcon.Error);
                    await Task.WhenAny(processStartedTask.Task, Task.Delay(1));
                    if (processStartedEvent != null)
                        processStartedEvent.Set();
                    await Task.WhenAll(readOutputTask, readErrorTask);
                }
                else
                {
                    Debug.WriteLine("Error: build file not found.", DebugIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error running the Game project: {ex.Message}", DebugIcon.Error);
            }
        }

        private static async Task ReadOutputAsync(StreamReader reader, DebugIcon debugIcon = DebugIcon.Message)
        {
            try
            {
                string line;
                while (!cancellationTokenSource.IsCancellationRequested && (line = await reader.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Debug.WriteLine($"Output: {line}", debugIcon);
                        });
                        processStartedTask.TrySetResult(true);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        public static void CancelRunGame()
        {

            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
                process.Kill();
                process.Close();
            }
        }
        public static void RunGame(string gameOutputFilePath)
        {
            try
            {
                if (File.Exists(gameOutputFilePath))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = gameOutputFilePath;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(gameOutputFilePath);

                    process.StartInfo.UseShellExecute = false;

                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.StartInfo.CreateNoWindow = false;


                    process.Start();
                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Debug.WriteLine($"Output: {process.StandardOutput.ReadToEnd()}");
                    });
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Debug.WriteLine($"Error: {process.StandardError.ReadToEnd()}", DebugIcon.Error);
                    });

                    process.WaitForExit();
                   
                    process.Close();
                }
                else
                {
                    Debug.WriteLine("Error: build file not found.", DebugIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error running the Game project: {ex.Message}", DebugIcon.Error);
            }
        }
    }
}
