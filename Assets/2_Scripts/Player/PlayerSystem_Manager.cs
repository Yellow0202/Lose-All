using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;
using Cargold.FrameWork;

public class PlayerSystem_Manager : SerializedMonoBehaviour, Cargold.FrameWork.GameSystem_Manager.IInitializer
{
    public static PlayerSystem_Manager Instance;
    [SerializeField, FoldoutGroup("플레이어 정보"), LabelText("플레이어 이동속도")] private float _player_MoveSpeed; public float player_MoveSpeed => this._player_MoveSpeed;

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
        InGameUISystem_Manager.Instance.playerMove_Script.Start_PlayerMove_Func();
    }
}
