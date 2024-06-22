using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUISystem_Manager : MonoBehaviour
{
    public static InGameUISystem_Manager Instance;

    [SerializeField, FoldoutGroup("������"), LabelText("��������")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("���̵���"), LabelText("��������")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [Button("���")]
    private void abc()
    {
        ProjectBackEnd_Manager.Instance.RangkUpdate_Func();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.Start_ManagerFuncs_Func();
    }

    private void Start_ManagerFuncs_Func()
    {
        //ItemSystem_Manager.Instance.Start_ItemSpawn_Func();

        //�ӽ�
        EnemySystem_Manager.Instance.Spawn_EnemyCharactor_Func();

        //����
        this.Sound_BgnStart_Func();
    }

    private void Sound_BgnStart_Func()
    {
        Cargold.FrameWork.SoundSystem_Sfx_Script a_BgmIntro = SoundSystem_Manager.Instance.Get_PlaySfx_Func(SfxType.�ΰ���BGMintro);
        a_BgmIntro.PlayEndToStart_Func(() => 
        {
            SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.�ΰ���BGMLoop);
        });
    }
}
