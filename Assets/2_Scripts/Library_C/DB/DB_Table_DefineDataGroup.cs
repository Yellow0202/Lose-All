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
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("아이템 생성 시간")] private float _item_Spawn_CoolTime; [HideInInspector]public float item_Spawn_CoolTime;
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("아이템 최대 생성 갯수")] private int _item_Spawn_Count; public int item_Spawn_Count => this._item_Spawn_Count;
    [SerializeField, FoldoutGroup("아이템 생성 정보"), LabelText("도깨비불 확률")] private float _item_Horror_Vlaue; public float item_Horror_Vlaue => this._item_Horror_Vlaue;

    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 이동속도")] private float _enemy_MoveSpeed; public float enemy_MoveSpeed => this._enemy_MoveSpeed;
    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 생성 조건")] private int _enemy_SpawnCondition; public int enemy_SpawnCondition => this._enemy_SpawnCondition;
    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 사람 수")] private int _enemy_SpawnMaxCount; public int enemy_SpawnMaxCount => this._enemy_SpawnMaxCount;
    [SerializeField, FoldoutGroup("사촌동생 정보"), LabelText("사촌동생 멈춤 시간")] private float _enemy_StopTime; public float enemy_StopTime => this._enemy_StopTime;

    [SerializeField, FoldoutGroup("캐치 정보"), LabelText("퍼펙트 퍼센트")] private float _chtch_PerpectPersent; public float chtch_PerpectPersent => this._chtch_PerpectPersent;

    [SerializeField, FoldoutGroup("난이도 상승"), LabelText("아이템 생성 시간 감소량")] private float _difficulty_DawonItemSpawnCoolTime; public float difficulty_DawonItemSpawnCoolTime => this._difficulty_DawonItemSpawnCoolTime;
    [SerializeField, FoldoutGroup("난이도 상승"), LabelText("아이템 생성 시간 감소 MAX")] private float _difficulty_ItemSpawnCoolTimeMAX; public float difficulty_ItemSpawnCoolTimeMAX => this._difficulty_ItemSpawnCoolTimeMAX;
    [SerializeField, FoldoutGroup("난이도 상승"), LabelText("아이템 생성 시간 감소 조건 금액(시작가)")] private int _difficulty_SpawnCoolTimeDownCondition; public int difficulty_SpawnCoolTimeDownCondition => this._difficulty_SpawnCoolTimeDownCondition;
    [SerializeField, FoldoutGroup("난이도 상승"), LabelText("아이템 생성 시간 감소 조건 금액 감소량")] private int _difficulty_SpawnCoolTimeDownConditionDownScore; public int difficulty_SpawnCoolTimeDownConditionDownScore => this._difficulty_SpawnCoolTimeDownConditionDownScore;

    [SerializeField, FoldoutGroup("게임오버"), LabelText("손해 스코어")] private int _gameOverScoreMax; public int gameOverScoreMax => this._gameOverScoreMax;
    [SerializeField, FoldoutGroup("게임오버"), LabelText("파손 피규어")] private int _gameOverCountMax; public int gameOverCountMax => this._gameOverCountMax;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.item_Spawn_CoolTime = _item_Spawn_CoolTime;

    }

    public void Item_SpawnCoolTimeDown_Func()
    {
        this.item_Spawn_CoolTime -= this._difficulty_DawonItemSpawnCoolTime;
        if (this.item_Spawn_CoolTime <= this._difficulty_ItemSpawnCoolTimeMAX)
            this.item_Spawn_CoolTime = this._difficulty_ItemSpawnCoolTimeMAX;

        Debug.Log("this.item_Spawn_CoolTime : " + this.item_Spawn_CoolTime);
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