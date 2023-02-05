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

    [System.NonSerialized]
    public bool CanMove = true;
    [System.NonSerialized]
    public Coroutine PickUpUI;
    public GameEvent<float> LifeEvent;
    private float CurrentLife;
    private Vector2 _inputVector;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerPickUp _playerPickUp;
    private float _timerPressHold;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerPickUp = GetComponent<PlayerPickUp>();
        _animator = GetComponentInChildren<Animator>();
        CurrentLife = MaxLife;
    }

    private void Update()
    {
        if (CanMove)
        {
            _rigidbody.velocity = transform.TransformDirection(new Vector3(_inputVector.x, 0, _inputVector.y));

            if(_inputVector != Vector2.zero) 
                Mesh.LookAt(Mesh.position + new Vector3(_inputVector.x, 0, _inputVector.y));
        }
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
        if (context.performed &&
            (_playerPickUp.EmptyHands() || _playerPickUp.IsHoldingPlant()))
        {
            PickUpUI = StartCoroutine(PickUpUICoroutine());
        }

        // Only use to throw plants
        if (context.canceled)
        {
            if(PickUpUI != null)
                StopCoroutine(PickUpUI);
            if (_playerPickUp.NeedReload)
                _playerPickUp.NeedReload = false;
            else if (_playerPickUp.IsHoldingPlant())
            {
                ThrowPlant();
                _animator.SetTrigger("Shoot");
            }
            _playerPickUp.UpdateUI(0);
        }
    }

    public void ThrowPlant()
    {
        if (_playerPickUp.IsHoldingPlant())
        {
            Projectile projectile = _playerPickUp.GetProjectile();
            projectile.transform.parent = null;
            
            Vector3 bulletForce = Mathf.Clamp(_timerPressHold, 0f, 3f) * (shootingNormaliseDirection * Vector3.right + Vector3.up) * 10f;
            projectile.Launch(bulletForce);
            _playerPickUp.OnThrowPlant();
        }
    }

    private IEnumerator PickUpUICoroutine()
    {
        _timerPressHold = 0f;
        while(_timerPressHold < 1f)
        {
            _playerPickUp.UpdateUI(_timerPressHold);
            yield return new WaitForSeconds(Time.deltaTime);
            _timerPressHold += Time.deltaTime;
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
        else
        {
            _animator.SetTrigger("Hurt");
            CanMove = false;
        }
    }

}
