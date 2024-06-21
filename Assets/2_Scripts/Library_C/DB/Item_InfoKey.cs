using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class Item_InfoKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("Item_Info Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public Item_InfoData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetItem_Info.TryGetData_Func(this.key, out Item_InfoData _item_InfoData);

            return _item_InfoData;
        }
    }

    public Item_InfoKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetItem_Info.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetItem_Info.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(Item_InfoKey _key)
    {
        return _key.key;
    }
}
