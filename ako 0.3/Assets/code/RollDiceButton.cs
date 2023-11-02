using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RollDiceButton : MonoBehaviour
{
    private Sprite[] dicesSides;
    public Image image;
    public Button button;
    public bool clicked;

    void Start()
    {
        dicesSides = Resources.LoadAll<Sprite>("Dice/");

    }


    public void ButtonRollDice()
    {
        if(clicked)
            StartCoroutine("RollTheDice");
    }
    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 3; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            image.sprite = dicesSides[randomDiceSide];
            yield return new WaitForSeconds(0.2f);
        }
        GameRules.diceNumber = randomDiceSide + 1;
        GameRules.MovePlayer();
        button.interactable = false;
    }
}
