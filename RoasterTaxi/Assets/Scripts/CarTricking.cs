using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTricking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CarController carController;

    [Header("Rotations Setting")]
    [Tooltip("The force applied to the rotation when performing tricks.")]
    public float rotationalForce;

    void Awake() 
    {
        carController = GetComponent<CarController>();
    }

    private bool CanTrick()
    {
        int tempGroundedWheels = 0;
        for (int i = 0; i < carController.wheelIsGrounded.Length; i++)
        {
            tempGroundedWheels += carController.wheelIsGrounded[i];
        }
        if (tempGroundedWheels <= 0)
        {
            return true;
        }
        return false;
    }

    public void FlipRotation(float input)
    {
        if(!CanTrick() || carController.isFailedLanding) return;
        transform.localRotation *= Quaternion.Euler((input * rotationalForce) * Time.deltaTime, 0 , 0);
    }

    public void TwistRotation(float input)
    {
        if(!CanTrick() || carController.isFailedLanding) return;
        transform.localRotation *= Quaternion.Euler(0, 0, (-input * rotationalForce) * Time.deltaTime);
    }


}
