using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrogPowerup : MonoBehaviour
{
    public void Use()
    {
        GameRules.diceNumber += 2;
        GetComponentInParent<Player>().activePowerup = false;
    }
}
