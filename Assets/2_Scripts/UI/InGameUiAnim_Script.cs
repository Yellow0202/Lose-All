using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUiAnim_Script : MonoBehaviour
{
    public static InGameUiAnim_Script Instance;

    [SerializeField, LabelText("Æ®·¦Ä«µå ¾Ö´Ï¸ÞÀÌ¼Ç")] private Animation _trapCardAnim;

    private void Awake()
    {
        Instance = this;
    }

    public void Call_ItemAnim_Func(int a_IntKey)
    {
        if(a_IntKey == 10011)
        {   //¿å¸ÁÀÇ Ç×¾Æ¸®.
            this.TrapCardAnimation_Func();
        }
    }

    private void TrapCardAnimation_Func()
    {
        this._trapCardAnim.Play("TrapCard_Anim");
    }
}
