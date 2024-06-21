using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Move
{
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
                    moveSpeed = 8f;
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
                GetComponentInParent<PlayerFrog>().powerupActive = false;
                ruch = false;
                AS.Stop();
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
}
