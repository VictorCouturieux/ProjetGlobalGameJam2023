using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTitleScreen : MonoBehaviour
{
    public bool isRock;
    private Animator _animator;
    private Vector3 InitPos;
    private Vector3 InitEul;

    private bool temp;

    private void Awake()
    {
        InitPos = transform.position;
        InitEul = transform.eulerAngles;
        _animator = GetComponent<Animator>();
        _animator.SetBool("isRock", isRock);
    }

    public void Dance()
    {
        _animator.SetTrigger("isMusicPlaying");
        transform.position = InitPos;
        transform.eulerAngles = InitEul;
    }

    public void Angry()
    {
        _animator.SetTrigger("isMusicStopped");
        transform.position = InitPos;
        transform.eulerAngles = InitEul;
    }
}
