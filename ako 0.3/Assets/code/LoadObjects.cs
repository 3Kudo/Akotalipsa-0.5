using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadObjects : MonoBehaviour
{
    public Sprite[] sprites;
    public Transform[] randomBack;
    public GameObject safePlacePrefabe;

    public Transform[] safePlace;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameRules.sprites[i] = sprites[i];
        }

        for(int i = 0; i < safePlace.Length; i++)
        {
            GameRules.safePlaceWaitPoints[i] = safePlace[i];
        }

        GameRules.randomBack[0] = randomBack[0];
        GameRules.randomBack[1] = randomBack[1];

        GameRules.safePlacePrefabe = safePlacePrefabe;
    }
}
