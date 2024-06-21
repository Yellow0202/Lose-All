using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Item_RarePersentDataGroup
{
    [LabelText("레어도 별 확률")] private Dictionary<float, Rare> itemPersentValueToRareDataDic;
    [LabelText("낮은 퍼센트부터 앞 인덱스 리스트")] private List<float> itemRarePersentDataList;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_ItemRarePersentDataList_Func();
        this.Set_ItemRareToPersentValueDataDic_Func();
    }

    private void Set_ItemRarePersentDataList_Func()
    {
        this.itemRarePersentDataList = new List<float>();

        foreach (KeyValuePair<string, Item_RarePersentData> item in dataDic)
        {
            this.itemRarePersentDataList.Add(item.Value.Item_Persent);
        }

        this.itemRarePersentDataList.Sort();
    }

    private void Set_ItemRareToPersentValueDataDic_Func()
    {
        this.itemPersentValueToRareDataDic = new Dictionary<float, Rare>();

        foreach (KeyValuePair<string, Item_RarePersentData> item in dataDic)
        {
            
            this.itemPersentValueToRareDataDic.Add(item.Value.Item_Persent, item.Value.Item_Rare);
        }
    }

    public Rare Get_ItemRareToPersentValueDataDic_Func(float a_itemRare)
    {
        for (int i = 0; i < this.itemRarePersentDataList.Count; i++)
        {
            if(a_itemRare <= this.itemRarePersentDataList[i])
            {
                if(this.itemPersentValueToRareDataDic.TryGetValue(this.itemRarePersentDataList[i], out Rare a_Value) == true)
                {
                    return a_Value;
                }
            }
        }

        return Rare.None;
    }

#if UNITY_EDITOR
    public override void CallEdit_OnDataImportDone_Func()
    {
        base.CallEdit_OnDataImportDone_Func();

        /* 테이블 임포트가 모두 마무리된 뒤 마지막으로 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */
    }
#endif

}