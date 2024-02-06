using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{

    public Transform waitPoint;
    public int price;
    public TMP_Text priceText;

    public void setPlace(Transform newWaitPoint)
    {
        waitPoint = newWaitPoint;
        transform.position = waitPoint.transform.position;
        price = Random.Range(1,4);
        priceText.text = price.ToString();
    }


}
