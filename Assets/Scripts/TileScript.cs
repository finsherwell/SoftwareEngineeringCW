/**using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Player[] playersOn;
    public Player player;
    private string tileName;
    private int ID;
    private bool isProperty;
<<<<<<< Updated upstream:Assets/Scripts/TileScript.cs
    private Tile nextTile;
=======
    public Tile nextTile;
    public Vector3 tilePosition;  // Position in world space where the tile is located
>>>>>>> Stashed changes:Assets/Scripts/Tile.cs

    void Start()
    {
        
    }

<<<<<<< Updated upstream:Assets/Scripts/TileScript.cs
    void Update()
    {

    }

=======
>>>>>>> Stashed changes:Assets/Scripts/Tile.cs
    public string GetName()
    {
        return this.tileName;
    }

    public Tile GetNext()
    {
<<<<<<< Updated upstream:Assets/Scripts/TileScript.cs
        return this.nextTile;
=======
        return nextTile;
>>>>>>> Stashed changes:Assets/Scripts/Tile.cs
    }

    public int GetID()
    {
        return this.ID;
    }

    public bool IsProperty()
    {
        return this.isProperty;
    }

    public bool LandedOn()
    {
        return (playersOn != null && playersOn.Length > 0);
    }
    private void updatePlayerOn()
    {
        if (player.currentTile == this)
        {
            palyersOn[player.id]=player;
        }
    }

    public void SetName(string name)
    {
        this.tileName = name;
    }

    public void SetID(int id)
    {
        this.ID = id;
    }

    public void SetIsProperty(bool isProperty)
    {
        this.isProperty = isProperty;
    }

<<<<<<< Updated upstream:Assets/Scripts/TileScript.cs
    public Player GetLandedOn()
    {
        return playersOn;
=======
    // Get the position of the tile in world space
    public Vector3 GetPosition()
    {
        return tilePosition;
>>>>>>> Stashed changes:Assets/Scripts/Tile.cs
    }
}
**/
