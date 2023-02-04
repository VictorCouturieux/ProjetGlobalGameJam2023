using System.Collections.Generic;
using UnityEngine;

using System;

public class GameEvent<T> : ScriptableObject
{
    private List<Action<T>> callbacks;
    public event Action<T> callEnd;
    public bool enabled = true;

    private void OnEnable() {
        callbacks = new List<Action<T>>();
    }

    public void AddCallback(Action<T> callback) {
        callbacks.Add(callback);
    }
    
    public void RemoveCallback(Action<T> callback) {
        callbacks.Remove(callback);
    }

    public void Call(T value) {
        if (enabled) {
            // Call all callbacks
            for (int i = callbacks.Count-1; i >= 0; i--) {
                callbacks[i].Invoke(value);
            }

            // Call end of event
            if (callEnd != null) {
                callEnd.Invoke(value);
            }
        }
    }
}

public class GameEvent<T, U> : ScriptableObject
{
    private List<Action<T, U>> callbacks;
    public event Action<T, U> callEnd;
    public bool enabled = true;

    private void OnEnable() {
        callbacks = new List<Action<T, U>>();
    }

    public void AddCallback(Action<T, U> callback) {
        callbacks.Add(callback);
    }
    
    public void RemoveCallback(Action<T, U> callback) {
        callbacks.Remove(callback);
    }

    public void Call(T value1, U value2) {
        if (enabled) {
            // Call all callbacks
            for (int i = callbacks.Count-1; i >= 0; i--) {
                callbacks[i].Invoke(value1, value2);
            }

            // Call end of event
            if (callEnd != null) {
                callEnd.Invoke(value1, value2);
            }
        }
    }
}

public class GameEvent<T, U, V> : ScriptableObject
{
    private List<Action<T, U, V>> callbacks;
    public event Action<T, U, V> callEnd;
    public bool enabled = true;

    private void OnEnable() {
        callbacks = new List<Action<T, U, V>>();
    }

    public void AddCallback(Action<T, U, V> callback) {
        callbacks.Add(callback);
    }
    
    public void RemoveCallback(Action<T, U, V> callback) {
        callbacks.Remove(callback);
    }

    public void Call(T value1, U value2, V value3) {
        if (enabled) {
            // Call all callbacks
            for (int i = callbacks.Count-1; i >= 0; i--) {
                callbacks[i].Invoke(value1, value2, value3);
            }

            // Call end of event
            if (callEnd != null) {
                callEnd.Invoke(value1, value2, value3);
            }
        }
    }
}
