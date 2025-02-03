using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : Component
{

    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) Instantiate();
            return instance;
        }
    }
    public static void Instantiate()
    {
        if (instance is null)
        {
            System.Type type = typeof(T);
            var go = new GameObject(type.Name, type);
            instance = go.GetComponent<T>();
        }
    }
    public static bool IsInstantiated => instance != null;

    protected void MakeSingleton(bool makeUndestroyableOnLoad = false)
    {
        if (instance is null)
        {
            instance = this as T;
            if (makeUndestroyableOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Debug.LogWarningFormat("[SingletonMonobehaviour] Instance not null at initialization: there can be only one instance of {0} on the map -> Destroying", this.GetType().ToString());
            Destroy(this.gameObject);
            return;
        }
    }

    protected virtual void Awake() => MakeSingleton();
    protected virtual void OnDestroy() => instance = null;

}

