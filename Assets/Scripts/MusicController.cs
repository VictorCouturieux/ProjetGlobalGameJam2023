using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Radio RockRadio;
    public Radio ReggaeRadio;
    
    private void Start()
    {
        MusicChanged(AudioManager.Instance.isRockPlaying);
    }
    
    public virtual void MusicChanged(bool isRock)
    {
        AudioManager.Instance.isRockPlaying = isRock;
        if (AudioManager.Instance.isRockPlaying)
        {
            AudioManager.Instance.RockMusic(true);
            AudioManager.Instance.ReggaeMusic(false);
            RockRadio.PlayEffect();
            ReggaeRadio.StopEffect();
        }
        else
        {
            AudioManager.Instance.RockMusic(false);
            AudioManager.Instance.ReggaeMusic(true);
            RockRadio.StopEffect();
            ReggaeRadio.PlayEffect();
        }
    }
}
