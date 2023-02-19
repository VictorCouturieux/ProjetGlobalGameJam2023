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

    private void Update()
    {
        /*if(isPlaying)
            _radioEffect.transform.LookAt(Camera.main.transform.position, -Vector3.up);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying && other.gameObject.GetComponent<Projectile>() != null)
        {
            _musicController.MusicChanged(!isRock);
            Debug.Log("Change music to : " + (isRock ? "Rock" : "Reggae"));
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
