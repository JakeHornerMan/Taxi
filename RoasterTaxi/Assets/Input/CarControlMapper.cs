using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControlMapper : MonoBehaviour, PlayerControls.IBaseDrivingActions
{
    [Header("References")]
    private CarController carController;
    private PlayerControls playerControls;
    
    private bool isDecelerating = false;

    private InputAction accelerateAction;
    private InputAction decelerateAction;
    void Start()
    {
        playerControls = new PlayerControls();
        carController = GetComponent<CarController>();

        accelerateAction = playerControls.BaseDriving.Accelerate;
        decelerateAction = playerControls.BaseDriving.Decelerate;

        playerControls.BaseDriving.SetCallbacks(this);
        playerControls.Enable();
    }

    void Update()
    {
        CarAcceleration();

        CarSteering();
    }

    private void CarAcceleration(){
        float accel = accelerateAction.ReadValue<float>();
        float decel = decelerateAction.ReadValue<float>();

        float throttle = accel - decel;
        carController.IsAccelerating(throttle);
    }

    public void OnAccelerate(InputAction.CallbackContext context){}

    public void OnDecelerate(InputAction.CallbackContext context){}

    public void OnSteer(InputAction.CallbackContext context){}
    private void CarSteering(){
        float steerInput = playerControls.BaseDriving.Steer.ReadValue<float>();
        carController.IsSteering(steerInput);
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            carController.IsBoosting(true);
        }
        else if (context.canceled)
        {
            carController.IsBoosting(false);
        }
    }

    public void OnHandbreak(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Handbreaking");
            carController.IsHandbreaking(true);
        }
        else if (context.canceled)
        {
            Debug.Log("Handbreaking ended");
            carController.IsHandbreaking(false);
        }
    }

    public void OnResetRotation(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("IsHardReseting");
            carController.IsHardReseting();
        }
    }

}
