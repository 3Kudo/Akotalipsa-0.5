using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private int wakeCounter;
    private int phase;
    private static int[] phaseThreshold = { 0, 3, 20, 35 };
    private bool isLocked;
    private bool isCatsTurn;

    public int WakeCounter { get => wakeCounter; set => wakeCounter = value; }
    public global::System.Boolean IsLocked { get => isLocked; set => isLocked = value; }
    public global::System.Int32 Phase { get => phase; set => phase = value; }
    public global::System.Boolean IsCatsTurn { get => isCatsTurn; set => isCatsTurn = value; }

    // Start is called before the first frame update
    void Start()
    {
        WakeCounter = 0;
        Phase = 0;
        IsLocked = false;
        IsCatsTurn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementWakeCounter(int value)
    {
        if (!IsLocked) WakeCounter += value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void decrementWakeCounter(int value)
    {
        if (WakeCounter > phaseThreshold[0] && !IsLocked) WakeCounter -= value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void isCounterLocked()
    {
        if (WakeCounter >= phaseThreshold[3]) IsLocked = true;
        Debug.Log("Cat phase 4 lock is " + IsLocked);
    }

    public void phaseCheck()
    {
        for (int i = 0; i < 4; i++)
        {
            if (WakeCounter >= phaseThreshold[i]) phase = i;
        }
        Debug.Log("Cat phase: " + phase);
    }
}
