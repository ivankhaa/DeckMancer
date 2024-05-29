using System;
using System.Collections.Generic;

namespace DeckMancer.Core
{
    [Serializable]
    public class GameObject : Object
    {
        private List<Component> components;
        public Transform Transform { get { return (Transform)components[0]; } set { components[0] = value; } }

        public void SetID(uint id) 
        {
            ID = id;
        }
        public GameObject(string name)
        {
            Name = name;
            components = new List<Component>();
            components.Add(new Transform());
        }

        public Component AddComponent(Type componentType)
        {
            foreach (Component c in components)
            {
                if (c.GetType() == componentType)
                {
                    return c;
                }
            }
            Component component = (Component)Activator.CreateInstance(componentType);
            components.Add(component);
            return component;
        }
        public T AddComponent<T>() where T : Component, new()
        {
            return (T)AddComponent(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }
        public List<T> GetComponents<T>() where T : Component
        {
            List<T> result = new List<T>();
            foreach (var component in components)
            {
                if (component is T)
                {
                    result.Add((T)component);
                }
            }
            return result;
        }
        public void RemoveComponent<T>() where T : Component
        {
            if (components == null || components.Count == 0)
            {
                return;
            }

            if (typeof(T) == typeof(Mesh) || typeof(T) == typeof(Script))
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    if (components[i] is T)
                    {
                        components.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
