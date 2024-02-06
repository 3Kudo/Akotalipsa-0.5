using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Move
{
    public bool poweruopActive;
    private void Update()
    {
        //wykonanie ruchu, nie wiem czy to jest dobry pomys³ ¿e to tutaj wstawi³em po porstu lepiej tutaj wygl¹da ruch
        if (ruch)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitPoints[pozycja].transform.position, moveSpeed * Time.deltaTime);


            if (transform.position == waitPoints[pozycja].transform.position && transform.position != waitPoints[waitPointIndex].transform.position)
            {
                if (waitPointIndex > pozycja)
                {
                    moveSpeed = 20f;
                    pozycja++;
                }
                else
                {
                    moveSpeed = 12f;
                    pozycja--;
                }
            }
            if (AS.isPlaying == false)
            {
                AS.clip = soundTracks[0];

                AS.Play();
            }
            if (transform.position == waitPoints[waitPointIndex].transform.position)
            {
                ruch = false;
                AS.Stop();
                if (waitPointIndex != 0)
                {

                    if (waitPointIndex == GetComponentInParent<PlayerMole>().molehillWaitPointsIndex[0])
                    {
                        waitPointIndex = GetComponentInParent<PlayerMole>().molehillWaitPointsIndex[1];
                        pozycja = waitPointIndex;
                        ruch = true;
                        return;
                    }

                    GetComponentInParent<Player>().MoveTheSame(pionek,
                        waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y,waitPointIndex);
                    GameRules.Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);

                    if (GameRules.diceNumber < 6)
                    {
                        GameRules.whoseTurn++;
                        if (GameRules.whoseTurn == 5)
                        {
                            GameRules.whoseTurn = 1;
                        }
                    }
                    
                    if (poweruopActive)
                    {
                        GetComponentInParent<PlayerMole>().molehill[1] = Instantiate(GetComponentInParent<PlayerMole>().molehillPattern) as GameObject;
                        GetComponentInParent<PlayerMole>().molehillWaitPointsIndex[1] = waitPointIndex;
                        GetComponentInParent<PlayerMole>().molehill[1].transform.position = waitPoints[waitPointIndex].transform.position;
                    }
                    GetComponentInParent<PlayerMole>().ResetPowerupActive(pionek);
                    poweruopActive = false;
                    GameRules.Turn();
                    GameRules.diceNumber = 0;
                }
            }
        }

    }

    

    public override bool MoveEnabled()
    {
        //wy³¹czenie oborny
        defence = false;


        if (finished)
            return false;
        if (waitPointIndex == 0 && (GameRules.diceNumber == 6))
            return true;
        if (waitPointIndex > 0)
            return true;
        return false;
    }
}
