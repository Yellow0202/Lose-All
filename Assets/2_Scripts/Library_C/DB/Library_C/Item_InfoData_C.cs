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
     [LabelText("이름")] public string Name;
     [LabelText("아이템 설명")] public string Comment;
     [LabelText("아이템 인트 키")] public int IntKey;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        Name = _cellDataArr[1];
        Comment = _cellDataArr[2];
        IntKey = _cellDataArr[3].ToInt();
    }
#endif
}