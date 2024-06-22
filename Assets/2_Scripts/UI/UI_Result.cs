using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Result : MonoSingleton<UI_Result>
{
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
    private UI_Result_RankingSlot[] rankingSlots;
    public UI_Result_RankingSlot[] RankingSlots { get => rankingSlots; }

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
