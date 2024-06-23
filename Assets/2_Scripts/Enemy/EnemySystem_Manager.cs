using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public class EnemySystem_Manager : SerializedMonoBehaviour, Cargold.FrameWork.GameSystem_Manager.IInitializer
{
    public static EnemySystem_Manager Instance;

    [SerializeField, LabelText("���̵��� ������")] private GameObject _enemyPrefab;
    [SerializeField, LabelText("���̵��� ���� ����"), ReadOnly] private int _enemySpawnCondition => DataBase_Manager.Instance.GetTable_Define.enemy_SpawnCondition;
    [SerializeField, LabelText("���� ����"), ReadOnly] private int _curEnemySpawnCondition;

    [SerializeField, LabelText("��ȯ�� ���̵��� ��"), ReadOnly] private int _curSpawnEnemyCount;

    //2�� 5õ���� ���������� ã��. �������� 0.2�� ����. ���̵����� ��ȯ�Ǹ� ���� �ݾ��� 5õ�� �پ��.
    //�ִ� 1�ʱ��� ����. �ִ� ���Ҵ� DB���� ������ ��.
    [SerializeField, LabelText("��ô�ð� ���ҿ� ���� �ݾ�"), ReadOnly] private int _curGetScoreValue;
    [SerializeField, LabelText("���� ��ô�ð� ���� �ݾ�"), ReadOnly] private int _curItemSpawnCoolTimeDownScore;

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

    [Button("���� ��ȯ")]
    public void Spawn_EnemyCharactor_Func()
    {   //���̵��� ��ȯ.
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
