using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;

public class GameRules : MonoBehaviour
{

    private static GameObject shark, duck, turtle, mole, sharkTura, duckTura, frogTura, moleTura;

    //liczniki tur dla poszczegolnych graczy
    public static int sharkTurCounter, duckTurCounter, turtleTurCounter, moleTurCounter;

    private static GameObject gameOverBack, gameOver; //gameobjecty do UI konca gry
    private TextMeshProUGUI firstName, secondName, thirdName, firstTurn, secondTurn, thirdTurn; //teksty do UI konca gry
    public string whichAnimalWasSecond; //string, ktory bedzie przechowywal, ktory gracz skonczyl drugi

    public static Transform[] randomBack = new Transform[2];


    public static List<GameObject> onBoard = new List<GameObject>();

    public static bool bDuckFinished, bSharkFinished, bTurtleFinished, bMoleFinished;


    public static int whoseTurn = 0;
    public static int diceNumber = 6;
    // Start is called before the first frame update
    void Start()
    {
        //przypisanie odpowiednich obiektów
        shark = GameObject.Find("Sharks");
        duck = GameObject.Find("Ducks");
        turtle = GameObject.Find("Turtles");
        mole = GameObject.Find("Moles");

        //przypisanie elementow UI
        sharkTura = GameObject.Find("SharkT");
        duckTura = GameObject.Find("DuckT");
        frogTura = GameObject.Find("FrogT");
        moleTura = GameObject.Find("MoleT");

        //na start ustawienie, ze zaden gracz jeszcze nie wygral i wszyscy sa dalej w grze
        bDuckFinished = false;
        bSharkFinished = false;
        bTurtleFinished = false;
        bMoleFinished = false;

        //ustawienie na poczatku wszystkich obiektow na nieaktywne
        sharkTura.gameObject.SetActive(false);
        duckTura.gameObject.SetActive(false);
        moleTura.gameObject.SetActive(false);
        frogTura.gameObject.SetActive(false);

        //przypisanie nazw
        shark.GetComponent<Player>().nazwa = "shark";
        duck.GetComponent<Player>().nazwa = "duck";
        turtle.GetComponent<Player>().nazwa = "turtle";
        mole.GetComponent<Player>().nazwa = "mole";

        //na poczatku gry wyzerowanie licznikow tur
        sharkTurCounter = 0;
        duckTurCounter = 0;
        turtleTurCounter = 0;
        moleTurCounter = 0;

        //przypisanie tekstow do konca gry
        gameOverBack = GameObject.Find("gameOverBack");
        gameOver = GameObject.Find("gameOver");
        firstName = GameObject.Find("firstName").GetComponent<TextMeshProUGUI>();
        firstTurn = GameObject.Find("firstTurn").GetComponent<TextMeshProUGUI>();
        secondName = GameObject.Find("secondName").GetComponent<TextMeshProUGUI>();
        secondTurn = GameObject.Find("secondTurn").GetComponent<TextMeshProUGUI>();
        thirdName = GameObject.Find("thirdName").GetComponent<TextMeshProUGUI>();
        thirdTurn = GameObject.Find("thirdTurn").GetComponent<TextMeshProUGUI>();

        //wy³¹czenie ui do koñca gry na start
        gameOverBack.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        firstName.gameObject.SetActive(false);
        firstTurn.gameObject.SetActive(false);
        secondName.gameObject.SetActive(false);
        secondTurn.gameObject.SetActive(false);
        thirdName.gameObject.SetActive(false);
        thirdTurn.gameObject.SetActive(false);

        //zainicjonowanie zmiennej przechowujacej kto bedzie drugi na start jako pusty string
        whichAnimalWasSecond = "";

        //losowanie czyja tura i ustawienie wszystkich na false ¿eby ka¿dy mog³ rzuciæ koœci¹
        shark.GetComponent<Player>().active = false;
        duck.GetComponent<Player>().active = false;
        turtle.GetComponent<Player>().active = false;
        mole.GetComponent<Player>().active = false;
        whoseTurn = Random.Range(1, 5);
        Debug.Log(whoseTurn);
        Turn();


    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------------Pierwsza Kaczka-------------------------------------------------------------------------------------------------------------//

        if (bDuckFinished)
        {
            firstName.text = "Duck";
            firstTurn.text = duckTurCounter.ToString();

            //-----#####-----Wyœwietlanie poprawnie-----#####-----//

            //-----#####-----przypisanie kto jest drugi-----#####-----//


            if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Shark";
                }
            }
            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Turtle";
                }
            }
            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Mole";
                }
            }

            //----------$-----Shark drugi-----$----------//

            else if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "Shark")
                {
                    secondName.text = "Shark";
                    secondTurn.text = sharkTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }

            //----------$-----turtle drugi-----$----------//

            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "Turtle")
                {
                    secondName.text = "Turtle";
                    secondTurn.text = turtleTurCounter.ToString();
                    if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }

            //----------$-----mole drugi-----$----------//

            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "Mole")
                {
                    secondName.text = "Mole";
                    secondTurn.text = moleTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
        }

        //--------------------------------------------------------------------Pierwszy Rekin-------------------------------------------------------------------------------------------------------------//

        else if (bSharkFinished)
        {
            firstName.text = "Shark";
            firstTurn.text = sharkTurCounter.ToString();

            //-----#####-----Wyœwietlanie poprawnie-----#####-----//

            //-----#####-----przypisanie kto jest drugi-----#####-----//


            if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Duck";
                }
            }
            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Turtle";
                }
            }
            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Mole";
                }
            }

            //----------$-----Duck drugi-----$----------//

            else if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "Duck")
                {
                    secondName.text = "Duck";
                    secondTurn.text = duckTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }


            //----------$-----turtle drugi-----$----------//

            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "Turtle")
                {
                    secondName.text = "Turtle";
                    secondTurn.text = turtleTurCounter.ToString();
                    if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
            //----------$-----mole drugi-----$----------//

            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "Mole")
                {
                    secondName.text = "Mole";
                    secondTurn.text = moleTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
        }

        //--------------------------------------------------------------------Pierwsza Zaba-------------------------------------------------------------------------------------------------------------//

        if (bTurtleFinished)
        {
            firstName.text = "Turtle";
            firstTurn.text = turtleTurCounter.ToString();

            //-----#####-----Wyœwietlanie poprawnie-----#####-----//

            //-----#####-----przypisanie kto jest drugi-----#####-----//


            if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Shark";
                }
            }
            else if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Duck";
                }
            }
            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Mole";
                }
            }

            //----------$-----Shark drugi-----$----------//

            else if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "Shark")
                {
                    secondName.text = "Shark";
                    secondTurn.text = sharkTurCounter.ToString();
                    if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }

            //----------$-----Duck drugi-----$----------//

            else if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "Duck")
                {
                    secondName.text = "Duck";
                    secondTurn.text = duckTurCounter.ToString();
                    if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bMoleFinished)
                    {
                        thirdName.text = "Mole";
                        thirdTurn.text = moleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }

            //----------$-----mole drugi-----$----------//

            else if (bMoleFinished == true)
            {
                if (whichAnimalWasSecond == "Mole")
                {
                    secondName.text = "Mole";
                    secondTurn.text = moleTurCounter.ToString();
                    if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
        }

        //--------------------------------------------------------------------Pierwszy Kret-------------------------------------------------------------------------------------------------------------//

        else if (bMoleFinished)
        {
            firstName.text = "Mole";
            firstTurn.text = moleTurCounter.ToString();

            //-----#####-----Wyœwietlanie poprawnie-----#####-----//

            //-----#####-----przypisanie kto jest drugi-----#####-----//


            if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Duck";
                }
            }
            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Turtle";
                }
            }
            else if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "")
                {
                    whichAnimalWasSecond = "Shark";
                }
            }

            //----------$-----Duck drugi-----$----------//

            else if (bDuckFinished == true)
            {
                if (whichAnimalWasSecond == "Duck")
                {
                    secondName.text = "Duck";
                    secondTurn.text = duckTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }


            //----------$-----turtle drugi-----$----------//

            else if (bTurtleFinished == true)
            {
                if (whichAnimalWasSecond == "Turtle")
                {
                    secondName.text = "Turtle";
                    secondTurn.text = turtleTurCounter.ToString();
                    if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bSharkFinished)
                    {
                        thirdName.text = "Shark";
                        thirdTurn.text = sharkTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
            //----------$-----Rekin drugi-----$----------//

            else if (bSharkFinished == true)
            {
                if (whichAnimalWasSecond == "Shark")
                {
                    secondName.text = "Shark";
                    secondTurn.text = sharkTurCounter.ToString();
                    if (bTurtleFinished)
                    {
                        thirdName.text = "Turtle";
                        thirdTurn.text = turtleTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                    else if (bDuckFinished)
                    {
                        thirdName.text = "Duck";
                        thirdTurn.text = duckTurCounter.ToString();
                        gameOverUI(firstName, firstTurn, secondName, secondTurn, thirdName, thirdTurn);
                    }
                }
            }
        }

    }

    //metoda opowiedzialna za za³¹czanie tury gracza
    public static void MovePlayer(ref int sharkTurCounter, ref int duckTurCounter, ref int turtleTurCounter, ref int moleTurCounter, ref int whoseTurn)
    {
        switch (whoseTurn)
        {
            case 1:
                //wydaje mi sie to lepsze do ogladnia niz wszystko w jednym ifie, ale mozna to zmienic
                if (!duck.GetComponent<Player>().pionek[0].GetComponent<Move>().bDalejWGrze || !duck.GetComponent<Player>().pionek[1].GetComponent<Move>().bDalejWGrze ||
                    !duck.GetComponent<Player>().pionek[2].GetComponent<Move>().bDalejWGrze || !duck.GetComponent<Player>().pionek[3].GetComponent<Move>().bDalejWGrze)//sprawdzanie czy wszystkie pionki skonczyly
                {
                    bDuckFinished = true; //gracz zakonczyl juz granie, przestawienie boola na true
                    Debug.Log(bDuckFinished);
                    whoseTurn++;//zmiana czyja tura zeby moc pominac ture gracza, ktory skonczyl
                    //wezwanie metody MovePlayer() by mogl sie ruszyc inny gracz
                    MovePlayer(ref sharkTurCounter, ref duckTurCounter, ref turtleTurCounter, ref moleTurCounter, ref whoseTurn);
                }
                else
                {
                    Turn();
                    duck.GetComponent<Player>().active = duck.GetComponent<Player>().EnambleMovement();//ustawienie obecnego playera na aktywny
                    Debug.Log("duck");
                    duckTurCounter += 1;//zwiekszenie ilosci tur o 1
                    Debug.Log(duckTurCounter);//wyswietlenie w konsoli ile tur minelo
                }
                break;
            case 2:
                if (!shark.GetComponent<Player>().pionek[0].GetComponent<Move>().bDalejWGrze || !shark.GetComponent<Player>().pionek[1].GetComponent<Move>().bDalejWGrze ||
                    !shark.GetComponent<Player>().pionek[2].GetComponent<Move>().bDalejWGrze || !shark.GetComponent<Player>().pionek[3].GetComponent<Move>().bDalejWGrze)//sprawdzanie czy wszystkie pionki skonczyly
                {
                    bSharkFinished = true; //gracz zakonczyl juz granie, przestawienie boola na true
                    Debug.Log(bSharkFinished);
                    whoseTurn++;//zmiana czyja tura zeby moc pominac ture gracza, ktory skonczyl
                    //wezwanie metody MovePlayer() by mogl sie ruszyc inny gracz
                    MovePlayer(ref sharkTurCounter, ref duckTurCounter, ref turtleTurCounter, ref moleTurCounter, ref whoseTurn);
                }
                else
                {
                    Turn();
                    shark.GetComponent<Player>().active = shark.GetComponent<Player>().EnambleMovement();//ustawienie obecnego playera na aktywny
                    Debug.Log("shark");
                    sharkTurCounter += 1;//zwiekszenie ilosci tur o 1
                    Debug.Log(sharkTurCounter);//wyswietlenie w konsoli ile tur minelo
                }
                break;
            case 3:
                if (!turtle.GetComponent<Player>().pionek[0].GetComponent<Move>().bDalejWGrze || !turtle.GetComponent<Player>().pionek[1].GetComponent<Move>().bDalejWGrze ||
                    !turtle.GetComponent<Player>().pionek[2].GetComponent<Move>().bDalejWGrze || !turtle.GetComponent<Player>().pionek[3].GetComponent<Move>().bDalejWGrze)//sprawdzanie czy wszystkie pionki skonczyly
                {
                    bTurtleFinished = true; //gracz zakonczyl juz granie, przestawienie boola na true
                    Debug.Log(bTurtleFinished);
                    whoseTurn++;//zmiana czyja tura zeby moc pominac ture gracza, ktory skonczyl
                    //wezwanie metody MovePlayer() by mogl sie ruszyc inny gracz
                    MovePlayer(ref sharkTurCounter, ref duckTurCounter, ref turtleTurCounter, ref moleTurCounter, ref whoseTurn);
                }
                else
                {
                    Turn();
                    turtle.GetComponent<Player>().active = turtle.GetComponent<Player>().EnambleMovement();//ustawienie obecnego playera na aktywny
                    Debug.Log("turtle");
                    turtleTurCounter += 1;//zwiekszenie ilosci tur o 1
                    Debug.Log(turtleTurCounter);//wyswietlenie w konsoli ile tur minelo
                }
                break;
           case 4:
                if (!mole.GetComponent<Player>().pionek[0].GetComponent<Move>().bDalejWGrze || !mole.GetComponent<Player>().pionek[1].GetComponent<Move>().bDalejWGrze ||
                    !mole.GetComponent<Player>().pionek[2].GetComponent<Move>().bDalejWGrze || !mole.GetComponent<Player>().pionek[3].GetComponent<Move>().bDalejWGrze)//sprawdzanie czy wszystkie pionki skonczyly
                {
                    bMoleFinished = true; //gracz zakonczyl juz granie, przestawienie boola na true
                    Debug.Log(bMoleFinished);
                    whoseTurn++;//zmiana czyja tura zeby moc pominac ture gracza, ktory skonczyl
                    //wezwanie metody MovePlayer() by mogl sie ruszyc inny gracz
                    MovePlayer(ref sharkTurCounter, ref duckTurCounter, ref turtleTurCounter, ref moleTurCounter, ref whoseTurn);
                }
                else
                {
                    Turn();
                    mole.GetComponent<Player>().active = mole.GetComponent<Player>().EnambleMovement();//ustawienie obecnego playera na aktywny
                    Debug.Log("mole");
                    moleTurCounter += 1;//zwiekszenie ilosci tur o 1
                    Debug.Log(moleTurCounter);//wyswietlenie w konsoli ile tur minelo
                }
                break;
        }
    }
    public static void Turn()
    {
        switch (whoseTurn)
        {
            case 1:
                moleTura.gameObject.SetActive(false);
                duckTura.gameObject.SetActive(true);
                Debug.Log("duck");
                break;
            case 2:
                duckTura.gameObject.SetActive(false);
                sharkTura.gameObject.SetActive(true);
                Debug.Log("shark");
                break;
            case 3:
                sharkTura.gameObject.SetActive(false);
                frogTura.gameObject.SetActive(true);
                Debug.Log("turtle");
                break;
            case 4:
                frogTura.gameObject.SetActive(false);
                moleTura.gameObject.SetActive(true);
                Debug.Log("mole");
                break;
        }
    }

    public static void Chceck(Transform waitPoints, string nazwa)
    {
        for (int i = 0; i < 2; i++)
        {
            if (randomBack[i] == waitPoints)
            {
                losoweCofanie();
                break;
            }
        }

        if (nazwa != shark.GetComponent<Player>().nazwa)
            for (int i = 0; i < 4; i++)
            {
                if (shark.GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
                {
                    onBoard.Remove(shark.GetComponent<Player>().pionek[i]);
                    shark.GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex = 0;
                    shark.GetComponent<Player>().pionek[i].GetComponent<Move>().ruch = true;
                }
            }


        if (nazwa != turtle.GetComponent<Player>().nazwa)
            for (int i = 0; i < 4; i++)
            {
                if (turtle.GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
                {
                    onBoard.Remove(turtle.GetComponent<Player>().pionek[i]);
                    turtle.GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex = 0;
                    turtle.GetComponent<Player>().pionek[i].GetComponent<Move>().ruch = true;
                }
            }


        if (nazwa != mole.GetComponent<Player>().nazwa)
            for (int i = 0; i < 4; i++)
            {
                if (mole.GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
                {
                    onBoard.Remove(mole.GetComponent<Player>().pionek[i]);
                    mole.GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex = 0;
                    mole.GetComponent<Player>().pionek[i].GetComponent<Move>().ruch = true;
                }
            }


        if (nazwa != duck.GetComponent<Player>().nazwa)
            for (int i = 0; i < 4; i++)
            {
                if (duck.GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
                {
                    onBoard.Remove(duck.GetComponent<Player>().pionek[i]);
                    duck.GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex = 0;
                    duck.GetComponent<Player>().pionek[i].GetComponent<Move>().ruch = true;
                }
            }
    }

    public static void losoweCofanie()
    {
        int los = Random.Range(0, onBoard.Count);
        GameObject pionek = onBoard.ElementAt(los);
        onBoard.Remove(pionek);
        pionek.GetComponent<Move>().waitPointIndex = 0;
        pionek.GetComponent<Move>().ruch = true;

    }

    static void gameOverUI(TextMeshProUGUI firstName, TextMeshProUGUI firstTurn, TextMeshProUGUI secondName, TextMeshProUGUI secondTurn, TextMeshProUGUI thirdName, TextMeshProUGUI thirdTurn)
    {
        gameOverBack.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
        firstName.gameObject.SetActive(true);
        firstTurn.gameObject.SetActive(true);
        secondName.gameObject.SetActive(true);
        secondTurn.gameObject.SetActive(true);
        thirdName.gameObject.SetActive(true);
        thirdTurn.gameObject.SetActive(true);
    }
}
