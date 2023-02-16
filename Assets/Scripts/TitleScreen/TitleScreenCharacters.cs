using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreenCharacters : MonoBehaviour
{
    public bool isRockPlaying;

    public CharacterTitleScreen Rocker;
    public CharacterTitleScreen Reggae;
    public Image leftTriggerImage;
    public Image rightTriggerImage;
    private void Start()
    {
        isRockPlaying = Random.value >= 0.5;

        if (isRockPlaying)
        {
            AudioManager.Instance.RockMusic(true);
            AudioManager.Instance.ReggaeMusic(false);
            Rocker.Dance();
            leftTriggerImage.color = Color.white;
            rightTriggerImage.color = Color.gray;
        }
        else
        {
            leftTriggerImage.color = Color.gray;
            rightTriggerImage.color = Color.white;
            Reggae.Dance();
            AudioManager.Instance.RockMusic(false);
            AudioManager.Instance.ReggaeMusic(true);
        }
    }

    private void MusicChanged()
    {
        if (isRockPlaying)
        {
            AudioManager.Instance.RockMusic(true);
            AudioManager.Instance.ReggaeMusic(false);
            Reggae.Angry();
            Rocker.Dance();
            leftTriggerImage.color = Color.white;
            rightTriggerImage.color = Color.gray;
        }
        else
        {
            AudioManager.Instance.RockMusic(false);
            AudioManager.Instance.ReggaeMusic(true);
            Reggae.Dance();
            Rocker.Angry();
            leftTriggerImage.color = Color.gray;
            rightTriggerImage.color = Color.white;
        }
    }

    public void LeftTrigger(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            AudioManager.Instance.UiGenericClick();
            isRockPlaying = true;
            MusicChanged();
        }
    }
    public void RightTrigger(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            AudioManager.Instance.UiGenericClick();
            isRockPlaying = false;
            MusicChanged();
        }
    }
}
