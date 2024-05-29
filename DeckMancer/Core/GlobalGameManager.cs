using System.IO;
using DeckMancer.Serialization;
using System;
using System.Linq;

namespace DeckMancer.Core
{
    [Serializable]
    public class GlobalGameManager
    {
        private static GlobalGameManager _instance;

        private string RelativeScenesFolderPath;
        public readonly string[] SceneNames;

        private GlobalGameManager(string scenesFolderPath, string[] sceneNames)
        {
            RelativeScenesFolderPath = scenesFolderPath;
            SceneNames = sceneNames;
        }

        public static GlobalGameManager SetInstance(string relativeScenesFolderPath, string[] sceneNames)
        {
            _instance = new GlobalGameManager(relativeScenesFolderPath, sceneNames);
            return _instance;
        }
        public static void SetInstance(GlobalGameManager instance)
        {
            _instance = instance;
        }
        public static Scene GetSceneByName(string name)
        {
            if (_instance == null)
            {
                return null;
            }

            if (!_instance.SceneNames.Contains(name))
            {
                return null;
            }

            string fullScenesFolderPath = Path.GetFullPath(_instance.RelativeScenesFolderPath);

            string scenePath = Path.Combine(fullScenesFolderPath, name + ".scene");

            return BinarySerialization.Deserialize<Scene>(scenePath);
        }
    }
}
