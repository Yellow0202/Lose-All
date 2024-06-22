using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Script : MonoBehaviour
{
    public void IntroPlay_Func()
    {
        SoundChild_Script.Instance.Stop_Bgm_Func();
    }

    public void Start_Game_Func()
    {
        InGameUISystem_Manager.Instance.Start_ManagerFuncs_Func();
        this.gameObject.SetActive(false);
    }
}
