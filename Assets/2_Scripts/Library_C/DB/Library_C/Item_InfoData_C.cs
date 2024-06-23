using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class Item_InfoData : Data_C
{
     public string Key;
     [LabelText("레어도")] public Rare Item_Rare;
     [LabelText("아이템 인트 키")] public int IntKey;
     [LabelText("아이템 스코어")] public int ItemScore;
     [LabelText("아이템 무게")] public float Mass;
     [LabelText("아이템 아이콘")] public Sprite Icon;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Item_Rare = _cellDataArr[1].ToEnum<Rare>();
        IntKey = _cellDataArr[2].ToInt();
        ItemScore = _cellDataArr[3].ToInt();
        Mass = _cellDataArr[4].ToFloat();
        Icon = Editor_C.GetLoadAssetAtPath_Func<Sprite>(_cellDataArr[5]);
    }
#endif
}