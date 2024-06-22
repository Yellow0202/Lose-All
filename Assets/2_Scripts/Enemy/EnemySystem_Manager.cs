using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public class EnemySystem_Manager : SerializedMonoBehaviour, Cargold.FrameWork.GameSystem_Manager.IInitializer
{
    public static EnemySystem_Manager Instance;

    [SerializeField, LabelText("사촌동생 프리팹")] private GameObject _enemyPrefab;
    [SerializeField, LabelText("사촌동생 생성 조건"), ReadOnly] private int _enemySpawnCondition => DataBase_Manager.Instance.GetTable_Define.enemy_SpawnCondition;
    [SerializeField, LabelText("현재 조건"), ReadOnly] private int _curEnemySpawnCondition;

    public void Init_Func(int _layer)
    {
        if (_layer == 0)
        {
            Instance = this;
        }
        else if (_layer == 1)
        {

        }
        else if (_layer == 2)
        {
            this._curEnemySpawnCondition = this._enemySpawnCondition;
        }
    }

    public void Spawn_EnemyCharactor_Func()
    {   //사촌동생 소환.
        GameObject a_NewEnemy = GameObject.Instantiate(_enemyPrefab);
        a_NewEnemy.transform.position = InGameUISystem_Manager.Instance.enemySpawnPoint.position;
        a_NewEnemy.transform.SetParent(InGameUISystem_Manager.Instance.enemySpawnPoint);
    }

    public void Check_EnemySpawnCondition_Func()
    {
        int a_CurScore = UserSystem_Manager.Instance.playInfo.Get_ScrorePlayInfo_Func();

        if(this._curEnemySpawnCondition  <= a_CurScore)
        {
            this._curEnemySpawnCondition += this._enemySpawnCondition;
            this.Spawn_EnemyCharactor_Func();
        }
    }
}
