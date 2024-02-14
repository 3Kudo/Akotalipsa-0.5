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
        for (int i = 0; i < sprites.Length; i++)
        {
            GameRules.sprites[i] = sprites[i];
            GameRules.images[i] = images[i];
        }

        for(int i = 0; i < safePlace.Length; i++)
        {
            GameRules.boardWaitPoints[i] = safePlace[i];
        }


        GameRules.randomBack[0] = randomBack[0];
        GameRules.randomBack[1] = randomBack[1];


        //GameRules.randomBack[0] = randomBack[0];
        //GameRules.randomBack[1] = randomBack[1];

        GameRules.fluffPrefab = fluffPrefab;
        GameRules.safePlacePrefabe = safePlacePrefabe;
        GameRules.CoinPrefabe = CoinPrefabe;
        GameRules.cat = cat;
        GameRules.milk = milk;
        GameRules.catnip = catnip;
    }
}
