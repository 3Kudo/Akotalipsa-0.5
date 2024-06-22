using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPowerup : MonoBehaviour
{
    public Sprite powerup;
    [SerializeField] private int price;

    private void Start()
    {
        if ((GetComponentInParent<Player>().coin < price || !GetComponentInParent<Player>().active) && !GetComponentInParent<PlayerShark>().powerupActive)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.40;
            color.g = (float)0.40;
            color.b = (float)0.40;
            GetComponent<SpriteRenderer>().color = color;
        }
        else if (GetComponentInParent<PlayerShark>().powerupActive)
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
        if ((GetComponentInParent<Player>().coin < price || !GetComponentInParent<Player>().active) && !GetComponentInParent<PlayerShark>().powerupActive)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.40;
            color.g = (float)0.40;
            color.b = (float)0.40;
            GetComponent<SpriteRenderer>().color = color;
        }
        else if (GetComponentInParent<PlayerShark>().powerupActive)
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
        if ((GetComponentInParent<Player>().coin >= price || GetComponentInParent<PlayerShark>().powerupActive) && GetComponentInParent<Player>().active)
        {
            MouseControle.instance.Default();
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<PlayerShark>().powerupActive = !GetComponentInParent<PlayerShark>().powerupActive;
            if (GetComponentInParent<PlayerShark>().active && GetComponentInParent<PlayerShark>().powerupActive)
            {
                GetComponentInParent<Player>().DecraseCoins(4);
                GetComponentInParent<Shark>().PowerSound = true;
                GetComponentInParent<GameRules>().SetCatnipMik();
            }
            else if (GetComponentInParent<PlayerShark>().active && !GetComponentInParent<PlayerShark>().powerupActive)
            {
                GetComponentInParent<Player>().IncreaseCoins(4);
                GetComponentInParent<Shark>().PowerSound = false;
                GetComponentInParent<GameRules>().SetCatnipMik();
            }

        }
    }

    private void OnMouseEnter()
    {
        if ((GetComponentInParent<Player>().coin >= price || GetComponentInParent<PlayerShark>().powerupActive) && GetComponentInParent<Player>().active)
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
        if ((GetComponentInParent<Player>().coin >= price || GetComponentInParent<PlayerShark>().powerupActive) && GetComponentInParent<Player>().active)
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
        if ((GetComponentInParent<Player>().coin >= price || GetComponentInParent<PlayerShark>().powerupActive) && GetComponentInParent<Player>().active)
        {
            MouseControle.instance.Half();
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.70;
            color.g = (float)0.70;
            color.b = (float)0.70;
            GetComponent<SpriteRenderer>().color = color;
        }
    }



}
