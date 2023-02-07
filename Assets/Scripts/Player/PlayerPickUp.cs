using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public InteractionWidget interactionWidget;

    private PlayerController _player;
    private Plant _plant;
    private Projectile _projectile;

    [System.NonSerialized] public bool NeedReload = false;

    [SerializeField] private float _timeToThrow = 0.5f;


    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Plant plant = other.GetComponent<Plant>();

        if (plant == null || IsHoldingPlant())
            return;

        _plant = plant;
        interactionWidget.Show(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_player.PickUpUI != null)
        {
            _player.StopCoroutine(_player.PickUpUI);
            UpdateUI(0);
        }
        Plant plant = other.GetComponent<Plant>();

        if (plant == null || IsHoldingPlant())
            return;

        _plant = null;
        interactionWidget.Hide();
    }

    /// <summary>
    /// Call when the plant is picked up and projectile created
    /// </summary>
    public void PickUpPlant()
    {
        AudioManager.Instance.PlayerUproot(gameObject);
        _projectile = _plant.PickUp(_player);
        _projectile.transform.SetParent(_player.getHandTransform());
        _projectile.transform.position = _player.getHandTransform().position;
        _player.StopCoroutine(_player.PickUpUI);
        UpdateUI(0);
        Destroy(_plant);
        _plant = null;
        NeedReload = true;
        interactionWidget.SetPickedUp();
    }

    public void UpdateUI(float timer)
    {
        if (interactionWidget.enabled && timer <= 0.5f)
        {
            if (!IsHoldingPlant())
                interactionWidget.SetPickUpMaskValue(timer / 0.5f);
            else if (IsHoldingPlant())
            {
                interactionWidget.SetThrowMaskValue(timer / _timeToThrow);
                  if (timer == Time.deltaTime)
                  {
                      AudioManager.Instance.UiThrowLoad(gameObject);
                    
                }
                  RTPC_manager.Instance._SetPitchLoadRTPC = (timer / _timeToThrow * 100);
                

            }

        }
    }

    public float GetTimeToThrow()
    {
        return _timeToThrow;
    }

    public void Interact()
    {
        if (IsHoldingPlant())
        {
            
            _player.ThrowPlant();
            OnThrowPlant();
        } 
        else 
            PickUpPlant();
    }

    public void CancelPickUp()
    {
        interactionWidget.SetPickUpMaskValue(0);
    }

    public void CancelThrow()
    {
        interactionWidget.SetThrowMaskValue(0);
    }

    public void OnThrowPlant()
    {

        CancelPickUp();
        CancelThrow();
        _projectile = null;
        interactionWidget.Hide();
    }

    public bool IsHoldingPlant()
    {
        return _projectile != null;
    }

    public bool EmptyHands()
    {
        return _plant != null && _projectile == null;
    }

    public Plant GetPlant()
    {
        return _plant;
    }

    public Projectile GetProjectile()
    {
        return _projectile;
    }
}
