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
    [SerializeField] private SphereCollider _pickUpCollider;

    [SerializeField] private GameObject _smashedPlant;

    private PlantStates _state; 

    // Start is called before the first frame update
    void Start()
    {
        _state = PlantStates.SPROUT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Plant PickUp()
    {
        Destroy(_pickUpCollider);
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

        Instantiate(_smashedPlant, crashPosition, Quaternion.identity); //TODO: use crash normal to set rotation
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