using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Shark : Move
{
    [HideInInspector] public bool powerupActive;
    [HideInInspector] public List<GameObject> onBoard = new List<GameObject>();
    private void Update()
    {
        //wykonanie ruchu, nie wiem czy to jest dobry pomys³ ¿e to tutaj wstawi³em po porstu lepiej tutaj wygl¹da ruch
        if (ruch)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitPoints[pozycja].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == waitPoints[pozycja].transform.position && transform.position != waitPoints[waitPointIndex].transform.position)
            {
                foreach (GameObject pawn in onBoard)
                {
                    if (pawn.GetComponent<Move>().waitPoints[pawn.GetComponent<Move>().waitPointIndex] == waitPoints[pozycja] && 
                        pawn.GetComponentInParent<Player>().gracz != GetComponentInParent<Player>().gracz)
                    {
                        pawn.GetComponent<Move>().waitPointIndex = 0;
                        pawn.GetComponent<Move>().ruch = true;
                        onBoard.Remove(pawn);
                    }
                }
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
                    GetComponentInParent<Player>().MoveTheSame(pionek,
                        waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y, waitPointIndex);
                    GameRules.Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);

                    if (GameRules.diceNumber < 6)
                    {
                        GameRules.whoseTurn++;
                        if (GameRules.whoseTurn == 5)
                        {
                            GameRules.whoseTurn = 1;
                        }
                    }
                    if (powerupActive)
                        GameRules.onBoard = onBoard;
                    powerupActive = false;
                    GetComponentInParent<PlayerShark>().ResetPowerupActive(pionek);
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
