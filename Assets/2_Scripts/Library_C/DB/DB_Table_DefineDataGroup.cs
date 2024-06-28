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


    [SerializeField, FoldoutGroup("한글_스프라이트_타이틀"), LabelText("메인 배경")] private Sprite _ko_Title_Sprite_MainBack; public Sprite ko_Title_Sprite_MainBack => this._ko_Title_Sprite_MainBack;
    [SerializeField, FoldoutGroup("한글_스프라이트_타이틀"), LabelText("메인 제목")] private Sprite _ko_Title_Sprite_MainTitle; public Sprite ko_Title_Sprite_MainTitle => this._ko_Title_Sprite_MainTitle;
    [SerializeField, FoldoutGroup("한글_스프라이트_타이틀"), LabelText("게임시작 버튼")] private Sprite _ko_Title_Sprite_GameStartBtn; public Sprite ko_Title_Sprite_GameStartBtn => this._ko_Title_Sprite_GameStartBtn;
    [SerializeField, FoldoutGroup("한글_스프라이트_타이틀"), LabelText("게임종료 버튼")] private Sprite _ko_Title_Sprite_GameClose; public Sprite ko_Title_Sprite_GameClose => this._ko_Title_Sprite_GameClose;
    [SerializeField, FoldoutGroup("한글_스프라이트_타이틀"), LabelText("이름 선택 후 게임시작")] private Sprite _ko_Title_Sprite_InGameGoBtn; public Sprite ko_Title_Sprite_InGameGoBtn => this._ko_Title_Sprite_InGameGoBtn;

    [SerializeField, FoldoutGroup("한글_스프라이트_인게임"), LabelText("튜토리얼")] private Sprite _ko_InGame_Sprite_TutorialBack; public Sprite ko_InGame_Sprite_TutorialBack => this._ko_InGame_Sprite_TutorialBack;
    [SerializeField, FoldoutGroup("한글_스프라이트_인게임"), LabelText("튜토리얼 시작하기")] private Sprite _ko_InGame_Sprite_TutorialStart; public Sprite ko_InGame_Sprite_TutorialStart => this._ko_InGame_Sprite_TutorialStart;
    [SerializeField, FoldoutGroup("한글_스프라이트_인게임"), LabelText("상단 스코어")] private Sprite _ko_InGame_Sprite_Score; public Sprite ko_InGame_Sprite_Score => this._ko_InGame_Sprite_Score;
    [SerializeField, FoldoutGroup("한글_스프라이트_인게임"), LabelText("그만하기")] private Sprite _ko_InGame_Sprite_Close; public Sprite ko_InGame_Sprite_Close => this._ko_InGame_Sprite_Close;

    [SerializeField, FoldoutGroup("한글_스프라이트_인게임_스코어"), LabelText("텍스트박스")] private Sprite _ko_Result_Sprite_TextBox; public Sprite ko_Result_Sprite_TextBox => this._ko_Result_Sprite_TextBox;
    [SerializeField, FoldoutGroup("한글_스프라이트_인게임_스코어"), LabelText("다시하기")] private Sprite _ko_Result_Sprite_Retry; public Sprite ko_Result_Sprite_Retry => this._ko_Result_Sprite_Retry;
    [SerializeField, FoldoutGroup("한글_스프라이트_인게임_스코어"), LabelText("그만하기")] private Sprite _ko_Result_Sprite_Close; public Sprite ko_Result_Sprite_Close => this._ko_Result_Sprite_Close;


    [SerializeField, FoldoutGroup("영어_스프라이트_타이틀"), LabelText("메인 배경")] private Sprite _en_Title_Sprite_MainBack; public Sprite en_Title_Sprite_MainBack => this._en_Title_Sprite_MainBack;
    [SerializeField, FoldoutGroup("영어_스프라이트_타이틀"), LabelText("메인 제목")] private Sprite _en_Title_Sprite_MainTitle; public Sprite en_Title_Sprite_MainTitle => this._en_Title_Sprite_MainTitle;
    [SerializeField, FoldoutGroup("영어_스프라이트_타이틀"), LabelText("게임시작 버튼")] private Sprite _en_Title_Sprite_GameStartBtn; public Sprite en_Title_Sprite_GameStartBtn => this._en_Title_Sprite_GameStartBtn;
    [SerializeField, FoldoutGroup("영어_스프라이트_타이틀"), LabelText("게임종료 버튼")] private Sprite _en_Title_Sprite_GameClose; public Sprite en_Title_Sprite_GameClose => this._en_Title_Sprite_GameClose;
    [SerializeField, FoldoutGroup("영어_스프라이트_타이틀"), LabelText("이름 선택 후 게임시작")] private Sprite _en_Title_Sprite_InGameGoBtn; public Sprite en_Title_Sprite_InGameGoBtn => this._en_Title_Sprite_InGameGoBtn;

    [SerializeField, FoldoutGroup("영어_스프라이트_인게임"), LabelText("튜토리얼")] private Sprite _en_InGame_Sprite_TutorialBack; public Sprite en_InGame_Sprite_TutorialBack => this._en_InGame_Sprite_TutorialBack;
    [SerializeField, FoldoutGroup("영어_스프라이트_인게임"), LabelText("튜토리얼 시작하기")] private Sprite _en_InGame_Sprite_TutorialStart; public Sprite en_InGame_Sprite_TutorialStart => this._en_InGame_Sprite_TutorialStart;
    [SerializeField, FoldoutGroup("영어_스프라이트_인게임"), LabelText("상단 스코어")] private Sprite _en_InGame_Sprite_Score; public Sprite en_InGame_Sprite_Score => this._en_InGame_Sprite_Score;
    [SerializeField, FoldoutGroup("영어_스프라이트_인게임"), LabelText("그만하기")] private Sprite _en_InGame_Sprite_Close; public Sprite en_InGame_Sprite_Close => this._en_InGame_Sprite_Close;

    [SerializeField, FoldoutGroup("영어_스프라이트_인게임_스코어"), LabelText("텍스트박스")] private Sprite _en_Result_Sprite_TextBox; public Sprite en_Result_Sprite_TextBox => this._en_Result_Sprite_TextBox;
    [SerializeField, FoldoutGroup("영어_스프라이트_인게임_스코어"), LabelText("다시하기")] private Sprite _en_Result_Sprite_Retry; public Sprite en_Result_Sprite_Retry => this._en_Result_Sprite_Retry;
    [SerializeField, FoldoutGroup("영어_스프라이트_인게임_스코어"), LabelText("그만하기")] private Sprite _en_Result_Sprite_Close; public Sprite en_Result_Sprite_Close => this._en_Result_Sprite_Close;

    protected override void Init_Project_Func()
    {
        base.Init_Project_Func();

        /* 런타임 즉시 이 함수가 호출됩니다.
         * 이 스크립트는 덮어쓰이지 않습니다.
         * 임의의 데이터 재가공을 원한다면 이 밑으로 코드를 작성하시면 됩니다.
         */

        this.ReSet_ItemSpeendCoolTime_Func();
    }

    public void ReSet_ItemSpeendCoolTime_Func()
    {
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