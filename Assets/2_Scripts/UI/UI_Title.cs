using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : MonoSingleton<UI_Title>
{
    [SerializeField]
    private Button button_Start;
    public Button Button_Start { get => button_Start; }

    [SerializeField]
    private Button button_Exit;
    public Button Button_Exit { get => button_Exit; }

    // Start is called before the first frame update
    override protected void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
