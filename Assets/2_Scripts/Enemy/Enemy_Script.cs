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
    [SerializeField, LabelText("�¿� ����"), ReadOnly] private float _horizontal;

    [SerializeField, LabelText("���� ���� ��Ҵ���"), ReadOnly] private bool is_GuardEnter = false;

    [LabelText("������ ���� �ڷ�ƾ ���� ����")] private CoroutineData _itemSpawnCorData;

    private void Start()
    {
        this._horizontal = this.RandomMoveDir_Func();
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
        float a_MoveDir = 0.0f;

        while(true)
        {
            if (DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime <= a_CurSpawnaTime)
            {
                a_MoveDir = this._horizontal;

                if(a_MoveDir == 0)
                    a_MoveDir = this.RandomMoveDir_Func();

                if (this.is_GuardEnter == true)
                    a_MoveDir *= -1;

                this._horizontal = 0.0f;

                a_CurSpawnaTime = 0.0f;
                ItemSystem_Manager.Instance.Start_ItemSpawn_Func(this.transform.position);

                Coroutine_C.Invoke_Func(() => { this._horizontal = a_MoveDir; }, DataBase_Manager.Instance.GetTable_Define.enemy_StopTime);
            }
            else
                a_CurSpawnaTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }

    private int RandomMoveDir_Func()
    {
        bool is_Rigth = Random.Range(0, 2) == 1 ? true : false;

        if (is_Rigth == true)
            return 1;
        else
            return -1;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Guard")
        {
            this.is_GuardEnter = true;
            this._horizontal *= -1;

            if (this._horizontal == 0)
                this._horizontal = this.RandomMoveDir_Func();

            //���� �ε����� �� 0�̾�� �ݴ�� ���� �ʴ� ��찡 ����. 0���� ������ ������? ��������.
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Guard")
        {
            this.is_GuardEnter = false;
        }
    }
}
