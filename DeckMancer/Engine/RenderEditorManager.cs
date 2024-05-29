using DeckMancer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeckMancer.Engine
{
    public static class RenderEditorManager
    {
        public static List<GameObjectRenderEditor> GameObjectsRender;
        public static CameraEditor CameraEditor = new CameraEditor();
        public static MainCamera Camera = null;
        private static Scene lastScene;
       static RenderEditorManager() 
        {
            SceneManager.SceneLoaded += _SceneLoaded;
            InitializGameObjectsRender();
        }
        private static void _SceneLoaded(object sender, EventArgs e)
        {
            InitializGameObjectsRender();
        }
        public static void InitializGameObjectsRender()
        {
            GameObjectsRender = new List<GameObjectRenderEditor>();
            List<GameObject> gameObjects = ComponentMapping.GetGameObjectsForComponent<Mesh>();
            Camera = ComponentMapping.GetGameObjectsForComponent<Camera>()[0] as MainCamera;
            foreach (var gameObject in gameObjects)
            {
                GameObjectsRender.Add(new GameObjectRenderEditor(CameraEditor, gameObject));
            }
            lastScene = SceneManager.CurrentScene;
        }
        public static void AddGameObjectRender(GameObject gameObject) 
        {
            if (SceneManager.CurrentScene == lastScene)
            {
                Mesh meshComponent = gameObject.GetComponent<Mesh>();

                if (meshComponent != null)
                {
                    GameObjectsRender.Add(new GameObjectRenderEditor(CameraEditor, gameObject));
                }
            }
        }
        public static void RmoveGameObjectRender(GameObject gameObject)
        {
            if (SceneManager.CurrentScene == lastScene)
            {
                List<GameObjectRenderEditor> rendersToRemove = GameObjectsRender.Where(render => render._gameObject == gameObject).ToList();

                foreach (GameObjectRenderEditor render in rendersToRemove)
                {
                    GameObjectsRender.Remove(render);
                }
            }
        }
    }
}
