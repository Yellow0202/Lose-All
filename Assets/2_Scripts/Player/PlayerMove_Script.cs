using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Script : MonoBehaviour
{
    [SerializeField, LabelText("ĳġ����Ʈ ������Ʈ")] private Transform _chtchPointTr;
    [SerializeField, LabelText("ĳ���� ��������Ʈ ������")] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField, LabelText("ĳ���� ��� ����Ʈ ǳ��")] private GameObject _main_r_Obj; public GameObject main_r_Obj => this._main_r_Obj;

    [SerializeField, LabelText("�÷��̾� �����̵� ���ӵ�")] private int _moveboost;

    [SerializeField, LabelText("�����̵� ��ƼŬ")] private ParticleSystem _slidingParticle;

    [LabelText("ĳ���� ������ٵ�")] private Rigidbody2D _rigid;
    [LabelText("ĳ���� �ִϸ�����")] private Animator _anim;
    [LabelText("�÷��̾� �̵��ӵ�")] private float _moveSpeed => PlayerSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("�¿� �Է�Ű ���Ⱚ")] private float _horizontal;
    [LabelText("�÷��̾� �̵� ���� ����")] private Vector2 _playerMoveInput;
    [LabelText("�÷��̾� �̵� ���� ���� * ���ǵ�")] private Vector2 _playerMoveVelocity;
    [LabelText("�̵� ����")] private bool is_MoveOn = true;
    [LabelText("�����̵� ��")] private bool is_SlidingOn = false;

    [LabelText("�÷��̾� ������ �ʱⰪ")] private Vector3 _playerStartScale;

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
