using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPawn : MonoBehaviour
{
    public int waitPointIndex = 0;
    private void OnMouseDown()
    {
        MouseControle.instance.Default();
        GetComponentInParent<Move>().MoveOn(waitPointIndex);
    }

    private void OnMouseEnter()
    {
        MouseControle.instance.Clickable();
    }

    private void OnMouseExit()
    {
        MouseControle.instance.Default();
    }
}
