using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Result_RankingSlot : MonoBehaviour
{
    [SerializeField]
    private Image image_Background;
    public Image Image_Background { get => image_Background; }
    [SerializeField]
    private Image image_New;
    public Image Image_New { get => image_New; }

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Rank;
    public TextMeshProUGUI TextMeshProUGUI_Rank { get => textMeshProUGUI_Rank; }
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Name;
    public TextMeshProUGUI TextMeshProUGUI_Name { get => textMeshProUGUI_Name; }
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_Score;
    public TextMeshProUGUI TextMeshProUGUI_Score { get => textMeshProUGUI_Score; }

    [SerializeField]
    private Sprite sprite_Normal;
    public Sprite Sprite_Normal { get => sprite_Normal; }
    [SerializeField]
    private Sprite sprite_Top3;
    public Sprite Sprite_Top3 { get => sprite_Top3; }
    [SerializeField]
    private Sprite sprite_New;
    public Sprite Sprite_New { get => sprite_New; }


    public void SetRanking(int InRank, string InName, int InScore, bool IsNew)
    {
        textMeshProUGUI_Rank.SetText("{0}", InRank);
        textMeshProUGUI_Name.SetText(InName);
        textMeshProUGUI_Name.SetText("{0}", InScore);

        if (IsNew)
        {
            image_Background.sprite = sprite_New;
            image_New.gameObject.SetActive(true);
        }
        else if(InRank >= 3)
        {
            image_Background.sprite = sprite_Top3;
            image_New.gameObject.SetActive(false);
        }
        else
        {
            image_Background.sprite = sprite_Normal;
            image_New.gameObject.SetActive(false);
        }
    }
}
