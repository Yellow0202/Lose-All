using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

[System.Serializable]
public partial class LocalizeData : Data_C
{
     public string Key;
     [LabelText("한글")] public string ko;
     [LabelText("영어")] public string en;
     [LabelText("일본어")] public string Jp;
     [LabelText("간체")] public string Ch;
     [LabelText("번체")] public string TW;

    

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(string[] _cellDataArr)
    {
        Key = _cellDataArr[0];
        ko = _cellDataArr[1];
        en = _cellDataArr[2];
        Jp = _cellDataArr[3];
        Ch = _cellDataArr[4];
        TW = _cellDataArr[5];
    }
#endif
}