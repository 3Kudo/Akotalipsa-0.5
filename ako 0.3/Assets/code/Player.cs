using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//klasa odpowiedzialna za wybór klasy pionka
public class Player : MonoBehaviour
{
    public GameObject[] pionek;

    //czy jest aktywna tura gracza
    public bool active = false;

    public string nazwa;
    private void Update()
    {
        //sprawdzam czy jakis pionek nie moze sie ruszyc jezeli tak to jest to tura gracza jesli nie to skipuje gracza
        if (active)
        {
            if (!pionek[0].GetComponent<Move>().MoveEnabled(ref pionek[0].GetComponent<Move>().bDalejWGrze) && !pionek[1].GetComponent<Move>().MoveEnabled(ref pionek[1].GetComponent<Move>().bDalejWGrze)
                && !pionek[2].GetComponent<Move>().MoveEnabled(ref pionek[2].GetComponent<Move>().bDalejWGrze) && !pionek[3].GetComponent<Move>().MoveEnabled(ref pionek[3].GetComponent<Move>().bDalejWGrze))
            {
                active = false;
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
            }
        }
    }

    public Transform WitchWaitpoint(int i)
    {
        return pionek[i].GetComponent<Move>().waitPoints[pionek[i].GetComponent<Move>().waitPointIndex];
    }

    public void MoveTheSame(GameObject pio, float x, float y, bool leave)
    {
        int move_x = 1;
        int move_y = 1;
        int liczba = 0;
        for (int i = 0; i < 4; i++)
        {

            if (pio != pionek[i])
            {
                if (pio.GetComponent<Move>().waitPointIndex == pionek[i].GetComponent<Move>().waitPointIndex)
                {
                    pionek[i].GetComponent<Move>().Set(x + (0.2F * move_x), y + (0.2F * move_y));
                    liczba++;
                    if (move_x == 1)
                        move_x = -1;
                    else if (move_y == 1)
                        move_y = -1;
                    else
                        move_x = 1;
                }
            }
        }
        if (liczba != 0 && !leave)
        {
            x = x + (0.2F * move_x);
            y = y + (0.2F * move_y);

            pio.GetComponent<Move>().Set(x, y);

            return;
        }



    }

}
