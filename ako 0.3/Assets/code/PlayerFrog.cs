using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : Player
{
    [HideInInspector] public bool powerupActive = false;

    public override void EnambleMovement()
    {
        if(powerupActive)
            GetComponentInParent<GameRules>().diceNumber += 2;
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
            if (pionek[i].GetComponent<Move>().IsChosen(powerupActive))
            {
                powerupWindow.GetComponent<PowerupWindow>().powerups[1].GetComponent<FrogPowerup>().SetUp();
                break;
            }
    }
}
