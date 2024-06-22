using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Name : MonoSingleton<UI_Name>
{
    [SerializeField]
    private Button button_Start;
    public Button Button_Start { get => button_Start; }
    [SerializeField]
    private Button button_Close;
    public Button Button_Close { get => button_Close; }
    [SerializeField]
    private InputField inputField_Name;
    public InputField InputField_Name { get => inputField_Name; }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
