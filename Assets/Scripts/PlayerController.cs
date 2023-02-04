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
    [SerializeField] private float shootingNormaliseDirection = 1;
    [SerializeField] private Transform Mesh;

    private Vector2 _inputVector;
    private float _bulletTimer;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerPickUp _playerPickUp;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerPickUp = GetComponent<PlayerPickUp>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _rigidbody.velocity = transform.TransformDirection(new Vector3(_inputVector.x, 0, _inputVector.y));
        if(_inputVector != Vector2.zero) 
            Mesh.LookAt(Mesh.position + new Vector3(_inputVector.x, 0, _inputVector.y));
    }
    
    public void Taunt(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("Taunt");
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>() * 10;
        _animator.SetBool("Run", _inputVector != Vector2.zero);
    }

    public void Fire(InputAction.CallbackContext context)
    {
       if (!_playerPickUp.HasPlant())
            return;

       if (context.started)
       {
           _bulletTimer = Time.time;
       }

        if (context.canceled)
        {
            _animator.SetTrigger("Shoot");
            Plant plant = _playerPickUp.GetPlant();
            Vector3 bulletForce = Mathf.Clamp(Time.time - _bulletTimer, 0f, 3f) * (shootingNormaliseDirection*Vector3.right + Vector3.up) * 10f;
            plant.Launch(bulletForce);
        }
    }

    public Transform getHandTransform()
    {
        return Hand;
    }

}
