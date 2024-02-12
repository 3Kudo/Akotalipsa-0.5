using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{

	private static GameObject koniec;
	private static GameObject[] pawn, tura, ranking;
	public static Sprite[] sprites = new Sprite[4];
	public static Image[] images= new Image[4];
	public static int miejsce;
	public static GameObject safePlacePrefabe;
	public static GameObject CoinPrefabe;


	public static GameObject cat;


	public static Transform[] randomBack = new Transform[2];

	public static Transform[] safePlaceWaitPoints = new Transform[44];
    public static Transform[] CoinWaitPoints = new Transform[44];


    public static List<GameObject> onBoard = new List<GameObject>();

	public static List<GameObject> safePlace = new List<GameObject>();
    public static List<GameObject> Coin = new List<GameObject>();

	
    public static int whoseTurn = 0;
	public static int diceNumber = 6;
	public static int turnCounter = 0;
	// Start is called before the first frame update
	void Start()
	{
		//przypisanie odpowiednich obiektów
		pawn = new GameObject[4];

		pawn[0] = GameObject.Find("Moles");
		pawn[1] = GameObject.Find("Sharks");
		pawn[2] = GameObject.Find("Turtles");
		pawn[3] = GameObject.Find("Ducks");



		koniec = GameObject.Find("Panel");
		koniec.gameObject.SetActive(false);
		tura = new GameObject[5];
		tura[0] = GameObject.Find("MoleT");
		tura[1] = GameObject.Find("SharkT");
		tura[2] = GameObject.Find("FrogT");
		tura[3] = GameObject.Find("DuckT");
		tura[4] = GameObject.Find("Napis_Tura");





		//ustawienie na poczatku wszystkich obiektow na nieaktywne
		tura[0].gameObject.SetActive(false);
		tura[1].gameObject.SetActive(false);
		tura[2].gameObject.SetActive(false);
		tura[3].gameObject.SetActive(false);

        //przypisanie nazw
        pawn[0].GetComponent<Player>().nazwa = "mole";
        pawn[1].GetComponent<Player>().nazwa = "shark";
        pawn[2].GetComponent<Player>().nazwa = "turtle";
        pawn[3].GetComponent<Player>().nazwa = "duck";
		

		//przypisanie UI koñca gry
		miejsce = 0;
		ranking = new GameObject[3];

		//tworzenie kota


		//losowanie czyja tura i ustawienie wszystkich na false ¿eby ka¿dy mog³ rzuciæ koœci¹
		pawn[1].GetComponent<Player>().active = false;
		pawn[3].GetComponent<Player>().active = false;
		pawn[2].GetComponent<Player>().active = false;
		pawn[0].GetComponent<Player>().active = false;
		whoseTurn = Random.Range(1, 5);
		AddSafePlace();
        AddSafePlace();
		AddCoin(2);
        Turn();

	}

	//metoda opowiedzialna za za³¹czanie gracza
	public static void MovePlayer()
	{
		pawn[whoseTurn - 1].GetComponent<Player>().EnambleMovement();
	}
	//metoda opowiedzialna za za³¹czanie tury gracza
	public static void Turn()
	{
		for (int i = 0; i < 4; i++)
			tura[i].gameObject.SetActive(false);
        if (miejsce != 3)
		{
            if (pawn[whoseTurn - 1].GetComponent<Player>().finished)
            {
                if (whoseTurn == 4)
				{
                    whoseTurn = 1;
				}
				else
					whoseTurn++;
                Turn();
			}
			else
			{
                tura[whoseTurn - 1].gameObject.SetActive(true);
			}
			//akcje kota
			//TODO: gdzies to trzeba przeniesc, bo wywoluje sie kilka razy...
			// catTurn();
		}
		else
			tura[4].gameObject.SetActive(false);

    }

    public static void TurnCounter()
    {
        turnCounter++;
        if (turnCounter % 4 == 0)
            AddCoin(Random.Range(1, 4));
    }


    public static GameObject GetTura()
	{
		return pawn[whoseTurn-1];
	}


	public static void catTurn()
	{
		if(cat.GetComponent<Cat>().Phase == 1)
		{
			losowePrzesuwanie(-2, 1);
		}
		else if(cat.GetComponent<Cat>().Phase == 2) 
		{
			losowePrzesuwanie(-4, 2);
			losowaSciana();
		}
		else if(cat.GetComponent<Cat>().Phase == 3) 
		{
			losowePrzesuwanie(-6, 3);
			losowaSciana();
			losoweCofanie();
		}
		//zwieksza sie tez przy wyjsciu z bazy po wyrzuceniu 6 - poprawic, czy git?
		cat.GetComponent<Cat>().incrementWakeCounter(1);
		cat.GetComponent<Cat>().phaseCheck();
	}

	//metoda odpowiedzialana za przypisaywanie pozycji w rankigu
	public static void PlayerFinishedGamed(GameObject gracz)
	{
		ranking[miejsce] = gracz;
		miejsce++;
		if (miejsce == 3)
			EndGame();
	}

	public static void EndGame()
	{
		koniec.gameObject.SetActive(true);
		for (int i = 0; i < 4; i++)
		{
			if (ranking[0] == pawn[i])
				images[0].sprite = sprites[i];
			else if (ranking[1] == pawn[i])
                images[1].sprite = sprites[i];
            else if (ranking[2] == pawn[i])
                images[2].sprite = sprites[i];
            else
                images[3].sprite = sprites[i];
        }
	}

	
	//funkcje zwi¹zane z mechanik¹ gry

	public static void Chceck(Transform waitPoints, string nazwa, GameObject pionek)
	{
		
		for (int i = 0; i < 2; i++)
		{
			if (randomBack[i] == waitPoints)
			{
				losoweCofanie();
				break;
			}
		}

		GameObject pio=null;
		int ammount = 0;
		
		for(int j = 0; j < 4; j++)
		{
            bool leave = false;
            if (nazwa != pawn[j].GetComponent<Player>().nazwa)
			{
				for (int i = 0; i < 4; i++)
				{
					if (pawn[j].GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
					{
						
						if (pawn[j].GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex == 1 || 
							pawn[j].GetComponent<Player>().pionek[i].GetComponent<Move>().defence)
						{
							pionek.GetComponent<Move>().waitPointIndex = 0;
							pionek.GetComponent<Move>().ruch = true;
							leave = true;
							break;
						}
						else
						{
							pio = pawn[j].GetComponent<Player>().pionek[i];
							ammount++;
						}
					}
				}
			}
			if (leave)
				break;
        }
		if (ammount == 1)
		{
			onBoard.Remove(pio);
            pio.GetComponent<Move>().waitPointIndex = 0;
            pio.GetComponent<Move>().ruch = true;
        }
		else if(ammount > 1)
		{
            onBoard.Remove(pionek);
            pionek.GetComponent<Move>().waitPointIndex = 0;
            pionek.GetComponent<Move>().ruch = true;
			return;
        }
		for(int i = 0; i < safePlace.Count; i++)
		{
			if (safePlace[i].GetComponent<SafePlace>().waitPoint == waitPoints)
			{
				GameObject toDestroy = safePlace[i];
				safePlace.RemoveAt(i);
				Destroy(toDestroy);
				OnSafePlace(pionek, waitPoints);
			}
		}
		for(int i = 0; i <Coin.Count; i++)
		{
			if (Coin[i].GetComponent<Coin>().waitPoint == waitPoints)
			{
                GameObject toDestroy = Coin[i];
                Coin.RemoveAt(i);
				pionek.GetComponentInParent<Player>().IncreaseCoins(toDestroy.GetComponent<Coin>().price);
                Destroy(toDestroy);               
            }
		}
    }

	public static void losoweCofanie()
	{
		int los = Random.Range(0, onBoard.Count);
		GameObject pionek = onBoard.ElementAt(los);
		onBoard.Remove(pionek);
		Transform position = pionek.GetComponent<Move>().GetWaitpoint();
		pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
        pionek.GetComponent<Move>().waitPointIndex = 0;
		pionek.GetComponent<Move>().ruch = true;
	}

	public static void losowePrzesuwanie(int maxMove, int minMove)
	{
		if (onBoard.Count > 0)
		{
			int los = Random.Range(0, onBoard.Count);
			GameObject pionek = onBoard.ElementAt(los);
			onBoard.Remove(pionek);
			Transform position = pionek.GetComponent<Move>().GetWaitpoint();
			pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
			pionek.GetComponent<Move>().waitPointIndex = pionek.GetComponent<Move>().pozycja + Random.Range(minMove, maxMove);
			pionek.GetComponent<Move>().ruch = true;
		}
	}

	public static void losowaSciana()
	{
		//TODO: mechanika losowej sciany (pole nie do przejscia)
	}

	public static void OnSafePlace(GameObject pionek, Transform waitPoint)
	{
		pionek.GetComponent<Move>().defence = true;
        Transform newWaitPoint = GetRandomPosition();
        List<Transform> safePlacePosition = new List<Transform>();
		for(int i = 0; i < safePlace.Count; i++)
			safePlacePosition.Add(safePlace[i].GetComponent<SafePlace>().waitPoint);
		for (int i = 0; i < 2; i++)
			safePlacePosition.Add(randomBack[i]);
        while(safePlacePosition.Contains(newWaitPoint))
            newWaitPoint = GetRandomPosition();
        safePlace.Add(Instantiate(safePlacePrefabe) as GameObject);
		safePlace[safePlace.Count-1].GetComponent<SafePlace>().setPlace(newWaitPoint);
    }

    public static void AddSafePlace()
    {
		Transform newWaitPoint = GetRandomPosition();
        List<Transform> safePlacePosition = new List<Transform>();
        for (int i = 0; i < safePlace.Count; i++)
            safePlacePosition.Add(safePlace[i].GetComponent<SafePlace>().waitPoint);
       while (safePlacePosition.Contains(newWaitPoint))
            newWaitPoint = GetRandomPosition();
        safePlace.Add(Instantiate(safePlacePrefabe) as GameObject);
        safePlace[safePlace.Count - 1].GetComponent<SafePlace>().setPlace(newWaitPoint);
    }

    public static void AddCoin(int NumberofCoins)
    {
		for (int j = 0; j < NumberofCoins; j++)
		{
			Transform newWaitPoint = GetRandomCoinPosition();
			List<Transform> CoinPosition = new List<Transform>();
			for (int i = 0; i < Coin.Count; i++)
				CoinPosition.Add(Coin[i].GetComponent<Coin>().waitPoint);
			while (CoinPosition.Contains(newWaitPoint))
				newWaitPoint = GetRandomCoinPosition();
			Coin.Add(Instantiate(CoinPrefabe) as GameObject);
			Coin[Coin.Count - 1].GetComponent<Coin>().setPlace(newWaitPoint);
		}
    }

    public static Transform GetRandomCoinPosition()
    {
        return CoinWaitPoints[Random.Range(0, 44)];
    }

    public static Transform GetRandomPosition()
	{
		return safePlaceWaitPoints[Random.Range(0, 44)];
	}

    public static List<GameObject> GetOnBoard()
	{
		int k = onBoard.Count;
		return onBoard;
	}
	
}
