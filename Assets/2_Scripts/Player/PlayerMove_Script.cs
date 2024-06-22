using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Script : MonoBehaviour
{
    [SerializeField, LabelText("캐치포인트 오브젝트")] private Transform _chtchPointTr;
    [SerializeField, LabelText("캐릭터 스프라이트 렌더러")] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField, LabelText("캐릭터 상단 퍼펙트 풍선")] private GameObject _main_r_Obj; public GameObject main_r_Obj => this._main_r_Obj;

    [SerializeField, LabelText("플레이어 슬라이딩 가속도")] private int _moveboost;

    [SerializeField, LabelText("슬라이드 파티클")] private ParticleSystem _slidingParticle;

    [LabelText("캐릭터 리지드바디")] private Rigidbody2D _rigid;
    [LabelText("캐릭터 애니메이터")] private Animator _anim;
    [LabelText("플레이어 이동속도")] private float _moveSpeed => PlayerSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("좌우 입력키 방향값")] private float _horizontal;
    [LabelText("플레이어 이동 방향 벡터")] private Vector2 _playerMoveInput;
    [LabelText("플레이어 이동 방향 벡터 * 스피드")] private Vector2 _playerMoveVelocity;
    [LabelText("이동 금지")] private bool is_MoveOn = true;
    [LabelText("슬라이딩 중")] private bool is_SlidingOn = false;

    [LabelText("플레이어 스케일 초기값")] private Vector3 _playerStartScale;

    public void Start_PlayerMove_Func()
    {
        this._rigid = gameObject.GetComponent<Rigidbody2D>();
        this._anim = gameObject.GetComponent<Animator>();

        this._playerStartScale = this._playerSpriteRenderer.transform.localScale;
    }

    void Update()
    {
        if (InGameUISystem_Manager.s_GameState != GameState.Playing)
            return;

        this.Move_InputKey_Func();
        this.Move_PlayerMoving_Func();
    }

    private void FixedUpdate()
    {
        if (InGameUISystem_Manager.s_GameState != GameState.Playing)
            return;

        if (this.is_MoveOn == true)
            this._rigid.MovePosition(this._rigid.position + this._playerMoveVelocity * Time.fixedDeltaTime);
        else if (this.is_SlidingOn == true)
            this._rigid.velocity = this._playerMoveInput.normalized * (this._moveSpeed * _moveboost) * Time.fixedDeltaTime;
        else
            this._rigid.velocity = Vector2.zero;
    }

    private void Move_InputKey_Func()
    {
        if (this.is_SlidingOn == true)
            return;

        if (Input.GetKeyDown(KeyCode.X) == true)
        {
            this._anim.SetBool("Catch_On", true);
            this.is_MoveOn = false;
        }

        if (Input.GetKeyUp(KeyCode.X) == true)
        {
            this._anim.SetBool("Catch_On", false);
            this.is_MoveOn = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) == true && this.is_MoveOn == true)
        {
            this._anim.SetTrigger("Silde_On");
            this.is_SlidingOn = true;

            this._slidingParticle.transform.position = new Vector2(this.transform.position.x, this._slidingParticle.transform.position.y);
            this._slidingParticle.Play();
        }
    }

    private void Move_PlayerMoving_Func()
    {
        if (this.is_MoveOn == false || this.is_SlidingOn == true)
            return;

        this._horizontal = Input.GetAxisRaw("Horizontal");

        if(0.1f < this._horizontal)
        {
            this._anim.SetFloat("Move", this._horizontal);
            if (0.0f < this._playerStartScale.x)
                this._playerStartScale.x *= -1;

            this._playerSpriteRenderer.transform.localScale = this._playerStartScale;


        }
        else if(this._horizontal < -0.1f)
        {
            this._anim.SetFloat("Move", this._horizontal * -1);
            if (this._playerStartScale.x < 0.0f)
                this._playerStartScale.x *= -1;

            this._playerSpriteRenderer.transform.localScale = this._playerStartScale;
        }
        else
        {
            this._anim.SetFloat("Move", 0);
        }

        this._playerMoveInput = new Vector2(this._horizontal, 0);
        this._playerMoveVelocity = _playerMoveInput * _moveSpeed;
    }

    public void Call_InputSliding_Func()
    {
        this._anim.SetBool("Silde_On", false);
        this.is_MoveOn = true;
        this.is_SlidingOn = false;
    }

    public Transform Get_ChtchPointTr_Func()
    {
        return this._chtchPointTr;
    }
}
