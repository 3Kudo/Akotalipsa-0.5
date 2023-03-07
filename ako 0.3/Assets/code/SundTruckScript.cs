using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundTruckScript : MonoBehaviour
{
    public AudioClip soundTracks;
    AudioSource AS;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (AS.isPlaying == false)
        {
            AS.clip = soundTracks;

            AS.Play();
        }
    }
}
