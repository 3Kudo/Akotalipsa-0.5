using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadObjects : MonoBehaviour
{
    public Sprite[] sprites;
    public Image[] images;
    public Transform[] randomBack;
    public GameObject safePlacePrefabe, CoinPrefabe, fluffPrefab;
    public GameObject cat, milk, catnip;

    public Transform[] safePlace;
    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < sprites.Length; i++)
        {
            GetComponent<GameRules>().sprites[i] = sprites[i];
            GetComponent<GameRules>().images[i] = images[i];
        }

        for(int i = 0; i < safePlace.Length; i++)
        {
            GetComponent<GameRules>().boardWaitPoints[i] = safePlace[i];
        }


        GetComponent<GameRules>().randomBack[0] = randomBack[0];
        GetComponent<GameRules>().randomBack[1] = randomBack[1];


        //GameRules.randomBack[0] = randomBack[0];
        //GameRules.randomBack[1] = randomBack[1];

        GetComponent<GameRules>().fluffPrefab = fluffPrefab;
        GetComponent<GameRules>().safePlacePrefabe = safePlacePrefabe;
        GetComponent<GameRules>().CoinPrefabe = CoinPrefabe;
        GetComponent<GameRules>().cat = cat;
        GetComponent<GameRules>().milk = milk;
        GetComponent<GameRules>().catnip = catnip; */
    }
}
