using Cargold.SDK.BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectBackEnd_Manager : BackEnd_Manager
{
    public static ProjectBackEnd_Manager Instance;

    public override void Init_Func(int _layer)
    {
        base.Init_Func(_layer);

        if (_layer == 0)
        {
            Instance = this;
        }
        else if(_layer == 1)
        {

        }
        else if (_layer == 2)
        {
            this.Login_Func("LODWK", "12345");
        }
    }

    //회원가입 -> 로그인 -> 닉네임 변경 -> 랭크 기입 -> 랭크 출력
    public void Login_Func(string _id, string _password)
    {
        this.OnSignUp_Func(_id, _password);
        this.OnLogin_Func(_id, _password);
        this.SetNickname_Func(_id);
    }

    public void RangkUpdate_Func()
    {
        this.SetRank_Func(this.rankUuid, this.tableName, this.gameDataColumn, UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func());
    }

}
