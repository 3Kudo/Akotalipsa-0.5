using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseOver()
    {
        MouseControle.instance.Clickable();
    }
    private void OnMouseExit() 
    {
        MouseControle.instance.Default();
    }
}
