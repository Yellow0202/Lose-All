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

    [LabelText("���ӽ��� ��ư�� ���� ��������")] private bool is_LoginBtnClick;

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
    {   //��ǲ�ʵ忡 �̸��� �߰��ؾ���. �߰����� ������ �ȵ�. �ּ� �г����� 1���� ���� 5���ڱ����� ���� ����

        if (this.is_LoginBtnClick == true)
            return;

        string a_InputStr = inputField_Name.text;
        this.is_LoginBtnClick = true;

        if (string.IsNullOrEmpty(a_InputStr) == true)
        {
            //�ȳ����� ������ ���� ��.
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

        // Ÿ��Ʋ�� ���� �������ϸ� ��Ʈ�� �ٽ� ���
        GameSystem_Manager.Instance.skipOpenning = false;

        //�ش� �г������� �α��� ��.
        //�������� �Ѿ.
        SceneManager.LoadScene("InGame");

    }

}
