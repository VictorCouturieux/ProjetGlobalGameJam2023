using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameInit : MonoBehaviour
{
    void OnEnable()
    {
        AudioManager.Instance.AmbSound(true);

    }

    void OnDisable()
    {
        AudioManager.Instance.AmbSound(false);
    }

}
