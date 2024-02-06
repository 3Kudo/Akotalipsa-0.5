using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PowerupWindow : MonoBehaviour
{
    public GameObject[] powerupsPatter;
    GameObject[] powerups = new GameObject[2];
    public Transform[] powerupsPosiotns;
    public Animator anim;
    public Sprite baseExit;

    public bool ChangeState(GameObject pawn)
    {
        bool state = anim.GetBool("IsActive");
        if(pawn!=null)
            anim.SetBool("IsActive", pawn.GetComponent<Move>().chosen);
        else
            anim.SetBool("IsActive", false);
        return state;
    }
    
    public void SetPowerupsButtons(Transform parent)
    {
        if (powerups[0] == null)
        {
            powerups[0] = Instantiate(powerupsPatter[0], parent) as GameObject;
            powerups[0].transform.position = powerupsPosiotns[0].position;
            powerups[0].GetComponent<SpriteRenderer>().sprite = baseExit;
            powerups[1] = Instantiate(powerupsPatter[1], parent) as GameObject;
            powerups[1].transform.position = powerupsPosiotns[1].position;
        }
        else
        {
            Destroy(powerups[0]);
            powerups[0] = null;
            Destroy(powerups[1]);
            powerups[1] = null;
        }
    }
}
