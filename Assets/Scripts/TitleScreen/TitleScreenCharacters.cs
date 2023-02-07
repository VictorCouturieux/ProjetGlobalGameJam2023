using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TitleScreenCharacters : MonoBehaviour
{
    public bool isRockPlaying;
    private bool lastMusic;

    public CharacterTitleScreen Rocker;
    public CharacterTitleScreen Reggae;
    private void Start()
    {
        isRockPlaying = Random.value >= 0.5;
        lastMusic = isRockPlaying;
        
        if (isRockPlaying)
            Rocker.Dance();
        else 
            Reggae.Dance();
    }

    private void Update()
    {
        if(isRockPlaying != lastMusic)
            MusicChanged();
    }

    private void MusicChanged()
    {
        if (isRockPlaying)
        {
            Reggae.Angry();
            Rocker.Dance();
        }
        else
        {
            Reggae.Dance();
            Rocker.Angry();
        }
        lastMusic = isRockPlaying;
    }
}
