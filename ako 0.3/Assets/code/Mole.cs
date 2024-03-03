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
        //wykonanie ruchu, nie wiem czy to jest dobry pomys� �e to tutaj wstawi�em po porstu lepiej tutaj wygl�da ruch
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
                        foreach(GameObject wall in GameRules.fluff)
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
                    GameRules.Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);
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

                    if (GameRules.diceNumber < 6)
                    {
                        GameRules.whoseTurn++;
                        if (GameRules.whoseTurn == 5)
                        {
                            GameRules.whoseTurn = 1;
                        }
                        GameRules.TurnCounter();
                    }
                    
                    
                    GameRules.Turn();
                    GameRules.diceNumber = 0;
                }
            }
        }
        if(GetComponentInParent<Player>().active && !isMouseOver && MoveEnabled())
            base.onUpdate();
        else
            base.setFlashAmount(0.0f);
    }

    

    public override bool MoveEnabled()
    {
        //wy��czenie oborny
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
