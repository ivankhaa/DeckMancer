using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    public class SceneManager
    {
        public static event EventHandler SceneLoaded;
        public static event EventHandler ChangeGameObjectSelected;
        public static Scene CurrentScene { get; private set; }
        private static GameObject _currentGameObject;
        public static GameObject CurrentGameObjectSelected { get { return _currentGameObject; } set { _currentGameObject = value; ChangeGameObjectSelected?.Invoke(null, EventArgs.Empty); } }
        public static List<Scene> Scenes { get; private set; }
        public SceneManager(List<Scene> scenes)
        {
            Scenes = scenes;
            LoadScene(Scenes[0]);
        }
        public static void LoadScene(Scene scene) 
        {
      
            CurrentGameObjectSelected = null;
            CurrentScene = scene;

            ComponentMapping.CreateMapFromGameObjectList(CurrentScene.GameObjects);
           
            SceneLoaded?.Invoke(null, EventArgs.Empty);
            
        }
        public static void LoadScene(string name) 
        {
            Scene scene =  GlobalGameManager.GetSceneByName(name);
            if (scene != null) 
            {
                LoadScene(scene);
            }
        }
    }
}
