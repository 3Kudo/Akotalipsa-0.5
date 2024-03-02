using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Move
{
    public bool poweruopActive;
    public GameObject[] molehill = new GameObject[2];
    public int[] molehillWaitPointsIndex = new int[2];
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
                AS.Stop();
                if (waitPointIndex != 0)
                {
                    if (poweruopActive)
                    {
                        foreach(GameObject wall in GetComponentInParent<GameRules>().fluff)
                        {
                            if(wall.GetComponent<Fluff>().waitPoint == waitPoints[waitPointIndex])
                            {
                                waitPointIndex++;
                                return;
                            }
                        }
                        molehill[1] = Instantiate(GetComponentInParent<PlayerMole>().molehillPattern) as GameObject;
                        molehillWaitPointsIndex[1] = waitPointIndex;
                        GetComponentInParent<PlayerMole>().molehillExit.Add(waitPointIndex);
                        molehill[1].transform.position = waitPoints[waitPointIndex].transform.position;
                        GetComponent<SpriteRenderer>().enabled = true;
                    }
                    ruch = false;
                    GetComponentInParent<GameRules>().Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);
                    GetComponentInParent<PlayerMole>().ResetPowerupActive(pionek);
                    poweruopActive = false;
                    for (int i = 0; i < GetComponentInParent<PlayerMole>().molehillEntrancce.Count; i++)
                    {
                        if (waitPointIndex == GetComponentInParent<PlayerMole>().molehillEntrancce[i])
                        {
                            waitPointIndex = GetComponentInParent<PlayerMole>().molehillExit[i];
                            pozycja = waitPointIndex;
                            ruch = true;
                            return;
                        }
                    }

                    GetComponentInParent<Player>().MoveTheSame(pionek,
                        waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y,waitPointIndex);

                    if (GetComponentInParent<GameRules>().diceNumber < 6)
                    {
                        GetComponentInParent<GameRules>().whoseTurn++;
                        if (GetComponentInParent<GameRules>().whoseTurn == 5)
                        {
                            GetComponentInParent<GameRules>().whoseTurn = 1;
                        }
                        GetComponentInParent<GameRules>().TurnCounter();
                    }


                    GetComponentInParent<GameRules>().Turn();
                    GetComponentInParent<GameRules>().diceNumber = 0;
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
        if (waitPointIndex == 0 && (GetComponentInParent<GameRules>().diceNumber == 6))
            return true;
        if (waitPointIndex > 0)
            return true;
        return false;
    }
}
