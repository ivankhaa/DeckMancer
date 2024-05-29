using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    [Serializable]
    public class Scene
    {
        [field: NonSerialized]
        public event EventHandler GameObjectsChanged;
        public uint IDs { get; private set; }
        public string Name { get; set; }
        public List<GameObject> GameObjects { get; private set;}
        public void AddGameObject(GameObject gameObject) 
        {
            gameObject.SetID(GetNewID());
            GameObjects.Add(gameObject);
            OnGameObjectsChanged(gameObject);
        }
        public void RemoveGameObject(GameObject gameObject) 
        {
            GameObjects.Remove(gameObject);
            OnGameObjectsChanged(gameObject);
        }
        public void AddGameObjects(List<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetID(GetNewID());
                GameObjects.Add(gameObject);
            }


            OnGameObjectsChanged(gameObjects);
        }
        public void RemoveGameObjects(List<GameObject> gameObjects)
        {
            GameObjects.RemoveAll(item => gameObjects.Contains(item));
            OnGameObjectsChanged(gameObjects);
        }
        public uint GetNewID()
        {
            IDs++;
            return IDs;
        }

        public Scene(string name) 
        {
            IDs = 0;
            GameObjects = new List<GameObject>();
            var MC = new MainCamera("MainCamera");

            AddGameObject(MC);
            Name = name;
          
        }
        protected virtual void OnGameObjectsChanged(object sender)
        {
            GameObjectsChanged?.Invoke(sender, EventArgs.Empty);
        }
    }
}
