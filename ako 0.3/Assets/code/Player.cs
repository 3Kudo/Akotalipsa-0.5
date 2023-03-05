using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//klasa odpowiedzialna za wyb�r klasy pionka
public class Player : MonoBehaviour
{
    public GameObject[] pionek;

    //czy jest aktywna tura gracza
    public bool active = false;

    public string nazwa;


    // sprawdza czy jest mo�liwy ruch
    public bool EnambleMovement()
    {
        if (!pionek[0].GetComponent<Move>().MoveEnabled(ref pionek[0].GetComponent<Move>().bDalejWGrze) && !pionek[1].GetComponent<Move>().MoveEnabled(ref pionek[1].GetComponent<Move>().bDalejWGrze)
                && !pionek[2].GetComponent<Move>().MoveEnabled(ref pionek[2].GetComponent<Move>().bDalejWGrze) && !pionek[3].GetComponent<Move>().MoveEnabled(ref pionek[3].GetComponent<Move>().bDalejWGrze))
        {
            if (GameRules.diceNumber != 6 || GameRules.diceNumber != 0)
            {
                GameRules.whoseTurn++;
                if (GameRules.whoseTurn == 5)
                {
                    GameRules.whoseTurn = 1;

                }
            }
            GameRules.diceNumber = 0;
            GameRules.Turn();
            return false;
        }
        return true;
    }

    public Transform WitchWaitpoint(int i)
    {
        return pionek[i].GetComponent<Move>().waitPoints[pionek[i].GetComponent<Move>().waitPointIndex];
    }

    public void MoveTheSame(GameObject pio, float x, float y)
    {
        List<GameObject> toMove = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            if (pio.GetComponent<Move>().waitPointIndex == pionek[i].GetComponent<Move>().waitPointIndex)
                toMove.Add(pionek[i]);
        }
        if (toMove.Count == 1)
        {
            pio.GetComponent<Move>().Set(x, y);
        }
        else if (toMove.Count == 2)
        {
            toMove[0].GetComponent<Move>().Set(x - 0.2F, y);
            toMove[1].GetComponent<Move>().Set(x + 0.2F, y);
        }
        else if(toMove.Count == 3)
        {
            toMove[0].GetComponent<Move>().Set(x - 0.2F, y + 0.2F);
            toMove[1].GetComponent<Move>().Set(x + 0.2F, y + 0.2F);
            toMove[2].GetComponent<Move>().Set(x, y - 0.2F);
        }
        else
        {
            toMove[0].GetComponent<Move>().Set(x - 0.2F, y + 0.2F);
            toMove[1].GetComponent<Move>().Set(x + 0.2F, y + 0.2F);
            toMove[2].GetComponent<Move>().Set(x - 0.2F, y - 0.2F);
            toMove[3].GetComponent<Move>().Set(x + 0.2F, y - 0.2F);
        }
    }

    public void MoveOut(Transform waitPoint, GameObject pio)
    {
        for(int i = 0; i < 4; i++)
        {
            if(waitPoint == pionek[i].GetComponent<Move>().GetWaitpoint() && pio != pionek[i])
            {
                MoveTheSame(pionek[i], waitPoint.transform.position.x,waitPoint.transform.position.y);
            }
        }
    }
}


