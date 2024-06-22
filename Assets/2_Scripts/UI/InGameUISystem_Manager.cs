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
    }
}
