using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float Damage = 10f;
    
    [SerializeField] private float timeBeforeDestroy = 1;

    protected Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public virtual void Explode()
    {
        Destroy(_rigidbody);
        Destroy(gameObject, timeBeforeDestroy);
    }
    
    
    public void Launch(Vector3 bulletForce)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(bulletForce, ForceMode.Impulse);
    }
}
