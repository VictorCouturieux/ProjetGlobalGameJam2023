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
            Rocker.Dance();
            leftTriggerImage.color = Color.white;
            rightTriggerImage.color = Color.gray;
        }
        else
        {
            leftTriggerImage.color = Color.gray;
            rightTriggerImage.color = Color.white;
            Reggae.Dance();
        }
    }

    private void MusicChanged()
    {
        if (isRockPlaying)
        {
            // TODO : Call Rock music
            Reggae.Angry();
            Rocker.Dance();
            leftTriggerImage.color = Color.white;
            rightTriggerImage.color = Color.gray;
        }
        else
        {
            // TODO : Call Reggae music
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
            // TODO : Call button SFX
            isRockPlaying = true;
            MusicChanged();
        }
    }
    public void RightTrigger(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            // TODO : Call button SFX
            isRockPlaying = false;
            MusicChanged();
        }
    }
}
