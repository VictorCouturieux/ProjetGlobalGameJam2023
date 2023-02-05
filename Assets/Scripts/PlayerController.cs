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
    [SerializeField] private float MaxLife = 100f;

    private float CurrentLife;
    private Vector2 _inputVector;
    private float _startPressTimer;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerPickUp _playerPickUp;

    private bool _isPressingFire = false;
    private float _timerPressLength;

    private Coroutine PickUpUI;
    public GameEvent<float> LifeEvent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerPickUp = GetComponent<PlayerPickUp>();
        _animator = GetComponentInChildren<Animator>();
        CurrentLife = MaxLife;
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

    bool hasReleaseButtonAfterPickUp = false;
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

            if (_playerPickUp.IsHoldingPlant() && !hasReleaseButtonAfterPickUp)
                hasReleaseButtonAfterPickUp = true;
            else if (_playerPickUp.IsHoldingPlant() && hasReleaseButtonAfterPickUp)
            {
                ThrowPlant();
                hasReleaseButtonAfterPickUp = false;
            }

            StopCoroutine(PickUpUI);
            
            _playerPickUp.UpdateUI(0);
        }
    }

    public void ThrowPlant()
    {
        if (_playerPickUp.IsHoldingPlant())
        {
            GameObject projectile = _playerPickUp.GetPlant().GetProjectile();
            projectile.layer = gameObject.layer;
            projectile.transform.parent = null;
            Vector3 bulletForce = Mathf.Clamp(_timerPressLength, 0f, 3f) * (-shootingNormaliseDirection * Vector3.right + Vector3.up) * 10f;
            projectile.GetComponent<Projectile>().Launch(bulletForce);
            _playerPickUp.OnThrowPlant();
        }
    }

    private IEnumerator PickUpUICoroutine()
    {
        _timerPressLength = 0f;
        while(_isPressingFire && _timerPressLength < 1f)
        {
            _playerPickUp.UpdateUI(_timerPressLength);
            yield return new WaitForSeconds(Time.deltaTime);
            _timerPressLength += Time.deltaTime;
        }

        _playerPickUp.Interact();
    }

    public Transform getHandTransform()
    {
        return Hand;
    }

    public void Hurt(float damage)
    {
        CurrentLife -= damage;
        LifeEvent.Call(CurrentLife / MaxLife);

        Debug.Log(CurrentLife / MaxLife);
        if (CurrentLife <= 0)
        {
            //TODO : Die / End
        }
    }
}
