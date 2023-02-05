using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuBehaviors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.ReggaeMusicMenu(true);
    }

    void OnDisable()
    {
        AudioManager.Instance.ReggaeMusicMenu(false);
    }

}
