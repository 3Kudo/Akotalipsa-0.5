using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : Player
{

    public override void EnambleMovement()
    {
        if (!pionek[0].GetComponent<Move>().MoveEnabled() && !pionek[1].GetComponent<Move>().MoveEnabled()
                && !pionek[2].GetComponent<Move>().MoveEnabled() && !pionek[3].GetComponent<Move>().MoveEnabled())
        {
            coin++;
            coinText.text = coin.ToString();
            GameRules.whoseTurn++;
            if (GameRules.whoseTurn == 5)
                GameRules.whoseTurn = 1;

            GameRules.TurnCounter();
            GameRules.Turn();
            GameRules.diceNumber = 0;
            SetPawnToNormal(null);
            PowerupWindowInteraction(pionek[0]);
            active = false;
            return;
        }
        active = true;
        for (int i = 0; i < 4; i++)
            if (pionek[i].GetComponent<Move>().IsChosen())
                break;
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
