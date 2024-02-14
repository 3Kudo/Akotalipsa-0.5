using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour
{
    public GameObject cat;


    public void setCatnipButton()
    {
        if (GameRules.GetTura().GetComponent<Player>().coin >= 3 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (GameRules.GetTura().GetComponent<Player>().coin >= 3 && !cat.GetComponent<Cat>().isLocked)
        {
            GameRules.GetTura().GetComponent<Player>().DecraseCoins(3);
            cat.GetComponent<Cat>().incrementWakeCounter(4);
            cat.GetComponent<Cat>().phaseCheck();
            setCatnipButton();
        }
    }

    private void OnMouseOver()
    {
        if (GameRules.GetTura().GetComponent<Player>().coin >= 3 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)1;
            color.g = (float)1;
            color.b = (float)1;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseExit()
    {
        if (GameRules.GetTura().GetComponent<Player>().coin >= 3 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseDrag()
    {
        if (GameRules.GetTura().GetComponent<Player>().coin >= 3 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.70;
            color.g = (float)0.70;
            color.b = (float)0.70;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
