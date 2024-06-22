using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUiAnim_Script : MonoBehaviour
{
    public static InGameUiAnim_Script Instance;

    [SerializeField, LabelText("Ʈ��ī�� �ִϸ��̼�")] private Animation _trapCardAnim;

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
        SoundSystem_Manager.Instance.PlaySfx_Func(SfxType.����ī��);
        this._trapCardAnim.Play("TrapCard_Anim");
    }
}
