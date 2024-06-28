using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class UI_Title : MonoSingleton<UI_Title>
{
    public static UI_Title Instance;

    [SerializeField]
    private Button button_Start;
    public Button Button_Start { get => button_Start; }

    [SerializeField]
    private Button button_Exit;
    public Button Button_Exit { get => button_Exit; }

    [SerializeField, LabelText("입력창")] private GameObject _inputView;

    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("메인 배경")] private Image _title_MainBack;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("메인 타이틀")] private Image _title_MainTitle;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("게임시작 버튼")] private Image _title_GameStartBtn;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("게임종료 버튼")] private Image _title_GameCloseBtn;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("입력창 게임시작 버튼")] private Image _title_InputInGameStartBtn;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("입력창 이름 묻는 텍스트")] private TextMeshProUGUI _title_InputWhatNameText;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("입력창 입력 텍스트")] private TextMeshProUGUI _title_InputInHelpText;
    [SerializeField, FoldoutGroup("언어_이미지_변경"), LabelText("언어선택 창 텍스트")] private TextMeshProUGUI _title_LanguageViewText;

    private void Awake()
    {
        Instance = this;

        //this.Start_Title_Func();
    }

    // Start is called before the first frame update
    //override protected void OnEnable()
    //{
    //    base.OnEnable();
    //}

    private void Start()
    {
        this.Start_Title_Func();
    }

    [Button("테스트")]
    public void Start_Title_Func()
    {
        Debug.Log("USER : " + UserSystem_Manager.Instance.GetUserData.langTypeID);

        SoundChild_Script.Instance.Start_InGameBgmSound_Func(BgmType.타이틀BGM);
        this.button_Start.onClick.AddListener(Play_BtnSound_Func);
        this.button_Exit.onClick.AddListener(Play_BtnSound_Func);
        this.button_Start.onClick.AddListener(() => { this._inputView.SetActive(true); });
        this.button_Exit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        switch (UserSystem_Manager.Instance.GetUserData.langTypeID)
        {
            case (int)SystemLanguage.Korean:
                this.LanguageToSpriteAllChange_Func("ko");
                break;
            case (int)SystemLanguage.English:
                this.LanguageToSpriteAllChange_Func("en");
                break;
        }
    }

    private void Play_BtnSound_Func()
    {
        SoundChild_Script.Instance.Click_UIBtnSound_Func();
    }

    public void Click_LanguageChange_Func(string a_LanguageStr)
    {   //언어 변경 창

        switch(a_LanguageStr)
        {
            case "ko": //한글
                UserSystem_Manager.Instance.GetUserData.langTypeID = (int)SystemLanguage.Korean;
                break;

            case "en": //영어
                UserSystem_Manager.Instance.GetUserData.langTypeID = (int)SystemLanguage.English;
                break;

            default:    //입력값 잘못됨
                break;
        }

        this.LanguageToSpriteAllChange_Func(a_LanguageStr);
    }

    private void LanguageToSpriteAllChange_Func(string a_LanguageStr)
    {
        switch (a_LanguageStr)
        {
            case "ko": //한글
                this._title_MainBack.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_MainBack;
                this._title_MainTitle.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_MainTitle;
                this._title_GameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_GameStartBtn;
                this._title_GameCloseBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_GameClose;
                this._title_InputInGameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_InGameGoBtn;

                this._title_InputWhatNameText.text = "당신의 이름은 무엇입니까?";
                this._title_InputInHelpText.text = "1 ~ 7 자 닉네임을 입력하여 주세요";
                this._title_LanguageViewText.text = "언어변경";

                UI_Name.Instance._nicknameHelpStr = "먼저 이름을 입력하여 주세요";
                UI_Name.Instance._nickCountHelpStr = "이름은 1 ~ 7자 사이로 정해주셔야 합니다";
                UI_Name.Instance._oldNameHelpStr = "이미 존재하는 이름입니다. 다시 입력 해주세요";

                break;

            case "en": //영어
                this._title_MainBack.sprite = DataBase_Manager.Instance.GetTable_Define.en_Title_Sprite_MainBack;
                this._title_MainTitle.sprite = DataBase_Manager.Instance.GetTable_Define.en_Title_Sprite_MainTitle;
                this._title_GameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.en_Title_Sprite_GameStartBtn;
                this._title_GameCloseBtn.sprite = DataBase_Manager.Instance.GetTable_Define.en_Title_Sprite_GameClose;
                this._title_InputInGameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.en_Title_Sprite_InGameGoBtn;

                this._title_InputWhatNameText.text = "What is your name\n (or Nickname)?";
                this._title_InputInHelpText.text = "1 to 7 letters please";
                this._title_LanguageViewText.text = "Change Language";

                UI_Name.Instance._nicknameHelpStr = "Please enter your name(or Nickname) first";
                UI_Name.Instance._nickCountHelpStr = "The name must be between 1 ~ 7 characters";
                UI_Name.Instance._oldNameHelpStr = "It's a name that already exists. Please re-enter";

                break;

            default:    //입력값 잘못됨
                break;
        }
    }
}
