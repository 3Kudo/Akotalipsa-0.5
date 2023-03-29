using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//klasas odpowiedzalna za ruch
public class Move : MonoBehaviour
{

    public GameObject pionek;

    public Transform[] waitPoints;

    private float moveSpeed = 2f;

    [HideInInspector]
    public int waitPointIndex = 0;

    public AudioClip[] soundTracks;
    AudioSource AS;

    //ruch pionka
    public bool ruch = false;
    public int Pozycja = 0;
    public int i = 0;

    //bool, ktory okresla czy pionek zakonczyl juz gre (jesli false to skonczyl i nie bedzie aktywny, jesli true to jest dalej w grze)
    public bool bDalejWGrze;

    // Start is called before the first frame update
    void Start()
    {
        //ustawienie pionków na miejscu startowym
        transform.position = waitPoints[waitPointIndex].transform.position;

        //ustwienie wartosci boola bDalejWGrze na starcie na true
        bDalejWGrze = true;
        AS = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void Update()
    {
        //wykonanie ruchu, nie wiem czy to jest dobry pomysł że to tutaj wstawiłem po porstu lepiej tutaj wygląda ruch
        if (ruch)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitPoints[Pozycja].transform.position, moveSpeed * Time.deltaTime);


            if (transform.position == waitPoints[Pozycja].transform.position && transform.position != waitPoints[waitPointIndex].transform.position)
            {
                if (waitPointIndex > Pozycja)
                {
                    moveSpeed = 4f;
                    Pozycja++;
                }
                else
                {
                    moveSpeed = 8f;
                    Pozycja--;
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
                    Debug.Log("done");

                    if (GameRules.diceNumber != 6)
                    {
                        GameRules.whoseTurn++;
                        if (GameRules.whoseTurn == 5)
                        {
                            GameRules.whoseTurn = 1;
                        }
                    }
                    GameRules.diceNumber = 0;
                    GameRules.Turn();
                }
            }
        }

    }
    private void OnMouseDown()
    {
        if (GetComponentInParent<Player>().active && MoveEnabled(ref bDalejWGrze))
            MoveOn();
    }

    private void MoveOn()
    {
        moveSpeed = 4f;
        GetComponentInParent<Player>().active = false;
        Debug.Log(GetComponentInParent<Player>().active);
        if (waitPointIndex == 0 && GameRules.diceNumber == 6)
        {
            Pozycja++;
            waitPointIndex++;
            GameRules.onBoard.Add(pionek);
        }
        else if ((waitPointIndex + GameRules.diceNumber) > waitPoints.Length - 1)
        {
            Pozycja++;
            Transform waitPoint = waitPoints[waitPointIndex];
            //jesli waitpoint index + wartosc na kostce jest wiecej niz ilosc waitpointow przypisanych to waitPointIndex zostaje zmieniony na ilosc elementow w tabeli - to co bylo ponad ilosc elementow
            waitPointIndex = (waitPoints.Length - 1) - ((waitPointIndex + GameRules.diceNumber) - (waitPoints.Length - 1));
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }
        else if(waitPointIndex !=0 && waitPointIndex < waitPoints.Length-6)
        {
            Pozycja++;
            Transform waitPoint = waitPoints[waitPointIndex];
            waitPointIndex += GameRules.diceNumber;
            GetComponentInParent<Player>().MoveOut(waitPoint, pionek);
        }


        ruch = true;
    }

    //sprawdza czy gracz moze sie poruszac
    public bool MoveEnabled(ref bool bDalejWGrze)
    {
        if (waitPointIndex == 0 && GameRules.diceNumber == 6)
        {
            bDalejWGrze = true;
            return true;
        }
        else if ((waitPointIndex + GameRules.diceNumber) == waitPoints.Length - 1)
        {
            bDalejWGrze = false;
            return false;
        }
        else if (waitPointIndex > 0 && waitPointIndex != (waitPoints.Length - 1))
        {
            bDalejWGrze = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Set(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    public Transform GetWaitpoint()
    {
        return waitPoints[waitPointIndex];
    }
}
