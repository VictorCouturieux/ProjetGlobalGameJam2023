using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [NonSerialized] public bool isPlaying = true;
    [SerializeField] private bool isRock;
    [SerializeField] private GameObject _radioEffect;
    [SerializeField] private MusicController _musicController;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying && other.gameObject.GetComponent<Projectile>() != null)
        {
            _musicController.MusicChanged(!isRock);
        }
    }

    public void PlayEffect()
    {
        isPlaying = true;
        _radioEffect.SetActive(true);
    }

    public void StopEffect()
    {
        isPlaying = false;
        _radioEffect.SetActive(false);
    }
}
