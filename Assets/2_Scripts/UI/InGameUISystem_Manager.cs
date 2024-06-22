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

public class InGameUISystem_Manager : SerializedMonoBehaviour
{
    public static InGameUISystem_Manager Instance;
    public static GameState s_GameState = GameState.Start;
    [SerializeField, FoldoutGroup("아이템"), LabelText("스폰지점")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("아이템"), LabelText("온오프 딕셔너리 리스트")] private Dictionary<int, List<GameObject>> _intKeyToGameObjectListDataDic; public Dictionary<int, List<GameObject>> intKeyToGameObjectListDataDic => this._intKeyToGameObjectListDataDic;
    [SerializeField, FoldoutGroup("아이템"), LabelText("랜덤 귀신 사진")] private List<Sprite> _ghostSpriteList; public List<Sprite> ghostSpriteList => this._ghostSpriteList;

    [SerializeField, FoldoutGroup("사촌동생"), LabelText("스폰지점")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [SerializeField, FoldoutGroup("UI"), LabelText("토탈 스코어 텍스트")] private TextMeshProUGUI _ui_TotalScoreText;
    [SerializeField, FoldoutGroup("UI"), LabelText("토탈 부순 피규어 텍스트")] private TextMeshProUGUI _ui_TotalSmashedText;
    [SerializeField, FoldoutGroup("UI"), LabelText("플레이어 스코어 등 정보 게임오브젝트")] private GameObject _ui_PlayerScoreInfoObj;
    [SerializeField, FoldoutGroup("UI"), LabelText("카운트 다운 텍스트 메쉬")] private TextMeshProUGUI _ui_CountDownText;

    [SerializeField, FoldoutGroup("플레이어"), LabelText("플레이어")] private PlayerMove_Script _playerMove_Script; public PlayerMove_Script playerMove_Script => this._playerMove_Script;

    [SerializeField, FoldoutGroup("파티클"), LabelText("아이템 오브젝트 온오프 스타 파티클")] private ParticleSystem _particle_ItemObjOn; public ParticleSystem particle_ItemObjOn => this._particle_ItemObjOn;

    private void Awake()
    {
        Instance = this;
    }



    public void Start_ManagerFuncs_Func()
    {
        StartCoroutine(Start_CountDown_Cor());
    }

    private IEnumerator Start_CountDown_Cor()
    {
        this._ui_CountDownText.gameObject.SetActive(true);

        this._ui_CountDownText.text = "3";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._ui_CountDownText.text = "2";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._ui_CountDownText.text = "1";
        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._ui_CountDownText.text = "START";

        yield return Coroutine_C.GetWaitForSeconds_Cor(1.0f);
        this._ui_CountDownText.gameObject.SetActive(false);
        this.Setting_StartValue_Func();

        yield return null;
        StopCoroutine(Start_CountDown_Cor());
    }

    public void Setting_StartValue_Func()
    {
        EnemySystem_Manager.Instance.Spawn_EnemyCharactor_Func();
        PlayerSystem_Manager.Instance.Start_PlayerSystem_Manger();

        //게임정보
        this._ui_PlayerScoreInfoObj.SetActive(true);

        //사운드
        this.Sound_BgnStart_Func();

        //텍스트
        this.Score_Update_Func();
        this.SmashedScore_Update_Func();

        //플레이어 이름 유저데이터에 저장
        UserSystem_Manager.Instance.loginData.Set_UserNameData_Func(ProjectBackEnd_Manager.s_playerName);

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
        if (s_GameState == GameState.GameOver)
            return;

        s_GameState = GameState.GameOver;

        int a_TotalScore = UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func() - UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func();
        UserSystem_Manager.Instance.loginData.Set_UserScoreData_Func(a_TotalScore);

        //연출 애니메이션 호출.
        //연출 애니메이션 종료 후 랭킹 갱신. 갱신 후 랭킹 애니메이션 호출
        UI_Result.Instance.Result_RankingCall_Func();
    }
}
