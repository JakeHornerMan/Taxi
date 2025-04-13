using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTricking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CarController carController;

    [Header("Rotations Setting")]
    public float rotationalForce;
    private bool leftRotate = false;
    private bool rightRotate = false;
    private bool forwardRotate = false;
    private bool backRotate = false;

    void Awake() 
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        GetPlayerInput();
        HandleRotation();
    }

    private void GetPlayerInput()
    { 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            leftRotate = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rightRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            rightRotate = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            forwardRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            forwardRotate = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            forwardRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            forwardRotate = false;
        }

    }

    private void HandleRotation()
    {
        if(carController.isGrounded || carController.isFailedLanding) return;
        if(leftRotate && !rightRotate)
        {
            LeftRotation();
        }
        
        if(rightRotate && !leftRotate)
        {
            RightRotation();
        }

        if(forwardRotate && !backRotate)
        {
            ForwardRotation();
        }

        if(backRotate && !forwardRotate)
        {
            BackRotation();
        }
    }

    private void LeftRotation()
    {
        transform.localRotation *= Quaternion.Euler(0, 0, rotationalForce * Time.deltaTime);
    }

    private void RightRotation()
    {
        transform.localRotation *= Quaternion.Euler(0, 0, -rotationalForce * Time.deltaTime);
    }

    private void ForwardRotation()
    {
        transform.localRotation *= Quaternion.Euler(-rotationalForce * Time.deltaTime, 0 , 0);
    }

    private void BackRotation()
    {
        transform.localRotation *= Quaternion.Euler(rotationalForce * Time.deltaTime, 0 , 0);
    }


}
