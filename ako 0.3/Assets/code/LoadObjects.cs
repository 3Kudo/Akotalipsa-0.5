using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadObjects : MonoBehaviour
{
    public Sprite[] sprites;
    public Image[] images;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameRules.sprites[i] = sprites[i];
            GameRules.images[i] = images[i];
        }

    }
}
