using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


public class BgmTypeSobj : SobjDropdown
{
    public const string Str = "BgmType";

    private static BgmTypeSobj instance;
    public static BgmTypeSobj Instance
    {
        get
        {
            if(instance == null)
                instance = Resources.Load<BgmTypeSobj>(CargoldLibrary_C.GetSobjPathS + BgmTypeSobj.Str);

            return instance;
        }
    }
}

[System.Serializable, InlineProperty, HideLabel]
public partial struct BgmType
{
    
    public const int 인게임BGMLoop = 1;
    public const int 타이틀BGM = 2;
    public const int 인게임BGMintro = 3;

#if UNITY_EDITOR
    private IEnumerable GetIEnumerable => BgmTypeSobj.Instance.GetIEnumerable;
#endif
    public new string ToString()
    {
#if UNITY_EDITOR
        return BgmTypeSobj.Instance.CallEdit_ToString_Func(this.ID); 
#endif

        return this.ID.ToString();
    }
}
