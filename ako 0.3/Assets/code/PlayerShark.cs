using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShark : Player
{
    public override void EnambleMovement()
    {
        for (int i = 0; i < 4; i++)
        {
            if (pionek[i].GetComponent<Shark>().powerupActive)
            {
                pionek[i].GetComponent<Shark>().onBoard = GetComponentInParent<GameRules>().GetOnBoard();
                pionek[i].GetComponent<Shark>().MoveOn(0);
                active = false;
                return;
            }
        }
        if (!pionek[0].GetComponent<Move>().MoveEnabled() && !pionek[1].GetComponent<Move>().MoveEnabled()
                && !pionek[2].GetComponent<Move>().MoveEnabled() && !pionek[3].GetComponent<Move>().MoveEnabled())
        {
            coin++;
            coinText.text = coin.ToString();
            GetComponentInParent<GameRules>().whoseTurn++;
            if (GetComponentInParent<GameRules>().whoseTurn == 5)
                GetComponentInParent<GameRules>().whoseTurn = 1;

            GetComponentInParent<GameRules>().TurnCounter();
            GetComponentInParent<GameRules>().Turn();
            GetComponentInParent<GameRules>().diceNumber = 0;
            SetPawnToNormal(null);
            PowerupWindowInteraction(pionek[0]);
            active = false;
            return;
        }
        active = true;
        for (int i = 0; i < 4; i++)
            if (pionek[i].GetComponent<Move>().IsChosen(false))
            {
                powerupWindow.GetComponent<PowerupWindow>().powerups[1].GetComponent<SharkPowerup>().SetUp();
                break;
            }
    }

    public void ResetPowerupActive(GameObject pawn)
    {
        for (int i = 0; i < 4; i++)
        {
            if (pionek[i] != pawn && pionek[i].GetComponent<Shark>().powerupActive)
            {
                pionek[i].GetComponent<Shark>().powerupActive = false;

                break;
            }
        }
    }
}
