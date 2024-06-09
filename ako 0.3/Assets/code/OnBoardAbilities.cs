using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardAbilities : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip abilitySound
        ;
    void Start()
    {
        AS = gameObject.AddComponent<AudioSource>();
        AS.clip = abilitySound;
        AS.Play();
    }

    public void DestoyObject()
    {
        Destroy(gameObject);
    }
}
