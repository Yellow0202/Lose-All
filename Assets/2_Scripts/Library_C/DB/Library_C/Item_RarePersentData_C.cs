using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class Item_RarePersentData : Data_C
{
     public string Key;
     [LabelText("레어도")] public Rare Item_Rare;
     [LabelText("확률")] public float Item_Persent;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Item_Rare = _cellDataArr[1].ToEnum<Rare>();
        Item_Persent = _cellDataArr[2].ToFloat();
    }
#endif
}