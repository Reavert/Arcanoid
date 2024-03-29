﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(this);
            Debug.Log($"An extra singleton {typeof(T).Name} component has been destroyed");
        }
    }

    protected abstract void Initialize();
}
