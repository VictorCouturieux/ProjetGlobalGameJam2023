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
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    
    
    [SerializeField] private GameObject winPL;
    [SerializeField] private GameObject losePL;
    [SerializeField] private GameObject winPR;
    [SerializeField] private GameObject losePR;
    
    [SerializeField] private EndGameEvent endGameEvent;
    [SerializeField] private VoidGameEvent mainMenuEvent;
    
    [SerializeField] private float timeRemaining = 6;
    private bool _timerIsRunning = false;

    private void OnEnable() {
        //endGameEvent.AddCallback(StartCinematic);
    }

    private void Start() {
        endGameEvent.Clear();
        endGameEvent.AddCallback(StartCinematic);
    }

    void StartCinematic(int numWinner)
    {
        player1.CanMove = false;
        player2.CanMove = false;
        if (numWinner == 1) {
            winPL.SetActive(true);
            losePR.SetActive(true);
            animPL.SetTrigger("Win");
            animPR.SetTrigger("Lose");
        }
        else if (numWinner == 2) {
            losePL.SetActive(true);
            winPR.SetActive(true);
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
        Debug.Log("EndGame");
        AudioManager.Instance.ReggaeMusic(false);
        mainMenuEvent.Call();
    }
}
