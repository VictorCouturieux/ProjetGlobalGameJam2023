using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private PlayerController _playerController;

    private Plant _plant;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Plant plant = other.GetComponent<Plant>();

        if (plant == null)
            return;

        if (HasPlant())
            return;

        if (!plant.CanBePickUp())
            return;

        PickUpPlant(plant);
    }

    private void PickUpPlant(Plant plant)
    {
        _plant = plant.PickUp();
        _plant.gameObject.transform.SetParent(_playerController.getHandTransform());
        _plant.gameObject.transform.position = _playerController.getHandTransform().position;
    }

    public bool HasPlant()
    {
        return _plant != null;
    }

    public Plant GetPlant()
    {
        if (!HasPlant())
            Debug.LogError("Error - get plant was called but plant is null");

        return _plant;
    }
}
