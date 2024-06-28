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

    [SerializeField, LabelText("�Է�â")] private GameObject _inputView;

    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("���� ���")] private Image _title_MainBack;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("���� Ÿ��Ʋ")] private Image _title_MainTitle;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("���ӽ��� ��ư")] private Image _title_GameStartBtn;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("�������� ��ư")] private Image _title_GameCloseBtn;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("�Է�â ���ӽ��� ��ư")] private Image _title_InputInGameStartBtn;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("�Է�â �̸� ���� �ؽ�Ʈ")] private TextMeshProUGUI _title_InputWhatNameText;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("�Է�â �Է� �ؽ�Ʈ")] private TextMeshProUGUI _title_InputInHelpText;
    [SerializeField, FoldoutGroup("���_�̹���_����"), LabelText("���� â �ؽ�Ʈ")] private TextMeshProUGUI _title_LanguageViewText;

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

    [Button("�׽�Ʈ")]
    public void Start_Title_Func()
    {
        Debug.Log("USER : " + UserSystem_Manager.Instance.GetUserData.langTypeID);

        SoundChild_Script.Instance.Start_InGameBgmSound_Func(BgmType.Ÿ��ƲBGM);
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
    {   //��� ���� â

        switch(a_LanguageStr)
        {
            case "ko": //�ѱ�
                UserSystem_Manager.Instance.GetUserData.langTypeID = (int)SystemLanguage.Korean;
                break;

            case "en": //����
                UserSystem_Manager.Instance.GetUserData.langTypeID = (int)SystemLanguage.English;
                break;

            default:    //�Է°� �߸���
                break;
        }

        this.LanguageToSpriteAllChange_Func(a_LanguageStr);
    }

    private void LanguageToSpriteAllChange_Func(string a_LanguageStr)
    {
        switch (a_LanguageStr)
        {
            case "ko": //�ѱ�
                this._title_MainBack.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_MainBack;
                this._title_MainTitle.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_MainTitle;
                this._title_GameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_GameStartBtn;
                this._title_GameCloseBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_GameClose;
                this._title_InputInGameStartBtn.sprite = DataBase_Manager.Instance.GetTable_Define.ko_Title_Sprite_InGameGoBtn;

                this._title_InputWhatNameText.text = "����� �̸��� �����Դϱ�?";
                this._title_InputInHelpText.text = "1 ~ 7 �� �г����� �Է��Ͽ� �ּ���";
                this._title_LanguageViewText.text = "����";

                UI_Name.Instance._nicknameHelpStr = "���� �̸��� �Է��Ͽ� �ּ���";
                UI_Name.Instance._nickCountHelpStr = "�̸��� 1 ~ 7�� ���̷� �����ּž� �մϴ�";
                UI_Name.Instance._oldNameHelpStr = "�̹� �����ϴ� �̸��Դϴ�. �ٽ� �Է� ���ּ���";

                break;

            case "en": //����
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

            default:    //�Է°� �߸���
                break;
        }
    }
}
