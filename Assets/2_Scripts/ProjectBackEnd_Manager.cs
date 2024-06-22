using Cargold.SDK.BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectBackEnd_Manager : BackEnd_Manager
{
    public static ProjectBackEnd_Manager Instance;

    public static string s_playerName;

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
            
        }
    }

    //회원가입 -> 로그인 -> 닉네임 변경 -> 랭크 기입 -> 랭크 출력
    public void Login_Func(string _id, string _password, out bool is_Success)
    {
        this.OnSignUp_Func(_id, _password, out is_Success);

        if (is_Success == false)
            return;

        this.OnLogin_Func(_id, _password, out is_Success);

        if (is_Success == false)
            return;

        this.SetNickname_Func(_id, out is_Success);

        if (is_Success == false)
            return;

        s_playerName = _id;
    }

    public void RangkUpdate_Func(int a_Score)
    {
        this.SetRank_Func(this.rankUuid, this.tableName, this.gameDataColumn, a_Score);
    }

}
