using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class InGameUISystem_Manager : MonoBehaviour
{
    public static InGameUISystem_Manager Instance;

    [SerializeField, FoldoutGroup("아이템"), LabelText("스폰지점")] private Transform _itemSpawnPoint; public Transform itemSpawnPoint => _itemSpawnPoint;
    [SerializeField, FoldoutGroup("사촌동생"), LabelText("스폰지점")] private Transform _enemySpawnPoint; public Transform enemySpawnPoint => _enemySpawnPoint;

    [Button("기록")]
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

        //임시
        EnemySystem_Manager.Instance.Spawn_EnemyCharactor_Func();

        //사운드
        this.Sound_BgnStart_Func();
    }

    private void Sound_BgnStart_Func()
    {
        Cargold.FrameWork.SoundSystem_Sfx_Script a_BgmIntro = SoundSystem_Manager.Instance.Get_PlaySfx_Func(SfxType.인게임BGMintro);
        a_BgmIntro.PlayEndToStart_Func(() => 
        {
            SoundSystem_Manager.Instance.PlayBgm_Func(BgmType.인게임BGMLoop);
        });
    }
}
