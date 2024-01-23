using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : Player
{

    public override bool EnambleMovement()
    {
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

    public int WaitPointIndex(GameObject pawn)
    {
        int moveAmmount = 0;
        for(int i = 0; i < 4; i++)
        {
            if (pionek[i] == pawn)
                continue;
            int count = pionek[i].GetComponent<Move>().waitPointIndex - pawn.GetComponent<Move>().waitPointIndex -1;
            if(count <=4 && count>=1 && moveAmmount<count)
                moveAmmount = count;
        }
        return moveAmmount;
    }

}
