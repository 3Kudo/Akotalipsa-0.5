using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//klasas odpowiedzalna za ruch
public class Move : MonoBehaviour
{

    public GameObject pionek;

    public Transform[] waitPoints;

    [SerializeField]
    private float moveSpeed = 2f;

    [HideInInspector]
    public int waitPointIndex = 0;

    //ruch pionka
    public bool ruch = false;

    public int StaraPozycja = 0;
    public int i = 0;

    //bool, ktory okresla czy pionek zakonczyl juz gre (jesli false to skonczyl i nie bedzie aktywny, jesli true to jest dalej w grze)
    public bool bDalejWGrze;

    // Start is called before the first frame update
    void Start()
    {
        //ustawienie pionk�w na miejscu startowym
        transform.position = waitPoints[waitPointIndex].transform.position;

        //ustwienie wartosci boola bDalejWGrze na starcie na true
        bDalejWGrze = true;
    }
    // Update is called once per frame
    private void Update()
    {
        //wykonanie ruchu, nie wiem czy to jest dobry pomys� �e to tutaj wstawi�em po porstu lepiej tutaj wygl�da ruch
        if (ruch)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitPoints[StaraPozycja + 1].transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == waitPoints[StaraPozycja + 1].transform.position && transform.position != waitPoints[waitPointIndex].transform.position)
                {
                StaraPozycja++;
                }
                if (transform.position == waitPoints[waitPointIndex].transform.position)
                {
                    ruch = false;
                if (waitPointIndex != 0)
                {
                    GetComponentInParent<Player>().MoveTheSame(pionek, waitPoints[waitPointIndex].transform.position.x, waitPoints[waitPointIndex].transform.position.y, false);
                    GameRules.Chceck(waitPoints[waitPointIndex], GetComponentInParent<Player>().nazwa);
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
        GetComponentInParent<Player>().active = false;
        Debug.Log(GetComponentInParent<Player>().active);
        if (waitPointIndex == 0 && GameRules.diceNumber == 6)
        {
            StaraPozycja = waitPointIndex;
            waitPointIndex++;
            GameRules.onBoard.Add(pionek);
           
        }
        else if ((waitPointIndex + GameRules.diceNumber) > waitPoints.Length - 1)
            {
            StaraPozycja = waitPointIndex;
            //jesli waitpoint index + wartosc na kostce jest wiecej niz ilosc waitpointow przypisanych to waitPointIndex zostaje zmieniony na ilosc elementow w tabeli - to co bylo ponad ilosc elementow
            waitPointIndex = (waitPoints.Length - 1) - ((waitPointIndex + GameRules.diceNumber) - (waitPoints.Length - 1));
            }
        else if (waitPointIndex != 0 && waitPointIndex < waitPoints.Length - 6)
            {
            StaraPozycja = waitPointIndex;
            waitPointIndex += GameRules.diceNumber;
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

}
