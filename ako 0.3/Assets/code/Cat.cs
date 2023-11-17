using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    private int wakeCounter;
    private static int phaseThreshold[5] = [0, 10, 20, 35, 50];
    private bool isLocked;

    public int WakeCounter { get => wakeCounter; set => wakeCounter = value; }
    public global::System.Boolean IsLocked { get => isLocked; set => isLocked = value; }

    // Start is called before the first frame update
    void Start()
    {
        WakeCounter = 0;
        IsLocked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementWakeCounter(int value)
    {
        WakeCounter += value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void decrementWakeCounter(int value)
    {
        if (WakeCounter > phaseThreshold[0]) WakeCounter -= value;
        Debug.Log("Cat counter: " + WakeCounter);
    }

    public void isCounterLocked() {
        if (WakeCounter >= phaseThreshold[4]) IsLocked = true;
    }
}
