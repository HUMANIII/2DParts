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
                // Find the first instance of the object (recommended in Unity >= 2023)
                _instance = Object.FindFirstObjectByType<T>();

                // Alternatively, if any instance is acceptable, use FindAnyObjectByType<T>()
                if (_instance == null)
                {
                    Debug.LogError($"Singleton of type {typeof(T)} could not be found!");
                }
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        // Ensure that there's only one instance of the Singleton
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Debug.LogError($"Multiple instances of Singleton of type {typeof(T)} detected. Destroying duplicate!");
            Destroy(this.gameObject);
        }
    }
}