using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExitClass : MonoBehaviour
{
    private void Start()
    {
        if (GetComponentInParent<Move>().waitPointIndex > 0 || GetComponentInParent<Player>().coin<5)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.40;
            color.g = (float)0.40;
            color.b = (float)0.40;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
    private void OnMouseUpAsButton()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 && GetComponentInParent<Player>().coin>=5)
        {
            MouseControle.instance.Default();
            GetComponentInParent<GameRules>().diceNumber = 6;
            GetComponentInParent<Player>().DecraseCoins(5);
            GetComponentInParent<GameRules>().SetCatnipMik();
            GetComponentInParent<Player>().PowerupWindowInteraction(null);
            GetComponentInParent<Move>().ToNormalState();
            GetComponentInParent<Move>().MoveOn(0);
        }
    }

    private void OnMouseOver()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 && GetComponentInParent<Player>().coin >= 5)
        {
            MouseControle.instance.Clickable();
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)1;
            color.g = (float)1;
            color.b = (float)1;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseExit()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 && GetComponentInParent<Player>().coin >= 5)
        {
            MouseControle.instance.Default();
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseDrag()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 && GetComponentInParent<Player>().coin >= 5)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
