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

    [SerializeField, LabelText("소환된 사촌동생 수"), ReadOnly] private int _curSpawnEnemyCount;

    //2만 5천원을 벌었는지를 찾고. 벌었으면 0.2초 감소. 사촌동생이 소환되면 조건 금액이 5천원 줄어듦.
    //최대 1초까지 감소. 최대 감소는 DB에서 조정할 것.
    [SerializeField, LabelText("투척시간 감소용 누적 금액"), ReadOnly] private int _curGetScoreValue;
    [SerializeField, LabelText("현재 투척시간 감소 금액"), ReadOnly] private int _curItemSpawnCoolTimeDownScore;

    public void Init_Func(int _layer)
    {
        if (_layer == 0)
        {
            Instance = this;
            this.ReSet_Enemy_Func();
        }
        else if (_layer == 1)
        {

        }
        else if (_layer == 2)
        {
            this._curEnemySpawnCondition = this._enemySpawnCondition;
            this._curItemSpawnCoolTimeDownScore = DataBase_Manager.Instance.GetTable_Define.difficulty_SpawnCoolTimeDownCondition;
        }
    }

    [Button("사촌 소환")]
    public void Spawn_EnemyCharactor_Func()
    {   //사촌동생 소환.
        GameObject a_NewEnemy = GameObject.Instantiate(_enemyPrefab);
        a_NewEnemy.transform.position = InGameUISystem_Manager.Instance.enemySpawnPoint.position;
        a_NewEnemy.transform.SetParent(InGameUISystem_Manager.Instance.enemySpawnPoint);

        this._curSpawnEnemyCount++;
    }

    public void ReSet_Enemy_Func()
    {
        this._curGetScoreValue = 0;
        this._curSpawnEnemyCount = 0;
        this._curEnemySpawnCondition = this._enemySpawnCondition;
        this._curItemSpawnCoolTimeDownScore = DataBase_Manager.Instance.GetTable_Define.difficulty_SpawnCoolTimeDownCondition;
    }

    public void Check_EnemySpawnCondition_Func()
    {
        if (DataBase_Manager.Instance.GetTable_Define.enemy_SpawnMaxCount <= this._curSpawnEnemyCount)
            return;

        int a_CurScore = UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func();

        if(this._curEnemySpawnCondition  <= a_CurScore)
        {
            this._curEnemySpawnCondition += this._enemySpawnCondition;
            this.Upgrade_CurGetScoreToItemCoolTimeDown_Func();
            this.Spawn_EnemyCharactor_Func();
        }
    }

    private void Upgrade_CurGetScoreToItemCoolTimeDown_Func()
    {
        this._curItemSpawnCoolTimeDownScore -= DataBase_Manager.Instance.GetTable_Define.difficulty_SpawnCoolTimeDownConditionDownScore;
        this._curGetScoreValue = 0;
    }

    public void Check_CurGetScoreToItemCoolTimeDown_Func(int a_GetScore)
    {
        this._curGetScoreValue += a_GetScore;

        if(this._curItemSpawnCoolTimeDownScore <= this._curGetScoreValue)
        {
            this._curGetScoreValue -= this._curItemSpawnCoolTimeDownScore;
            DataBase_Manager.Instance.GetTable_Define.Item_SpawnCoolTimeDown_Func();
        }
    }
}
