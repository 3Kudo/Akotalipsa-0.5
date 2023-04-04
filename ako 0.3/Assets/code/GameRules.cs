using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{

	private static GameObject koniec;
	private static GameObject[] pawn, tura, ranking;
	public static Sprite[] sprites = new Sprite[4];
	public static Image[] images= new Image[4];
	public static int miejsce;


	public static Transform[] randomBack = new Transform[2];


	public static List<GameObject> onBoard = new List<GameObject>();


	public static int whoseTurn = 0;
	public static int diceNumber = 6;
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


		//losowanie czyja tura i ustawienie wszystkich na false ¿eby ka¿dy mog³ rzuciæ koœci¹
		pawn[1].GetComponent<Player>().active = false;
		pawn[3].GetComponent<Player>().active = false;
		pawn[2].GetComponent<Player>().active = false;
		pawn[0].GetComponent<Player>().active = false;
		whoseTurn = Random.Range(1, 5);
		Turn();

	}

	//metoda opowiedzialna za za³¹czanie gracza
	public static void MovePlayer()
	{
		pawn[whoseTurn - 1].GetComponent<Player>().active = pawn[whoseTurn - 1].GetComponent<Player>().EnambleMovement();
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
					whoseTurn = 1;
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

		for(int j = 0; j < 4; j++)
		{
			if (nazwa != pawn[j].GetComponent<Player>().nazwa)
			{
				for (int i = 0; i < 4; i++)
				{
					if (pawn[j].GetComponent<Player>().WitchWaitpoint(i) == waitPoints)
					{
						if (pawn[j].GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex == 1)
						{
							pionek.GetComponent<Move>().waitPointIndex = 0;
							pionek.GetComponent<Move>().ruch = true;
						}
						else
						{
							onBoard.Remove(pawn[i].GetComponent<Player>().pionek[i]);
							pawn[j].GetComponent<Player>().pionek[i].GetComponent<Move>().waitPointIndex = 0;
							pawn[j].GetComponent<Player>().pionek[i].GetComponent<Move>().ruch = true;
						}
					}
				}
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
}
