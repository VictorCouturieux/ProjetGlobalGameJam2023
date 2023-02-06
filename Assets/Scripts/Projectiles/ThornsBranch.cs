using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsBranch : MonoBehaviour
{
    [SerializeField] private Collider _trigger;
    [SerializeField] private Collider _collider;

    private float Damage = 10f;
    
    public void EnableCollider()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt(Damage);
        }
    }
}
