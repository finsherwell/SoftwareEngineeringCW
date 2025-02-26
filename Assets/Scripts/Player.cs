using UnityEngine;

public class Player : MonoBehaviour
{
    public int money;

    [SerializeField] public string playerName;
    [SerializeField] private int playerID;
    [SerializeField] private Tile currentTile;
    bool inJail = false;
    private int positionID;

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
<<<<<<< Updated upstream
=======

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
>>>>>>> Stashed changes
}
