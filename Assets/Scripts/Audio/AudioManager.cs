using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(Instance.gameObject);
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    [SerializeField] AK.Wwise.Event _menuMusic = null;
    [SerializeField] AK.Wwise.Event _Lvl1Music = null;
    [SerializeField] AK.Wwise.Event _ambSound = null;

    // Musics

    public void MusicMenu(bool play)
    {
        if (play)
        {
            _menuMusic.Post(gameObject);
        }
        else
        {
            _menuMusic.Stop(gameObject, 200);
        }
        
    }

    public void Lvl1Music(bool play)
    {
        if (play)
        {
            _Lvl1Music.Post(gameObject);
        }
        else
        {
            _Lvl1Music.Stop(gameObject, 200);
        }

    }


    // Amb
    public void AmbSound(bool play)
    {
        

        if (play)
        {
            _ambSound.Post(gameObject);
        }

        else
        {
            _ambSound.Stop(gameObject, 200);
        }
       
    }


}
