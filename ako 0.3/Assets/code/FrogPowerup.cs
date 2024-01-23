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
        }
    }

    private void OnMouseDown()
    {
        if (true)
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
                GetComponentInParent<Move>().IsChosen();
            }
            if(GetComponentInParent<PlayerFrog>().active && !GetComponentInParent<PlayerFrog>().powerupActive)
            {
                Destroy(GetComponentInParent<Move>().shadowPawn);
                int position = GetComponentInParent<Move>().ShadowPawnPosition();
                GetComponentInParent<Player>().MoveOut(GetComponentInParent<Move>().waitPoints[position], null);
                GameRules.diceNumber -= 2;
                GetComponentInParent<Move>().IsChosen();
            }

        }
    }
}
