using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMole : Player
{
    public GameObject molehillPattern;
    public List<int> molehillEntrancce = new List<int>();
    public List<int> molehillExit = new List<int>();
    
    public override void EnambleMovement()
    {
        for(int i = 0; i < 4; i++)
        {
            if (pionek[i].GetComponent<Mole>().poweruopActive)
            {
                pionek[i].GetComponent<SpriteRenderer>().enabled = false;
                Destroy(pionek[i].GetComponent<Mole>().molehill[0]);
                pionek[i].GetComponent<Mole>().molehill[0] = null;
                Destroy(pionek[i].GetComponent<Mole>().molehill[1]);
                pionek[i].GetComponent<Mole>().molehill[1] = null;

                molehillEntrancce.Remove(pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[0]);
                pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[0] = -1;
                molehillExit.Remove(pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[1]);
                pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[1] = -1;


                pionek[i].GetComponent<Mole>().molehill[0] = Instantiate(molehillPattern) as GameObject;
                pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[0] = pionek[i].GetComponent<Mole>().waitPointIndex;
                molehillEntrancce.Add(pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[0]);
                pionek[i].GetComponent<Mole>().molehill[0].transform.position = pionek[i].GetComponent<Mole>().waitPoints[pionek[i].GetComponent<Mole>().molehillWaitPointsIndex[0]].transform.position;
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
