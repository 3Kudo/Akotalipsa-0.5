using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public Animator anim;
    public bool spin=false;
    int klatki = 0;
    public GameObject gracz;


    void Update()
    {
        if (gracz == GetComponentInParent<GameRules>().GetTura())
        {
            klatki++;
            if (klatki == 10)
            {
                klatki = 0;
                spin = !spin;
                anim.SetBool("Spin", spin);
            }
        }
        
    }

   

}
