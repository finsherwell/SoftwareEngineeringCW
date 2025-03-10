using Codice.Client.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MenuEnums;

public class CardLobby : MonoBehaviour
{
    public Icon icon = Icon.Empty;
    public string playerName = "";
    public Colours cardColour = Colours.Grey;
    public Image cardImage;
    public TextMeshProUGUI cardText;

    public Image cardBackground;

    public Sprite emptySprite;
    public Sprite catSprite;
    public Sprite shipSprite;
    public Sprite hatStandSprite;
    public Sprite ironSprite;
    public Sprite bootSprite;
    public Sprite smartphoneSprite;
    public Sprite robotSprite;


    public void refreshCard()
    {
        cardText.text = playerName;
        //Debug.Log(cardText.text);
        Sprite s;
        switch (icon)
        {
            case Icon.Empty:
                s = emptySprite;
                break;
            case Icon.Cat:
                s = catSprite;
                break;
            case Icon.Ship:
                s = shipSprite;
                break;
            case Icon.HatStand:
                s = hatStandSprite;
                break;
            case Icon.Iron:
                s = ironSprite;
                break;
            case Icon.Boot:
                s = bootSprite;
                break;
            case Icon.Smartphone:
                s = smartphoneSprite;
                break;
            case Icon.Robot:
                s = robotSprite;
                break;
            default:
                s = emptySprite;
                break;
        }

        cardImage.sprite = s;

        Color c = Color.white;
        switch (cardColour)
        {
            case Colours.Grey:
                ColorUtility.TryParseHtmlString("#D9D9D9", out c);
                break;
            case Colours.Green:
                ColorUtility.TryParseHtmlString("#33FF57", out c);
                break;
            case Colours.Yellow:
                ColorUtility.TryParseHtmlString("#FFD700", out c);
                break;
            case Colours.Blue:
                ColorUtility.TryParseHtmlString("#3357FF", out c);
                break;
            case Colours.Purple:
                ColorUtility.TryParseHtmlString("#8A2BE2", out c);
                break;
            case Colours.Red:
                ColorUtility.TryParseHtmlString("#FF5733", out c);
                break;
            case Colours.Cyan:
                ColorUtility.TryParseHtmlString("#00FFFF", out c);
                break;


        }
        cardBackground.color = c;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        refreshCard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
