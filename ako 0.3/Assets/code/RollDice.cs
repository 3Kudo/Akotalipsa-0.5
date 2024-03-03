using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA;

public class RollDice : MonoBehaviour
{
    // og�lnie to nie wiem czy wgl musicie rusza� t� klas�

    private Sprite[] dicesSides;
    private SpriteRenderer rend;
    private static bool diceThrowAllowed = true;
    private AudioSource AS;
    public AudioClip[] soundTracks;
    public float changeValue = 0.01f;
    public float maxBlinkValue = 0.4f;
    public float minBlinkValue = 0.0f;
    [HideInInspector]
    public bool isMouseOver = false;

    public void setFlashAmount(float amount)
    {
        this.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", amount);
    }

    public float getFlashAmount()
    {
        return this.GetComponent<SpriteRenderer>().material.GetFloat("_FlashAmount");
    }
    

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
        if (GameRules.diceNumber == 0)
        {      
            diceThrowAllowed = true;
            Color color = rend.color;
            color.r = (float)0.80;
            color.g = (float)0.80;
            color.b = (float)0.80;
            rend.color = color;
            GameRules.diceNumber=7;
        }
        if (diceThrowAllowed && !isMouseOver)
        {
            if (getFlashAmount() > maxBlinkValue|| getFlashAmount() < minBlinkValue)
                changeValue = -changeValue;
            setFlashAmount(getFlashAmount() + changeValue);
        }
        else
        {
            setFlashAmount(0.0f);
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
        isMouseOver = true;
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
        isMouseOver = false;
    }

    private void OnMouseUpAsButton()
    {
        if (diceThrowAllowed && GameRules.miejsce != 3)
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
        GameRules.diceNumber = randomDiceSide + 1;
        if (GameRules.diceNumber == 6)
        {
            AS.clip = soundTracks[1];
            AS.Play();
        }
        GameRules.MovePlayer();
    }

    public static void RollDiceBlock()
    {
        diceThrowAllowed = false;
    }
}