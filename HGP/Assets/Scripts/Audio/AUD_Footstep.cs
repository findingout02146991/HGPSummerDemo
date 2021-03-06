﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUD_Footstep : MonoBehaviour
{
    // Start is called before the first frame update
    //This script is to be attached to the animated sprite when it is ready. 

    //Source and Clip
    [SerializeField]
   private AudioSource source;
    [SerializeField]
   private AudioClip [] step;

    //Variables

    //Volume
    [SerializeField]
    private float maxVolume = 1;
    [SerializeField]
    private float minVolume = .5f;
    private float Volume;

    //Pitch
    [SerializeField]
    private float maxPitch = 1;
    [SerializeField]
    private float minPitch = .5f;
    private float pitch;
    void Start()
    {
        //Gets audiosource component from game object. 
        source = GetComponent<AudioSource>();   
    }

    void footstep()
    {
        //Selects what audio clip to use.
        source.clip = step[Random.Range(0, step.Length)];
        //Sets Volume
        Volume = Random.Range(minVolume, maxVolume);
        source.volume = Volume;
        //Sets Pitch
        pitch = Random.Range(minPitch, maxPitch);
        source.pitch = pitch;
        //Play
        source.Play();
    }
}
