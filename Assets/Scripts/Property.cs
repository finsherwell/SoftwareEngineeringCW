using UnityEngine;

public class Property : Tile 
{
    [SerializeField] private string propertyName; // Name of the property (e.g. "The Old Creek")
    [SerializeField] private string group; // Color set (e.g., "Brown", "Blue")
    [SerializeField] private int price; // Original cost to buy the property
    [SerializeField] private int houseCost; // 
    [SerializeField] private int[] rentLevels; // Rent for different levels (0-4 houses + hotel)

    private bool isOwned = false; // Will be true if the property is owned
    private int houses = 0; // Updates if you upgrade houses
    [SerializeField] private Player owner = null; // Assigned to the player that owns the property

    private void Awake()
    {

        propertyName = tileName;

    }
    /*
    Returns the name of the property.
    */
    public string GetPropertyName()
    {
        return propertyName;
    }


    /*
    Returns the price of the property.
    */
    public int GetPrice()
    {
        return price;
    }

    /*
//    Returns the cost of the house.
//    */
//    public int GetHouseCost()
//    {
//        return houseCost;
//    }

//    /*
//    Returns the rent of the property.
//    */
//    public int GetRent()
//    {
//        return rentLevels[houses];
//    }

   /*
    Returns whether the property is owned or not.
    */
    public bool IsOwned()
    {
        return isOwned;
    }

    /*
    Returns the owner of the property.
    */
    public Player GetOwner()
    {
        return owner;
    }

    public void SetOwner(Player newOwner)
    {
        owner = newOwner;
        newOwner.addProperty(this);
        isOwned = true;
    }

    /*
    Allows a user to buy the property as long as it isn't owned and they have the funds to buy it.
    */
    public void PurchaseProperty(Player buyer)
    {
        if (!isOwned && buyer.money >= price)
        {
            buyer.takeMoney(price);
            owner = buyer;
            isOwned = true;
        }
    }
}

//    /*
//    Allows a player to upgrade their property as long as it does not exceed the boundaries for that property.
//    */
//    public void UpgradeProperty()
//    {
//        if (isOwned && owner.money >= houseCost && houses < 5) // Max: 4 houses + hotel
//        {
//            owner.takeMoney(houseCost);
//            houses++;
//        }
//    }

//    /*
//    Handles rent payment between the tenant and the owner of the property.
//    */
//    public void PayRent(Player tenant)
//    {
//        if (isOwned && tenant != owner)
//        {
//            int rent = GetRent();
//            tenant.takeMoney(rent);
//            owner.addMoney(rent);
//        }
//    }
//}
