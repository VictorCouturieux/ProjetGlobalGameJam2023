using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    public ParticleSystem GrenadeExplosion;

    public override void Explode()
    {
        AudioManager.Instance.ExplosionPatate(gameObject);
        Instantiate(GrenadeExplosion, transform.position, transform.rotation);
        base.Explode();
    }
}
