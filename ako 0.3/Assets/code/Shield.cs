using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip shieldSound;
    void Start()
    {
        AS = gameObject.AddComponent<AudioSource>();
        AS.clip = shieldSound;
        AS.Play();
    }

    public void DestoyShield()
    {
        Destroy(gameObject);
    }
}
