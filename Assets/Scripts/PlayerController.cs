using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform Hand;
    [SerializeField] private List<GameObject> BulletPrefabs;
    
    private Vector2 _inputVector;
    private float _bulletTimer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = transform.TransformDirection(new Vector3(_inputVector.x, 0, _inputVector.y));
    }

    public void Move(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>() * 5;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
            _bulletTimer = Time.time;
        if (context.canceled)
        {
            GameObject Bullet = Instantiate(BulletPrefabs[Random.Range(0, BulletPrefabs.Count - 1)], Hand.position, Quaternion.identity);
            Vector3 bulletForce = Mathf.Clamp(Time.time - _bulletTimer, 0f, 5f) * (-Vector3.right + Vector3.up / 2) * 10f;
            Bullet.GetComponent<Rigidbody>().AddForce(bulletForce, ForceMode.Impulse);
        }
    }

}
