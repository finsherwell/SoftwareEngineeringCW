using UnityEngine;

public class Player : MonoBehaviour
{
    public int money;

    [SerializeField] public string playerName;
<<<<<<< Updated upstream
    [SerializeField] public int playerID;
=======
    [SerializeField] private int playerID;
    [SerializeField] private Tile currentTile;
    //Property properties[];
    bool inJail = false;
>>>>>>> Stashed changes

    public Tile currentTile;  // The tile the player is currently on
    private bool inJail = false;
    private int positionID;

    // Start is called before the first frame update
    void Start()
    {
        // You can initialize the player’s starting tile here if you need.
    }

    // Returns the player's current position ID
    public int getPositionID()
    {
        return positionID;
    }

<<<<<<< Updated upstream
    // Adds money to the player’s account
    public void addMoney(int amount)
    {
        this.money += amount;
    }

    // Removes money from the player’s account
    void takeMoney(int amount)
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

    // Move the player to the specified tile
    public void moveToTile(Tile tile)
    {
        currentTile = tile;
        transform.position = tile.GetPosition();  // Move the player’s GameObject to the tile’s position
    }
}
=======
     void takeMoney(int amount) { this.money -= amount; }
     
    public Tile getTile()
    {
        return currentTile;
    }
    public void setTile(Tile tile)
    {
        currentTile= tile;
    }
    
 }
>>>>>>> Stashed changes
