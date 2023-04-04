using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    // ogólnie to nie wiem czy wgl musicie ruszaæ t¹ klasê

    private Sprite[] dicesSides;
    private SpriteRenderer rend;
    private bool diceThrowAllowed = true;

    public Transform[] randomBack;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        dicesSides = Resources.LoadAll<Sprite>("Dice/");
        rend.sprite = dicesSides[5];

        GameRules.randomBack[0] = randomBack[0];
        GameRules.randomBack[1] = randomBack[1];

    }

    private void Update()
    {
        if (GameRules.diceNumber == 0)
        {
            diceThrowAllowed = true;
            GameRules.diceNumber++;
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
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = dicesSides[randomDiceSide];
            yield return new WaitForSeconds(0.2f);
        }

        GameRules.diceNumber = randomDiceSide + 1;
        GameRules.MovePlayer();
    }

    private void RollDiceBlock()
    {
        diceThrowAllowed = false;
    }
}