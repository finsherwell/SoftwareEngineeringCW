using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] public List<Player> players;
    [SerializeField] private int startingMoney = 1500;
}

void Start
{

}
private void GiveStartingMoney(Player player)
{
    player.AddMoney(startingMoney);
}