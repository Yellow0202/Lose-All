using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
{
    // Start is called before the first frame update
    virtual protected void OnEnable()
    {
        GameSystem_Manager.Instance.RegistMonoSingleton<T>(this);
    }

    protected void OnDestroy()
    {
        if (GameSystem_Manager.Exists())
        {
            GameSystem_Manager.Instance.UnregistMonoSingleton<T>();
        }
    }
}
