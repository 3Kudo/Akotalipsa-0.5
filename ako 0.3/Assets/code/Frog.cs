using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Move
{
    public bool PowerSound = false;

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
                    moveSpeed = 5f;
                    pozycja++;

                    if (AS.isPlaying == false)
                    {
                        if (PowerSound == true)
                        {
                            AS.clip = soundTracks[1];

                            AS.Play();
                            PowerSound = false;
                        }
                        else
                        {
                            AS.clip = soundTracks[0];

                            AS.Play();
                        }
                    }

                }
                else
                {
                    moveSpeed = 9f;
                    pozycja--;
                }
            }

            if (transform.position == waitPoints[waitPointIndex].transform.position)
            {
                if (paw != null)
                {
                    Destroy(paw);
                    paw = null;
                }
                GetComponentInParent<PlayerFrog>().powerupActive = false;
                ruch = false;
                //AS.Stop();
                if (waitPointIndex != 0)
                {
                    GetComponentInParent<Player>().MoveTheSame(pionek,
                        waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y, waitPointIndex);
                    GetComponentInParent<GameRules>().Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);

                    if (GetComponentInParent<GameRules>().diceNumber < 6)
                    {
                        GetComponentInParent<GameRules>().whoseTurn++;
                        if (GetComponentInParent<GameRules>().whoseTurn == 5)
                        {
                            GetComponentInParent<GameRules>().whoseTurn = 1;
                        }
                        GetComponentInParent<GameRules>().TurnCounter();
                    }
                    if (waitPointIndex == waitPoints.Length - 1)
                    {
                        finished = true;
                        GetComponentInParent<GameRules>().onBoard.Remove(pionek);
                        GetComponent<PolygonCollider2D>().enabled = false;
                        GetComponentInParent<Player>().ChceckPlayerFinished();
                    }
                    GetComponentInParent<GameRules>().Turn();
                    GetComponentInParent<GameRules>().diceNumber = 0;
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
        if (waitPointIndex == 0 && (GetComponentInParent<GameRules>().diceNumber == 6 || (GetComponentInParent<GameRules>().diceNumber >= 6 && GetComponentInParent<PlayerFrog>().powerupActive)))
            return true;
        if (waitPointIndex > 0)
            return true;
        return false;
    }

    public override void SetPawFront()
    {
        if ((waitPointIndex >= 1 && waitPointIndex < 11) || (waitPointIndex >= 47 && waitPointIndex < 53))
        {
            paw.transform.rotation.Set(0, 0, 0, 0);
            paw.transform.position.Set(0.23f, 0, 0);
        }
        else if (waitPointIndex >= 11 && waitPointIndex < 23)
        {
            paw.transform.rotation.Set(0, 0, 270, 0);
            paw.transform.position.Set(0, -0.26f, 0);
        }
        else if (waitPointIndex >= 23 && waitPointIndex < 35)
        {
            paw.transform.rotation.Set(0, 0, 180, 0);
            paw.transform.position.Set(-0.23f, 0, 0);
        }
        else if (waitPointIndex >= 35 && waitPointIndex <= 47)
        {
            paw.transform.rotation.Set(0, 0, 90, 0);
            paw.transform.position.Set(0, 0.26f, 0);
        }
    }

    public override void SetPawBack()
    {
        if ((waitPointIndex >= 1 && waitPointIndex <= 13) || (waitPointIndex > 48 && waitPointIndex <= 53))
        {
            paw.transform.rotation.Set(0, 0, 180, 0);
            paw.transform.position.Set(-0.23f, 0, 0);
        }
        else if (waitPointIndex > 11 && waitPointIndex <= 25)
        {
            paw.transform.rotation.Set(0, 0, 90, 0);
            paw.transform.position.Set(0, 0.26f, 0);
        }
        else if (waitPointIndex > 23 && waitPointIndex <= 37)
        {
            paw.transform.rotation.Set(0, 0, 0, 0);
            paw.transform.position.Set(0.23f, 0, 0);
        }
        else if (waitPointIndex > 35 && waitPointIndex <= 48)
        {
            paw.transform.rotation.Set(0, 0, 270, 0);
            paw.transform.position.Set(0, -0.26f, 0);
        }
    }
}
