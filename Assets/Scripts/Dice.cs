using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int diceNum = 1;
    private bool diceRolling;
    public int finalValue;

    void Start()
    {
        spriteRenderer.sprite = sprites[0];
        diceRolling = false;
    }

    // This function will start the roll and set up a coroutine to wait for completion
    public void rollAndReturn(System.Action<int> onRollComplete)
    {
        if (diceRolling) return; // Don't start another roll if it's already rolling

        StartCoroutine(RollDice(onRollComplete)); // Start the roll and wait for it to complete
    }

    // Coroutine to simulate the dice roll over time
    private IEnumerator RollDice(System.Action<int> onRollComplete)
    {
        diceRolling = true; // Set the dice as rolling
        for (int i = 0; i < 20; i++) // Simulate 20 roll steps
        {
            changeDiceRandomly(); // Change the dice appearance
            yield return new WaitForSeconds(0.05f); // Wait between changes
        }
        diceRolling = false; // Dice roll is finished
        finalValue = diceNum; // Store the final rolled value

        // After roll is finished, trigger the callback to return the final value
        onRollComplete(finalValue);
    }

    // Change the dice sprite randomly
    private void changeDiceRandomly()
    {
        int randomNumber = UnityEngine.Random.Range(0, 6);
        spriteRenderer.sprite = sprites[randomNumber];
        diceNum = randomNumber + 1; // Set dice number to 1-6
    }
}
