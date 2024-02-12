using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private int wakeCounter;
    private int phase;
    private static int[] phaseThreshold = { 0, 3, 20, 35, 55};
    private bool isLocked;
    public Sprite[] catSprites;
    public Sprite[] akotametrSprites;
    public GameObject akotametr;
    public GameObject[] pawns;


    public int WakeCounter { get => wakeCounter; set => wakeCounter = value; }
    public global::System.Boolean IsLocked { get => isLocked; set => isLocked = value; }
    public global::System.Int32 Phase { get => phase; set => phase = value; }



    // Start is called before the first frame update
    void Start()
    {
        WakeCounter = 0;
        Phase = 0;
        IsLocked = false;
    }

    public void catTurn()
    {
        if (Phase == 1)
        {
            losowePrzesuwanie(-2, 2);
        }
        else if (Phase == 2)
        {
            losowePrzesuwanie(-4, 3);
            losowaSciana();
        }
        else if (Phase == 3)
        {
            losowePrzesuwanie(-6, 4);
            losowaSciana();
            GameRules.losoweCofanie();
        }
        //zwieksza sie tez przy wyjsciu z bazy po wyrzuceniu 6 - poprawic, czy git?
        incrementWakeCounter(1);
        phaseCheck();
    }

    public void losowePrzesuwanie(int maxMove, int minMove)
    {
        GameObject pionek = pawns[0];
        Transform position = pionek.GetComponent<Move>().GetWaitpoint();
        pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
        pionek.GetComponent<Move>().waitPointIndex = pionek.GetComponent<Move>().pozycja + Random.Range(minMove, maxMove);
        pionek.GetComponent<Move>().ruch = true;
    }

    public void losowaSciana()
    {
        //TODO: mechanika losowej sciany (pole nie do przejscia)
    }

    public void incrementWakeCounter(int value)
    {
        WakeCounter += value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void decrementWakeCounter(int value)
    {
        if (WakeCounter > phaseThreshold[0])
            WakeCounter -= value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void isCounterLocked()
    {
        if (WakeCounter >= phaseThreshold[3])
            IsLocked = true;
        Debug.Log("Cat phase 4 lock is " + IsLocked);
    }

    public void phaseCheck()
    {
        for (int i = 0; i < 5; i++)
        {
            if (WakeCounter >= phaseThreshold[i])
                phase = i;
        }
        if (phase == 3)
            isLocked = true;
        if (phase == 4)
        {
            isLocked = false;
            phase = 0;
            wakeCounter = 0;
        }
        Debug.Log("Cat phase: " + phase);
    }
}
