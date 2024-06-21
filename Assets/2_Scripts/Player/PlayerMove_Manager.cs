using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Manager : MonoBehaviour
{
    [LabelText("ĳ���� ������ٵ�")] private Rigidbody2D _rigid;

    [LabelText("�÷��̾� �̵��ӵ�")] private float _moveSpeed => Game.Instance.player_MoveSpeed;
    [LabelText("�¿� �Է�Ű ���Ⱚ")] private float _horizontal;
    [LabelText("�÷��̾� �̵� ���� ����")] private Vector2 _playerMoveInput;
    [LabelText("�÷��̾� �̵� ���� ���� * ���ǵ�")] private Vector2 _playerMoveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        this._rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move_PlayerMoving_Func();
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
}
