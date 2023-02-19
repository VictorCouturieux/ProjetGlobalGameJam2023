using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreenCharacters : MusicController
{
    public CharacterTitleScreen Rocker;
    public CharacterTitleScreen Reggae;
    public Image leftTriggerImage;
    public Image rightTriggerImage;

    public override void MusicChanged(bool isRock)
    {
        base.MusicChanged(isRock);
        
        if (AudioManager.Instance.isRockPlaying)
        {
            Reggae.Angry();
            Rocker.Dance();
            leftTriggerImage.color = Color.white;
            rightTriggerImage.color = Color.gray;
        }
        else
        {
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
            MusicChanged(true);
        }
    }
    public void RightTrigger(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            AudioManager.Instance.UiGenericClick();
            MusicChanged(false);
        }
    }
}
