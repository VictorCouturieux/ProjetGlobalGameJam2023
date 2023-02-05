using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    public ParticleSystem GrenadeExplosion;
    public override void Explode()
    {
        GrenadeExplosion.Play();
        base.Explode();
    }
}