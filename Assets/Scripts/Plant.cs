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

public class Plant : MonoBehaviour
{
    [SerializeField] private float timeToGrow = 2;
    [SerializeField] private float timeToFade = 10;

    [SerializeField] private SphereCollider _pickUpCollider;
    [SerializeField] private GameObject _plantProjectile;
    [SerializeField] private GameObject _smashedPlant;

    private Animator _animator;

    private PlantStates _state;
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
        _animator.Play("Pickable");
    }

    private void DispawnPlant()
    {
        Destroy(gameObject);
    }

    public bool CanBePickUp()
    {
        return _state == PlantStates.READY_TO_PICK_UP;
    }

    public Plant PickUp()
    {
        Destroy(_pickUpCollider);
        Destroy(transform.Find("PlantMesh").gameObject);
        Instantiate(_plantProjectile, gameObject.transform);
        _state = PlantStates.PICKED_UP;
        return this;
    }

    public void Launch(Vector3 bulletForce)
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        _state = PlantStates.IN_THE_AIR;
        rb.AddForce(bulletForce, ForceMode.Impulse);
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