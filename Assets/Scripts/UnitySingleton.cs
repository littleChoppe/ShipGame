using UnityEngine;
using System.Collections;

public abstract class UnitySingleton<T> : MonoBehaviour where  T : UnitySingleton<T> {
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    Debug.Log("No instance of " + typeof(T).ToString() + " found!");
                    return null;
                }
                _instance.Init();
            }
            return _instance;
        }
    }

    public bool _persist = false;

    public bool Persist
    {
        get { return _persist; }
        protected set { _persist = value; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            _instance.Init();
        }
    }

    protected virtual void Init() { }

    void OnApplicationQuit()
    {
        _instance = null;
    }
}
