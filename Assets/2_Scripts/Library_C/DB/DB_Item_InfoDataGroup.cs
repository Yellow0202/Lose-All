using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Item_InfoDataGroup
{
    [LabelText("레어도에 따라 아이템 리스트를 보관")] private Dictionary<Rare, List<Item_InfoData>> itemRareToItemInfoDataListDataDic;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.Set_ItemRareToItemInfoDataListDataDic_Func();
    }

    private void Set_ItemRareToItemInfoDataListDataDic_Func()
    {
        this.itemRareToItemInfoDataListDataDic = new Dictionary<Rare, List<Item_InfoData>>();

        foreach (KeyValuePair<string, Item_InfoData> item in dataDic)
        {
            if(this.itemRareToItemInfoDataListDataDic.TryGetValue(item.Value.Item_Rare, out List<Item_InfoData> a_ValueList))
            {
                a_ValueList.Add(item.Value);
            }
            else
            {
                List<Item_InfoData> a_newList = new List<Item_InfoData>();
                a_newList.Add(item.Value);
                this.itemRareToItemInfoDataListDataDic.Add(item.Value.Item_Rare, a_newList);
            }
        }
    }

    public Item_InfoData Get_ItemRareToItemInfoDataListDataDic_Func(Rare a_ItemRare)
    {
        if (this.itemRareToItemInfoDataListDataDic.TryGetValue(a_ItemRare, out List<Item_InfoData> a_ValueList))
        {
            return a_ValueList.GetRandItem_Func();
        }
        else
            return null;
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