using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Script : MonoBehaviour
{
    [LabelText("캐릭터 리지드바디")] private Rigidbody2D _rigid;

    [LabelText("플레이어 이동속독")] private float _moveSpeed => PlayerSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("좌우 입력키 방향값")] private float _horizontal;
    [LabelText("플레이어 이동 방향 벡터")] private Vector2 _playerMoveInput;
    [LabelText("플레이어 이동 방향 벡터 * 스피드")] private Vector2 _playerMoveVelocity;

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

        //애니메이션 변경 얼마 후 다시 돌아옴

        //슬라이딩 시 아이템 습득 판정 범위가 따라 움직여야 함.
        //아이템 습득 여부는 이후 캐치 했는지에서 판단할 것.
    }

    private void Click_PlayerCatch_Func()
    {
        if (Input.GetKeyDown(KeyCode.X) == false)
            return;

        //애니메이션 변경 얼마 후 다시 돌아옴

        //버튼 눌렀을 때 판정 범위 내 오브젝트가 닿았는지 여부를 파악해야 함.
    }
}
