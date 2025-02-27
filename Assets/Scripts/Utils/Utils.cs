using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Paradise
{
    public static class Utils
    {
        public static Vector2 GetTouchPosition()
        {
            return Pointer.current.position.ReadValue();
        }
        
        public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
        {
            Transform transform = FindChild<Transform>(go, name, recursive);
            if (transform == null)
                return null;

            return transform.gameObject;
        }
        
        public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if (go == null)
                return null;

            if (recursive == false)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform transform = go.transform.GetChild(i);
                    if (string.IsNullOrEmpty(name) || transform.name == name)
                    {
                        T component = transform.GetComponent<T>();
                        if (component != null)
                            return component;
                    }
                }
            }
            else
            {
                foreach (T component in go.GetComponentsInChildren<T>(true))
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }

            return null;
        }
        
        public static T DemandComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            T component = go.GetComponent<T>();
            if (component == null)
                component = go.AddComponent<T>();
            return component;
        }

        public static int GetMaxCount(UnitType type)
        {
            int count = type switch
            {
                UnitType.Basic => MaxCount.Basic,
                UnitType.Elite => MaxCount.Elite,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return count;
        }
    }
}