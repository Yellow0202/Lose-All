using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using TMPro;

public enum GameState
{
    Start,
    Playing,
    GameOver
}

public class InGameUISystem_Manager : MonoBehaviour
{
    public static InGameUISystem_Manager Instance;
    public static GameState s_GameState = GameState.Start;
    [SerializeField, FoldoutGroup("아이템"), LabelText("스폰지점")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("사촌동생"), LabelText("스폰지점")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [SerializeField, FoldoutGroup("UI"), LabelText("토탈 스코어 텍스트")] private TextMeshProUGUI _ui_TotalScoreText;
    [SerializeField, FoldoutGroup("UI"), LabelText("토탈 부순 피규어 텍스트")] private TextMeshProUGUI _ui_TotalSmashedText;

    [SerializeField, FoldoutGroup("플레이어"), LabelText("플레이어")] private PlayerMove_Script _playerMove_Script; public PlayerMove_Script playerMove_Script => this._playerMove_Script;

    [Button("기록")]
    private void abc()
    {
        ProjectBackEnd_Manager.Instance.RangkUpdate_Func();
    }

    private void Awake()
    {
        Instance = this;
    }



    public void Start_ManagerFuncs_Func()
    {
        EnemySystem_Manager.Instance.Spawn_EnemyCharactor_Func();
        PlayerSystem_Manager.Instance.Start_PlayerSystem_Manger();

        //사운드
        this.Sound_BgnStart_Func();

        //텍스트
        this.Score_Update_Func();
        this.SmashedScore_Update_Func();

        s_GameState = GameState.Playing;
    }

    private void Sound_BgnStart_Func()
    {
        SoundChild_Script.Instance.Start_InGameBgmSound_Func(BgmType.인게임BGMintro);
    }

    public void Score_Update_Func()
    {
        this._ui_TotalScoreText.text = UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func().ToString("N0") + "원";
    }

    public void SmashedScore_Update_Func()
    {
        this._ui_TotalSmashedText.text = UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func().ToString("N0") + "원(" + UserSystem_Manager.Instance.playInfo.Get_SmashedItemCountPlayInfo_Func().ToString() + "개)";

        if (DataBase_Manager.Instance.GetTable_Define.gameOverScoreMax <= UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func() ||
            DataBase_Manager.Instance.GetTable_Define.gameOverCountMax <= UserSystem_Manager.Instance.playInfo.Get_SmashedItemCountPlayInfo_Func())
        {
            //게임 오버 상태임.
            this.GameOver_ManagerFuncs_Func();
        }
    }

    private void GameOver_ManagerFuncs_Func()
    {
        s_GameState = GameState.GameOver;
        Debug.Log("게임오버");
    }
}
