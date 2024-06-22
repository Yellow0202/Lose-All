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
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.ÅÍÄ¡À½);
        InGameUiAnim_Script.Instance.Call_TutorialOpenAnim_Func();
        this.gameObject.SetActive(false);
    }
}
