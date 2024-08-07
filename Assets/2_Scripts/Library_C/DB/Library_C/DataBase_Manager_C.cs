using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DataBase_Manager : Cargold.FrameWork.DataBase_Manager
{
    private static DataBase_Manager instance;
    public static new DataBase_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<DataBase_Manager>("DataBase_Manager");
            }

            return instance;
        }
    }

    [SerializeField] private Debug_C.PrintLogType printLogType = Debug_C.PrintLogType.Common;
    #region Variable
    
    [InlineEditor, LabelText("Item_Info"), SerializeField] private DB_Item_InfoDataGroup item_Info;
    public DB_Item_InfoDataGroup GetItem_Info
    {
        get
        {
            if (this.item_Info == null)
                this.item_Info = Resources.Load<DB_Item_InfoDataGroup>(base.dataGroupSobjPath + "DB_Item_InfoDataGroup");

            return this.item_Info;
        }
    }
    [InlineEditor, LabelText("Item_RarePersent"), SerializeField] private DB_Item_RarePersentDataGroup item_RarePersent;
    public DB_Item_RarePersentDataGroup GetItem_RarePersent
    {
        get
        {
            if (this.item_RarePersent == null)
                this.item_RarePersent = Resources.Load<DB_Item_RarePersentDataGroup>(base.dataGroupSobjPath + "DB_Item_RarePersentDataGroup");

            return this.item_RarePersent;
        }
    }
    [InlineEditor, LabelText("Localize"), SerializeField] private DB_LocalizeDataGroup localize;
    public DB_LocalizeDataGroup GetLocalize
    {
        get
        {
            if (this.localize == null)
                this.localize = Resources.Load<DB_LocalizeDataGroup>(base.dataGroupSobjPath + "DB_LocalizeDataGroup");

            return this.localize;
        }
    }
    [InlineEditor, LabelText("Table_Define"), SerializeField] private DB_Table_DefineDataGroup table_Define;
    public DB_Table_DefineDataGroup GetTable_Define
    {
        get
        {
            if (this.table_Define == null)
                this.table_Define = Resources.Load<DB_Table_DefineDataGroup>(base.dataGroupSobjPath + "DB_Table_DefineDataGroup");

            return this.table_Define;
        }
    }
    #endregion

    #region Library
    
    #endregion

    protected override Debug_C.PrintLogType GetPrintLogType => this.printLogType;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);
        
        if(_layer == 0)
        {
            Debug_C.Init_Func(this);

            
            this.item_Info.Init_Func();
            this.item_RarePersent.Init_Func();
            this.localize.Init_Func();
            this.table_Define.Init_Func();
        }
    }

#if UNITY_EDITOR
    public override void CallEdit_OnDataImport_Func(bool _isDataImport = true)
    {
        this.GetItem_Info.CallEdit_OnDataImportDone_Func();
        this.GetItem_RarePersent.CallEdit_OnDataImportDone_Func();
        this.GetLocalize.CallEdit_OnDataImportDone_Func();
        this.GetTable_Define.CallEdit_OnDataImportDone_Func();
        
        base.CallEdit_OnDataImport_Func();
    }
#endif
}