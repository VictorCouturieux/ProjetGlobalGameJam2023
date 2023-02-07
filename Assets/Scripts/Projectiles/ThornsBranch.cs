using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsBranch : MonoBehaviour
{
    [SerializeField] private Collider _trigger;
    private Animator _animator;

    [NonSerialized] public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Hurt(Damage);
        }
    }

    // Anim Event
    // Start dealing damages
    public void SpinesAppeared()
    {
        _trigger.enabled = true;
    }
    
    // Anim Event
    // Start dealing damages
    public void DestroyBranch()
    {
        Destroy(gameObject);
    }
}
