using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public PlayerController Player;
    // For Anim Event
    public void EnableMovement()
    {
        Player.CanMove = true;
    }
}
