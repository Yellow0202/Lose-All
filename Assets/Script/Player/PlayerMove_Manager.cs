using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove_Manager : MonoBehaviour
{
    [LabelText("캐릭터 컨트롤러")] private CharacterController _characterController;

    [LabelText("플레이어 이동속독")] private float _moveSpeed => GameSystem_Manager.Instance.player_MoveSpeed;
    [LabelText("좌우 입력키 방향값")] private float _horizontal;
    [LabelText("플레이어 이동 방향 벡터")] private Vector3 _playerMoveVec;

    // Start is called before the first frame update
    void Start()
    {
        this._characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move_PlayerMoving_Func();
    }

    private void Move_PlayerMoving_Func()
    {
        this._horizontal = Input.GetAxis("Horizontal");

        this._playerMoveVec = transform.TransformDirection(new Vector3(this._horizontal, 0, 0));

        this._characterController.Move(this._playerMoveVec * Time.deltaTime * this._moveSpeed);
    }
}
