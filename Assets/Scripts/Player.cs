using System.Collections.Generic;
using Codice.Client.Common;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 0;

    [SerializeField] public string playerName;
    [SerializeField] private int playerID;
    [SerializeField] public Tile currentTile;
    [SerializeField] protected List<Property> ownedproperties;

    public MenuEnums.Colours colour;
    public MenuEnums.Icon icon;
    public bool inJail = false;
    public bool hasGOOJ = false;
    private int positionID;

    void Awake()
    {
        if (ownedproperties == null)
        {
            ownedproperties = new List<Property>();
        }
    }
    void Start()
    {
        // Initialize the player’s starting tile if needed.
    }

    // Returns the player's current position ID
    public int getPositionID()
    {
        return positionID;
    }

    // Adds money to the player’s account
    public void addMoney(int amount)
    {
        this.money += amount;
    }

    public void setIcon()
    {
        Sprite i;
        switch (icon)
        {
            case MenuEnums.Icon.Boot:
                i = Resources.Load<Sprite>("boot");
                print("should be a boot ");
                break;
            case MenuEnums.Icon.Cat:
                i = Resources.Load<Sprite>("cat");
                break;
            case MenuEnums.Icon.Ship:
                i = Resources.Load<Sprite>("ship");
                break;
            case MenuEnums.Icon.HatStand:
                i = Resources.Load<Sprite>("hat_stand");
                break;
            case MenuEnums.Icon.Iron:
                i = Resources.Load<Sprite>("iron");
                break;
            case MenuEnums.Icon.Smartphone:
                i = Resources.Load<Sprite>("smartphone");
                break;
            default:
                i = Resources.Load<Sprite>("boot");
                break;
        }

        GetComponent<SpriteRenderer>().sprite = i;
    }

    // Removes money from the player’s account
    public void takeMoney(int amount)
    {
        this.money -= amount;
    }

    public int getMoney() { return money; }

    // Sets the current tile the player is on
    public void setCurrentTile(Tile tile)
    {
        currentTile = tile;
    }

    // Gets the current tile the player is on
    public Tile getCurrentTile()
    {
        return currentTile;
    }

    public Tile getTile()
    {
        return currentTile;
    }

    public void setTile(Tile tile)
    {
        currentTile = tile;
    }

    public bool isInJail()
    {
        return inJail;
    }

    public void setID(int id)
    {
        playerID = id;
    }

    public int getID()
    {
        return playerID;
    }

    public string getName()
    {
        return playerName;
    }
    public void setInJail(bool state)
    {
        inJail = state;
    }
    public void addProperty(Property property)
    {
        if (!ownedproperties.Contains(property))
        {
            ownedproperties.Add(property);
        }
    }

    public void removeProperty(Property property)
    {
        if (ownedproperties.Contains(property))
        {
            ownedproperties.Remove(property);
        }
    }

    public List<Property> GetProperties()
    {
        return ownedproperties;
    }

    public bool OwnsProperty(Property property)
    {
        return ownedproperties.Contains(property);
    }

}

