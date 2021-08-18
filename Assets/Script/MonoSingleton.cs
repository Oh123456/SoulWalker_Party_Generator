using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var listObjects = Resources.FindObjectsOfTypeAll<T>();

                if (listObjects.Length > 0)
                    (listObjects[0] as MonoSingleton<T>).Awake();
            }
            return instance;
        }

    }

    protected virtual void Awake()
    {
        if (instance != null) return;

        instance = this as T;
    }

}
