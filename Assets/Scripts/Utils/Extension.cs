using UnityEngine;

namespace Paradise
{
    public static class Extension
    {
        public static T DemandComponent<T>(this GameObject origin) where T : Component
        {
            T component = origin.GetComponent<T>();
            if (component == null)
            {
                component = origin.AddComponent<T>();
            }
            return component;
        }
        
        public static T FetchComponent<T>(this Transform origin) where T : Component
        {
            origin.TryGetComponent(out T component);
            return component;
        }
        
        public static T FetchComponent<T>(this GameObject origin) where T : Component
        {
            origin.TryGetComponent(out T component);
            return component;
        }
    }
}