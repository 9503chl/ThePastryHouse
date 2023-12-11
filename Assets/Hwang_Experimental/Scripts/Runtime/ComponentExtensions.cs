using System;

namespace UnityEngine
{
    public static class ComponentExtensions
    {
        public static Component EnsureComponent(this Component component, Type type)
        {
            Component result = component.GetComponent(type);
            if (result == null)
            {
                result = component.gameObject.AddComponent(type);
            }
            return result;
        }

        public static T EnsureComponent<T>(this Component component) where T : Component
        {
            T result = component.GetComponent<T>();
            if (result == null)
            {
                result = component.gameObject.AddComponent<T>();
            }
            return result;
        }

        public static Component EnsureComponent(this GameObject go, Type type)
        {
            Component result = go.GetComponent(type);
            if (result == null)
            {
                result = go.AddComponent(type);
            }
            return result;
        }

        public static T EnsureComponent<T>(this GameObject go) where T : Component
        {
            T result = go.GetComponent<T>();
            if (result == null)
            {
                result = go.AddComponent<T>();
            }
            return result;
        }
    }
}
