using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Cat : MonoBehaviour
{

    public int wakeCounter;
    public int phase;
    private static int[] phaseThreshold = { 0, 3, 10, 20, 35};
    public bool isLocked;
    public Sprite[] catSprites;
    public Sprite[] akotametrSprites;
    public GameObject akotametr;
    public GameObject[] pawns;
    public AudioClip[] SFX;
    AudioSource AS;


    public int WakeCounter { get => wakeCounter; set => wakeCounter = value; }
    public global::System.Boolean IsLocked { get => isLocked; set => isLocked = value; }
    public global::System.Int32 Phase { get => phase; set => phase = value; }



    void Start()
    {
        AS = GetComponent<AudioSource>();
        Phase = 0;
        IsLocked = false;
        this.GetComponent<SpriteRenderer>().sprite = catSprites[0];
        akotametr.GetComponent<SpriteRenderer>().sprite = akotametrSprites[0];
    }

    public void catTurn()
    {
        if (Phase == 1)
        {
            losowePrzesuwanie(-2, 1);
        }
        else if (Phase == 2)
        {
            losowePrzesuwanie(-3, 2);
            GetComponentInParent<GameRules>().RandomFluff();
        }
        else if (Phase == 3)
        {
            RandomBack();
            losowePrzesuwanie(-5, 3);
            GetComponentInParent<GameRules>().RandomFluff();
        }
        incrementWakeCounter(1);
        phaseCheck();
    }

    public void losowePrzesuwanie(int maxMove, int minMove)
    {
        int los = Random.Range(1, 4);
        if (los == 3 && GetComponentInParent<GameRules>().onBoard.Count > 0)
        {
            int money = 4;
            for (int i = 0; i < 4; i++)
                money += pawns[i].GetComponent<Player>().coin;
            GameObject[] players = new GameObject[money];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <= pawns[i].GetComponent<Player>().coin; j++)
                {
                    players[money - 1] = pawns[i];
                    money--;
                }
            }
            back:
            GameObject pionek = players[Random.Range(0, players.Length)];
            pionek = pionek.GetComponent<Player>().pionek[Random.Range(0, 4)];
            int move = Random.Range(minMove, maxMove);
            if (pionek.GetComponent<Move>().waitPointIndex < 1)
                goto back;
            if(pionek.GetComponent<Move>().defence)
            {
                GetComponentInParent<GameRules>().AS.clip = GetComponentInParent<GameRules>().sfx[2];
                GetComponentInParent<GameRules>().AS.Play();
            }
            if (move == 0 || pionek.GetComponent<Move>().defence || pionek.GetComponent<Move>().GetFinish())
                return;
            Transform position = pionek.GetComponent<Move>().GetWaitpoint();

            if (move < 0)
            {
                for(int i = -1; i >= move; i--)
                {
                    for(int j = 0; j < GetComponentInParent<GameRules>().fluff.Count; j++)
                    {
                        if (pionek.GetComponent<Move>().waitPoints[pionek.GetComponent<Move>().waitPointIndex+i] == GetComponentInParent<GameRules>().fluff[j].GetComponent<Fluff>().waitPoint)
                        {
                            pionek.GetComponent<Move>().waitPointIndex += (i + 1);
                            pionek.GetComponent<Move>().ruch = true;
                            pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
                            return;
                        }
                    }
                }
                move = move + pionek.GetComponent<Move>().waitPointIndex;
                if (move < 1)
                    move = 1;
                pionek.GetComponent<Move>().waitPointIndex = move;
                AS.clip = SFX[0];
                AS.Play();
            }
            else
            {
                for (int i = 1; i <= move; i++)
                {
                    for (int j = 0; j < GetComponentInParent<GameRules>().fluff.Count; j++)
                    {
                        if (pionek.GetComponent<Move>().waitPoints[pionek.GetComponent<Move>().waitPointIndex + i] == GetComponentInParent<GameRules>().fluff[j].GetComponent<Fluff>().waitPoint)
                        {
                            pionek.GetComponent<Move>().waitPointIndex += (i - 1);
                            pionek.GetComponent<Move>().ruch = true;
                            pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
                            return;
                        }
                    }
                }
                move = move + pionek.GetComponent<Move>().waitPointIndex;
                if (move > 53)
                    move = 53;
                pionek.GetComponent<Move>().waitPointIndex = move;
                AS.clip = SFX[1];
                AS.Play();
            }

            pionek.GetComponent<Move>().ruch = true;
            pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
        }
    }
    public void RandomBack()
    {
        int los = Random.Range(1, 5);
        if (los == 3)
        {
            int money = 4;
            for (int i = 0; i < 4; i++)
                money += pawns[i].GetComponent<Player>().coin;
            GameObject[] players = new GameObject[money];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <= pawns[i].GetComponent<Player>().coin; j++)
                {
                    players[money - 1] = pawns[i];
                    money--;
                }
            }
            GameObject pionek = players[Random.Range(0, players.Length)];
            pionek = pionek.GetComponent<Player>().pionek[Random.Range(0, 4)];
            if (pionek.GetComponent<Move>().waitPointIndex == 0 || pionek.GetComponent<Move>().defence || pionek.GetComponent<Move>().GetFinish())
                return;
            Transform position = pionek.GetComponent<Move>().GetWaitpoint();
            pionek.GetComponent<Move>().pozycja = 0;
            pionek.GetComponent<Move>().waitPointIndex = 0;
            pionek.GetComponent<Move>().ruch = true;
            pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
            AS.clip = SFX[0];
            AS.Play();
        }
    }


    public void incrementWakeCounter(int value)
    {
        WakeCounter += value;
    }

    public void decrementWakeCounter(int value)
    {
        WakeCounter -= value;
        if (wakeCounter < 0)
            wakeCounter = 0;

    }

    public void isCounterLocked()
    {
        if (WakeCounter >= phaseThreshold[3])
            IsLocked = true;
    }

    public void phaseCheck()
    {
        int counPhase = 0;
        for (int i = 0; i < 5; i++)
        {
            if (WakeCounter >= phaseThreshold[i])
                counPhase = i;
        }
        phase = counPhase;
        if (phase == 3)
            isLocked = true;
        if (phase == 4)
        {
            isLocked = false;
            phase = 0;
            wakeCounter = 0;
        }
        this.GetComponent<SpriteRenderer>().sprite = catSprites[phase];
        akotametr.GetComponent<SpriteRenderer>().sprite = akotametrSprites[phase];
    }
}
