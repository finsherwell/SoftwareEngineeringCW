using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    public Engine gameEngine; 
    
    public enum ActionType {
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

    public ActionType GetActionType()
    {
        return actionType;
    }

    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                gameEngine.passGo(player);
                break;
            
            case ActionType.GoToJail:
                if (!player.hasGOOJ) {
                    GoToJail(player);
                }
                break;

            case ActionType.Jail:
                if (player.inJail) {
                    EscapeJail(player);
                }
                break;

            case ActionType.FreeParking:
                CollectFines(player);
                break;

            case ActionType.IncomeTax:
                player.takeMoney(20);
                break;

            case ActionType.SuperTax:
                player.takeMoney(100);
                break;

            case ActionType.PotLuck:
                // Draw Pot Luck card
                break;

            case ActionType.OpportunityKnocks:
                // Draw Opportunity Knocks card
                break;
        }
    }

    public void GoToJail(Player player)
    {
        // player.setCurrentTile(/*jail tile ID*/);
        print("Send player to jail");
    }

    public void EscapeJail(Player player)
    {
        // Implement logic for escaping jail
    }

    public void CollectFines(Player player)
    {
        // Display message about fines collected
        player.addMoney(gameEngine.CollectFines());
    }

    public void SetActionType(ActionType type)
    {
        actionType = type;
    }
}

