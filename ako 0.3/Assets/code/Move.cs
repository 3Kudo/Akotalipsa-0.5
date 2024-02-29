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

    public int waitPointIndex;
      
    public AudioClip[] soundTracks;
    [HideInInspector] public AudioSource AS;

    public bool defence;

    //ruch pionka
    [HideInInspector] public bool ruch = false, chosen = false;
    [HideInInspector] public int pozycja;
    [HideInInspector] public bool finished;

    [HideInInspector] public float changeValue = (float)0.1;
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
        pozycja = waitPointIndex;
        
    }

    public void setFlashAmount(float amount)
    {
        this.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", amount);
    }

    public float getFlashAmount()
    {
        return this.GetComponent<SpriteRenderer>().material.GetFloat("_FlashAmount");
    }

    public void onUpdate()
    {
        if (getFlashAmount() > (float)1.0 || getFlashAmount() < (float)0.0)
            changeValue = -changeValue;
        setFlashAmount(getFlashAmount() + changeValue);
    }

    private IEnumerator OnMouseDown()
    {
        int position = 0;
        GetComponentInParent<Player>().SetPawnToNormal(pionek);
        if (GameRules.GetTura() == GetComponentInParent<Player>().gracz)
        {
            Sprite tymSrpite = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            PawnSprite = tymSrpite;
            chosen = !chosen;
            position = ShadowPawnPosition(false);
            if (!GetComponentInParent<Player>().PowerupWindowInteraction(pionek))
                yield return new WaitForSeconds(0.45f);
            GetComponentInParent<Player>().SetPowerups(parent);
        }


        


        if (shadowPawn != null)
        {
            Destroy(shadowPawn);
            shadowPawn = null;
            GetComponentInParent<Player>().MoveOut(waitPoints[position], null);
        }
        else if (GetComponentInParent<Player>().active && MoveEnabled())
        {
            shadowPawn = Instantiate(shadowPawnPattern, parent) as GameObject;
            shadowPawn.transform.position = waitPoints[position].transform.position;
            shadowPawn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            shadowPawn.GetComponent<PolygonCollider2D>().points = this.GetComponent<PolygonCollider2D>().points;
            shadowPawn.GetComponent<ShadowPawn>().waitPointIndex = position;
            GetComponentInParent<Player>().MoveTheSame(shadowPawn, waitPoints[position].transform.position.x, waitPoints[position].transform.position.y, position);
        }
    }

    private void OnMouseEnter()
    {
        if(GameRules.GetTura() == GetComponentInParent<Player>().gracz)
            MouseControle.instance.Clickable();
    }

    private void OnMouseExit()
    {
        if (GameRules.GetTura() == GetComponentInParent<Player>().gracz)
            MouseControle.instance.Default();
    }

    public int ShadowPawnPosition(bool activPowerup)
    {
        if (waitPointIndex == 0 && GameRules.diceNumber >= 6)
            return waitPointIndex + 1;
        if (activPowerup || GameRules.fluff.Count == 0)
        {
            if ((waitPointIndex + GameRules.diceNumber) <= waitPoints.Length - 1)
            {
                for (int j = 0; j < GameRules.fluff.Count; j++)
                    if (waitPoints[waitPointIndex + GameRules.diceNumber] == GameRules.fluff[j].GetComponent<Fluff>().waitPoint)
                        return waitPointIndex + GameRules.diceNumber - 1;
                return waitPointIndex + GameRules.diceNumber;
            }
        }
        else
        {
            if ((waitPointIndex + GameRules.diceNumber) <= waitPoints.Length - 1)
            {
                for (int j = 1; j <= GameRules.diceNumber; j++)
                    for (int i = 0; i < GameRules.fluff.Count; i++)
                        if (waitPoints[waitPointIndex + j] == GameRules.fluff[i].GetComponent<Fluff>().waitPoint)
                            return waitPointIndex + j - 1;
                return waitPointIndex + GameRules.diceNumber;
            }
        }
        int countPosition = waitPointIndex;
        countPosition = countPosition + GameRules.diceNumber;
        countPosition = countPosition - (waitPoints.Length - 1);
        countPosition = (waitPoints.Length - 1) - countPosition;
        for (int j = 0; j < GameRules.fluff.Count; j++)
            if (waitPoints[countPosition] == GameRules.fluff[j].GetComponent<Fluff>().waitPoint)
                countPosition++;
        return countPosition;
    }

    public void MoveOn(int moveTo)
    {
        moveSpeed = 20f;
        GetComponentInParent<Player>().active = false;
        if(moveTo != 0)
        {
            Transform waitPoint = waitPoints[waitPointIndex];
            if (moveTo < waitPointIndex)
                pozycja--;
            else if (moveTo > waitPointIndex)
                pozycja++;
            if (waitPointIndex == 0)
                GameRules.onBoard.Add(pionek);
            waitPointIndex = moveTo;
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }
        else if (waitPointIndex == 0 && GameRules.diceNumber >= 6)
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
        ruch = true;
        if (waitPointIndex == waitPoints.Length - 1)
        {
            finished = true;
            GameRules.onBoard.Remove(pionek);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<Player>().ChceckPlayerFinished();
        }
        ToNormalState();
    }


    public void ToNormalState()
    {
        int index = 0;
        if (shadowPawn!=null)
             index = shadowPawn.GetComponent<ShadowPawn>().waitPointIndex;
        Destroy(shadowPawn);
        shadowPawn = null;
        if (chosen)
        {
            GetComponentInParent<Player>().SetPowerups(parent);
            Sprite tymSrpite = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            PawnSprite = tymSrpite;
            chosen = !chosen;
            GetComponentInParent<Player>().PowerupWindowInteraction(pionek);
        }
        if (index != 0)
            GetComponentInParent<Player>().MoveOut(waitPoints[index], pionek);
    }

    public bool IsChosen(bool isPowerup)
    {
        if(!MoveEnabled())
            return chosen;
        if (chosen)
        {
            shadowPawn = Instantiate(shadowPawnPattern, parent) as GameObject;
            int position = ShadowPawnPosition(isPowerup);
            shadowPawn.GetComponent<ShadowPawn>().waitPointIndex = position;
            shadowPawn.transform.position = waitPoints[position].transform.position;
            shadowPawn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
            shadowPawn.GetComponent<PolygonCollider2D>().points = this.GetComponent<PolygonCollider2D>().points;
            GetComponentInParent<Player>().MoveTheSame(shadowPawn, waitPoints[position].transform.position.x, waitPoints[position].transform.position.y, position);
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
