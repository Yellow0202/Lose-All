using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


public class SfxTypeSobj : SobjDropdown
{
    public const string Str = "SfxType";

    private static SfxTypeSobj instance;
    public static SfxTypeSobj Instance
    {
        get
        {
            if(instance == null)
                instance = Resources.Load<SfxTypeSobj>(CargoldLibrary_C.GetSobjPathS + SfxTypeSobj.Str);

            return instance;
        }
    }
}

[System.Serializable, InlineProperty, HideLabel]
public partial struct SfxType
{
    
    public const int 떨어지는효과음 = 1;
    public const int 유령 = 2;
    public const int 굿 = 3;
    public const int 터치음 = 5;
    public const int 퍼펙트 = 6;
    public const int 함정카드 = 7;
    public const int 소름1 = 8;
    public const int 소름2 = 9;
    public const int 슬라이딩 = 10;

#if UNITY_EDITOR
    private IEnumerable GetIEnumerable => SfxTypeSobj.Instance.GetIEnumerable;
#endif
    public new string ToString()
    {
#if UNITY_EDITOR
        return SfxTypeSobj.Instance.CallEdit_ToString_Func(this.ID); 
#endif

        return this.ID.ToString();
    }
}
