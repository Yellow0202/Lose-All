using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using static Cargold.SDK.BackEnd.BackEnd_Manager;
using System;

public class UI_Result : MonoSingleton<UI_Result>
{
    public static UI_Result Instance;

    [SerializeField]
    private Button button_Retry;
    public Button Button_Retry { get => button_Retry; }
    [SerializeField]
    private Button button_Quit;
    public Button Button_Quit { get => button_Quit; }
    
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Name;
    public TextMeshProUGUI TextMeshProUGUI_Name { get => textMeshProUGUI_Name; }
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Money;
    public TextMeshProUGUI TextMeshProUGUI_Money { get => textMeshProUGUI_Money; }
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Broken;
    public TextMeshProUGUI TextMeshProUGUI_Broken { get => textMeshProUGUI_Broken; }

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_TotalScore;
    public TextMeshProUGUI TextMeshProUGUI_TotalScore { get => textMeshProUGUI_TotalScore; }


    [SerializeField, LabelText("랭킹 슬롯")]
    private UI_Result_RankingSlot[] rankingSlots;
    public UI_Result_RankingSlot[] RankingSlots { get => rankingSlots; }

    [SerializeField, LabelText("온 오프 오브젝트")] private GameObject _rankingOnOffObj;
    [SerializeField, LabelText("현재 랭킹 정보")] private List<RankUserData> _rankList;

    [LabelText("플레이어 닉네임")] private string _nickName;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    override protected void OnEnable()
    {
        base.OnEnable();
    }

    public void Result_RankingCall_Func()
    {
        this.Result_RankingUpdate_Func();
    }

    private void Result_RankingUpdate_Func()
    {   //랭킹에 내 정보를 기입.

        this._nickName = UserSystem_Manager.Instance.loginData.Get_UserData_Func().userName;
        Debug.Log("Name : " + _nickName);
        ProjectBackEnd_Manager.Instance.RangkUpdate_Func(UserSystem_Manager.Instance.loginData.Get_UserData_Func().userScore);

        //UI에 내 정보 기입
        this.textMeshProUGUI_Name.text = _nickName;
        this.textMeshProUGUI_Money.text = UserSystem_Manager.Instance.playInfo.Get_ScorePlayInfo_Func().ToString("N0");
        this.textMeshProUGUI_Broken.text = UserSystem_Manager.Instance.playInfo.Get_SmashedScorePlayInfo_Func().ToString("N0");
        this.textMeshProUGUI_TotalScore.text = UserSystem_Manager.Instance.loginData.Get_UserData_Func().userScore.ToString("N0");

        this.Result_RankingDataDownload_Func();
    }

    private void Result_RankingDataDownload_Func()
    {
        ProjectBackEnd_Manager.Instance.GetRank_Func((a_TotalList) =>
        {
            this._rankList = a_TotalList;
        });

        this.Result_RankingSlotSetting_Func();
    }

    private void Result_RankingSlotSetting_Func()
    {//랭킹에 의거하여 정보 입력.

        bool is_MyData = false;

        for (int i = 0; i < this._rankList.Count; i++)
        {
            if (this.rankingSlots.Length <= i)
                break;

            if (this._rankList[i].nickname == this._nickName)
                is_MyData = true;
            else
                is_MyData = false;


            this.rankingSlots[i].SetRanking(this._rankList[i].rank, this._rankList[i].nickname, this._rankList[i].value, is_MyData);
        }

        if(this._rankList.Count < this.rankingSlots.Length)
        {
            for (int i = this._rankList.Count; i < this.rankingSlots.Length; i++)
            {
                this.rankingSlots[i].SetRanking(i+1, "-", 0, false);
            }
        }


        this.Result_RankingObjectOpen_Func();
    }

    private void Result_RankingObjectOpen_Func()
    {
        this._rankingOnOffObj.SetActive(true);
    }

    //게임 끝
    //랭킹에 내 정보를 기입해야함.
    //기존 랭킹 정보를 다운받음.
    //상위 19명만 입력. 나머지 하나는 내 점수.
    //오름차순으로 정렬.



}
