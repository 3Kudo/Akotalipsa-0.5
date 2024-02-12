using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public Animator anim;
    public Sprite menuSprite;
    public GameObject curtain;
    public GameObject[] buttons;


    public IEnumerator OnMouseDown()
    {
        curtain.SetActive(true);
        anim.SetBool("Rozwiniete", true);
        this.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.65f);
        for (int i = 0; i < 4; i++)
            buttons[i].SetActive(true);

    }

    public void OnMouseEnter()
    {
        Sprite tym = this.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<SpriteRenderer>().sprite = menuSprite;
        menuSprite = tym;
    }

    public void OnMouseExit()
    {
        Sprite tym = this.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<SpriteRenderer>().sprite = menuSprite;
        menuSprite = tym;
    }
}
