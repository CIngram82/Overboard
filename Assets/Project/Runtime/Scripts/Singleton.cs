using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => _instance;

    static T _instance;

    protected void InitSingleton(T singleton)
    {
        if (_instance == null)
        {
            _instance = singleton;
            DontDestroyChildOnLoad(singleton.gameObject);
        }
        else if (!_instance.Equals(singleton))
        {
            Debug.LogWarning($"Multiple instances of {GetType()}, destroying this GameObject.");
            Destroy(this);
        }
    }

    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        DontDestroyOnLoad(parentTransform.gameObject);
    }
}





