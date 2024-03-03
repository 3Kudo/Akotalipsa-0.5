using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControle : MonoBehaviour
{
    public Texture2D defaultCursor, clickableCursor, halfClicked;

    public static MouseControle instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Clickable()
    {
        Cursor.SetCursor(clickableCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Default()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Half()
    {
        Cursor.SetCursor(halfClicked, Vector2.zero, CursorMode.Auto);
    }
}
