using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void Start()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)0.70;
        color.g = (float)0.70;
        color.b = (float)0.70;
        GetComponent<SpriteRenderer>().color = color;
    }
    public void OnMouseDown()
    {
        MouseControle.instance.Default();
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)0.70;
        color.g = (float)0.70;
        color.b = (float)0.70;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseOver()
    {
        MouseControle.instance.Clickable();
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)1;
        color.g = (float)1;
        color.b = (float)1;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseExit()
    {
        MouseControle.instance.Default();
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)0.70;
        color.g = (float)0.70;
        color.b = (float)0.70;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseUpAsButton()
    {
        MouseControle.instance.Default();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
