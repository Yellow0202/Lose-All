using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UI_Title : MonoSingleton<UI_Title>
{
    [SerializeField]
    private Button button_Start;
    public Button Button_Start { get => button_Start; }

    [SerializeField]
    private Button button_Exit;
    public Button Button_Exit { get => button_Exit; }

    [SerializeField, LabelText("입력창")] private GameObject _inputView;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        SoundChild_Script.Instance.Start_InGameBgmSound_Func(BgmType.타이틀BGM);
        this.button_Start.onClick.AddListener(Play_BtnSound_Func);
        this.button_Exit.onClick.AddListener(Play_BtnSound_Func);
        this.button_Start.onClick.AddListener(() => { this._inputView.SetActive(true); });
    }

    private void Play_BtnSound_Func()
    {
        SoundChild_Script.Instance.Click_UIBtnSound_Func();
    }
}
