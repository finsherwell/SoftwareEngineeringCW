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

    public void rollButtonPressed()
    {
        if (diceRolling == false)
        {
            diceRolling = true;
            StartCoroutine(roll());
        }
    }

    public IEnumerator roll()
    {
        for (int i = 0; i < 20; i++)
        {
            changeDiceRandomly();
            yield return new WaitForSeconds(0.01f * i);
        }
        diceRolling = false;
        finalValue = diceNum;
    }

    private void changeDiceRandomly()
    {
        int randomNumber = UnityEngine.Random.Range(0, 6);
        spriteRenderer.sprite = sprites[randomNumber];
        diceNum = randomNumber + 1;
    }

    public IEnumerator returnRoll(System.Action<int> onRollComplete)
    {
        yield return StartCoroutine(WaitForRoll());
        onRollComplete(finalValue);
    }

    private IEnumerator WaitForRoll()
    {
        while (diceRolling)
        {
            yield return null;
        }
    }

    public int getValue()
    {
        return finalValue;
    }
}
