using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMole : Player
{
    public GameObject molehillPattern;
    public GameObject[] molehill = new GameObject[2];
    public int[] molehillWaitPointsIndex = new int[2];
    public override void EnambleMovement()
    {
        for(int i = 0; i < 4; i++)
        {
            if (pionek[i].GetComponent<Mole>().poweruopActive)
            {
                Destroy(molehill[0]);
                molehill[0] = null;
                Destroy(molehill[1]);
                molehill[1] = null;
                molehillWaitPointsIndex[0] = -1;
                molehillWaitPointsIndex[1] = -1;


                molehill[0] = Instantiate(molehillPattern) as GameObject;
                molehillWaitPointsIndex[0] = pionek[i].GetComponent<Mole>().waitPointIndex;
                molehill[0].transform.position = pionek[i].GetComponent<Mole>().waitPoints[molehillWaitPointsIndex[0]].transform.position;
                pionek[i].GetComponent<Mole>().MoveOn(0);
                active = false;
                return;
            }
        }
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
            if (pionek[i].GetComponent<Move>().IsChosen(false))
            {
                powerupWindow.GetComponent<PowerupWindow>().powerups[1].GetComponent<MolePowerup>().SetUp();
                break;
            }
    }


    public void ResetPowerupActive(GameObject pawn)
    {
        for(int i = 0; i < 4; i++)
        {
            if (pionek[i] != pawn && pionek[i].GetComponent<Mole>().poweruopActive)
            {
                pionek[i].GetComponent<Mole>().poweruopActive = false;

                break;
            }
        }
    }
}
