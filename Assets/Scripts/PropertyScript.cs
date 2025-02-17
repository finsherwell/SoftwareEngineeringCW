/**using UnityEngine;

public class PropertyScript : MonoBehaviour
{
    public Tile tile;
    private int setID;
    private int price;
    private int rent;
<<<<<<< Updated upstream:Assets/Scripts/PropertyScript.cs
    private bool isOwned;

    public bool isProperty()
    {
        return tile.isProperty();
    }
=======
    private bool owned;
    
>>>>>>> Stashed changes:Assets/Scripts/Property.cs
    public int getSet()
    {
        return this.setID;
    }
    public int getPrice()
    {
        return this.price;
    }
    public int getRent()
    {
        return this.rent;
    }
    private bool isOwned()
    {
        return this.owned;
    }
<<<<<<< Updated upstream:Assets/Scripts/PropertyScript.cs
    public player OwnedBy()
    {
        if (isOwned())
        {
            //return player that owns property 
        }
    }
=======
    //public player OwnedBy()
    //{
    //    if (isOwned())
    //    {
    //        //return player that owns property 
    //    }
    //}
>>>>>>> Stashed changes:Assets/Scripts/Property.cs
}
**/

