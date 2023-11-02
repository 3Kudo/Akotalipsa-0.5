using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWindow : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(GetComponentInParent<Move>().arrow);
        GetComponentInParent<Move>().arrow = null;
    }
}