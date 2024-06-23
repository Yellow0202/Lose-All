using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUiAnim_Script : MonoBehaviour
{
    public static InGameUiAnim_Script Instance;

    [SerializeField, LabelText("트랩카드 애니메이션")] private Animation _trapCardAnim;

    [SerializeField, LabelText("튜토리얼 애니메이션")] private Animation _tutorialAnim;

    [SerializeField, LabelText("랭킹창 애니메이션")] private Animation _ranKingAnim;

    [SerializeField, LabelText("귀신 애니메이션")] private Animation _ghostAnim;
    [SerializeField, LabelText("귀신 이미지")] private RawImage _ghostImg;

    private void Awake()
    {
        Instance = this;
    }

    public void Call_ItemAnim_Func(int a_IntKey)
    {
        if(a_IntKey == 10022)
        {   //욕망의 항아리.
            this.TrapCardAnimation_Func();
        }
    }

    private void TrapCardAnimation_Func()
    {
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.함정카드);
        this._trapCardAnim.Play("TrapCard_Anim");
    }
    
    public void Call_TutorialOpenAnim_Func()
    {
        this._tutorialAnim.Play("Tutorial_Open_Anim");
    }

    public void TutorialCloseEnd_Func()
    {
        InGameUISystem_Manager.Instance.Start_ManagerFuncs_Func();
    }

    private void Call_TutorialCloseAnim_Func()
    {
        this._tutorialAnim.Play("Tutorial_Close_Anim");
    }

    public void Click_GameStartBtn_Func()
    {
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.터치음);
        this.Call_TutorialCloseAnim_Func();
    }

    public void Call_RanKingViewOpewn_Func()
    {
        this._ranKingAnim.Play("Result_Opwn_Anim");
    }

    public void Call_GhostAnimOn_Func()
    {
        this._ghostImg.texture = InGameUISystem_Manager.Instance.ghostSpriteList.GetRandItem_Func().texture;
        this._ghostAnim.Play("Ghost_On_Anim");
    }
}
