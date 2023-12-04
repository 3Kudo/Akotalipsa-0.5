using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExit : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (true && GetComponentInParent<Move>().waitPointIndex==0 && GameRules.diceNumber==7)
        {
            // tutaj bedzie liczenie pieniedzy
            GetComponentInParent<Move>().waitPointIndex=1;
            GameRules.diceNumber = 6;
            GetComponentInParent<Move>().ruch = true;
            Destroy(GetComponentInParent<Move>().arrow);
            GetComponentInParent<Move>().arrow = null;
        }
    }
}
