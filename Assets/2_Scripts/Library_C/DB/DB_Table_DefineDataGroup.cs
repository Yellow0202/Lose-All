using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cargold;
using Sirenix.OdinInspector;
using Cargold.DB.TableImporter;

// 카라리 테이블 임포터에 의해 생성된 스크립트입니다.

public partial class DB_Table_DefineDataGroup
{
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("아이템 낙하 값")] private float _item_Falling_Speed; public float item_Falling_Speed => this._item_Falling_Speed;
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("아이템 생성 시간")] private float _item_Spawn_CoolTime; public float item_Spawn_CoolTime => this._item_Spawn_CoolTime;
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("아이템 최대 생성 갯수")] private int _item_Spawn_Count; public int item_Spawn_Count => this._item_Spawn_Count;

    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 이동속도")] private float _enemy_MoveSpeed; public float enemy_MoveSpeed => this._enemy_MoveSpeed;
    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 생성 조건")] private int _enemy_SpawnCondition; public int enemy_SpawnCondition => this._enemy_SpawnCondition;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */
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