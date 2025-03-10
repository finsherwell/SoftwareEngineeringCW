using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    public Engine gameEngine; 
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
            // Implements game engine go method
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
                gameEngine.passGo(player);
>>>>>>> Stashed changes
=======
                gameEngine.passGo(player);
>>>>>>> Stashed changes
=======
                gameEngine.passGo(player);
>>>>>>> Stashed changes
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
            // If they can't pay, they get put into bankrupcy
                break;

            case ActionType.SuperTax:
            // Simple mathematical deduction
            // If they can't pay, they get put into bankrupcy
                break;

            case ActionType.PotLuck:
            // Use Action Card Class
                break;

            case ActionType.OpportunityKnocks:
            // Use Action Card Class
                break;

        }
    }
<<<<<<< Updated upstream
=======

    public void GoToJail(Player player){
       // player.setCurrentTile(/*jails tile ID*/)
       print("send player to jail");
    }

    public void EscapeJail(Player player){
        //dice rolls no longer move the player and instead they must roll a double to escape 
    }

    public void CollectFines(Player player){
        //display message to player about how much they have gained
        player.addMoney(gameEngine.CollectFines());
    }

    public void setActionType(ActionType type){
        actionType = type;
    }
>>>>>>> Stashed changes
}