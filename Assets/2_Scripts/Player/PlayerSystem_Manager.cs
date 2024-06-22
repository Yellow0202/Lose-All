using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public enum ChtchBalloon
{
    miss,
    good,
    perfect
}

public class PlayerSystem_Manager : SerializedMonoBehaviour, Cargold.FrameWork.GameSystem_Manager.IInitializer
{
    public static PlayerSystem_Manager Instance;

    [LabelText("�÷��̾� ������ ��ũ��Ʈ")] private PlayerMove_Script _playerMove;

    [LabelText("ǳ�� �ִϸ��̼�")] private Animation _balloonAnim;
    [LabelText("ǳ�� ��������Ʈ ������")] private SpriteRenderer _balloonSpriteRenderer;

    [SerializeField, LabelText("ǳ�� ��������Ʈ ��ųʸ�")] private Dictionary<ChtchBalloon, Sprite> chtchBalloonToSpriteDataDic;
    [SerializeField, FoldoutGroup("�÷��̾� ����"), LabelText("�÷��̾� �̵��ӵ�")] private float _player_MoveSpeed; public float player_MoveSpeed => this._player_MoveSpeed;

    public void Init_Func(int _layer)
    {
        if(_layer == 0)
        {
            Instance = this;
        }
        else if(_layer == 1)
        {

        }
        else if(_layer == 2)
        {

        }
    }

    public void Start_PlayerSystem_Manger()
    {
        this._playerMove = InGameUISystem_Manager.Instance.playerMove_Script;
        this._playerMove.Start_PlayerMove_Func();

        this.PlayerMoveScriptToValue_Func();
    }

    public void PlayerMoveScriptToValue_Func()
    {
        this._balloonAnim = this._playerMove.main_r_Obj.GetComponent<Animation>();
        this._balloonSpriteRenderer = this._playerMove.main_r_Obj.GetComponent<SpriteRenderer>();
    }

    public void Call_ChtchBalloon_Func(ChtchBalloon a_State)
    {
        Sprite a_sprite = null;

        if(a_State == ChtchBalloon.miss)
        {
            a_sprite = this.chtchBalloonToSpriteDataDic.GetValue_Func(ChtchBalloon.miss);
        }
        else if(a_State == ChtchBalloon.good)
        {
            a_sprite = this.chtchBalloonToSpriteDataDic.GetValue_Func(ChtchBalloon.good);
        }
        else
        {
            a_sprite = this.chtchBalloonToSpriteDataDic.GetValue_Func(ChtchBalloon.perfect);
        }

        this._balloonSpriteRenderer.sprite = a_sprite;
        this._balloonAnim.Play("Main_r_On_Anim");
    }
}
