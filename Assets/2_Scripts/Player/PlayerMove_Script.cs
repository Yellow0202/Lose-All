using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Script : MonoBehaviour
{
    [LabelText("ĳ���� ������ٵ�")] private Rigidbody2D _rigid;

    [LabelText("�÷��̾� �̵��ӵ�")] private float _moveSpeed => PlayerSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("�¿� �Է�Ű ���Ⱚ")] private float _horizontal;
    [LabelText("�÷��̾� �̵� ���� ����")] private Vector2 _playerMoveInput;
    [LabelText("�÷��̾� �̵� ���� ���� * ���ǵ�")] private Vector2 _playerMoveVelocity;

    public void Start_PlayerMove_Func()
    {
        this._rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move_PlayerMoving_Func();
        this.Click_PlayerSliding_Func();
        this.Click_PlayerCatch_Func();
    }

    private void FixedUpdate()
    {
        this._rigid.MovePosition(this._rigid.position + this._playerMoveVelocity * Time.fixedDeltaTime);
    }

    private void Move_PlayerMoving_Func()
    {
        this._horizontal = Input.GetAxis("Horizontal");

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
}
