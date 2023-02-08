using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerDetection : MonoBehaviour
{
	[SerializeField] private PlayerInput p1PlayerInput;
    [SerializeField] private PlayerInput p2PlayerInput;
    
    private InputDevice p1Device;
    private InputDevice p2Device;

    private void Awake() {
        //p1PlayerInput = player1.GetComponent<PlayerInput>();
        //p2PlayerInput = player2.GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //DisableControlDevice(p1PlayerInput);
        //DisableControlDevice(p2PlayerInput);
        if (Gamepad.all.Count >= 2) {
	        p1PlayerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
	        p1Device = Gamepad.all[0];
	        p2PlayerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
	        p2Device = Gamepad.all[1];
        }
        // else if (Gamepad.all.Count == 1) {
	       //  p1PlayerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
	       //  p1Device = Gamepad.all[0];
	       //  p2PlayerInput.SwitchCurrentControlScheme("",null);
        // }
        InputSystem.onDeviceChange += (device, change) => InputDeviceChange(device, change);
    }

    void InputDeviceChange(InputDevice device, InputDeviceChange change) {
	    switch (change) {
            case UnityEngine.InputSystem.InputDeviceChange.Added:
	            if (device is Gamepad) {
		            if (Gamepad.all.Count >= 2) {
			            if (p1Device == null) {
				            p1Device = device;
			            } 
			            if (p2Device == null) {
				            p2Device = device;
			            }
		            } else if (Gamepad.all.Count == 1) {
			            if (p1Device == null) {
				            p1Device = device;
			            } else if (p2Device == null) {
				            p2Device = device;
			            }
		            }
	            }
	            if (p1Device != null) {
		            p1PlayerInput.SwitchCurrentControlScheme("Gamepad", p1Device);
	            }
	            else {
		            p1PlayerInput.SwitchCurrentControlScheme();
	            }
	            if (p2Device != null) {
		            p2PlayerInput.SwitchCurrentControlScheme("Gamepad", p2Device);
	            }
	            else {
		            p2PlayerInput.SwitchCurrentControlScheme();
	            }
	            break;
            case UnityEngine.InputSystem.InputDeviceChange.Removed:
	            // Debug.Log(Gamepad.all.Count);
	            if (Gamepad.all.Count <= 1) {
		            if (device == p1Device) {
			            p1Device = null;
		            }
		            if (device == p2Device) {
			            p2Device = null;
		            }
	            }
	            if (p1Device != null) {
		            p1PlayerInput.SwitchCurrentControlScheme("Gamepad", p1Device);
	            }
	            else {
		            p1PlayerInput.SwitchCurrentControlScheme();
	            }
	            if (p2Device != null) {
		            p2PlayerInput.SwitchCurrentControlScheme("Gamepad", p2Device);
	            }
	            else {
		            p2PlayerInput.SwitchCurrentControlScheme();
	            }
	            break;
            case UnityEngine.InputSystem.InputDeviceChange.Reconnected:
	            // Plugged back in.
	            // Debug.Log("p1 : " + p1.devices.Count + "p2 : " + p2.devices.Count);
	            // Debug.Log("Device Reconnected: " + device.description.interfaceName);
	            break;
            case UnityEngine.InputSystem.InputDeviceChange.Disconnected:
	            // Device got unplugged.
	            // Debug.Log("p1 : " + p1.devices.Count + "p2 : " + p2.devices.Count);
	            // Debug.Log("device Disconnected: " + device.description.interfaceName);
	            break;
            default:
	            // See InputDeviceChange reference for other event types.
	            break;
            }
    }

    // Update is called once per frame
    void Update()
    {
        // if (p1Device == null || p2Device == null ) {
        //     // freeze game + ui error message 
        //     //errorGamepadNotConnected.SetActive(true);
        //     Time.timeScale = 0;
        // }
        // else {
        //     //errorGamepadNotConnected.SetActive(false);
        //     Time.timeScale = 1.0f;
        // }
    }
    
    private void EnableControlDevice(PlayerInput playerInput) {
        if (playerInput.devices.Count >= 1) {
            InputSystem.EnableDevice(playerInput.devices[0]);
        }
    }
	
    private void DisableControlDevice(PlayerInput playerInput) {
        if (playerInput.devices.Count >= 1) {
            InputSystem.DisableDevice(playerInput.devices[0]);
        }
    }
    
}
