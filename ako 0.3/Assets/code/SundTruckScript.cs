using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundTruckScript : MonoBehaviour
{
    public AudioClip[] soundTracks;
    AudioSource AS;
    public GameObject cat;
    int clip;
    void Start()
    {
        clip = 0;
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (AS.isPlaying == false)
        {
            clip++;
            if (clip == 6)
                clip = 0;


            AS.clip = soundTracks[cat.GetComponent<Cat>().phase * 6 + clip];

            AS.Play();
        }
    }
}
