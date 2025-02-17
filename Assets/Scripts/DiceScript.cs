using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine;



public class DiceScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;   //reference to sprite renderer in the dice game object
    public Sprite[] sprites;                 //sprite sheet reference
    public int diceNum = 1;                 //the actual number the dice has rolled
    private bool diceRolling;               //prevents spamming the dice - which could lead to odd behavior


    void Start()
    {
        //when the dice is first initialised, set its sprite to show the dice face with 1 on it
        spriteRenderer.sprite = sprites[0];

        diceRolling = false;
    }

    //this function will be called when the roll button is pressed
    public void rollButtonPressed()
    {
        if (diceRolling == false)
        {
            diceRolling = true;
            StartCoroutine(roll());
        }
    }

    //change dice num then wait
    public IEnumerator roll()
    {
        for (int i = 0; i < 20; i++)
        {
            changeDiceRandomly();
            yield return new WaitForSeconds(0.01f * i);
        }
        diceRolling = false;

    }

    //randomly select a new dice number, change the sprite accordingly
    private void changeDiceRandomly()
    {
        int randomNumber = UnityEngine.Random.Range(0, 6);
        spriteRenderer.sprite = sprites[randomNumber];
        diceNum = randomNumber + 1;
    }

}


