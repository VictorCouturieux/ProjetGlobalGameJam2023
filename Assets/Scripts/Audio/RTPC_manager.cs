using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPC_manager : MonoBehaviour
{

    /* utilisation : On peut appeler cette classe par -> public RTPC_manager _rtpcManager;
     puis faire un _rtpcManager.
    et envoyer les valeurs à ce Get Set qui va update les valeurs
      
    */
    [SerializeField]
    private AK.Wwise.RTPC _sfxVolumeRTPC = null;
    [SerializeField]
    private AK.Wwise.RTPC _pitchLoadRTPC = null;

    [SerializeField]
    [Range(0f, 100f)]
    private float _pitchLoad = 0f;

    [SerializeField]
    [Range(0f, 100f)]
    private float _sfxVolume = 100f;

   

    public float _SFXsetVolume
    {
        get => _sfxVolume;

        set 
        {
            _sfxVolume = value;
            _sfxVolumeRTPC.SetGlobalValue(value);
        }

    }

    public float _SetPitchLoadRTPC
    {

        get => _pitchLoad;
        set 
        {
            _pitchLoad = value;
            _pitchLoadRTPC.SetGlobalValue(value);
        }


    }

    private void Start()
    {
        _pitchLoadRTPC.SetGlobalValue(0f);
        _sfxVolumeRTPC.SetGlobalValue(100f);

    }


}
