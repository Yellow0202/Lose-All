using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUiAnim_Script : MonoBehaviour
{
    public static InGameUiAnim_Script Instance;

    [SerializeField, LabelText("트랩카드 애니메이션")] private Animation _trapCardAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void Call_ItemAnim_Func(int a_IntKey)
    {
        if(a_IntKey == 10011)
        {   //욕망의 항아리.
            this.TrapCardAnimation_Func();
        }
    }

    private void TrapCardAnimation_Func()
    {
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.함정카드);
        this._trapCardAnim.Play("TrapCard_Anim");
    }
}
