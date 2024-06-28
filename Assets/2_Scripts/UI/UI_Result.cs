using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using static Cargold.SDK.BackEnd.BackEnd_Manager;
using System;
using UnityEngine.SceneManagement;

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
    //override protected void OnEnable()
    //{
    //    base.OnEnable();
    //}

    private void Start()
    {
                this.button_Retry.onClick.AddListener(Btn_ClickReStart_Func);
        this.button_Quit.onClick.AddListener(Btn_ClickGoToTitle_Func);
    }

    public void Result_RankingCall_Func()
    {
        this.Result_RankingUpdate_Func();
    }

    private void Result_RankingUpdate_Func()
    {   //랭킹에 내 정보를 기입.

        this._nickName = UserSystem_Manager.Instance.loginData.Get_UserData_Func().userName;

        ProjectBackEnd_Manager.Instance.RangkUpdate_Func(this._nickName, UserSystem_Manager.Instance.loginData.Get_UserData_Func().userScore);

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
        }, 20);

        this.Result_RankingSlotSetting_Func();
    }

    private void Result_RankingSlotSetting_Func()
    {//랭킹에 의거하여 정보 입력.

        bool is_MyData = false;

        for (int i = 0; i < this._rankList.Count; i++)
        {
            if (this.rankingSlots.Length <= i)
            {
                Debug.Log("브레이크 카운트 : " + i);
                break;
            }


            if (this._rankList[i].nickname == this._nickName)
            {
                is_MyData = true;

                if(UserSystem_Manager.Instance.loginData.Get_UserData_Func().userScore < this._rankList[i].value)
                {
                    is_MyData = false;
                }
            }
            else
                is_MyData = false;


            this.rankingSlots[i].SetRanking(this._rankList[i].rank, this._rankList[i].nickname, this._rankList[i].value, is_MyData);
        }

        Debug.Log("랭킹리스트 카운트 : " + this._rankList.Count);

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
        InGameUiAnim_Script.Instance.Call_RanKingViewOpewn_Func();
    }

    public void Btn_ClickReStart_Func()
    {
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.터치음);
        SoundChild_Script.Instance.Stop_Bgm_Func();
        UserSystem_Manager.Instance.playInfo.Set_ReSetPlayInfo_Func();
        EnemySystem_Manager.Instance.ReSet_Enemy_Func();
        DataBase_Manager.Instance.GetTable_Define.ReSet_ItemSpeendCoolTime_Func();
        SceneManager.LoadScene("InGame");
    }

    public void Btn_ClickGoToTitle_Func()
    {
        SoundChild_Script.Instance.Play_SFXSound_Func(SfxType.터치음);
        SoundChild_Script.Instance.Stop_Bgm_Func();
        UserSystem_Manager.Instance.playInfo.Set_ReSetPlayInfo_Func();
        SceneManager.LoadScene("Title");
    }

}
