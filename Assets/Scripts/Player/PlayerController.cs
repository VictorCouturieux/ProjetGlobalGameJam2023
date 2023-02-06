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
    [SerializeField] private float DashForce = 3f;
    [SerializeField] private float DashCooldown = 3f;
    [SerializeField] private SkinnedMeshRenderer MeshRenderer;

    [System.NonSerialized]
    public bool CanMove = true;
    private bool CanDash = true;
    [System.NonSerialized]
    public Coroutine PickUpUI;
    public GameEvent<float> LifeEvent;
    private float CurrentLife;
    private Vector2 _inputVector;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private PlayerPickUp _playerPickUp;
    private TrajectoryHelper _trajectoryHelper;
    private float _timerPressHold;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerPickUp = GetComponent<PlayerPickUp>();
        _animator = GetComponentInChildren<Animator>();
        _trajectoryHelper = GetComponentInChildren<TrajectoryHelper>();
        CurrentLife = MaxLife;
    }

    private void Update()
    {
        if (CanMove)
        {
            _rigidbody.velocity = transform.TransformDirection(new Vector3(_inputVector.x, 0, _inputVector.y));

            if (_inputVector != Vector2.zero)
                Mesh.LookAt(Mesh.position + new Vector3(_inputVector.x, 0, _inputVector.y));
        }
    }
    
    public void Dash(InputAction.CallbackContext context)
    {
        if (CanDash && context.performed)
        {
            CanMove = false;
            _animator.SetTrigger("Dash");
            _rigidbody.AddForce(Mesh.forward * DashForce, ForceMode.Impulse);
            StartCoroutine(DashCooldownCoroutine());
        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        _animator.SetBool("Tired", true);
        CanDash = false;
        yield return new WaitForSeconds(DashCooldown);
        CanDash = true;
        _animator.SetBool("Tired", false);
    }
    
    public void Taunt(InputAction.CallbackContext context)
    {
        if(context.performed)
            _animator.SetTrigger("Taunt");
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>() * 6;
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
            // Si le player pouvait plus bouger, c'est qu'il ramassait une plante
            if (!CanMove)
            {
                CanMove = true;
                _animator.SetTrigger("Cancel");
            }
            
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

            if (projectile is Grenade)
            {
                //Vector3 bulletForce = Mathf.Clamp(_timerPressHold, 0f, 3f) * (shootingNormaliseDirection * Vector3.right + Vector3.up) * 20f;
                float throwPercentage = Math.Clamp(_timerPressHold / _playerPickUp.GetTimeToThrow(), 0, 1);
                Vector3 velocity = _trajectoryHelper.CalculateVelocity(throwPercentage);
                projectile.Launch(velocity);
            } else if(projectile is Thorns)
            {
                Vector3 direction = Mesh.forward * 10f;
                projectile.Launch(direction);
            }
            _playerPickUp.OnThrowPlant();
        }
    }

    private IEnumerator PickUpUICoroutine()
    {
        if (_playerPickUp.EmptyHands())
        {
            CanMove = false;
            _rigidbody.velocity = Vector3.zero;
            _animator.SetTrigger("PickUp");
        }
        _timerPressHold = 0f;
        while(_timerPressHold < 0.5f)
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
            StartCoroutine(BlinkCoroutine(4,.1f));
            _animator.SetTrigger("Hurt");
            CanMove = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private IEnumerator BlinkCoroutine(int times, float duration)
    {
        int count = 0;
        while (count < times)
        {
            SetMeshHue(Color.red);
            yield return new WaitForSeconds(duration);
            Debug.Log(count);
            SetMeshHue(Color.white);
            yield return new WaitForSeconds(duration);
            count++;
        }
    }

    private void SetMeshHue(Color hue)
    {
        foreach (Material material in MeshRenderer.materials)
        {
            material.color = hue;
        }
    }
}
