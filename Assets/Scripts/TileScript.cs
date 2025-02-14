/**using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Player[] playersOn;
    public Player player;
    private string tileName;
    private string ID;
    private bool isProperty;
    private Tile nextTile;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public string GetName()
    {
        return this.tileName;
    }

    public TileScript GetNext()
    {
        return this.nextTile;
    }

    public string GetID()
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

    public void SetID(string id)
    {
        this.ID = id;
    }

    public void SetIsProperty(bool isProperty)
    {
        this.isProperty = isProperty;
    }

    public Player GetLandedOn()
    {
        return playersOn;
    }
}
**/
