using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{

	private GameObject koniec;
	private GameObject[] pawn, tura, ranking;
	public Sprite[] sprites = new Sprite[4];
	public Image[] images= new Image[4];
	public int miejsce;
	public GameObject safePlacePrefabe, CoinPrefabe, fluffPrefab;
    public AudioSource AS;
    public AudioClip[] sfx;


    public GameObject cat, milk, catnip;


	public Transform[] randomBack = new Transform[2];

	public Transform[] boardWaitPoints = new Transform[44];

	public List<GameObject> onBoard = new List<GameObject>();

	public List<GameObject> fluff = new List<GameObject>();

	public List<GameObject> safePlace = new List<GameObject>();
	public List<GameObject> Coin = new List<GameObject>();

	public int fluffCount = 0;
    public int whoseTurn = 0;
	public int diceNumber = 6;
	public int turnCounter = 0;
	// Start is called before the first frame update
	public void Start()
	{
        AS = GetComponent<AudioSource>();
        //przypisanie odpowiednich obiektów
        pawn = new GameObject[4];

		pawn[0] = GameObject.Find("Moles");
		pawn[1] = GameObject.Find("Sharks");
		pawn[2] = GameObject.Find("Frogs");
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
        pawn[2].GetComponent<Player>().nazwa = "frog";
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
		milk.GetComponent<Milk>().setMilkButton();
		catnip.GetComponent<Catnip>().setCatnipButton();


    }

	//metoda opowiedzialna za za³¹czanie gracza
	public void MovePlayer()
	{
		pawn[whoseTurn - 1].GetComponent<Player>().EnambleMovement();

	}
	//metoda opowiedzialna za za³¹czanie tury gracza
	public void Turn()
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
		}
		else
			tura[4].gameObject.SetActive(false);

    }

    public void TurnCounter()
    {
        milk.GetComponent<Milk>().setMilkButton();
        catnip.GetComponent<Catnip>().setCatnipButton();
        turnCounter++;
		if (turnCounter % 4 == 0)
		{
            for (int i = 0; i < fluff.Count(); i++)
                fluff[i].GetComponent<Fluff>().FadeAway();
            cat.GetComponent<Cat>().catTurn();
		}
		if(turnCounter % 7 == 0)
		{
            AddCoin(Random.Range(1, 3));
        }
		
    }


    public GameObject GetTura()
	{
		return pawn[whoseTurn-1];
	}


	

	//metoda odpowiedzialana za przypisaywanie pozycji w rankigu
	public void PlayerFinishedGamed(GameObject gracz)
	{
		ranking[miejsce] = gracz;
		miejsce++;
		if (miejsce == 3)
			EndGame();
	}

	public void EndGame()
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

	public void Chceck(Transform waitPoints, string nazwa, GameObject pionek)
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
                            GetComponentInParent<GameRules>().AS.clip = GetComponentInParent<GameRules>().sfx[2];
                            GetComponentInParent<GameRules>().AS.Play();
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
                AS.clip = toDestroy.GetComponent<Coin>().soundTracks;
                AS.Play();
                Coin.RemoveAt(i);
				pionek.GetComponentInParent<Player>().IncreaseCoins(toDestroy.GetComponent<Coin>().price);
                Destroy(toDestroy);               
            }
		}
    }

	public void losoweCofanie()
	{
		int los = Random.Range(0, onBoard.Count);
		GameObject pionek = onBoard.ElementAt(los);
		if (pionek.GetComponent<Move>().defence)
		{
            AS.clip = sfx[2];
            AS.Play();
            return;
        }
		onBoard.Remove(pionek);
		Transform position = pionek.GetComponent<Move>().GetWaitpoint();
		pionek.GetComponentInParent<Player>().MoveOut(position, pionek);
        pionek.GetComponent<Move>().waitPointIndex = 0;
		pionek.GetComponent<Move>().ruch = true;
		AS.clip = sfx[0];
        AS.Play();
    }

	public void RandomFluff()
	{
        int los = Random.Range(1, 4);
		if (los == 3)
		{
            Transform newWaitPoint;
            List<Transform> fluffPosition = new List<Transform>();
            foreach (GameObject wall in fluff)
				fluffPosition.Add(wall.GetComponent<Fluff>().waitPoint);
            while (true)
			{
				bool con = false;
				newWaitPoint = GetRandomPosition();
				for (int i = 0; i < onBoard.Count(); i++)
				{
					if (onBoard[i].GetComponent<Move>().waitPoints[onBoard[i].GetComponent<Move>().waitPointIndex] == newWaitPoint)
					{
						con = true;
						break;
					}
				}
				if (con)
					continue;
				con = fluffPosition.Contains(newWaitPoint);
				if (con)
					continue;
				for(int i = 0; i < pawn[0].GetComponent<PlayerMole>().molehillEntrancce.Count; i++)
				{
					if(newWaitPoint == pawn[0].GetComponent<Player>().pionek[0].GetComponent<Move>().waitPoints[pawn[0].GetComponent<PlayerMole>().molehillEntrancce[i]]
						|| newWaitPoint == pawn[0].GetComponent<Player>().pionek[0].GetComponent<Move>().waitPoints[pawn[0].GetComponent<PlayerMole>().molehillExit[i]])
                    {
                        con = true;
                        break;
                    }
				}
                if (con)
                    continue;
                break;
			}
			fluff.Add(Instantiate(fluffPrefab) as GameObject);
			fluff[fluff.Count - 1].GetComponent<Fluff>().setPlace(newWaitPoint);
            AS.clip = fluff[0].GetComponent<Fluff>().soundTracks;
            AS.Play();
        }
	}

	

	public void OnSafePlace(GameObject pionek, Transform waitPoint)
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
        AS.clip = sfx[1];
        AS.Play();
    }

    public void AddSafePlace()
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

    public void AddCoin(int NumberofCoins)
    {
		for (int j = 0; j < NumberofCoins; j++)
		{
			if (Coin.Count < 12)
			{
				Transform newWaitPoint = GetRandomPosition();
				List<Transform> CoinPosition = new List<Transform>();
				for (int i = 0; i < Coin.Count; i++)
					CoinPosition.Add(Coin[i].GetComponent<Coin>().waitPoint);
				while (CoinPosition.Contains(newWaitPoint))
					newWaitPoint = GetRandomPosition();
				Coin.Add(Instantiate(CoinPrefabe) as GameObject);
				Coin[Coin.Count - 1].GetComponent<Coin>().setPlace(newWaitPoint);
			}
			else
				break;
		}
    }

    public Transform GetRandomPosition()
	{
		return boardWaitPoints[Random.Range(0, 44)];
	}

    public List<GameObject> GetOnBoard()
	{
		int k = onBoard.Count;
		return onBoard;
	}

	public void SetCatnipMik()
	{
		milk.GetComponent<Milk>().setMilkButton();
		catnip.GetComponent<Catnip>().setCatnipButton();
	}
	
}
