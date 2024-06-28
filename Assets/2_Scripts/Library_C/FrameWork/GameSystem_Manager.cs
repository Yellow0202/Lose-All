using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using System;

public class GameSystem_Manager : Cargold.FrameWork.GameSystem_Manager
{
    static private GameSystem_Manager _instance;
    static public GameSystem_Manager Instance;
    //{
    //    get
    //    {
    //        if (Exists() == false)
    //        {
    //            Debug.LogError("GameSystem_Manager doesn't exists!");
    //        }
    //        return _instance;
    //    }

    //}

    static public bool Exists() { return Instance != null; }

    protected override void Init_Func()
    {
        // 프로젝트가 시작되면 가장 먼저 호출되는 곳입니다.

        if (Instance == false)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
                Destroy(this.gameObject);
        }

        base.Init_Func();
    }

    public bool skipOpenning = false;

    private Dictionary<Type, MonoBehaviour> monoSingletons = new();

    /// <summary> 
    /// returns false if same type of MonoSingle already exists.
    /// GameObject will be destroyed if false returned.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="InMonoSingleton"></param>
    /// <returns></returns>
    public bool RegistMonoSingleton<T>(MonoSingleton<T> InMonoSingleton)
    {
        if (InMonoSingleton == null)
        {
            Debug.LogError("do not regist null");
        }

        bool alreadyExists = monoSingletons.TryGetValue(typeof(T), out MonoBehaviour findee);
        bool registingAgain = findee == InMonoSingleton;

        if (alreadyExists && registingAgain == false)
        {
            Destroy(InMonoSingleton, 0.1f);
        }
        else if (registingAgain == false)
        {
            monoSingletons.Add(typeof(T), InMonoSingleton);
        }

        return ! alreadyExists;
    }

    //public void UnregistMonoSingleton<T>()
    //{
    //    monoSingletons.Remove(typeof(T));
    //}

    public T GetMonoSingleton<T>() where T : MonoSingleton<T>
    {
        return (T)monoSingletons.GetValueOrDefault(typeof(T));
    }
}
