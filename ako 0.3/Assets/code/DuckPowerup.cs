using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPowerup : MonoBehaviour
{
    public Sprite powerup;
    [SerializeField] private int price;
    private void Start()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount == 0 || GetComponentInParent<Move>().waitPointIndex == 0 || GetComponentInParent<Player>().coin < price)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.40;
            color.g = (float)0.40;
            color.b = (float)0.40;
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex!=0 && GetComponentInParent<Player>().coin >= price)
        {
            MouseControle.instance.Default();
            GetComponentInParent<Move>().ToNormalState(true);
            GetComponentInParent<Player>().DecraseCoins(3);
            GetComponentInParent<Duck>().PowerSound = true;
            GetComponentInParent<GameRules>().SetCatnipMik();
            Destroy(GetComponentInParent<Move>().shadowPawn);
            GetComponentInParent<Duck>().powerupActive = true;
            foreach (GameObject wall in GetComponentInParent<GameRules>().fluff)
            {
                if (wall.GetComponent<Fluff>().waitPoint == GetComponentInParent<Move>().waitPoints[GetComponentInParent<Move>().waitPointIndex + ammount])
                {
                    ammount++;
                    break;
                }
            }
            GetComponentInParent<Move>().waitPointIndex += ammount;
            GetComponentInParent<Player>().MoveOut(GetComponentInParent<Move>().waitPoints[GetComponentInParent<Move>().waitPointIndex - ammount], GetComponentInParent<Move>().pionek);
            GetComponentInParent<Move>().ruch = true;
        }
    }

    private void OnMouseOver()
    {
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= price)
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= price)
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
        int ammount = GetComponentInParent<PlayerDuck>().WaitPointIndex(GetComponentInParent<Move>().pionek);
        if (ammount > 0 && GetComponentInParent<Move>().waitPointIndex != 0 && GetComponentInParent<Player>().coin >= price)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
