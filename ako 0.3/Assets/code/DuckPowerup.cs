using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount == 0 || GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<Player>().coin < 3)
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex!=0 && GetComponentInParent<Player>().coin >= 3)
        {
            GetComponentInParent<Move>().ToNormalState();
            GetComponentInParent<Player>().DecraseCoins(3);
            Destroy(GetComponentInParent<Move>().shadowPawn);
            GetComponentInParent<Duck>().powerupActive = true;
            GetComponentInParent<Move>().waitPointIndex += ammount;
            GetComponentInParent<Move>().ruch = true;
        }
    }

    private void OnMouseEnter()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= 3)
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= 3)
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= 3)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
