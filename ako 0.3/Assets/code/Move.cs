using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//klasas odpowiedzalna za ruch
public class Move : MonoBehaviour
{

    public GameObject pionek, arrowPattern, arrow;
    public Transform parent;

    public Transform[] waitPoints;

    [HideInInspector]
    public int waitPointIndex = 0;
      
    public AudioClip[] soundTracks;
    AudioSource AS;

    public bool defence, activeArrow;

    //ruch pionka
    public bool ruch = false;
    public int pozycja = 0;
    private bool finished;
    private float moveSpeed = 2f;

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
    // Update is called once per frame
    private void Update()
    {
        //wykonanie ruchu, nie wiem czy to jest dobry pomysł że to tutaj wstawiłem po porstu lepiej tutaj wygląda ruch
        if (ruch)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitPoints[pozycja].transform.position, moveSpeed * Time.deltaTime);


            if (transform.position == waitPoints[pozycja].transform.position && transform.position != waitPoints[waitPointIndex].transform.position)
            {
                if (waitPointIndex > pozycja)
                {
                    moveSpeed = 20f;
                    pozycja++;
                }
                else
                {
                    moveSpeed = 12f;
                    pozycja--;
                }
            }

            if (AS.isPlaying == false)
            {
                AS.clip = soundTracks[0];

                AS.Play();
            }
            if (transform.position == waitPoints[waitPointIndex].transform.position)
            {
                ruch = false;
                AS.Stop();
                if (waitPointIndex != 0)
                {
                    GetComponentInParent<Player>().MoveTheSame(pionek,
                        waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y);
                    GameRules.Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa, pionek);

                    if (GameRules.diceNumber < 6)
                    {
                        GameRules.whoseTurn++;
                        if (GameRules.whoseTurn == 5)
                        {
                            GameRules.whoseTurn = 1;
                        }
                    }
                    GameRules.Turn();
                    GameRules.diceNumber = 0;
                }
            }
        }

    }

    private void OnMouseEnter()
    {
        Debug.Log(GameRules.GetTura());
        if (GameRules.GetTura() == GetComponentInParent<Player>().gracz && arrow==null)
        {
            activeArrow = false;
            arrow = Instantiate(arrowPattern, parent) as GameObject;
            arrow.SetActive(true);
        }
            
    }

    private IEnumerator OnMouseExit()
    {
        yield return new WaitForSeconds(0.1f);
        if (activeArrow == false)
        {
            Destroy(arrow);
            arrow = null;
        }
    }
    private void OnMouseDown()
    {
        if (GetComponentInParent<Player>().active && MoveEnabled())
            MoveOn();
    }

    private void MoveOn()
    {
        moveSpeed = 20f;
        GetComponentInParent<Player>().active = false;
        if (GetComponentInParent<Player>().activePowerup)
            pionek.GetComponent<FrogPowerup>().Use();
        if (waitPointIndex == 0 && GameRules.diceNumber >= 6)
        {
            pozycja++;
            waitPointIndex++;
            GameRules.onBoard.Add(pionek);
        }
        else if((waitPointIndex + GameRules.diceNumber) <= waitPoints.Length - 1)
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
            countPosition = (waitPoints.Length-1) - countPosition;
            if (countPosition < waitPointIndex)
                pozycja--;
            else if (countPosition > waitPointIndex)
                pozycja++;
            waitPointIndex = countPosition;
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }
        int dice=GameRules.diceNumber;
        ruch = true;
        if (waitPointIndex == waitPoints.Length - 1)
        {
            finished = true;
            GameRules.onBoard.Remove(pionek);
            GetComponentInParent<Player>().ChceckPlayerFinished(dice);
        }
    }

    //sprawdza czy gracz moze sie poruszac
    public bool MoveEnabled()
    {
        //wyłączenie oborny
        defence = false;


        if(finished)
            return false;
        if(waitPointIndex == 0 && (GameRules.diceNumber == 6 || (GameRules.diceNumber >= 6 && GetComponentInParent<Player>().activePowerup)))
            return true;
        if (waitPointIndex > 0)
            return true;
        return false;
    }

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
