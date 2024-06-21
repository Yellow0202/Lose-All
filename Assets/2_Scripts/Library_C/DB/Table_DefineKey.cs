using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable, InlineProperty, HideLabel]
public partial class Table_DefineKey : Cargold.FrameWork.TableDataKeyDropdown
{
    [LabelWidth(DropdownDefine.LabelWidth)]
    [SerializeField, LabelText("Table_Define Key"), ValueDropdown("CallEdit_KeyDropdown_Func")] private string key = null;

    public string GetKey => this.key;

    [ShowInInspector, ReadOnly, HideLabel, FoldoutGroup("Preview")]
    public Table_DefineData GetData
    {
        get
        {
            DataBase_Manager.Instance.GetTable_Define.TryGetData_Func(this.key, out Table_DefineData _table_DefineData);

            return _table_DefineData;
        }
    }

    public Table_DefineKey(string _keyStr = null)
    {
        this.key = _keyStr;
    }

#if UNITY_EDITOR
    private IEnumerable<string> CallEdit_KeyDropdown_Func()
    {
        return DataBase_Manager.Instance.GetTable_Define.GetKeyArr;
    }
    public bool CallEdit_IsUnitTestDone_Func()
    {
        if(this.key.IsNullOrWhiteSpace_Func() == false)
            return DataBase_Manager.Instance.GetTable_Define.IsContain_Func(this.key);

        return false;
    }
#endif

    public static implicit operator string(Table_DefineKey _key)
    {
        return _key.key;
    }
}
