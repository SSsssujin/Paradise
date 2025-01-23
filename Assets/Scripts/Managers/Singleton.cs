using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    GameObject sObject = new GameObject(typeof(T).Name);
                    _instance = sObject.AddComponent<T>();
                    DontDestroyOnLoad(sObject);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}