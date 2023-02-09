using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private EndGameEvent endGameEvent;
    
    [SerializeField] private Image pRightLifeBarre;
    [SerializeField] private Image pLeftLifeBarre;
    
    [SerializeField] private LifeCountEvent pRightLifeCountEvent;
    [SerializeField] private LifeCountEvent pLeftLifeCountEvent;
    
    [SerializeField] private float lerpDuration = 3f;
    
    private float _pRightTimeElapsed;
    private float _pRightStartValue = 1;
    private float _pRightEndValue = 1;
    private float _pRightLifeValue;
    private IEnumerator pRightCoroutine;
    private bool pRightIsRunning = false;
    
    private float _pLeftTimeElapsed;
    private float _pLeftStartValue = 1;
    private float _pLeftEndValue = 1;
    private float _pLeftLifeValue;
    private IEnumerator pLeftCoroutine;
    private bool pLeftIsRunning = false;

    private bool _gameEnd = false;
    
    private void OnEnable() {
        pRightLifeCountEvent.AddCallback(PlayerRightLostHp);
        pLeftLifeCountEvent.AddCallback(PlayerLeftLostHp);
    }
    
    private void Start() {
        if (pRightLifeCountEvent.Size() == 0) {
            pRightLifeCountEvent.AddCallback(PlayerRightLostHp);
        }
        if (pRightLifeCountEvent.Size() == 0) {
            pLeftLifeCountEvent.AddCallback(PlayerLeftLostHp);
        }
    }

    private void Update() {
        if (!_gameEnd) {
            if (pRightLifeBarre.fillAmount <= 0.05f ) {
                endGameEvent.Call(1);
                _gameEnd = true;
            } else if (pLeftLifeBarre.fillAmount <= 0.05f ) {
                endGameEvent.Call(2);
                _gameEnd = true;
            }
        }
    }

    private void PlayerRightLostHp(float newLifeValue) {
        if (pRightCoroutine != null && pRightIsRunning) {
            StopCoroutine(pRightCoroutine);
        }
        _pRightTimeElapsed = 0;
        _pRightStartValue = pRightLifeBarre.fillAmount;
        _pRightEndValue = newLifeValue;
        pRightCoroutine = LerpPlayRightLifeCount();
        StartCoroutine(pRightCoroutine);
    }

    private void PlayerLeftLostHp(float newLifeValue) {
        if (pLeftCoroutine != null && pLeftIsRunning) {
            StopCoroutine(pLeftCoroutine);
        }
        _pLeftTimeElapsed = 0;
        _pLeftStartValue = pLeftLifeBarre.fillAmount;
        _pLeftEndValue = newLifeValue;
        pLeftCoroutine = LerpPlayLeftLifeCount();
        StartCoroutine(pLeftCoroutine);
    }

    IEnumerator LerpPlayRightLifeCount() {
        pRightIsRunning = true;
        while (_pRightTimeElapsed < lerpDuration)
        {
            _pRightLifeValue = Mathf.Lerp(_pRightStartValue, _pRightEndValue, _pRightTimeElapsed / lerpDuration);
            _pRightTimeElapsed += Time.deltaTime;
            pRightLifeBarre.fillAmount = _pRightLifeValue;
            yield return null;
        }
        _pRightLifeValue = _pRightEndValue;
        pRightIsRunning = false;
    }
    
    IEnumerator LerpPlayLeftLifeCount() {
        pLeftIsRunning = true;
        while (_pLeftTimeElapsed < lerpDuration)
        {
            _pLeftLifeValue = Mathf.Lerp(_pLeftStartValue, _pLeftEndValue, _pLeftTimeElapsed / lerpDuration);
            _pLeftTimeElapsed += Time.deltaTime;
            pLeftLifeBarre.fillAmount = _pLeftLifeValue;
            yield return null;
        }
        _pLeftLifeValue = _pLeftEndValue;
        pLeftIsRunning = false;
    }
    
    
}
