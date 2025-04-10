using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Vehicle Stats")]
    public float currentSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;
    [SerializeField] private Transform accelerationPoint;
    [SerializeField] private GameObject[] tires = new GameObject[4];
    [SerializeField] private GameObject[] frontTireParents = new GameObject[2];

    [Header("Suspension Settings")]
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float wheelRadius;

    [Header("Input Settings")]
    private float moveInput = 0f;
    private float steerInput = 0f;

    [Header("Car Settings")]
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float steerStrength = 15f;
    [SerializeField] private AnimationCurve turningCurve;
    [Tooltip("GRIP: Force applied reduce slide (increase this to make the car have more grip)")]
    [SerializeField] private float dragCoefficient = 1f;

    [Header("Air-Bourne Settings")]
    [Tooltip("AIR-BOURNE FALL SPEED: Downwards force applied to the car when it is in the air (reduce this to make the car float more)")]
    [SerializeField] private float airFloat = 1f;
    [Tooltip("AIR-BOURNE Distance: Forwards force applied to the car when it is in the air (increase this to make the car go further in air)")]
    [SerializeField] private float airTravel = 0.2f;

    [Header("Boost Settings")]
    public bool isBoosting = false;
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float maxBoostSpeed = 300f;

    [Header("Handbreak Settings")]
    public bool isBreaking = false;
    

    private Vector3 currentCarLocalVelocity = Vector3.zero;
    private float carVelocityRatio = 0f;

    private int[] wheelIsGrounded = new int[4];
    private bool isGrounded = false;

    [Header("Visuals")]
    [SerializeField] private float tireRotSpeed = 3000f;
    [SerializeField] private float maxSteeringAngle = 30f;

    
    void Awake() 
    {
        carRB = GetComponent<Rigidbody>();
        if (carRB == null) Debug.LogError("Rigidbody not found on the car object.");
    }

    void FixedUpdate() {
        Suspension();
        GroundCheck();
        CalculateCarVelocity();
        Movement();
        Visuals();
    }

     void Update() {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log(" Boosting! ");
            isBoosting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log(" Not Boosting! ");
            isBoosting = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBoosting = false;
            isBreaking = true;
            Debug.Log("Hand Break!");

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBreaking = false;
            Debug.Log("Hand Break released.");
        }

        if (Input.GetKeyDown(KeyCode.E) && !isGrounded)
        {
            // ResetRotationInAir();
            if (rotationResetRoutine != null)
                StopCoroutine(rotationResetRoutine);

            rotationResetRoutine = StartCoroutine(SmoothResetRotation(0.5f));
        }

    }

    private void Suspension()
    {
        for(int i = 0; i < rayPoints.Length; i++)
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxLength + wheelRadius, drivable))
            {

                wheelIsGrounded[i] = 1;

                float curretSpringLength = hit.distance - wheelRadius;
                float springCompression = (restLength - curretSpringLength) / springTravel;

                //Calculate damper force (proportional to the velocity of the suspension compression)
                float springVelocity = Vector3.Dot(carRB.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                carRB.AddForceAtPosition(netForce * rayPoints[i].up , rayPoints[i].position);

                //Tire Visuals
                SetTirePosition(tires[i], hit.point + rayPoints[i].up * wheelRadius);

                Debug.DrawRay(rayPoints[i].position, -rayPoints[i].up * maxLength, Color.red);
            }
            else
            {
                wheelIsGrounded[i] = 0;

                //Tire Visuals
                SetTirePosition(tires[i], rayPoints[i].position - rayPoints[i].up * maxLength);

                Debug.DrawRay(rayPoints[i].position, rayPoints[i].position + (wheelRadius + maxLength) * -rayPoints[i].up, Color.green);
            }

        }
    }

    private void GroundCheck()
    {
        int tempGroundedWheels = 0;
        for (int i = 0; i < wheelIsGrounded.Length; i++)
        {
            tempGroundedWheels += wheelIsGrounded[i];
        }

        if (tempGroundedWheels > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void CalculateCarVelocity()
    {
        currentCarLocalVelocity = transform.InverseTransformDirection(carRB.velocity);
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;
    }

    private void Movement()
    {
        if (isBreaking)
        {
            HandBrake();
            return;
        }

        if(isGrounded){
            Acceleration();
            Deceleration();
            Turn();
            SidewaysDrag();
        }
        else
        {
            AirbornePhysics();
        }

        if (isBoosting)
        {
            Boost();
        }
    }

    private void Acceleration()
    {
        currentSpeed = Vector3.Dot(carRB.velocity, transform.forward);

        if (currentSpeed < maxSpeed)
        {
            carRB.AddForceAtPosition(acceleration * moveInput * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
        }
    }

    private void Deceleration()
    {
        carRB.AddForceAtPosition(deceleration * moveInput * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private Coroutine handBrakeRoutine;
    public void HandBrake()
    {
        // Stop any existing handbrake coroutine to prevent stacking
        if (handBrakeRoutine != null)
            StopCoroutine(handBrakeRoutine);

        handBrakeRoutine = StartCoroutine(SmoothStop(0.5f));
    }

    private IEnumerator SmoothStop(float duration)
    {
        Vector3 startVelocity = carRB.velocity;
        Vector3 startAngularVelocity = carRB.angularVelocity;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            // Smoothly interpolate both linear and angular velocity
            carRB.velocity = Vector3.Lerp(startVelocity, Vector3.zero, t);
            carRB.angularVelocity = Vector3.Lerp(startAngularVelocity, Vector3.zero, t);

            time += Time.deltaTime;
            yield return null;
        }

        // Ensure it's completely stopped at the end
        carRB.velocity = Vector3.zero;
        carRB.angularVelocity = Vector3.zero;

        handBrakeRoutine = null;
    }

    private void Boost()
    {
        currentSpeed = Vector3.Dot(carRB.velocity, transform.forward);
        if (currentSpeed < maxBoostSpeed)
        {
            carRB.AddForceAtPosition(acceleration * boostMultiplier * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
        }
    }

    private void AirbornePhysics()
    {
        carRB.AddForceAtPosition(acceleration * airFloat * -transform.up, accelerationPoint.position, ForceMode.Acceleration);
        carRB.AddForceAtPosition(acceleration * airTravel * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Turn()
    {
        carRB.AddRelativeTorque(steerStrength * steerInput * 
            turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * 
            Mathf.Sign(carVelocityRatio) * carRB.transform.up, 
            ForceMode.Acceleration
        );
    }

    private void SidewaysDrag()
    {
        float currentSidewaysSpeed = currentCarLocalVelocity.x;
        
        float dragMagnitude = -currentSidewaysSpeed * dragCoefficient;

        Vector3 dragForce = transform.right * dragMagnitude;

        carRB.AddForceAtPosition(dragForce, carRB.worldCenterOfMass, ForceMode.Acceleration);
    }

    private void Visuals()
    {
        TireVisuals();
        if(isGrounded == false && DetectFalling() == false){ // need to check if is tricking
            if (tiltDownRoutine != null)
            StopCoroutine(tiltDownRoutine);

            tiltDownRoutine = StartCoroutine(SmoothTiltDownward(1f, 20f));
        }
    }

    private bool DetectFalling()
    {
        return !isGrounded && carRB.velocity.y < -0.1f;
    }

    private void TireVisuals()
    {
        float steeringAngle = maxSteeringAngle * steerInput;

        for(int i = 0; i < tires.Length; i++)
        {
            tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);

            if(i < 2){
                frontTireParents[i].transform.localEulerAngles = new Vector3(frontTireParents[i].transform.localEulerAngles.x, steeringAngle, frontTireParents[i].transform.localEulerAngles.z);
            }
            
        }
    } 

    private void SetTirePosition(GameObject tire, Vector3 targetPosition)
    {
        tire.transform.position = targetPosition;
    }

    private Coroutine rotationResetRoutine;
    private IEnumerator SmoothResetRotation(float duration)
    {
        Quaternion startRotation = carRB.rotation;

        // Find flat forward direction (remove vertical tilt)
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(flatForward, Vector3.up);

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            Quaternion newRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            carRB.MoveRotation(newRotation);

            time += Time.deltaTime;
            yield return null;
        }

        // Ensure final alignment
        carRB.MoveRotation(targetRotation);

        rotationResetRoutine = null;
    }

    private Coroutine tiltDownRoutine;
    private IEnumerator SmoothTiltDownward(float duration, float tiltAngle)
    {
        Quaternion startRotation = carRB.rotation;

        // Calculate the downward-tilted rotation (pitch the car's nose down)
        Quaternion tiltRotation = Quaternion.AngleAxis(tiltAngle, transform.right);
        Quaternion targetRotation = startRotation * tiltRotation;

        float time = 0f;
        while (time < duration && isGrounded == false)
        {
            float t = time / duration;
            Quaternion newRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            carRB.MoveRotation(newRotation);

            time += Time.deltaTime;
            yield return null;
        }

        carRB.MoveRotation(targetRotation);
        tiltDownRoutine = null;
    }
}
