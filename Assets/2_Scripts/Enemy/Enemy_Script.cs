using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class Enemy_Script : MonoBehaviour
{
    //�¿�� ���ƴٴϸ� �������� ����.
    [SerializeField, LabelText("������ٵ�")] private Rigidbody2D _rigid;

    [LabelText("������ ���� ����")] private Vector2 _movedir;
    [LabelText("������ ���ν�Ƽ ��")] private Vector2 _enemyMoveVelocity;
    [SerializeField, LabelText("�̵��ӵ�"), ReadOnly] private float _enemyMoveSpeed => DataBase_Manager.Instance.GetTable_Define.enemy_MoveSpeed;
    [LabelText("�¿� ����")] private float _horizontal;

    [LabelText("������ ���� �ڷ�ƾ ���� ����")] private CoroutineData _itemSpawnCorData;

    private void Start()
    {
        this._horizontal = 1;
        this._itemSpawnCorData.StartCoroutine_Func(this.ItemSpawn_Cor(), CoroutineStartType.StartWhenStop);
    }

    private void Update()
    {
        this.Move_EnemyMoving_Func();
    }

    private void FixedUpdate()
    {
        this._rigid.MovePosition(this._rigid.position + this._enemyMoveVelocity * Time.fixedDeltaTime);
    }

    private void Move_EnemyMoving_Func()
    {
        this._movedir = new Vector2(this._horizontal, 0);

        this._enemyMoveVelocity = this._movedir * _enemyMoveSpeed;
    }

    private IEnumerator ItemSpawn_Cor()
    {
        float a_CurSpawnaTime = DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime;

        while(true)
        {
            if (DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime <= a_CurSpawnaTime)
            {
                a_CurSpawnaTime = 0.0f;
                ItemSystem_Manager.Instance.Start_ItemSpawn_Func(this.transform.position);
            }
            else
                a_CurSpawnaTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Guard")
        {
            this._horizontal *= -1;
        }
    }
}
