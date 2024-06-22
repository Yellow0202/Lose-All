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
    [SerializeField, FoldoutGroup("������"), LabelText("��������")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("���̵���"), LabelText("��������")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [SerializeField, FoldoutGroup("UI"), LabelText("��Ż ���ھ� �ؽ�Ʈ")] private TextMeshProUGUI _ui_TotalScoreText;
    [SerializeField, FoldoutGroup("UI"), LabelText("��Ż �μ� �ǱԾ� �ؽ�Ʈ")] private TextMeshProUGUI _ui_TotalSmashedText;

    [SerializeField, FoldoutGroup("�÷��̾�"), LabelText("�÷��̾�")] private PlayerMove_Script _playerMove_Script; public PlayerMove_Script playerMove_Script => this._playerMove_Script;

    [Button("���")]
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

        //����
        this.Sound_BgnStart_Func();

        //�ؽ�Ʈ
        this.Score_Update_Func();
        this.SmashedScore_Update_Func();

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
        s_GameState = GameState.GameOver;
        Debug.Log("���ӿ���");
    }
}
