using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExitClass : MonoBehaviour
{
    private void Start()
    {
        if (GetComponentInParent<Move>().waitPointIndex > 0)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
    private void OnMouseDown()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0)
        {
            GameRules.diceNumber = 6;
            GetComponentInParent<Player>().PowerupWindowInteraction(null);
            GetComponentInParent<Move>().ToNormalState();
            GetComponentInParent<Move>().MoveOn();
        }
    }
}
