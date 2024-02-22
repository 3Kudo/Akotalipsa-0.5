using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        if (GetComponentInParent<Shark>().powerupActive)
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
        else if (GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<PlayerShark>().active || GetComponentInParent<Player>().coin < 5)
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

    public void SetUp()
    {
        if (GetComponentInParent<Shark>().powerupActive)
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
        else if (GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<PlayerShark>().active || GetComponentInParent<Player>().coin < 5)
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
        if ( (GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerShark>().active && GetComponentInParent<Player>().coin >= 5) ||
            GetComponentInParent<Shark>().powerupActive)
        {
            MouseControle.instance.Default();
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<Shark>().powerupActive = !GetComponentInParent<Shark>().powerupActive;
            if (GetComponentInParent<Shark>().powerupActive)
            {
                GetComponentInParent<Player>().DecraseCoins(5);
                GameRules.SetCatnipMik();
                GetComponentInParent<PlayerShark>().ResetPowerupActive(GetComponentInParent<Shark>().pionek);
            }
            else
            {
                GetComponentInParent<Player>().IncreaseCoins(5);
                GameRules.SetCatnipMik();
            }
        }
    }

    private void OnMouseOver()
    {
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 5) ||
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
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 5) ||
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
        if ((GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerMole>().active && GetComponentInParent<Player>().coin >= 5) ||
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
