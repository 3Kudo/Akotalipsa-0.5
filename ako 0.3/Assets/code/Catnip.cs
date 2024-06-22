using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour
{
    public GameObject cat;

    private AudioSource AS;
    public AudioClip[] sfx;

    public void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void setCatnipButton()
    {
        if (GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().coin >= 2 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().coin >= 2 && !cat.GetComponent<Cat>().isLocked)
        {
            MouseControle.instance.Default();
            GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().DecraseCoins(2);
            cat.GetComponent<Cat>().incrementWakeCounter(4);
            cat.GetComponent<Cat>().phaseCheck();
            GetComponentInParent<GameRules>().SetCatnipMik();
            if (cat.GetComponent<Cat>().Phase == 0 || cat.GetComponent<Cat>().Phase == 1)
            {
                AS.clip = sfx[0];
                AS.Play();
            }
            else
            {
                AS.clip = sfx[1];
                AS.Play();
            }
        }
    }

    private void OnMouseOver()
    {
        if (GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().coin >= 2 && !cat.GetComponent<Cat>().isLocked)
        {
            MouseControle.instance.Clickable();
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)1;
            color.g = (float)1;
            color.b = (float)1;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseExit()
    {
        if (GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().coin >= 2 && !cat.GetComponent<Cat>().isLocked)
        {
            MouseControle.instance.Default();
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseDrag()
    {
        if (GetComponentInParent<GameRules>().GetTura().GetComponent<Player>().coin >= 2 && !cat.GetComponent<Cat>().isLocked)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.70;
            color.g = (float)0.70;
            color.b = (float)0.70;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
