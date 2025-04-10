using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    public Engine gameEngine;
    
    private int finesTotal;

    public enum ActionType
    {
        Go,
        GoToJail,
        Jail,
        FreeParking,
        IncomeTax,
        SuperTax,
        PotLuck,
        OpportunityKnocks
    }

    [SerializeField] private ActionType actionType;
    
    // Returns the action type
    public ActionType GetActionType()
    {
        return actionType;
    }
    
    // Handles the action type for the space that you land on
    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                gameEngine.passGo();
                Debug.Log("Player has passed go!");
                break;
                    
            case ActionType.GoToJail:
                gameEngine.GoToJail();
                break;

            case ActionType.FreeParking:
                CollectFines(player);
                break;

            case ActionType.IncomeTax:
                player.takeMoney(200);
                gameEngine.parkingFines += 200; // Add to parking fines for consistency
                Debug.Log($"{player.playerName} paid 200 Income Tax");
                gameEngine.logText.text = $"{player.playerName} paid 200 Income Tax\n\n" + gameEngine.logText.text;
                break;

            case ActionType.SuperTax:
                player.takeMoney(200);
                gameEngine.parkingFines += 200; // Add to parking fines for consistency
                Debug.Log($"{player.playerName} paid 200 Super Tax");
                gameEngine.logText.text = $"{player.playerName} paid 200 Super Tax\n\n" + gameEngine.logText.text;
                break;

            case ActionType.PotLuck:
                gameEngine.cardManager.DrawPotLuck();
                break;

            case ActionType.OpportunityKnocks:
                gameEngine.cardManager.DrawOpportunityKnocks();
                break;
        }
    }

    public void CollectFines(Player player)
    {
        // Display message about fines collected
        int amount = gameEngine.parkingFines;
        player.addMoney(amount);
        Debug.Log($"{player.playerName} collected {amount} from Free Parking");
        gameEngine.logText.text = $"{player.playerName} collected {amount} from Free Parking\n\n" + gameEngine.logText.text;
        gameEngine.parkingFines = 0; // Reset the parking fines
    }

    public void SetActionType(ActionType type)
    {
        actionType = type;
    }

    public void addtoFinesTotal(int fine)
    {
        finesTotal += fine; // Corrected the addition to finesTotal
    }

    public int getFinesTotal()
    {
        return finesTotal;
    }
}
