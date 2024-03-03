using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public Animator anim;
    public Sprite menuSprite;
    public GameObject curtain;
    public GameObject[] buttons;
    public bool mouseExit=true;

    public void OnMouseEnter()
    {
        MouseControle.instance.Clickable();
        Sprite tym = this.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<SpriteRenderer>().sprite = menuSprite;
        menuSprite = tym;
    }

    public void OnMouseExit()
    {
        if (mouseExit)
        {
            MouseControle.instance.Default();
            Sprite tym = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = menuSprite;
            menuSprite = tym;
        }
    }

    public IEnumerator OnMouseUpAsButton()
    {
        mouseExit = false;
        MouseControle.instance.Clickable();
        this.GetComponent<PolygonCollider2D>().enabled = false;
        anim.SetBool("Rozwiniete", true);
        yield return new WaitForSeconds(0.65f);
        MouseControle.instance.Default();
        curtain.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(true);
    }

    public void OnMouseDrag()
    {
        MouseControle.instance.Half();
    }
}
