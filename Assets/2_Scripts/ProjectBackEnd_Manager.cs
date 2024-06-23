using Cargold.SDK.BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectBackEnd_Manager : BackEnd_Manager
{
    public static ProjectBackEnd_Manager Instance;

    public static string s_playerName;

    public string project_tableName = null;
    public string project_gameDataColumn = null;

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
        bool is_OnSignup = false;

        this.OnSignUp_Func(_id, _password, out is_Success);

        if (is_Success == false)
        {
            is_OnSignup = true;
        }

        this.OnLogin_Func(_id, _password, out is_Success);

        if (is_Success == false)
            return;

        if(is_OnSignup == false)
        {
            this.SetNickname_Func(_id, out is_Success);

            if (is_Success == false)
                return;
        }

        s_playerName = _id;
    }

    public void RangkUpdate_Func(string a_Name, int a_Score)
    {
        List<RankUserData> a_List = new List<RankUserData>();

        Instance.GetRank_Func((a_TotalList) =>
        {
            a_List = a_TotalList;
        });

        for (int i = 0; i < a_List.Count; i++)
        {
            if(a_List[i].nickname == a_Name)
            {
                if (a_Score < a_List[i].value)
                    return;
                else
                    break;
            }
        }

        this.SetRank_Func(this.rankUuid, this.project_tableName, this.project_gameDataColumn, a_Score);
    }

}
