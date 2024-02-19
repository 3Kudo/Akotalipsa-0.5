using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolePowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        if (GetComponentInParent<Mole>().poweruopActive)
        {
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
        else if(GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<Player>().coin < 2 || GetComponentInParent<PlayerMole>().active)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseUpAsButton()
    {
        if ( (GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 2) ||
            GetComponentInParent<Mole>().poweruopActive)
        {
            MouseControle.instance.Default();
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<Mole>().poweruopActive = !GetComponentInParent<Mole>().poweruopActive;
            if (GetComponentInParent<Mole>().poweruopActive)
            {
                GetComponentInParent<Player>().DecraseCoins(2);
                GetComponentInParent<PlayerMole>().ResetPowerupActive(GetComponentInParent<Mole>().pionek);
            }
            else
                GetComponentInParent<Player>().IncreaseCoins(2);
            
        }
    }

    private void OnMouseOver()
    {
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 2) ||
            GetComponentInParent<Mole>().poweruopActive)
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
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 2) ||
            GetComponentInParent<Mole>().poweruopActive)
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
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 2) ||
            GetComponentInParent<Mole>().poweruopActive)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
