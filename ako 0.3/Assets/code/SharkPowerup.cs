using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        if (GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<PlayerShark>().active)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            if (GetComponentInParent<Shark>().powerupActive)
            {
                Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
                GetComponentInParent<SpriteRenderer>().sprite = powerup;
                powerup = sprite;
            }
        }
    }

    private void OnMouseDown()
    {
        if (GetComponentInParent<Move>().waitPointIndex > 0 && !GetComponentInParent<PlayerShark>().active)
        {
            Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
            GetComponentInParent<SpriteRenderer>().sprite = powerup;
            powerup = sprite;
            GetComponentInParent<Shark>().powerupActive = !GetComponentInParent<Shark>().powerupActive;
            GetComponentInParent<PlayerShark>().ResetPowerupActive(GetComponentInParent<Shark>().pionek);
        }
    }



}
