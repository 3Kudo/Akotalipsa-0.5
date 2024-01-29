using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class RollDice : MonoBehaviour
{
    // ogólnie to nie wiem czy wgl musicie ruszaæ t¹ klasê

    private Sprite[] dicesSides;
    private SpriteRenderer rend;
    private static bool diceThrowAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        dicesSides = Resources.LoadAll<Sprite>("Dice/");
        rend.sprite = dicesSides[5];
        Color color = rend.color;
        color.r = (float)0.90;
        color.g = (float)0.90;
        color.b = (float)0.90;
        rend.color = color;

    }

    private void Update()
    {
        if (GameRules.diceNumber == 0)
        {      
            diceThrowAllowed = true;
            Color color = rend.color;
            color.r = (float)0.90;
            color.g = (float)0.90;
            color.b = (float)0.90;
            rend.color = color;
            GameRules.diceNumber++;
        }
    }

    private void OnMouseEnter()
    {
        if (diceThrowAllowed)
        {
            Color color = rend.color;
            color.r = 1;
            color.g = 1;
            color.b = 1;
            rend.color = color;
        }
    }

    private void OnMouseExit()
    {
        if (diceThrowAllowed)
        {
            Color color = rend.color;
            color.r = (float)0.90;
            color.g = (float)0.90;
            color.b = (float)0.90;
            rend.color = color;
        }
    }

    private void OnMouseDown()
    {
        if (diceThrowAllowed && GameRules.miejsce!=3)
            StartCoroutine("RollTheDice");
    }
    private IEnumerator RollTheDice()
    {
        diceThrowAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 3; i++)
        {
            randomDiceSide = Random.Range(5, 6);
            rend.sprite = dicesSides[randomDiceSide];
            yield return new WaitForSeconds(0.2f);
        }
        Color color = rend.color;
        color.r = (float)0.60;
        color.g = (float)0.60;
        color.b = (float)0.60;
        rend.color = color;
        GameRules.diceNumber = randomDiceSide + 1;
        GameRules.MovePlayer();
    }

    public static void RollDiceBlock()
    {
        diceThrowAllowed = false;
    }
}