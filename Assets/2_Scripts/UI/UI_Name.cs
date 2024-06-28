using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class UI_Name : MonoSingleton<UI_Name>
{
    public static UI_Name Instance;

    [SerializeField]
    private Button button_Start;
    public Button Button_Start { get => button_Start; }
    [SerializeField]
    private Button button_Close;
    public Button Button_Close { get => button_Close; }
    [SerializeField]
    private TMP_InputField inputField_Name;
    public TMP_InputField InputField_Name { get => inputField_Name; }

    [LabelText("게임시작 버튼이 눌린 상태인지")] private bool is_LoginBtnClick;

    [SerializeField, LabelText("경고 텍스트")] private TextMeshProUGUI _help_Text;

    [LabelText("이름 혹은 닉네임 입력")] public string _nicknameHelpStr;
    [LabelText("1에서 7글자까지")] public string _nickCountHelpStr;
    [LabelText("이미 있는 닉네임")] public string _oldNameHelpStr;

    [SerializeField, LabelText("온오프 오브젝트")] private GameObject _onoffObj;
    [SerializeField, LabelText("입력 텍스트 창")] private TMP_InputField _inputField;
    [SerializeField, LabelText("경고 텍스트")] private TextMeshProUGUI _inputText;

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
        this.button_Start.onClick.AddListener(Play_BtnSound_Func);
        this.button_Start.onClick.AddListener(Clikc_Login_Func);
        this.button_Close.onClick.AddListener(Play_BtnSound_Func);
        this.button_Close.onClick.AddListener(() =>
        {
            this._onoffObj.SetActive(false);
            this._help_Text.text = "";
            this._inputField.text = "";
            this._inputText.text = "";
        });

        this.is_LoginBtnClick = false;
    }

    private void Play_BtnSound_Func()
    {
        SoundChild_Script.Instance.Click_UIBtnSound_Func();
    }

    private void Clikc_Login_Func()
    {   //인풋필드에 이름을 추가해야함. 추가하지 않으면 안됨. 최소 닉네임은 1글자 부터 5글자까지로 임의 지정

        if (this.is_LoginBtnClick == true)
            return;

        string a_InputStr = inputField_Name.text;
        this.is_LoginBtnClick = true;

        if (string.IsNullOrWhiteSpace(a_InputStr) == true)
        {
            //안내말이 있으면 좋을 듯.
            this.is_LoginBtnClick = false;
            this._help_Text.text = this._nicknameHelpStr;
            return;
        }

        if(a_InputStr.Length < 1 || 7 < a_InputStr.Length)
        {
            this.is_LoginBtnClick = false;
            this._help_Text.text = this._nickCountHelpStr;
            return;
        }

        ProjectBackEnd_Manager.Instance.Login_Func(a_InputStr, Time.time.ToString(), out bool is_Success);

        if(is_Success == false)
        {
            this.is_LoginBtnClick = false;
            this._help_Text.text = this._oldNameHelpStr;
            return;
        }

        UserSystem_Manager.Instance.loginData.Set_UserNameData_Func(a_InputStr);
        SoundChild_Script.Instance.Stop_Bgm_Func();

        // 타이틀을 통해 재진입하면 인트로 다시 재생
        GameSystem_Manager.Instance.skipOpenning = false;

        //해당 닉네임으로 로그인 됨.
        //게임으로 넘어감.
        SceneManager.LoadScene("InGame");

    }

}
