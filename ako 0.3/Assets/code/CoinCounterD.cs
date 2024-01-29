using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterD : MonoBehaviour
{
    public static CoinCounterD instance;

    public TMP_Text coinTextM;
    public TMP_Text coinTextS;
    public TMP_Text coinTextF;
    public TMP_Text coinTextD;
    public int currentCoinsM = 0;
    public int currentCoinsS = 0;
    public int currentCoinsF = 0;
    public int currentCoinsD = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        coinTextM.text = currentCoinsM.ToString();
        coinTextS.text = currentCoinsS.ToString();
        coinTextF.text = currentCoinsF.ToString();
        coinTextD.text = currentCoinsD.ToString();
    }

    public void IncreaseCoins(int v)
    {
        if (GameRules.whoseTurn == 1)
        {
            currentCoinsM += v;
            coinTextM.text = currentCoinsM.ToString();
        }
        else if (GameRules.whoseTurn == 2)
        {
            currentCoinsS += v;
            coinTextS.text = currentCoinsS.ToString();
        }
        else if (GameRules.whoseTurn == 3)
        {
            currentCoinsF += v;
            coinTextF.text = currentCoinsF.ToString();
        }
        else if (GameRules.whoseTurn == 4)
        {
            currentCoinsD += v;
            coinTextD.text = currentCoinsD.ToString();
        }
    }

    public bool DecreaseCoins(int v)
    {
        if (GameRules.whoseTurn == 1)
        {
            if (currentCoinsM - v >= 0)
            {
                currentCoinsM -= v;
                coinTextM.text = currentCoinsM.ToString();
                return true;
            }
            else
                return false;
        }
        else if (GameRules.whoseTurn == 2)
        {
            if (currentCoinsS - v >= 0)
            {
                currentCoinsS -= v;
                coinTextS.text = currentCoinsS.ToString();
                return true;
            }
            else
                return false;
        }
        else if (GameRules.whoseTurn == 3)
        {
            if (currentCoinsF - v >= 0)
            {
                currentCoinsF -= v;
                coinTextF.text = currentCoinsF.ToString();
                return true;
            }
            else
                return false;
        }
        else if (GameRules.whoseTurn == 4)
        {
            if (currentCoinsD - v >= 0)
            {
                currentCoinsD -= v;
                coinTextD.text = currentCoinsD.ToString();
                return true;
            }
            else
                return false;
        }
        return false;
    }
}
