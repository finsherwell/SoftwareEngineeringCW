//using UnityEngine;

//public class AiPlayer : MonoBehaviour
//{
//    private Player aiPlayer;
//    private string state; 
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        aiPlayer = new Player();
//    }
//    public void takeTurn()
//    {
//        switch (state) {
//            case "jail":
//                if (aiPlayer.getMoney() > 200)
//                {
//                    pressGOJ();
//                }
//                else
//                {
//                    pressRollDice();
//                }
//                break;
//            case "auction":
//                //if owns at least one from that set bid up to 1.5x the property price if they can afford it 2.5x
//                //else withdraw from bid 
//                break;
//            case default:
//                pressRollDice();
//                if (aiPlayer.getCurrentTile().getComponent<properties>)
//                {
//                    //if property unowned then: 
//                    //if owns at least one from that set buy the property 
//                    //else if cash = 2.5xproperty price then buy 
//                    //else pay rent 
//                }
//                if (aiPlayer.getCurrentTile().getComponent < eventspace)
//                {
//                    //follow game action 
//                }
//                if (//ai player owns full set){
//                    //if cash = 2.5 x house price buy house 
//                    //else do nothing
//                }
//                break;

        
//    }
//}
