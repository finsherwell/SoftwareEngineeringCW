using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AiPlayer : Player
{
    private Player aiPlayer;
    public string state;
    private float currentPrice;
    [SerializeField] public Engine gameEngine;

    void Start()
    {
        aiPlayer = this; // This should refer to the current AiPlayer instance.
        gameEngine = FindObjectOfType<Engine>();
    }
    // Your original takeTurn() method that is executed after the wait
    public void takeTurn()
    {
        switch (state)
        {
            case "jail":
                if (aiPlayer.getMoney() > 200)
                {
                    pressGOJ();
                }
                else
                {
                    pressRollDice();
                }
                break;

            case "auction":
                string propset = "someGroup"; // Replace with actual group name/logic
                if (ownedproperties.Any(p => p.GetGroup() == propset))
                {
                    if (money > 2.5f * currentPrice)
                    {
                        bid();
                    }
                }
                break;

            default:
                pressRollDice();

                // Get the current tile of the AI player
                Tile currentTile = getTile();
                ActionSpace actionSpace = currentTile.GetComponent<ActionSpace>();
                if (actionSpace != null)
                {
                    // If ActionSpace exists, exit the current logic and break (end turn or action)
                    break;
                }

                // If ownedproperties is not null, proceed with upgrading properties
                if (ownedproperties != null)
                {
                    foreach (var prop in ownedproperties)
                    {
                        // If the AI has enough money to upgrade a property
                        if (money > 2.0f * prop.getHouseCost())
                        {
                            prop.UpgradeProperty(); // Upgrade the property
                        }
                    }
                }
                else
                {
                    // If ownedproperties is null or empty
                    Debug.LogWarning("ownedproperties is null or empty.");
                }
                gameEngine.nextTurnButtonAI();
                break;
        }
    }

    // Empty method stubs
    private void pressGOJ()
    {
        gameEngine.PayBail();
    }

    private void pressRollDice()
    {
        gameEngine.rollDice();
    }

    private void bid()
    {
        return;
    }


    private void payrent()
    {
        return;
        //engine handles this automatically 
    }

    private Tile getCurrentTile()
    {
        return currentTile;
    }
    public int decidePurchase(Property property)
    {
        string propertyGroup = "someGroup"; // Use a unique name for the property group

        // Check if the property group matches and AI has enough money to buy
        if (property.GetGroup() == propertyGroup)
        {
            if (money > 2.5f * property.GetPrice())
            {
                return 1; // AI buys the property
            }
        }
        // If the property group doesn't match, AI buys if they have enough money
        else if (money > 2.5f)
        {
            return 1; // AI buys the property
        }
        return 0;
    }
}

