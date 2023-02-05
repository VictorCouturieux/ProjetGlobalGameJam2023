
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantType
{
    GRENADE,
    THORNS,
    ICE
}

public class Plant : MonoBehaviour
{
    [SerializeField] private float timeToGrow = 2;
    [SerializeField] private float timeToFade = 10;

    [SerializeField] private GameObject ExplosionProjectile;
    [SerializeField] private GameObject ThornsProjectile;
    [SerializeField] private GameObject IceProjectile;
    [SerializeField] private EndLifeVegeEvent endLifeVegetable;

    private Animator _animator;
    private Collider _pickUpCollider;
    private PlantType _type = PlantType.GRENADE;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _pickUpCollider = GetComponent<Collider>();
        _pickUpCollider.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(GrowCoroutine());
    }

    private IEnumerator GrowCoroutine()
    {
        yield return new WaitForSeconds(timeToGrow);
        GrowPlant();
        yield return new WaitForSeconds(timeToFade);
        DispawnPlant();
    }

    private void GrowPlant()
    {
        _pickUpCollider.enabled = true;
        _animator.SetTrigger("NextState");
        AudioManager.Instance.PousseUsable(gameObject);
    }

    private void DispawnPlant()
    {
        _pickUpCollider.enabled = false;
        _animator.SetTrigger("NextState");
        
    }

    public Projectile PickUp(PlayerController owner)
    {
        DestroyPlant();
        return SpawnProjectile(owner);
    }

    private Projectile SpawnProjectile(PlayerController owner)
    {
        GameObject projectile = null;
        switch (_type)
        {
            case PlantType.GRENADE:
                projectile = Instantiate(ExplosionProjectile, gameObject.transform);
                break;
            case PlantType.THORNS:
                projectile = Instantiate(ThornsProjectile, gameObject.transform);
                break;
            case PlantType.ICE:
                projectile = Instantiate(IceProjectile, gameObject.transform);
                break;
            default:
                break;
        }
        
        projectile.gameObject.layer = owner.gameObject.layer;

        return projectile.GetComponent<Projectile>();
    }

    public void DestroyPlant()
    {
        endLifeVegetable.Call(gameObject);
        Destroy(gameObject);
    }
}