using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPawn : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<Move>().MoveOn();
        GetComponentInParent<Player>().SetPawnToNormal(null);
    }
}
