using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float Damage = 10f;
    
    [SerializeField]
    protected Collider ExplosionTrigger;
    

    private void Awake()
    {
        ExplosionTrigger.enabled = false;
    }

    public void Explode()
    {
        ExplosionTrigger.enabled = true;
        Destroy(gameObject, 2f);
    }
    
    
    public void Launch(Vector3 bulletForce)
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(bulletForce, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Explode();
        } else if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Hurt(Damage);
        }
    }
}
