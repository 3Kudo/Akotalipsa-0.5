using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
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
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)0.70;
        color.g = (float)0.70;
        color.b = (float)0.70;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseEnter()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)1;
        color.g = (float)1;
        color.b = (float)1;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseExit() 
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.r = (float)0.70;
        color.g = (float)0.70;
        color.b = (float)0.70;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnMouseUpAsButton()
    {
        for (int i = 0; i < 4; i++)
            GetComponentInParent<SideMenu>().buttons[i].SetActive(false);
        GetComponentInParent<SideMenu>().curtain.SetActive(false);
        GetComponentInParent<SideMenu>().anim.SetBool("Rozwiniete", false);
        GetComponentInParent<SideMenu>().GetComponent<PolygonCollider2D>().enabled = true;
    }
}
