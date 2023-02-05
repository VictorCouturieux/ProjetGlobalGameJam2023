using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;


    //can call event with AudioManager.Instance.FunctionName(...);
    private void Awake()
    {
        if (instance != null)
            Destroy(Instance.gameObject);
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    [SerializeField] AK.Wwise.Event _rockMusic = null;
    [SerializeField] AK.Wwise.Event _reggaeMusic = null;
    [SerializeField] AK.Wwise.Event _reggaeMusicMenu = null;
    [SerializeField] AK.Wwise.Event _ambSound = null;
    [SerializeField] AK.Wwise.Event _uiThrowLoad = null;
    [SerializeField] AK.Wwise.Event _uiThrowLoadMax = null;
    [SerializeField] AK.Wwise.Event _uiGenericClick = null;
    [SerializeField] AK.Wwise.Event _uiGenericTransition = null;
    [SerializeField] AK.Wwise.Event _pousseApparition = null;
    [SerializeField] AK.Wwise.Event _pousseUsable = null;
    [SerializeField] AK.Wwise.Event _pousseMoisted = null;

    [SerializeField] AK.Wwise.Event _playerThrow = null;
    [SerializeField] AK.Wwise.Event _playerDash = null;
    [SerializeField] AK.Wwise.Event _playerUproot = null;
    [SerializeField] AK.Wwise.Event _hitPlayer1 = null;
    [SerializeField] AK.Wwise.Event _hitPlayer2 = null;
    [SerializeField] AK.Wwise.Event _explosionPatate = null;

    // Musics

    public void RockMusic(bool play)
    {
        if (play)
        {
            _rockMusic.Post(gameObject);
        }
        else
        {
            _rockMusic.Stop(gameObject, 200);
        }
        
    }

    public void ReggaeMusic(bool play)
    {
        if (play)
        {
            _reggaeMusic.Post(gameObject);
        }
        else
        {
            _reggaeMusic.Stop(gameObject, 200);
        }
        

    }
    public void ReggaeMusicMenu(bool play)
    {
        if (play)
        {
            _reggaeMusicMenu.Post(gameObject);
        }
        else
        {
            _reggaeMusicMenu.Stop(gameObject, 200);
        }


    }

    //POUSSES

    public void PousseApparition()
    {
        _pousseApparition.Post(gameObject);
    }
    public void PousseUsable()
    {
        _pousseUsable.Post(gameObject);
    }

    public void PousseMoisted()
    {
        _pousseMoisted.Post(gameObject);
    }

    //Player Abilities

    public void PlayerThrow()
    {
        _playerThrow.Post(gameObject);
    }

    public void PlayerDash()
    {
        _playerDash.Post(gameObject);
    }
    public void PlayerUproot()
    {
        _playerUproot.Post(gameObject);
    }

    public void ExplosionPatate()
    {
        _explosionPatate.Post(gameObject);
    }
   
    //Voices

    public void HitPlayer1()
    {
        _hitPlayer1.Post(gameObject);
    }
    public void HitPlayer2()
    {
        _hitPlayer2.Post(gameObject);
    }



    //UI
    public void UiThrowLoad()
    {
        _uiThrowLoad.Post(gameObject);
    }

    public void UiThrowLoadMax()
    {
        _uiThrowLoadMax.Post(gameObject);
    }
    public void UiGenericClick()
    {
        _uiGenericClick.Post(gameObject);
    }
    public void UiGenericTransition()
    {
        _uiGenericTransition.Post(gameObject);
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
