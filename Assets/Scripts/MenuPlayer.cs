using UnityEngine;
using MenuEnums;
public class MenuPlayer
{
    public string name;
    public Colours colour;
    public Icon icon;

    public bool isAI;

    public MenuPlayer(string pName, Colours pColour, Icon pIcon, bool pisIA)
    {
        name = pName;
        colour = pColour;
        icon = pIcon;
        isAI = pisIA;
    }
}