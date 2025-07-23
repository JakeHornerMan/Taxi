using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControlMapper : MonoBehaviour, PlayerControls.IBaseDrivingActions
{
    [Header("References")]
    private CarController carController;
    private CarTricking carTricking;
    private PlayerControls playerControls;

    private bool isDecelerating = false;

    private InputAction accelerateAction;
    private InputAction decelerateAction;
    void Start()
    {
        playerControls = new PlayerControls();
        carController = GetComponent<CarController>();
        carTricking = GetComponent<CarTricking>();

        accelerateAction = playerControls.BaseDriving.Accelerate;
        decelerateAction = playerControls.BaseDriving.Decelerate;

        playerControls.BaseDriving.SetCallbacks(this);
        playerControls.Enable();
    }

    void Update()
    {
        HandleCarAcceleration();

        HandleCarSteering();

        HandleCarTricking();

        HandleTwistRotation();

        HandleFlipRotation();
    }

    private void HandleCarAcceleration()
    {
        float accel = accelerateAction.ReadValue<float>();
        float decel = decelerateAction.ReadValue<float>();

        float throttle = accel - decel;
        carController.IsAccelerating(throttle);
    }

    public void OnAccelerate(InputAction.CallbackContext context) { }

    public void OnDecelerate(InputAction.CallbackContext context) { }

    public void OnSteer(InputAction.CallbackContext context) { }
    private void HandleCarSteering()
    {
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

    public void OnTricking(InputAction.CallbackContext context) { }

    private void HandleCarTricking()
    {
        Vector2 trickingInput = playerControls.BaseDriving.Tricking.ReadValue<Vector2>();

        if (Mathf.Abs(trickingInput.x) > 0.2f)
        {
            Debug.Log($"Tricking Input X: {trickingInput.x}");
            carTricking.TwistRotation(trickingInput.x);
        }

        if (Mathf.Abs(trickingInput.y) > 0.2f)
        {
            Debug.Log($"Tricking Input Y: {trickingInput.y}");
            carTricking.FlipRotation(trickingInput.y);
        }
    }

    public void OnTwistRotation(InputAction.CallbackContext context) { }
    private void HandleTwistRotation()
    {
        float twistInput = playerControls.BaseDriving.TwistRotation.ReadValue<float>();
        carTricking.TwistRotation(twistInput);
    }

    public void OnFlipRotation(InputAction.CallbackContext context) { }

    private void HandleFlipRotation()
    {
        float flipInput = playerControls.BaseDriving.FlipRotation.ReadValue<float>();
        carTricking.FlipRotation(flipInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        carController.IsJumping();
    }

}
