using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{

    public Transform waitPoint;
    public int price;
    public TMP_Text priceText;

    public Animator anim;
    public bool spin=false;
    int klatki = 0;

    public AudioClip soundTracks;

    private void Update()
    {
        klatki++;
        if (klatki == 30)
        {
            klatki = 0;
            spin = !spin;
            anim.SetBool("Spin", spin);
        }
    }

    public void setPlace(Transform newWaitPoint)
    {
        waitPoint = newWaitPoint;
        transform.position = waitPoint.transform.position;
        price = Random.Range(1,4);
        priceText.text = price.ToString();
    }


}
