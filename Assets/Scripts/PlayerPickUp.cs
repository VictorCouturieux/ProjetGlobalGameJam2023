using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InteractionState {
    DEFAULT,
    CAN_PICK_UP,
    CAN_THROW,
}
public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private InteractionWidget _interactionWidget;

    private PlayerController _playerController;

    private Plant _plantPlayerCanInteractWith;
    private Plant _plant;

    private InteractionState _state;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _state = InteractionState.DEFAULT;
    }

    private void OnTriggerEnter(Collider other)
    {
        Plant plant = other.GetComponent<Plant>();

        if (plant == null || IsHoldingPlant() || !plant.CanBePickUp())
            return;

        _plantPlayerCanInteractWith = plant;
        _state = InteractionState.CAN_PICK_UP;

        _interactionWidget.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        Plant plant = other.GetComponent<Plant>();

        if (plant == null || IsHoldingPlant() || _plantPlayerCanInteractWith != plant)
            return;

        _state = InteractionState.DEFAULT;

        _interactionWidget.Hide();
    }

    public void PickUpPlant(Plant plant)
    {
        _plant = _plantPlayerCanInteractWith.PickUp();
        _plant.gameObject.transform.SetParent(_playerController.getHandTransform());
        _plant.gameObject.transform.position = _playerController.getHandTransform().position;
        _plantPlayerCanInteractWith = null;
        _state = InteractionState.CAN_THROW;
    }

    public void UpdateUI(float timer)
    {
        if (CanPickUpPlant())
        {
            if (timer <= 1)
                _interactionWidget.SetPickUpMaskValue(timer / 1);
        }
        else if (IsHoldingPlant())
        {
            if (timer <= 1)
                _interactionWidget.SetThrowMaskValue(timer / 1);
        }
    }

    public void Interact()
    {
        if(CanPickUpPlant())
            PickUpPlant(_plantPlayerCanInteractWith);
        else if (IsHoldingPlant())
        {
            _playerController.ThrowPlant();
            OnThrowPlant();
        }
    }

    public void CancelPickUp()
    {
        _interactionWidget.SetPickUpMaskValue(0);
    }

    public void CancelThrow()
    {
        _interactionWidget.SetThrowMaskValue(0);
    }

    public void OnThrowPlant()
    {
        _state = InteractionState.DEFAULT;
        CancelPickUp();
        CancelThrow();
    }

    public bool IsHoldingPlant()
    {
        //return _plant != null;
        return _state == InteractionState.CAN_THROW;
    }

    public bool CanPickUpPlant()
    {
        //return _plantPlayerCanInteractWith != null && _plant == null;
        return _state == InteractionState.CAN_PICK_UP;
    }

    public Plant GetPlant()
    {
        if (!IsHoldingPlant())
            Debug.LogError("Error - get plant was called but plant is null");

        return _plant;
    }
}
