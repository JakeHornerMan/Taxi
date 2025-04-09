using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;

    [Header("Suspension Settings")]
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float wheelRadius;
    
    void Awake() 
    {
        carRB = GetComponent<Rigidbody>();
        if (carRB == null) Debug.LogError("Rigidbody not found on the car object.");
    }

    void FixedUpdate() {
        Suspension();
    }

    private void Suspension()
    {
        foreach(Transform rayPoint in rayPoints)
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, maxLength + wheelRadius, drivable))
            {
                float curretSpringLength = hit.distance - wheelRadius;
                float springCompression = (restLength - curretSpringLength) / springTravel;

                float springVelocity = Vector3.Dot(carRB.GetPointVelocity(rayPoint.position), rayPoint.up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                carRB.AddForceAtPosition(netForce * rayPoint.up , rayPoint.position);
                Debug.DrawRay(rayPoint.position, -rayPoint.up * maxLength, Color.red);
            }
            else
            {
                Debug.DrawRay(rayPoint.position, rayPoint.position + (wheelRadius + maxLength) * -rayPoint.up, Color.green);
            }

        }
    }
}
