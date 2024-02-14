using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPawn : MonoBehaviour
{
    public int waitPointIndex = 0;
    private void OnMouseDown()
    {
        GetComponentInParent<Move>().MoveOn(waitPointIndex);
    }
}
