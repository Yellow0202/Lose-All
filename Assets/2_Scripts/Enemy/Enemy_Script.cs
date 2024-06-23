using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;

public class Enemy_Script : MonoBehaviour
{
    //좌우로 돌아다니며 아이템을 떨굼.
    [SerializeField, LabelText("리지드바디")] private Rigidbody2D _rigid;
    [SerializeField, LabelText("애니메이터")] private Animator _anim;
    [SerializeField, LabelText("사촌동생 초기 스케일값")] private Vector3 _enemyStartScale;

    [SerializeField, LabelText("아이템 출발 위치")] private Transform _itemSpawnPoint;

    [LabelText("움직일 방향 벡터")] private Vector2 _movedir;
    [LabelText("움직임 벨로시티 값")] private Vector2 _enemyMoveVelocity;

    [LabelText("아이템 던지기 쿨타임")] private float _curSpawnaTime;
    [LabelText("아이템을 위로 던질지 말지")] private bool is_Up;

    [SerializeField, LabelText("이동속도"), ReadOnly] private float _enemyMoveSpeed => DataBase_Manager.Instance.GetTable_Define.enemy_MoveSpeed;
    [SerializeField, LabelText("좌우 방향"), ReadOnly] private float _horizontal;

    [LabelText("아이템 스폰 코루틴 관리 변수")] private CoroutineData _itemSpawnCorData;


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
                //동작을 취함.
                //던지고 마무리에 아이템 생성.
                //아이템 생성 후 다시금 타이머 가동.

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
