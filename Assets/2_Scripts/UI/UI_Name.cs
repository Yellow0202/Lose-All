using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class UI_Name : MonoSingleton<UI_Name>
{
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

    // Start is called before the first frame update
    override protected void OnEnable()
    {
        base.OnEnable();

        this.button_Start.onClick.AddListener(Play_BtnSound_Func);
        this.button_Start.onClick.AddListener(Clikc_Login_Func);
        this.button_Close.onClick.AddListener(Play_BtnSound_Func);
        this.button_Close.onClick.AddListener(() => this.gameObject.SetActive(false));

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

        if (string.IsNullOrEmpty(a_InputStr) == true)
        {
            //안내말이 있으면 좋을 듯.
            this.is_LoginBtnClick = false;
            return;
        }

        if(a_InputStr.Length < 1 || 6 < a_InputStr.Length)
        {
            this.is_LoginBtnClick = false;
            return;
        }

        ProjectBackEnd_Manager.Instance.Login_Func(a_InputStr, "1234", out bool is_Success);

        if(is_Success == false)
        {
            this.is_LoginBtnClick = false;
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
