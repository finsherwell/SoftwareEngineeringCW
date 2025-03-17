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

    public ActionType GetActionType()
    {
        return actionType;
    }

    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                gameEngine.passGo();
                Debug.Log("player has passed go!");
                break;
                    
            case ActionType.GoToJail:
                gameEngine.GoToJail();
                break;

            case ActionType.Jail:
                if (player.inJail)
                {
                    EscapeJail(player);
                }
                break;

            case ActionType.FreeParking:
                CollectFines(player);
                break;

            case ActionType.IncomeTax:
                player.takeMoney(100);
                addtoFinesTotal(100);
                break;

            case ActionType.SuperTax:
                player.takeMoney(200);
                addtoFinesTotal(200);
                break;

            case ActionType.PotLuck:
                // Draw Pot Luck card
                break;

            case ActionType.OpportunityKnocks:
                // Draw Opportunity Knocks card
                break;
        }
    }
    public void EscapeJail(Player player)
    {
        // Implement logic for escaping jail
    }

    public void CollectFines(Player player)
    {
        // Display message about fines collected
        player.addMoney(finesTotal);
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
