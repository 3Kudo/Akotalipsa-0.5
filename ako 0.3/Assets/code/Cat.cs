using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Cat : MonoBehaviour
{

    private int wakeCounter;
    private int phase;
    private static int[] phaseThreshold = { 0, 3, 10, 20, 35};
    public bool isLocked;
    public Sprite[] catSprites;
    public Sprite[] akotametrSprites;
    public GameObject akotametr;
    public GameObject[] pawns;
    public AudioClip[] soundTracks;
    int clip=0;
    AudioSource AS;


    public int WakeCounter { get => wakeCounter; set => wakeCounter = value; }
    public global::System.Boolean IsLocked { get => isLocked; set => isLocked = value; }
    public global::System.Int32 Phase { get => phase; set => phase = value; }



    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        WakeCounter = 0;
        Phase = 0;
        IsLocked = false;
        this.GetComponent<SpriteRenderer>().sprite = catSprites[0];
        akotametr.GetComponent<SpriteRenderer>().sprite = akotametrSprites[0];
    }
    /*private void Update()
    {
        if (AS.isPlaying == false)
        {
            int clipOn = 4 * phase + clip;
            AS.clip = soundTracks[clipOn];

            AS.Play();
            clip++;
            if (clip == 4)
                clip = 0;
        }
    }*/

    public void catTurn()
    {
        if (Phase == 1)
        {
            losowePrzesuwanie(-2, 2);
        }
        else if (Phase == 2)
        {
            losowePrzesuwanie(-4, 3);
            GameRules.RandomFluff();
        }
        else if (Phase == 3)
        {
            RandomBack();
            losowePrzesuwanie(-6, 4);
            GameRules.RandomFluff();
        }
        //zwieksza sie tez przy wyjsciu z bazy po wyrzuceniu 6 - poprawic, czy git?
        incrementWakeCounter(1);
        phaseCheck();
    }

    public void losowePrzesuwanie(int maxMove, int minMove)
    {
        int money = 4;
        for (int i = 0; i < 4; i++)
            money += pawns[i].GetComponent<Player>().coin;
        GameObject[] players = new GameObject[money];
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j <= pawns[i].GetComponent<Player>().coin; j++)
            {
                players[money-1] = pawns[i];
                money--;
            }
        }
        GameObject pionek = players[Random.Range(0, players.Length)];
        pionek = pionek.GetComponent<Player>().pionek[Random.Range(0, 4)];
        int move = Random.Range(minMove, maxMove);
        Debug.Log(move);
        if (pionek.GetComponent<Move>().waitPointIndex < 1 || move == 0 || pionek.GetComponent<Move>().defence)
            return;
        Transform position = pionek.GetComponent<Move>().GetWaitpoint();
        if (move < 0)
        {
            move = move + pionek.GetComponent<Move>().waitPointIndex;
            if (move < 1)
                move = 1;
            pionek.GetComponent<Move>().waitPointIndex = move;
        }
        else
        {
            move = move + pionek.GetComponent<Move>().waitPointIndex;
            if (move > 53)
                move = 53;
            pionek.GetComponent<Move>().waitPointIndex = move;
        }

        pionek.GetComponent<Move>().ruch = true;
        pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
    }

    public void RandomBack()
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
        Debug.Log("Cat phase: " + phase);
    }
}
