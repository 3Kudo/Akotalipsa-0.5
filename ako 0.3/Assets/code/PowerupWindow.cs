using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PowerupWindow : MonoBehaviour
{
    public GameObject[] powerupsPatter;
    public GameObject[] powerups = new GameObject[2];
    public Transform[] powerupsPosiotns;
    public Animator anim;
    public Sprite baseExit;
    private AudioSource AS;
    public AudioClip AC;
    public Transform parent;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void ChangeState(GameObject pawn, Transform changeParent)
    {
        parent = changeParent;
        if(anim.GetBool("IsActive") == pawn.GetComponent<Move>().chosen)
        {
            SetPowerupsButtons();
            SetPowerupsButtons();
            return;
        }

        if (pawn != null)
        {
            if (!pawn.GetComponent<Move>().chosen)
                SetPowerupsButtons();
            if (anim.GetBool("IsActive") != pawn.GetComponent<Move>().chosen)
            {
                AS.clip = AC;
                AS.Play();
            }
            anim.SetBool("IsActive", pawn.GetComponent<Move>().chosen);
        }
        else
        {
            SetPowerupsButtons();
            anim.SetBool("IsActive", false);
            AS.clip = AC;
            AS.Play();
        }
    }
    
    public void SetPowerupsButtons()
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
