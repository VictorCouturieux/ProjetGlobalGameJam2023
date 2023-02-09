using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnLemming : MonoBehaviour
{
	[SerializeField] private Animator animLemming;
	[SerializeField] private float timerValue = 20;
	
	private float _timer;
	private bool _isPlayed = false;

	private void Start() {
		_timer = timerValue;
	}

	private void Update() {
		_timer -= Time.deltaTime;
		if (_timer <= 0f) {
			int roll = Random.Range(1, 8);
			if (roll == 7) {
				animLemming.SetTrigger("Anim");
			}
			_timer = timerValue;
		}
	}
}
