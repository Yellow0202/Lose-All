using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class Enemy_Script : MonoBehaviour
{
    //�¿�� ���ƴٴϸ� �������� ����.
    [SerializeField, LabelText("������ٵ�")] private Rigidbody2D _rigid;
    [SerializeField, LabelText("�ִϸ�����")] private Animator _anim;
    [SerializeField, LabelText("���̵��� �ʱ� �����ϰ�")] private Vector3 _enemyStartScale;

    [SerializeField, LabelText("������ ��� ��ġ")] private Transform _itemSpawnPoint;

    [LabelText("������ ���� ����")] private Vector2 _movedir;
    [LabelText("������ ���ν�Ƽ ��")] private Vector2 _enemyMoveVelocity;

    [LabelText("������ ������ ��Ÿ��")] private float _curSpawnaTime;
    [LabelText("�������� ���� ������ ����")] private bool is_Up;

    [SerializeField, LabelText("�̵��ӵ�"), ReadOnly] private float _enemyMoveSpeed => DataBase_Manager.Instance.GetTable_Define.enemy_MoveSpeed;
    [SerializeField, LabelText("�¿� ����"), ReadOnly] private float _horizontal;

    [LabelText("������ ���� �ڷ�ƾ ���� ����")] private CoroutineData _itemSpawnCorData;


    private void Start()
    {
        this._horizontal = this.RandomMoveDir_Func();
        this._itemSpawnCorData.StartCoroutine_Func(this.ItemSpawn_Cor(), CoroutineStartType.StartWhenStop);
    }

    private void Update()
    {
        if (InGameUISystem_Manager.s_GameState != GameState.Playing)
            return;

        this.Move_EnemyMoving_Func();
    }

    private void FixedUpdate()
    {
        if (InGameUISystem_Manager.s_GameState != GameState.Playing)
            return;

        this._rigid.MovePosition(this._rigid.position + this._enemyMoveVelocity * Time.fixedDeltaTime);
    }

    private void Move_EnemyMoving_Func()
    {
        this._movedir = new Vector2(this._horizontal, 0);

        this._enemyMoveVelocity = this._movedir * _enemyMoveSpeed;
    }

    private IEnumerator ItemSpawn_Cor()
    {
        this._curSpawnaTime = 0;
        float a_Plustime = this.Random_SpawnTime_Func();

        while (true)
        {
            if (InGameUISystem_Manager.s_GameState != GameState.Playing)
                break;

            if (DataBase_Manager.Instance.GetTable_Define.item_Spawn_CoolTime + a_Plustime <= _curSpawnaTime)
            {
                //������ ����.
                //������ �������� ������ ����.
                //������ ���� �� �ٽñ� Ÿ�̸� ����.

                this._curSpawnaTime = 0.0f;
                this._horizontal = 0.0f;
                a_Plustime = this.Random_SpawnTime_Func();

                this.is_Up = Random.Range(0, 2) == 0 ? true : false;

                this._anim.SetBool("Is_On", this.is_Up);
                this._anim.SetTrigger("ItemSpawn");
            }
            else
                this._curSpawnaTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }

    private float Random_SpawnTime_Func()
    {
        return Random.Range(0.0f, 1.0f);
    }

    public void Spawn_ItemSpawn_Func()
    {
        ItemSystem_Manager.Instance.Start_ItemSpawn_Func(this._itemSpawnPoint.position, this.is_Up);
        Coroutine_C.Invoke_Func(() => { this._horizontal = this.RandomMoveDir_Func(); }, DataBase_Manager.Instance.GetTable_Define.enemy_StopTime);
    }

    private int RandomMoveDir_Func()
    {
        int is_Move = Random.Range(0, 3);

        if (is_Move == 0)
        {
            this._anim.SetInteger("Move", 1);

            if (0.0f < this._enemyStartScale.x)
                this._enemyStartScale.x *= -1;

            this.transform.localScale = this._enemyStartScale;

            return 1;
        }
        else if (is_Move == 1)
        {
            this._anim.SetInteger("Move", -1);

            if (this._enemyStartScale.x < 0.0f)
                this._enemyStartScale.x *= -1;

            this.transform.localScale = this._enemyStartScale;

            return -1;
        }
        else if (is_Move == 2)
        {
            this._anim.SetInteger("Move", 0);

            return 0;
        }
        else
            return 1;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Guard")
        {
            this._horizontal *= -1;

            if (this._horizontal == 1)
            {
                if (0.0f < this._enemyStartScale.x)
                    this._enemyStartScale.x *= -1;

                this._anim.SetInteger("Move", 1);
            }
            else if (this._horizontal == -1)
            {
                if (this._enemyStartScale.x < 0.0f)
                    this._enemyStartScale.x *= -1;

                this._anim.SetInteger("Move", -1);
            }
            else
            {
                this._anim.SetInteger("Move", 0);
            }

            this.transform.localScale = this._enemyStartScale;


            if (this._horizontal == 0)
                this._horizontal = this.RandomMoveDir_Func();
        }
    }
}
