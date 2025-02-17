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

    // This function is called when the roll button is pressed
    public void rollButtonPressed()
    {
        if (diceRolling == false)
        {
            diceRolling = true;
            StartCoroutine(roll());
        }
    }

    // Coroutine that handles the dice roll animation
    public IEnumerator roll()
    {
        for (int i = 0; i < 20; i++)
        {
            changeDiceRandomly();
            yield return new WaitForSeconds(0.01f * i); // Simulate delay
        }
        diceRolling = false;
        finalValue = diceNum; // Set final value after roll is finished
    }

    // Randomly change the dice number and sprite
    private void changeDiceRandomly()
    {
        int randomNumber = UnityEngine.Random.Range(0, 6);
        spriteRenderer.sprite = sprites[randomNumber];
        diceNum = randomNumber + 1;
    }

    // Coroutine that waits until the dice roll is finished and then returns the final value
    public IEnumerator returnRoll(System.Action<int> onRollComplete)
    {
        // Wait until the roll is complete
        yield return StartCoroutine(WaitForRoll());

        // Call the callback with the final value
        onRollComplete(finalValue);
    }

    // Wait for the dice roll to finish
    private IEnumerator WaitForRoll()
    {
        while (diceRolling)
        {
            yield return null; // Wait until diceRolling is false
        }
    }
    public int getValue()
    {
        return finalValue;
    }
}
