using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuManager : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.RTPC _musicVolumeRTPC = null;
    [SerializeField]
    [Range(0f, 100f)]
    private float _musicvolume = 100f;


    [SerializeField]
    private AK.Wwise.RTPC _sfxVolumeRTPC = null;
    [SerializeField]
    [Range(0f, 100f)]
    private float _sfxvolume = 100f;

    public float MusiqueVolume {
        get => _musicvolume;
        set
        {
            _musicvolume = value;
            _musicVolumeRTPC.SetGlobalValue(value);
        }
    }

    public float SFXVolume
    {
        get => _sfxvolume;
        set
        {
            _sfxvolume = value;
            _sfxVolumeRTPC.SetGlobalValue(value);
        }
    }

    void Start()
    {
        _musicvolume = _musicVolumeRTPC.GetGlobalValue();
        _sfxvolume = _sfxVolumeRTPC.GetGlobalValue();

        AudioManager.Instance.AmbSound(true);
    }

    private void OnDisable()
    {
        AudioManager.Instance.MusicMenu(false);
    }
}
