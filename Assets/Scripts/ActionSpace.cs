using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    // Enum to store the different types of actions that you can land on
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

    /*
    Returns the action type.
    */
    public ActionType GetActionType()
    {
        return actionType;
    }

    /*
    Handles the action type for the space that you land on.
    */
    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                player.addMoney(200);
                break;
            
            case ActionType.GoToJail:
            // Checks if user has GOOJ, otherwise jails them
                break;

            case ActionType.Jail:
            // Nothing, allows you to pass if not in jail
                break;

            case ActionType.FreeParking:
            // Nothing, collect fines
                break;

            case ActionType.IncomeTax:
            // Simple mathematical deduction
                break;

            case ActionType.SuperTax:
            // Simple mathematical deduction
                break;

            case ActionType.PotLuck:
            // Use Action Card Class
                break;

            case ActionType.OpportunityKnocks:
            // Use Action Card Class
                break;

        }
    }
}