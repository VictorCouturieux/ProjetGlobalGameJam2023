using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField]
    protected Collider ExplosionTrigger;
    
    public ParticleSystem GrenadeExplosion;

    private void Start()
    {
        ExplosionTrigger.enabled = false;
    }

    public override void Explode()
    {
        AudioManager.Instance.ExplosionPatate(gameObject);
        //GrenadeExplosion.Play();
        ExplosionTrigger.enabled = true;
        Instantiate(GrenadeExplosion, transform.position, transform.rotation);
        base.Explode();
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
