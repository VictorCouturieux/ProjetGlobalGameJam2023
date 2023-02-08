using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float Damage = 10f;
    
    [SerializeField] private float timeBeforeDestroy = 1;
    [SerializeField] private Collider SmashTrigger;
    

    protected Rigidbody _rigidbody;
    protected PlayerController _enemy;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }
    
    

    public virtual void Explode()
    {
        Destroy(SmashTrigger);
        if (_enemy != null)
        {
            _enemy.CanSmash = false;
            _enemy.GetInteractionWidget().Hide();
        }

        _rigidbody.isKinematic = true;
        Destroy(gameObject);
    }
    
    
    public void Launch(Vector3 bulletForce, bool resetVelocity=false)
    {
        if(resetVelocity)
            _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(bulletForce, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
            Explode();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.CanSmash = false;
            _enemy.GetInteractionWidget().Hide();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si le projectile n'a pas explosé : smash possible
            _enemy = other.gameObject.GetComponent<PlayerController>();
            _enemy.CanSmash = true;
            _enemy._smashProjectile = this;
            _enemy.GetInteractionWidget().Show(false);
        }
    }
}
