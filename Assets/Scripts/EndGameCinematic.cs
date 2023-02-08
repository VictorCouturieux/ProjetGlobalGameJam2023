using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndGameCinematic : MonoBehaviour
{
    [SerializeField] private Animator animCam;
    [SerializeField] private Animator animPL;
    [SerializeField] private Animator animPR;
    [SerializeField] private PlayerInput inputPL;
    [SerializeField] private PlayerInput inputPR;
    
    [SerializeField] private EndGameEvent endGameEvent;
    [SerializeField] private VoidGameEvent mainMenuEvent;
    
    [SerializeField] private float timeRemaining = 6;
    private bool _timerIsRunning = false;

    private void OnEnable() {
        endGameEvent.AddCallback(StartCinematic);
    }

    void StartCinematic(int numWinner) {
        InputSystem.DisableDevice(inputPL.devices[0]);
        InputSystem.DisableDevice(inputPR.devices[0]);
        
        if (numWinner == 1) {
            animPL.SetTrigger("Win");
            animPR.SetTrigger("Lose");
        }
        else if (numWinner == 2) {
            animPR.SetTrigger("Win");
            animPL.SetTrigger("Lose");
        }
        _timerIsRunning = true;
    }
    
    private void Update() {
        if (_timerIsRunning) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                _timerIsRunning = false;
                animCam.SetTrigger("EndGame");
            } 
        }
    }

    public void EndGame()
    {
        InputSystem.EnableDevice(inputPL.devices[0]);
        InputSystem.EnableDevice(inputPR.devices[0]);
        Debug.Log("EndGame");
        AudioManager.Instance.ReggaeMusic(false);
        mainMenuEvent.Call();
    }
}
