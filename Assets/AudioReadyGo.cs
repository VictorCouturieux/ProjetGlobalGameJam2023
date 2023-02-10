using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReadyGo : MonoBehaviour
{
    void SoundReady()
    {
        AudioManager.Instance.UiReadySound();
    }
    void SoundGO()
    {
        AudioManager.Instance.UiGoSound();
    }
    
}
