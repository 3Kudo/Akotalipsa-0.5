using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPowerupButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        // tutaj bedzie liczenie pieniedzy
        if (GetComponentInParent<Player>().active)
        {
            GetComponentInParent<Player>().activePowerup = false;
            Destroy(GetComponentInParent<Move>().arrow);
            GetComponentInParent<Move>().arrow = null;
        }
        else
        {
            GetComponentInParent<Player>().activePowerup = true;
            Destroy(GetComponentInParent<Move>().arrow);
            GetComponentInParent<Move>().arrow = null;
        }
    }
}
