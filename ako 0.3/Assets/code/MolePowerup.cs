using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolePowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<PlayerMole>().active)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            if (GetComponentInParent<Mole>().poweruopActive)
            {
                Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
                GetComponentInParent<SpriteRenderer>().sprite = powerup;
                powerup = sprite;
            }
        }
    }

    private void OnMouseDown()
    {
        if (GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active)
        {
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<Mole>().poweruopActive = !GetComponentInParent<Mole>().poweruopActive;
            GetComponentInParent<PlayerMole>().ResetPowerupActive(GetComponentInParent<Mole>().pionek);
        }
    }
}
