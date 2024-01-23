using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : Player
{
    [HideInInspector] public bool powerupActive = false;

    public override bool EnambleMovement()
    {
        if(powerupActive)
            GameRules.diceNumber += 2;
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
}