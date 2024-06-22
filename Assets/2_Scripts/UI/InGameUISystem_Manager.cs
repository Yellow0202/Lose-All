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
    [SerializeField, FoldoutGroup("������"), LabelText("��������")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("������"), LabelText("�¿��� ��ųʸ� ����Ʈ")] private Dictionary<int, List<GameObject>> _intKeyToGameObjectListDataDic; public Dictionary<int, List<GameObject>> intKeyToGameObjectListDataDic => this._intKeyToGameObjectListDataDic;
    [SerializeField, FoldoutGroup("������"), LabelText("���� �ͽ� ����")] private List<Sprite> _ghostSpriteList; public List<Sprite> ghostSpriteList => this._ghostSpriteList;

    [SerializeField, FoldoutGroup("���̵���"), LabelText("��������")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [SerializeField, FoldoutGroup("UI"), LabelText("��Ż ���ھ� �ؽ�Ʈ")] private TextMeshProUGUI _ui_TotalScoreText;
    [SerializeField, FoldoutGroup("UI"), LabelText("��Ż �μ� �ǱԾ� �ؽ�Ʈ")] private TextMeshProUGUI _ui_TotalSmashedText;
    [SerializeField, FoldoutGroup("UI"), LabelText("�÷��̾� ���ھ� �� ���� ���ӿ�����Ʈ")] private GameObject _ui_PlayerScoreInfoObj;
    [SerializeField, FoldoutGroup("UI"), LabelText("ī��Ʈ �ٿ� �ؽ�Ʈ �޽�")] private TextMeshProUGUI _ui_CountDownText;

    [SerializeField, FoldoutGroup("�÷��̾�"), LabelText("�÷��̾�")] private PlayerMove_Script _playerMove_Script; public PlayerMove_Script playerMove_Script => this._playerMove_Script;

    [SerializeField, FoldoutGroup("��ƼŬ"), LabelText("������ ������Ʈ �¿��� ��Ÿ ��ƼŬ")] private ParticleSystem _particle_ItemObjOn; public ParticleSystem particle_ItemObjOn => this._particle_ItemObjOn;

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

        //��������
        this._ui_PlayerScoreInfoObj.SetActive(true);

        //����
        this.Sound_BgnStart_Func();

        //�ؽ�Ʈ
        this.Score_Update_Func();
        this.SmashedScore_Update_Func();

        //�÷��̾� �̸� ���������Ϳ� ����
        UserSystem_Manager.Instance.loginData.Set_UserNameData_Func(ProjectBackEnd_Manager.s_playerName);

        s_GameState = GameState.Playing;
    }

    private void Sound_BgnStart_Func()
    {
        SoundChild_Script.Instance.Start_InGameBgmSound_Func(BgmType.�ΰ���BGMintro);
    }

    public void Score_Update_Func()
    {
        this._ui_TotalScoreText.text = UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func().ToString("N0") + "��";
    }

    public void SmashedScore_Update_Func()
    {
        this._ui_TotalSmashedText.text = UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func().ToString("N0") + "��(" + UserSystem_Manager.Instance.playInfo.Get_SmashedItemCountPlayInfo_Func().ToString() + "��)";

        if (DataBase_Manager.Instance.GetTable_Define.gameOverScoreMax <= UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func() ||
            DataBase_Manager.Instance.GetTable_Define.gameOverCountMax <= UserSystem_Manager.Instance.playInfo.Get_SmashedItemCountPlayInfo_Func())
        {
            //���� ���� ������.
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

        //���� �ִϸ��̼� ȣ��.
        //���� �ִϸ��̼� ���� �� ��ŷ ����. ���� �� ��ŷ �ִϸ��̼� ȣ��
        UI_Result.Instance.Result_RankingCall_Func();
    }
}
