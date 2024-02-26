using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public Slider masterVol, musickVol, sfxVol;
    public AudioMixer mixer;
    
    void Start()
    {
        float value;
        mixer.GetFloat("MasterVol", out value);
        masterVol.value = value;

        mixer.GetFloat("MusicVol", out value);
        musickVol.value = value;

        mixer.GetFloat("SfxVol", out value);
        sfxVol.value = value;

    }

    public void ChangeMasterVol()
    {
        mixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVol()
    {
        mixer.SetFloat("MusicVol", musickVol.value);
    }

    public void ChangeSFXVol()
    {
        mixer.SetFloat("SfxVol", sfxVol.value);
    }


}
