using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShark : Player
{
    public override bool EnambleMovement()
    {
        for (int i = 0; i < 4; i++)
        {
            if (pionek[i].GetComponent<Shark>().powerupActive)
            {
                pionek[i].GetComponent<Shark>().onBoard = GameRules.GetOnBoard();
                pionek[i].GetComponent<Shark>().MoveOn();
                return false;
            }
        }
        if (!pionek[0].GetComponent<Move>().MoveEnabled() && !pionek[1].GetComponent<Move>().MoveEnabled()
                && !pionek[2].GetComponent<Move>().MoveEnabled() && !pionek[3].GetComponent<Move>().MoveEnabled())
        {
            if (GameRules.diceNumber != 6 || GameRules.diceNumber != 0)
            {
                GameRules.whoseTurn++;
                if (GameRules.whoseTurn == 5)
                {
                    GameRules.whoseTurn = 1;

                }
            }
            GameRules.Turn();
            GameRules.diceNumber = 0;
            SetPawnToNormal(null);
            PowerupWindowInteraction(pionek[0]);
            return false;
        }
        for (int i = 0; i < 4; i++)
            if (pionek[i].GetComponent<Move>().IsChosen())
                break;
        return true;
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
