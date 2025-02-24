using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    // Enum to store the different types of actions that you can land on.
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
    Returns the action type
    */
    public ActionType GetActionType()
    {
        return actionType;
    }

    /*
    Handles the action type for the space that you land on
    */
    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                player.addMoney(200);
                break;
        }
    }
}