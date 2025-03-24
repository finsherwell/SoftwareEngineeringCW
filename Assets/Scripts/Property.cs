using Codice.Client.Common.GameUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Property : Tile 
{
    [SerializeField] private string propertyName; // Name of the property (e.g. "The Old Creek")
    [SerializeField] private string group; // Color set (e.g., "Brown", "Blue")
    [SerializeField] private int price; // Original cost to buy the property
    [SerializeField] private int houseCost; // 
    [SerializeField] private int[] rentLevels; // Rent for different levels (0-4 houses + hotel)
    [SerializeField] private Button button; // Button to or upgrade property
    [SerializeField] private Button sellbutton; // button to sell house / property
    [SerializeField] private bool isOwned = false; // Will be true if the property is owned
    [SerializeField] private int houses = 0; // Updates if you upgrade houses
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
    private void Start()
    {
        UpdatebuyButtonText();
        SetupButtonListener();
        SetupSellButtonListener();
    }
    private void SetupButtonListener()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnbuyButtonClick);
        }
    }
    private void SetupSellButtonListener()
    {
        if (sellbutton!= null)
        {
            button.onClick.AddListener(OnsellButtonClick);
        }
    }

    private void OnbuyButtonClick()
    {
        if (isOwned && owner.money >= houseCost && houses < 5 ) // Max: 4 houses + hotel
        {
            UpgradeProperty();
            UpdatebuyButtonText();
        }
    }
    private void OnsellButtonClick()
    {
        DowngradeProperty();
    }
    public void UpgradeProperty() // allows player to buy houses for their property
    {

           owner.takeMoney(houseCost);
           houses++;
    }

    public void DowngradeProperty() // allows player to sell houses for their property
    {
        owner.addMoney(houseCost);
        houses--;
    }
        private void UpdatebuyButtonText()
    {
        if (button != null && button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            if (houses < 5)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = $"Purchase for ${houseCost}";
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Fully Upgraded";
            }
        }
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

    public void ShowButtonCheck(Player player)
    {
        if (player == owner)
        {
            button.gameObject.SetActive(true);
        }
    }
    public void ShowSellButtonCheck(Player currentPlayer)
    {
        if (sellbutton != null && IsOwned() && owner == currentPlayer && houses > 0)
        {
            sellbutton.gameObject.SetActive(true);
        }
    }

    public void HideButtonCheck(Player player)
    {
        if (player == owner)
        {
            button.gameObject.SetActive(false);
        }
    }
   public void HideSellButtonCheck(Player currentPlayer)
    {
        if (sellbutton != null)
        {
            sellbutton.gameObject.SetActive(false);
        }
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
