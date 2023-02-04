using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private InteractionWidget _interactionWidget;

    private PlayerController _playerController;

    private Plant _plantPlayerCanInteractWith;
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

        if (IsHoldingPlant())
            return;

        if (!plant.CanBePickUp())
            return;

        _plantPlayerCanInteractWith = plant;

        _interactionWidget.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        Plant plant = other.GetComponent<Plant>();

        if (plant == null)
            return;

        if (IsHoldingPlant())
            return;

        if (_plantPlayerCanInteractWith != plant)
            return;

        _interactionWidget.Hide();
    }

    public void PickUpPlant(Plant plant)
    {
        _plant = _plantPlayerCanInteractWith.PickUp();
        _plant.gameObject.transform.SetParent(_playerController.getHandTransform());
        _plant.gameObject.transform.position = _playerController.getHandTransform().position;
        _plantPlayerCanInteractWith = null;
    }

    public void Update(float timer)
    {
        if (CanPickUpPlant())
        {
            if (timer <= 1)
                _interactionWidget.SetPickUpMaskValue(timer / 1);
        }
        else if (IsHoldingPlant())
        {
            Debug.Log("IS HOLDING PLANT");
            if (timer <= 1)
            {
                _interactionWidget.SetThrowMaskValue(timer / 1);
            }
        }
    }

    public void Interact()
    {
        if(CanPickUpPlant())
            PickUpPlant(_plantPlayerCanInteractWith);
        else if (IsHoldingPlant())
            _playerController.ThrowPlant();
    }

    public void CancelPickUp()
    {
        _interactionWidget.SetPickUpMaskValue(0);
    }

    public void CancelThrow()
    {
        _interactionWidget.SetThrowMaskValue(0);
    }

    public bool IsHoldingPlant()
    {
        return _plant != null;
    }

    public bool CanPickUpPlant()
    {
        return _plantPlayerCanInteractWith != null && _plant == null;
    }

    public Plant GetPlant()
    {
        if (!IsHoldingPlant())
            Debug.LogError("Error - get plant was called but plant is null");

        return _plant;
    }
}
