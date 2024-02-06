using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

//klasa odpowiedzialna za wybór klasy pionka
public abstract class Player : MonoBehaviour
{
    public GameObject[] pionek;
    public GameObject gracz, powerupWindow;

    //czy jest aktywna tura gracza
    public bool active = false;

    public string nazwa;

    public bool finished;


    // sprawdza czy jest mo¿liwy ruch
    public abstract void EnambleMovement();

    public Transform WitchWaitpoint(int i)
    {
        return pionek[i].GetComponent<Move>().waitPoints[pionek[i].GetComponent<Move>().waitPointIndex];
    }

    public void MoveTheSame(GameObject pio, float x, float y, int waitPoint)
    {
        List<GameObject> toMove = new List<GameObject>();
        toMove.Add(pio);
        for (int i = 0; i < 4; i++)
        {
            if (waitPoint == pionek[i].GetComponent<Move>().waitPointIndex && pio != pionek[i])
                toMove.Add(pionek[i]);
        }
        if (toMove.Count == 1)
        {
            pio.transform.position = new Vector2(x,y);
        }
        else if (toMove.Count == 2)
        {
            toMove[0].transform.position = new Vector2(x - 0.2F, y);
            toMove[1].transform.position = new Vector2(x + 0.2F, y);
            //toMove[0].GetComponent<Move>().Set(x - 0.2F, y);
            //toMove[1].GetComponent<Move>().Set(x + 0.2F, y);
        }
        else if(toMove.Count == 3)
        {
            toMove[0].transform.position = new Vector2(x - 0.2F, y + 0.2F);
            toMove[1].transform.position = new Vector2(x + 0.2F, y + 0.2F);
            toMove[2].transform.position = new Vector2(x, y - 0.2F);
        }
        else
        {
            toMove[0].transform.position = new Vector2(x - 0.2F, y + 0.2F);
            toMove[1].transform.position = new Vector2(x + 0.2F, y + 0.2F);
            toMove[2].transform.position = new Vector2(x - 0.2F, y - 0.2F);
            toMove[3].transform.position = new Vector2(x + 0.2F, y - 0.2F);
        }
    }

    public void MoveOut(Transform waitPoint, GameObject pio)
    {
        for(int i = 0; i < 4; i++)
        {
            if(waitPoint == pionek[i].GetComponent<Move>().GetWaitpoint() && pio != pionek[i])
            {
                MoveTheSame(pionek[i], waitPoint.transform.position.x,waitPoint.transform.position.y, pionek[i].GetComponent<Move>().waitPointIndex);
            }
        }
    }

    public void ChceckPlayerFinished(int dice)
    {
        if (pionek[0].GetComponent<Move>().GetFinish() && pionek[1].GetComponent<Move>().GetFinish()
                && pionek[2].GetComponent<Move>().GetFinish() && pionek[3].GetComponent<Move>().GetFinish())
        {
            finished = true;
            GameRules.PlayerFinishedGamed(gracz);
        }
    }

    public void SetPawnToNormal(GameObject pawn)
    {
        for(int i = 0; i < 4; i++)
        {
            if (pionek[i] == pawn)
                continue;
            else
                pionek[i].GetComponent<Move>().ToNormalState();
        }
    }

    public bool PowerupWindowInteraction(GameObject pawn)
    {
        return powerupWindow.GetComponent<PowerupWindow>().ChangeState(pawn);
    }

    public void SetPowerups(Transform parent)
    {
        powerupWindow.GetComponent<PowerupWindow>().SetPowerupsButtons(parent);
        
    }


}


