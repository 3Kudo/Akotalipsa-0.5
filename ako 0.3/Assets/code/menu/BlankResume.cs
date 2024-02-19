using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankResume : MonoBehaviour
{
    private void OnMouseDown()
    {
        for (int i = 0; i < GetComponentInParent<SideMenu>().buttons.Length; i++)
            GetComponentInParent<SideMenu>().buttons[i].SetActive(false);
        GetComponentInParent<SideMenu>().curtain.SetActive(false);
        GetComponentInParent<SideMenu>().anim.SetBool("Rozwiniete", false);
        GetComponentInParent<SideMenu>().GetComponent<PolygonCollider2D>().enabled = true;
    }
}
