using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControlMapper : MonoBehaviour
{
    [Header("References")]
    private CarController carController;
    private PlayerControls playerControls;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Boosting");
            carController.isBoosting = true;
        }
        else if (context.canceled)
        {
            Debug.Log("Boosting ended");
            carController.isBoosting = false;
        }
    }
}
