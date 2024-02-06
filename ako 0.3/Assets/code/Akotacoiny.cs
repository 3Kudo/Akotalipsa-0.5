using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akotacoiny : MonoBehaviour
{

    public int coin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCoins(int CoinToAdd)
    {
        coin += CoinToAdd;
    }

    public void subCoins(int CoinToSub)
    {
        if (coin - CoinToSub < 0)
        {
            Debug.Log("Not enough Coins !!!");
        }
        else
        {
            coin -= CoinToSub;
        }
    }

    public void ACoin()
    {
        addCoins(3);
    }
    public void SCoin()
    {
        subCoins(2);
    }
}
