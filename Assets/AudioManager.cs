using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource chirpSound;
    public float chirpStart = 3f;   
    public float chirpEnd = 15f;     
    public float chirpStart2 = 35f;   
    public float chirpEnd2 = 40f;     

    public AudioSource seaSound;
    public float seaStart = 15f;   
    public float seaEnd = 60f;     

    public AudioSource hootSound;
    public float hootStart = 20f;   
    public float hootEnd = 35f;     

    private void Update()
    {
        float currentTime = Time.time%60;

        // control chirpsounds
        if ((currentTime >= chirpStart && currentTime <= chirpEnd) || (currentTime >= chirpStart2 && currentTime <= chirpEnd2))
        {
            // Enable the audio source if it's not already enabled
            if (!chirpSound.enabled)
            {
                chirpSound.enabled = true;
            }
        }
        else
        {
            // Disable the audio source if it's not already disabled
            if (chirpSound.enabled)
            {
                chirpSound.enabled = false;
            }
        }
        // control seasounds
        if (currentTime >= seaStart && currentTime <= seaEnd)
        {
            // Enable the audio source if it's not already enabled
            if (!seaSound.enabled)
            {
                seaSound.enabled = true;
            }
        }
        else
        {
            // Disable the audio source if it's not already disabled
            if (seaSound.enabled)
            {
                seaSound.enabled = false;
            }
        }
        // control hootsounds
        if (currentTime >= hootStart && currentTime <= hootEnd)
        {
            // Enable the audio source if it's not already enabled
            if (!hootSound.enabled)
            {
                hootSound.enabled = true;
            }
        }
        else
        {
            // Disable the audio source if it's not already disabled
            if (hootSound.enabled)
            {
                hootSound.enabled = false;
            }
        }
    }
}
