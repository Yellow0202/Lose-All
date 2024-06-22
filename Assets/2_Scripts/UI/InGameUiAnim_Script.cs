using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUiAnim_Script : MonoBehaviour
{
    public static InGameUiAnim_Script Instance;

    [SerializeField, LabelText("Ʈ��ī�� �ִϸ��̼�")] private Animation _trapCardAnim;

    [SerializeField, LabelText("Ʃ�丮�� �ִϸ��̼�")] private Animation _tutorialAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void Call_ItemAnim_Func(int a_IntKey)
    {
        if(a_IntKey == 10011)
        {   //����� �׾Ƹ�.
            this.TrapCardAnimation_Func();
        }
    }

    private void TrapCardAnimation_Func()
    {
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.����ī��);
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
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.��ġ��);
        this.Call_TutorialCloseAnim_Func();
    }
}
