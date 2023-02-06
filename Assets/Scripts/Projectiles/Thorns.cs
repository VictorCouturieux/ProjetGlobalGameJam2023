using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Projectile
{
    public GameObject ThornBranch;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Separator"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, -contact.normal);
            Vector3 pos = contact.point;
            GameObject go = Instantiate(ThornBranch, pos, rot);
            Destroy(gameObject);
        }
    }
}
