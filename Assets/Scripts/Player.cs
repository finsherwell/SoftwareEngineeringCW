using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 0;

    [SerializeField] public string playerName;
    [SerializeField] private int playerID;
    [SerializeField] public Tile currentTile;
    [SerializeField] protected List<Property> ownedproperties = new List<Property>();
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

    public void removeProperty (Property property)
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

