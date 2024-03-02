using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA;

public class RollDice : MonoBehaviour
{
    // ogólnie to nie wiem czy wgl musicie ruszaæ t¹ klasê

    private Sprite[] dicesSides;
    private SpriteRenderer rend;
    private static bool diceThrowAllowed = true;
    private AudioSource AS;
    public AudioClip[] soundTracks;
    

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        dicesSides = Resources.LoadAll<Sprite>("Dice/");
        rend.sprite = dicesSides[5];
        Color color = rend.color;
        color.r = (float)0.90;
        color.g = (float)0.90;
        color.b = (float)0.90;
        rend.color = color;

    }

    private void Update()
    {
        if (GetComponentInParent<GameRules>().diceNumber == 0)
        {      
            diceThrowAllowed = true;
            Color color = rend.color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            rend.color = color;
            GetComponentInParent<GameRules>().diceNumber =7;
        }
    }

    private void OnMouseEnter()
    {
        if (diceThrowAllowed)
        {
            MouseControle.instance.Clickable();
            Color color = rend.color;
            color.r = 1;
            color.g = 1;
            color.b = 1;
            rend.color = color;
        }
    }

    private void OnMouseExit()
    {
        if (diceThrowAllowed)
        {
            MouseControle.instance.Default();
            Color color = rend.color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            rend.color = color;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (diceThrowAllowed && GetComponentInParent<GameRules>().miejsce != 3)
        {
            MouseControle.instance.Clickable();
            Color color = rend.color;
            color.r = 1;
            color.g = 1;
            color.b = 1;
            rend.color = color;
            AS.clip = soundTracks[0];
            AS.Play();
            StartCoroutine("RollTheDice");
        }
    }
    private void OnMouseDrag()
    {
        if (diceThrowAllowed)
        {
            MouseControle.instance.Half();
            Color color = rend.color;
            color.r = (float)0.70;
            color.g = (float)0.70;
            color.b = (float)0.70;
            rend.color = color;
        }
    }
    private IEnumerator RollTheDice()
    {
        diceThrowAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 4; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = dicesSides[randomDiceSide];
            yield return new WaitForSeconds(0.2f);
            MouseControle.instance.Default();
        }
        Color color = rend.color;
        color.r = (float)0.60;
        color.g = (float)0.60;
        color.b = (float)0.60;
        rend.color = color;
        GetComponentInParent<GameRules>().diceNumber = randomDiceSide + 1;
        if (GetComponentInParent<GameRules>().diceNumber == 6)
        {
            AS.clip = soundTracks[1];
            AS.Play();
        }
        GetComponentInParent<GameRules>().MovePlayer();
    }

    public static void RollDiceBlock()
    {
        diceThrowAllowed = false;
    }
}