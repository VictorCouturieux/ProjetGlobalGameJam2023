using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    [SerializeField] private float ExplosionRange = 2f;
    public ParticleSystem GrenadeExplosion;

    public override void Explode()
    {
        AudioManager.Instance.ExplosionPatate(gameObject);
        Instantiate(GrenadeExplosion, transform.position, transform.rotation);
        // Vérification si le joueur est dans la zone d'explosion
        if (_enemy == null)
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                if (player.layer != gameObject.layer)
                    _enemy = player.GetComponent<PlayerController>();
        
        if(Vector3.Distance(_enemy.transform.position, transform.position) <= ExplosionRange)
            _enemy.Hurt(Damage);
        base.Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, ExplosionRange);
    }
}
