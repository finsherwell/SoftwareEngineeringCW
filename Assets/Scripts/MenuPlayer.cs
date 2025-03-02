using UnityEngine;
using MenuEnums;
public class MenuPlayer
{
    public string name;
    public Colours colour;
    public Icon icon;

    public MenuPlayer(string pName, Colours pColour, Icon pIcon)
    {
        name = pName;
        colour = pColour;
        icon = pIcon;
        Debug.Log("new menu player made");
    }

}
