using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public int money = 0;

    [SerializeField] public string playerName;
    [SerializeField] private int playerID;
    [SerializeField] public Tile currentTile;
    [SerializeField] public List<Property> ownedproperties;

    public MenuEnums.Colours colour;
    public MenuEnums.Icon icon;
    private int jailTime = 0;
    public bool hasGOOJ = false;
    public bool hasCompletedCircuit = false;
    private int positionID;

    public int totalAssetValue = 0; //stores the value of players property portfolio + house portfolio + cash on hand

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
        totalAssetValue += amount;
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
        totalAssetValue -= amount;
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

    public int GetJailTime()
    {
        return jailTime;
    }
    public void setInJail(int Turns)
    {
        jailTime = Turns;
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

    public void addProperty(Property property)
    {
        if (!ownedproperties.Contains(property))
        {
            ownedproperties.Add(property);
            totalAssetValue += property.GetPrice();
        }
    }

    public void removeProperty(Property property)
    {
        if (ownedproperties.Contains(property))
        {
            property.sellHouse();
            property.setOwned(false);
            ownedproperties.Remove(property);
            totalAssetValue -= property.GetPrice();
        }
    }

    public List<Property> GetProperties()
    {
        return ownedproperties;
    }
    public int CountUtilities()
    {
        int count = 0;
        foreach (Property property in ownedproperties)
        {
            if (property.IsUtility()== true)
            {
                count++;
            }
        }
        return count;
    }
    public int CountStations()
    {
        int count = 0;
        foreach (Property property in ownedproperties)
        {
            if (property.IsStation())
            {
                count++;
            }
        }
        return count;
    }

    public bool OwnsProperty(Property property)
    {
        return ownedproperties.Contains(property);
    }
    public (bool hasSet, List<string> setColors) HasPropertySet()
    {
        Dictionary<string, List<Property>> propertyGroups = new Dictionary<string, List<Property>>();
        List<string> completeSets = new List<string>();

        // Group properties by color
        foreach (Property property in ownedproperties)
        {
            string group = property.GetGroup();
            if (!propertyGroups.ContainsKey(group))
            {
                propertyGroups[group] = new List<Property>();
            }
            propertyGroups[group].Add(property);
        }

        // Check if any group forms a set
        foreach (var group in propertyGroups)
        {
            int requiredCount = GetRequiredCountForSet(group.Key);
            if (group.Value.Count >= requiredCount)
            {
                completeSets.Add(group.Key);
            }
        }

        return (completeSets.Count > 0, completeSets);
    }
    private int GetRequiredCountForSet(string group)
    {
        // Define the required count for each group (color)
        switch (group)
        {
            case "Brown":
            case "Dark Blue":
                return 2;
            default:
                return 3;
        }
    }
    public bool HasCompleteSet(string group)
    {
        int requiredCount = (group == "Brown" || group == "Dark Blue") ? 2 : 3;
        return ownedproperties.Count(p => p.GetGroup() == group) >= requiredCount;
    }

    //called by a property when a house is purchased. This is needed to update the totalAssetValue
    public void houseBought(int money)
    {
        totalAssetValue += money;
    }
    //same as function above - but when a house is sold
    public void houseSold(int money)
    {
        totalAssetValue -= money;
    }
}


