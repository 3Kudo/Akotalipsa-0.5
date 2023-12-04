using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowButton : MonoBehaviour
{
    public GameObject window;
    public SpriteRenderer sprite;

    void Start()
    {
        window.SetActive(false);
    }

    private void OnMouseEnter()
    {
        GetComponentInParent<Move>().activeArrow = true;
    }
    private IEnumerator OnMouseExit()
    {
        yield return new WaitForSeconds(0.1f);
        if (sprite.enabled)
        {
            Destroy(GetComponentInParent<Move>().arrow);
            GetComponentInParent<Move>().arrow=null;
        }
    }
    private void OnMouseDown()
    {
        sprite.enabled = false;
        window.SetActive(true);
    }
}
