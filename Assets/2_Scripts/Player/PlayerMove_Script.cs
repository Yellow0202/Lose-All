using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Script : MonoBehaviour
{
    [SerializeField, LabelText("ĳġ����Ʈ ������Ʈ")] private Transform _chtchPointTr;

    [LabelText("ĳ���� ������ٵ�")] private Rigidbody2D _rigid;
    [LabelText("ĳ���� �ִϸ�����")] private Animator _anim;
    [LabelText("�÷��̾� �̵��ӵ�")] private float _moveSpeed => PlayerSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("�¿� �Է�Ű ���Ⱚ")] private float _horizontal;
    [LabelText("�÷��̾� �̵� ���� ����")] private Vector2 _playerMoveInput;
    [LabelText("�÷��̾� �̵� ���� ���� * ���ǵ�")] private Vector2 _playerMoveVelocity;
    public void Start_PlayerMove_Func()
    {
        this._rigid = gameObject.GetComponent<Rigidbody2D>();
        this._anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        this.Move_InputKey_Func();
        this.Move_PlayerMoving_Func();
        this.Click_PlayerSliding_Func();
        this.Click_PlayerCatch_Func();
    }

    private void FixedUpdate()
    {
        this._rigid.MovePosition(this._rigid.position + this._playerMoveVelocity * Time.fixedDeltaTime);
    }

    private void Move_InputKey_Func()
    {
        if(Input.GetKeyDown(KeyCode.X) == true)
        {
            this._anim.SetBool("Catch_On", true);
        }

        if (Input.GetKeyUp(KeyCode.X) == true)
        {
            this._anim.SetBool("Catch_On", false);
        }

        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            //this._anim.SetBool("Catch_On", true);
        }

        if (Input.GetKeyUp(KeyCode.Z) == true)
        {
            //this._anim.SetBool("Catch_On", false);
        }

    }

    private void Move_PlayerMoving_Func()
    {
        this._horizontal = Input.GetAxis("Horizontal");

        this._anim.SetFloat("Move", this._horizontal);

        this._playerMoveInput = new Vector2(this._horizontal, 0);
        this._playerMoveVelocity = _playerMoveInput * _moveSpeed;
    }

    private void Click_PlayerSliding_Func()
    {
        if (Input.GetKeyDown(KeyCode.Z) == false)
            return;

        //�ִϸ��̼� ���� �� �� �ٽ� ���ƿ�

        //�����̵� �� ������ ���� ���� ������ ���� �������� ��.
        //������ ���� ���δ� ���� ĳġ �ߴ������� �Ǵ��� ��.
    }

    private void Click_PlayerCatch_Func()
    {
        if (Input.GetKeyDown(KeyCode.X) == false)
            return;

        //�ִϸ��̼� ���� �� �� �ٽ� ���ƿ�

        //��ư ������ �� ���� ���� �� ������Ʈ�� ��Ҵ��� ���θ� �ľ��ؾ� ��.
    }

    public Transform Get_ChtchPointTr_Func()
    {
        return this._chtchPointTr;
    }
}
