using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class Item_RarePersentKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("Item_RarePersent Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public Item_RarePersentData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetItem_RarePersent.TryGetData_Func(this.key, out Item_RarePersentData _item_RarePersentData);

            return _item_RarePersentData;
        }
    }

    public Item_RarePersentKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetItem_RarePersent.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetItem_RarePersent.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(Item_RarePersentKey _key)
    {
        return _key.key;
    }
}
