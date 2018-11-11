using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Get => instance;

    public static T Instance => instance;

    protected void SetSingleton()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
}
