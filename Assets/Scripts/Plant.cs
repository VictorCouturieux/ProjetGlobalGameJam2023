
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantStates {
    SPROUT,
    READY_TO_PICK_UP,
    PICKED_UP,
    IN_THE_AIR,
    CRASHED_ON_THE_FLOOR,
}

enum PlantType
{
    GRENADE,
    ROOTS,
    ICE
}

public class Plant : MonoBehaviour
{
    [SerializeField] private float timeToGrow = 2;
    [SerializeField] private float timeToFade = 10;

    [SerializeField] private SphereCollider _pickUpCollider;
    [SerializeField] private GameObject ExplosionProjectile;
    [SerializeField] private GameObject _smashedPlant;
    
    [SerializeField] private EndLifeVegeEvent endLifeVegetable;

    private Animator _animator;

    private GameObject SpawnedProjectile;
    private PlantStates _state;
    private PlantType _type = PlantType.GRENADE;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _state = PlantStates.SPROUT;
        _pickUpCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == PlantStates.SPROUT || _state == PlantStates.READY_TO_PICK_UP)
            timer += Time.deltaTime;

        if (_state == PlantStates.SPROUT && timer >= timeToGrow)
            GrowPlant();

        if (_state == PlantStates.READY_TO_PICK_UP && timer >= (timeToGrow + timeToFade))
            DispawnPlant();
    }

    private void GrowPlant()
    {
        _pickUpCollider.enabled = true;
        _state = PlantStates.READY_TO_PICK_UP;
        _animator.SetTrigger("NextState");
        
    }

    private void DispawnPlant()
    {
        _pickUpCollider.enabled = false;
        endLifeVegetable.Call(gameObject);
        _animator.SetTrigger("NextState");
        
    }

    public bool CanBePickUp()
    {
        return _state == PlantStates.READY_TO_PICK_UP;
    }

    public Plant PickUp()
    {
        endLifeVegetable.Call(gameObject);
        Destroy(_pickUpCollider);
        Destroy(transform.Find("PlantMesh").gameObject);
        SpawnProjectile();
        _state = PlantStates.PICKED_UP;
        return this;
    }

    private void SpawnProjectile()
    {
        switch (_type)
        {
            case PlantType.GRENADE:
                SpawnedProjectile = Instantiate(ExplosionProjectile, gameObject.transform);
                break;
            default:
                break;
        }
    }
    
    
    public GameObject GetProjectile()
    {
        return SpawnedProjectile;
    }

    private void CrashOnTheFloor(Vector3 crashPosition, Vector3 crashNormal)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        //TODO: use crash normal to set rotation
        Instantiate(_smashedPlant, crashPosition, Quaternion.identity); 
        _state = PlantStates.CRASHED_ON_THE_FLOOR;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_state != PlantStates.IN_THE_AIR)
            return;

        CrashOnTheFloor(collision.contacts[0].point, collision.contacts[0].normal);
    }
}