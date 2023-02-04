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
    private float _startPressTimer;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerPickUp _playerPickUp;

    private bool _isPressingFire = false;
    private float _timerPressLength;

    private Coroutine PickUpUI;

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

    public void Move(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>() * 10;
        _animator.SetBool("Run", _inputVector != Vector2.zero);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isPressingFire = true;
            PickUpUI = StartCoroutine(PickUpUICoroutine());
        }

        if (context.canceled)
        {
            _animator.SetTrigger("Shoot");
            
            _isPressingFire = false;
            if(_playerPickUp.CanPickUpPlant())
                _playerPickUp.CancelPickUp();
            else if (_playerPickUp.IsHoldingPlant())
                _playerPickUp.CancelThrow();
            StopCoroutine(PickUpUI);
            _playerPickUp.Update(0);
        }
    }

    public void ThrowPlant()
    {
        if (_playerPickUp.IsHoldingPlant())
        {
            Plant plant = _playerPickUp.GetPlant();
            Vector3 bulletForce = Mathf.Clamp(_timerPressLength, 0f, 3f) * (shootingNormaliseDirection * Vector3.right + Vector3.up) * 10f;
            plant.Launch(bulletForce);
        }
    }

    private IEnumerator PickUpUICoroutine()
    {
        _timerPressLength = 0f;
        while(_isPressingFire && _timerPressLength < 1f)
        {
            _playerPickUp.Update(_timerPressLength);
            yield return new WaitForSeconds(Time.deltaTime);
            _timerPressLength += Time.deltaTime;
        }

        _playerPickUp.Interact();
    }

    public Transform getHandTransform()
    {
        return Hand;
    }
}
