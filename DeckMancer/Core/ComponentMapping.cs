using System;
using System.Collections.Generic;
using System.Text;

namespace DeckMancer.Core
{
    public static class ComponentMapping
    {
        private static Dictionary<Type, List<GameObject>> componentToObjectMap;

        static ComponentMapping()
        {
            componentToObjectMap = new Dictionary<Type, List<GameObject>>();
        }
        public static void Clear() 
        {
            componentToObjectMap = new Dictionary<Type, List<GameObject>>();
        }
        public static void AddComponentMapping<T>(T component, GameObject gameObject) where T : Component
        {
            Type componentType = typeof(T);

            if (!componentToObjectMap.ContainsKey(componentType))
            {
                componentToObjectMap[componentType] = new List<GameObject>();
            }

            componentToObjectMap[componentType].Add(gameObject);
        }

        public static List<GameObject> GetGameObjectsForComponent<T>() where T : Component
        {
            Type componentType = typeof(T);

            if (componentToObjectMap.ContainsKey(componentType))
            {
                return componentToObjectMap[componentType];
            }
            else
            {
                return new List<GameObject>();
            }
        }
        public static void AddComponentMapping(Type componentType, GameObject gameObject)
        {
            if (!componentToObjectMap.ContainsKey(componentType))
            {
                componentToObjectMap[componentType] = new List<GameObject>();
            }

            componentToObjectMap[componentType].Add(gameObject);
        }

        public static void CreateMapFromGameObjectList(List<GameObject> gameObjects)
        {
            componentToObjectMap = new Dictionary<Type, List<GameObject>>();
            foreach (GameObject gameObject in gameObjects)
            {
                List<Component> components = gameObject.GetComponents<Component>();

                foreach (Component component in components)
                {
                    Type componentType = component.GetType();
                    AddComponentMapping(componentType, gameObject);
                }
            }
        }

    }

}
