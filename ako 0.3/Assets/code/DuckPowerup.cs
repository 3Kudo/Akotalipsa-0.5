using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPowerup : MonoBehaviour
{
    public Sprite powerup;

    private void Start()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount == 0 || GetComponentInParent<Move>().waitPointIndex == 0)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.50;
            color.g = (float)0.50;
            color.b = (float)0.50;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnMouseDown()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex!=0)
        {
            GetComponentInParent<Move>().ToNormalState();
            Destroy(GetComponentInParent<Move>().shadowPawn);
            GetComponentInParent<Duck>().powerupActive = true;
            GetComponentInParent<Move>().waitPointIndex += ammount;
            GetComponentInParent<Move>().ruch = true;
        }
    }
}
