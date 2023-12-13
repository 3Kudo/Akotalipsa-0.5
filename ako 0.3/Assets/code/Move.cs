using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//klasas odpowiedzalna za ruch
public abstract class Move : MonoBehaviour
{

    public GameObject pionek, shadowPawnPattern, shadowPawn;
    public Transform parent;
    public Sprite PawnSprite;

    public Transform[] waitPoints;

    [HideInInspector]
    public int waitPointIndex = 0;
      
    public AudioClip[] soundTracks;
    [HideInInspector] public AudioSource AS;

    public bool defence,activePowerup=false;

    //ruch pionka
    [HideInInspector] public bool ruch = false, chosen = false;
    [HideInInspector] public int pozycja = 0;
    [HideInInspector] public bool finished;
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //ustawienie pionków na miejscu startowym
        transform.position = waitPoints[waitPointIndex].transform.position;


        //ustwienie wartosci boola bDalejWGrze na starcie na true
        AS = GetComponent<AudioSource>();
        finished = false;
        defence = false;
        
    }

    private void OnMouseDown()
    {
        if (GameRules.GetTura() == GetComponentInParent<Player>().gracz)
        {
            Sprite tymSrpite = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            PawnSprite = tymSrpite;
            chosen = !chosen;
        }


        if (shadowPawn != null)
        {
            Destroy(shadowPawn);
            shadowPawn = null;
        }
        else if (GetComponentInParent<Player>().active && MoveEnabled())
        {
            shadowPawn = Instantiate(shadowPawnPattern, parent) as GameObject;
            int position = ShadowPawn();
            shadowPawn.transform.position = waitPoints[position].transform.position;
            shadowPawn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            shadowPawn.GetComponent<PolygonCollider2D>().points = this.GetComponent<PolygonCollider2D>().points;
        }
        GetComponentInParent<Player>().SetPawnToNormal(pionek);
    }

    private int ShadowPawn()
    {
        if (waitPointIndex == 0 && GameRules.diceNumber >= 6)
            return waitPointIndex + 1;
        else if ((waitPointIndex + GameRules.diceNumber) <= waitPoints.Length - 1)
            return waitPointIndex + GameRules.diceNumber;
        else
        {
            int countPosition = waitPointIndex;
            countPosition = countPosition + GameRules.diceNumber;
            countPosition = countPosition - (waitPoints.Length - 1);
            countPosition = (waitPoints.Length - 1) - countPosition;
            return countPosition;
        }
    }

    public void MoveOn()
    {
        moveSpeed = 20f;
        GetComponentInParent<Player>().active = false;
        if (waitPointIndex == 0 && GameRules.diceNumber >= 6)
        {
            pozycja++;
            waitPointIndex++;
            GameRules.onBoard.Add(pionek);
        }
        else if ((waitPointIndex + GameRules.diceNumber) <= waitPoints.Length - 1)
        {
            pozycja++;
            Transform waitPoint = waitPoints[waitPointIndex];
            waitPointIndex = waitPointIndex + GameRules.diceNumber;
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }
        else
        {
            Transform waitPoint = waitPoints[waitPointIndex];
            int countPosition = waitPointIndex;
            countPosition = countPosition + GameRules.diceNumber;
            countPosition = countPosition - (waitPoints.Length - 1);
            countPosition = (waitPoints.Length - 1) - countPosition;
            if (countPosition < waitPointIndex)
                pozycja--;
            else if (countPosition > waitPointIndex)
                pozycja++;
            waitPointIndex = countPosition;
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }
        int dice = GameRules.diceNumber;
        ruch = true;
        if (waitPointIndex == waitPoints.Length - 1)
        {
            finished = true;
            GameRules.onBoard.Remove(pionek);
            GetComponentInParent<Player>().ChceckPlayerFinished(dice);
        }
    }

    public void ToNormalState()
    {
        Destroy(shadowPawn);
        shadowPawn = null;
        if (chosen)
        {
            Sprite tymSrpite = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            PawnSprite = tymSrpite;
            chosen = !chosen;
        }
    }

    public bool IsChosen()
    {
        if (chosen)
        {
            shadowPawn = Instantiate(shadowPawnPattern, parent) as GameObject;
            int position = ShadowPawn();
            shadowPawn.transform.position = waitPoints[position].transform.position;
            shadowPawn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            shadowPawn.GetComponent<PolygonCollider2D>().points = this.GetComponent<PolygonCollider2D>().points;
        }
        return chosen;
    }

    //sprawdza czy gracz moze sie poruszac
    public abstract bool MoveEnabled();

    public void Set(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }



    public Transform GetWaitpoint()
    {
        return waitPoints[waitPointIndex];
    }

    public bool GetFinish()
    {
        return finished;
    }
}
