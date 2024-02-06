using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        if (GetComponentInParent<PlayerFrog>().powerupActive)
        {
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
        else if(GetComponentInParent<Player>().coin < 3)
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
        if (GetComponentInParent<Player>().coin >= 3 || GetComponentInParent<PlayerFrog>().powerupActive)
        {
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<PlayerFrog>().powerupActive = !GetComponentInParent<PlayerFrog>().powerupActive;
            if (GetComponentInParent<PlayerFrog>().active && GetComponentInParent<PlayerFrog>().powerupActive)
            {
                Destroy(GetComponentInParent<Move>().shadowPawn);
                int position = GetComponentInParent<Move>().ShadowPawnPosition();
                GetComponentInParent<Player>().MoveOut(GetComponentInParent<Move>().waitPoints[position], null);
                GameRules.diceNumber += 2;
                GetComponentInParent<Player>().DecraseCoins(3);
                GetComponentInParent<Move>().IsChosen();
            }
            if(GetComponentInParent<PlayerFrog>().active && !GetComponentInParent<PlayerFrog>().powerupActive)
            {
                Destroy(GetComponentInParent<Move>().shadowPawn);
                int position = GetComponentInParent<Move>().ShadowPawnPosition();
                GetComponentInParent<Player>().MoveOut(GetComponentInParent<Move>().waitPoints[position], null);
                GameRules.diceNumber -= 2;
                GetComponentInParent<Player>().IncreaseCoins(3);
                GetComponentInParent<Move>().IsChosen();
            }

        }
    }

    private void OnMouseOver()
    {
        if (GetComponentInParent<Player>().coin >= 3 || GetComponentInParent<PlayerFrog>().powerupActive)
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
        if (GetComponentInParent<Player>().coin >= 3 || GetComponentInParent<PlayerFrog>().powerupActive)
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
        if (GetComponentInParent<Player>().coin >= 3 || GetComponentInParent<PlayerFrog>().powerupActive)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    
}
