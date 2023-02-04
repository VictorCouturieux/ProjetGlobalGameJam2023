using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(fileName = "New Void Game Event", menuName = "Game Event/Void")]
public class VoidGameEvent : ScriptableObject
{
	private List<Action> callbacks;
	public event Action callEnd;
	public bool enabled = true;

	private void OnEnable() {
		callbacks = new List<Action>();
	}

	public void AddCallback(Action callback) {
		callbacks.Add(callback);
	}
    
	public void RemoveCallback(Action callback) {
		callbacks.Remove(callback);
	}

	public void Call() {
		if (enabled) {
			// Call all callbacks
			for (int i = callbacks.Count-1; i >= 0; i--) {
				callbacks[i].Invoke();
			}

			// Call end of event
			if (callEnd != null) {
				callEnd.Invoke();
			}
		}
	}
}