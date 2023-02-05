using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private VoidGameEvent mainMenuEvent;
    
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
    
    private float _pLeftTimeElapsed;
    private float _pLeftStartValue = 1;
    private float _pLeftEndValue = 1;
    private float _pLeftLifeValue;
    private IEnumerator pLeftCoroutine;
    
    private void OnEnable() {
        pRightLifeCountEvent.AddCallback(PlayerRightLostHp);
        pLeftLifeCountEvent.AddCallback(PlayerLeftLostHp);
    }

    private void Update() {
        if (pRightLifeBarre.fillAmount <= 0.01f || pLeftLifeBarre.fillAmount <= 0.01f ) {
            mainMenuEvent.Call();
        }
    }

    private void PlayerRightLostHp(float newLifeValue) {
        if (pRightCoroutine != null) {
            StopCoroutine(pRightCoroutine);
        }
        _pRightTimeElapsed = 0;
        _pRightStartValue = pRightLifeBarre.fillAmount;
        _pRightEndValue = newLifeValue;
        pRightCoroutine = LerpPlayRightLifeCount();
        StartCoroutine(pRightCoroutine);
    }

    private void PlayerLeftLostHp(float newLifeValue) {
        if (pLeftCoroutine != null) {
            StopCoroutine(pLeftCoroutine);
        }
        _pLeftTimeElapsed = 0;
        _pLeftStartValue = pLeftLifeBarre.fillAmount;
        _pLeftEndValue = newLifeValue;
        pLeftCoroutine = LerpPlayLeftLifeCount();
        StartCoroutine(pLeftCoroutine);
    }

    IEnumerator LerpPlayRightLifeCount()
    {
        while (_pRightTimeElapsed < lerpDuration)
        {
            _pRightLifeValue = Mathf.Lerp(_pRightStartValue, _pRightEndValue, _pRightTimeElapsed / lerpDuration);
            _pRightTimeElapsed += Time.deltaTime;
            pRightLifeBarre.fillAmount = _pRightLifeValue;
            yield return null;
        }
        _pRightLifeValue = _pRightEndValue;
    }
    
    IEnumerator LerpPlayLeftLifeCount()
    {
        while (_pLeftTimeElapsed < lerpDuration)
        {
            _pLeftLifeValue = Mathf.Lerp(_pLeftStartValue, _pLeftEndValue, _pLeftTimeElapsed / lerpDuration);
            _pLeftTimeElapsed += Time.deltaTime;
            pLeftLifeBarre.fillAmount = _pLeftLifeValue;
            yield return null;
        }
        _pLeftLifeValue = _pLeftEndValue;
    }
    
    
}
