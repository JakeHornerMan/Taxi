using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarController : MonoBehaviour
{
    [Header("Vehicle Stats")]
    public float currentSpeed;
    private Vector3 currentCarLocalVelocity = Vector3.zero;
    [HideInInspector] public float carVelocityRatio = 0f;

    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private CapsuleCollider carCollider; 
    [SerializeField] private CinemachineVirtualCamera vcam; 
    [SerializeField] private GameObject[] carVisuals = new GameObject[2]; 
    [HideInInspector] private CinemachineTransposer transposer;
    [HideInInspector] private CarSounds carSounds;
    [HideInInspector] private PlayerControls playerControls;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;
    [SerializeField] private Transform accelerationPoint;
    [SerializeField] private GameObject[] tires = new GameObject[4];
    [SerializeField] private GameObject[] frontTireParents = new GameObject[2];
    [SerializeField] private TrailRenderer[] skidMarks = new TrailRenderer[2];
    [SerializeField] private ParticleSystem[] skidSmokes = new ParticleSystem[2];

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
    [SerializeField] private float failedRayLength = 1.5f;

    [Header("Air-Bourne Settings")]
    [Tooltip("AIR-BOURNE FALL SPEED: Downwards force applied to the car when it is in the air (reduce this to make the car float more)")]
    [SerializeField] private float airFloat = 1f;
    [Tooltip("AIR-BOURNE Distance: Forwards force applied to the car when it is in the air (increase this to make the car go further in air)")]
    [SerializeField] private float airTravel = 0.2f;

    [Header("Boost Settings")]
    [HideInInspector] public bool isBoosting = false;
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float maxBoostSpeed = 300f;

    [Header("Handbreak Settings")]
    [HideInInspector] public bool isBreaking = false;
    [HideInInspector] public bool isDrifting = false;
    

    //ground Triggers
    [HideInInspector] public int[] wheelIsGrounded = new int[4];
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isFailedLanding = false;

    [Header("Visuals")]
    [SerializeField] private float tireRotSpeed = 3000f;
    [SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] private float minSideSkidVelocity = 10f;
    

    
    void Awake() 
    {
        carRB = GetComponent<Rigidbody>();
        carCollider = GetComponent<CapsuleCollider>();
        carSounds = GetComponent<CarSounds>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        if (carRB == null) Debug.LogError("Rigidbody not found on the car object.");
    }

    void FixedUpdate() {
        Debug.DrawRay(transform.position, transform.up * failedRayLength, Color.blue);
        Suspension();
        GroundCheck();
        if(!isGrounded && !isFailedLanding) FailedLandCheck();
        CalculateCarVelocity();
        Movement();
        Visuals();
        carSounds.EngineSound(carVelocityRatio);
    }

    void Update() {
        GetPlayerInput();
    }

#region Controller Input
    public void IsAccelerating(float ans){
        moveInput = ans;
    }

    public void IsSteering(float ans){
        steerInput = ans;
    }

    public void IsBoosting(bool ans){
        if (ans)
        {
            Debug.Log(" Boosting! "); 
            // CameraChangeFollowBoost();
            isBoosting = true;
            carSounds.PlayBoostSound();
        }
        else
        {
            Debug.Log(" Not Boosting! ");
            // CameraChangeFollowOffsetReturn();
            isBoosting = false;
        }
    }

    public void IsHandbreaking(bool ans){
        if (ans)
        {
            isBoosting = false;
            isBreaking = true;
            Debug.Log("Hand Break!");
            carSounds.PlayHandbrakeSound();

        }
        else
        {
            isBreaking = false;
            Debug.Log("Hand Break released.");
        }
    }

    public void IsHardReseting(){
        HardResetRotation();
        carSounds.PlayCarHorn();
    }

    private void GetPlayerInput()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            carSounds.PlayCarHorn();
            DriftRotation();
            isDrifting = true;
        }
        if (Input.GetKeyUp(KeyCode.V ))
        {
            carSounds.PlayCarHorn();
            ResetDriftRotation();
            isDrifting = false;
        }
        
    }
#endregion

    private void ResetRotationInAir()
    {
        if (rotationResetRoutine != null)
            StopCoroutine(rotationResetRoutine);

        rotationResetRoutine = StartCoroutine(SmoothResetRotation(1f));
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
                //old code from video
                // SetTirePosition(tires[i], hit.point + rayPoints[i].up * wheelRadius);
                float springLength = Mathf.Clamp(hit.distance - wheelRadius, restLength - springTravel, restLength);
                Vector3 tirePos = rayPoints[i].position - rayPoints[i].up * springLength;
                SetTirePosition(tires[i], tirePos);

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

        //I had this set to 3 for a while it fixed the car from flipping over but it was not realistic.
        if (tempGroundedWheels >= 2)
        {
            if(!isGrounded){
                carRB.drag = 1f;
                carRB.angularDrag = 10f;
            }
            isGrounded = true;
            
        }
        else
        {
            // ResetRotationInAir();
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

        if (isGrounded){
            if (isBreaking)
            {
                HandBrake();
                return;
            }
            Acceleration();
            Deceleration();
            Turn();
            SidewaysDrag();
        }
        else{
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
        carRB.AddForceAtPosition(deceleration * Mathf.Abs(carVelocityRatio) * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Turn()
    {
        carRB.AddRelativeTorque(steerStrength * steerInput * 
            turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * 
            Mathf.Sign(carVelocityRatio) * carRB.transform.up, 
            ForceMode.Acceleration
        );
    }

    private Coroutine handBrakeRoutine;
    public void HandBrake()
    {
        if(isGrounded == false) return; // Only apply handbrake when grounded
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

        while (time < duration && isBreaking)
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
        if (currentSpeed < maxBoostSpeed){
            if(isGrounded)
            {
                carRB.AddForceAtPosition(acceleration * boostMultiplier * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
            }
        }
    }

    public void CameraChangeFollowBoost()
    {
        if (offsetRoutine != null)
            StopCoroutine(offsetRoutine);

        offsetRoutine = StartCoroutine(LerpFollowOffset(new Vector3(0f, 2.5f, -15f), 5f));
    }

    public void CameraChangeFollowOffsetReturn()
    {
        if (offsetRoutine != null)
            StopCoroutine(offsetRoutine);

        offsetRoutine = StartCoroutine(LerpFollowOffset(new Vector3(0f, 2.5f, -10f), 2f));
    }

    private Coroutine offsetRoutine;
    private IEnumerator LerpFollowOffset(Vector3 targetOffset, float duration)
    {
        Vector3 startOffset = transposer.m_FollowOffset;

        float time = 0f;
        while (time < duration && isBoosting)
        {
            float t = time / duration;
            transposer.m_FollowOffset = Vector3.Lerp(startOffset, targetOffset, t);
            time += Time.deltaTime;
            yield return null;
        }

        transposer.m_FollowOffset = targetOffset;
        offsetRoutine = null;
    }

    private void AirbornePhysics()
    {
        if(isGrounded) return; // Only apply airborne physics when not grounded
        carRB.AddForce(acceleration * airFloat * Vector3.down, ForceMode.Acceleration);
        Vector3 flatForward = transform.forward;
        flatForward.y = 0f;
        flatForward.Normalize();
        if(!isBoosting)
        {
            carRB.AddForce(acceleration * airTravel * flatForward, ForceMode.Acceleration);
        }
        else{
            carRB.AddForce(acceleration * (airTravel*2) * flatForward, ForceMode.Acceleration);
        }
    }

    private void SidewaysDrag()
    {
        float currentSidewaysSpeed = currentCarLocalVelocity.x;
        
        float dragMagnitude = -currentSidewaysSpeed * dragCoefficient;

        Vector3 dragForce = transform.right * dragMagnitude;

        carRB.AddForceAtPosition(dragForce, carRB.worldCenterOfMass, ForceMode.Acceleration);
    }

    private void DriftRotation()
    {
        foreach(var obj in carVisuals)
        {
            obj.transform.Rotate(0f, -50.0f, 0f, Space.Self);
        }
    }

    private void ResetDriftRotation()
    {
        foreach(var obj in carVisuals)
        {
            obj.transform.Rotate(0f, 50f, 0f, Space.Self);
        }
    }

    private void Visuals()
    {
        TireVisuals();
        TireVfx();
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

    private void TireVfx()
    {
        if(isGrounded && currentCarLocalVelocity.x > minSideSkidVelocity || isGrounded && currentCarLocalVelocity.x < -minSideSkidVelocity){
            ToggleSkidMarks(true);
            ToggleSkidSmokes(true);
            carSounds.ToggleSkidSound(true);
        }
        else{
            ToggleSkidMarks(false);
            ToggleSkidSmokes(false);
            carSounds.ToggleSkidSound(false);
        }
    }

    private void ToggleSkidMarks(bool toggle){
        foreach(var skidMark in skidMarks){
            skidMark.emitting = toggle;
        }
    }

    private void ToggleSkidSmokes(bool toggle){
        foreach(var smoke in skidSmokes){
            if(toggle){
                smoke.Play();
            }
            else{
                smoke.Stop();
            }
        }
    }

    private void HardResetRotation()
    {
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(flatForward, Vector3.up);
        carRB.MoveRotation(targetRotation);
    }

    private Coroutine rotationResetRoutine;
    private IEnumerator SmoothResetRotation(float duration)
    {
        Quaternion startRotation = carRB.rotation;

        // Find flat forward direction (remove vertical tilt)
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(flatForward, Vector3.up);

        float time = 0f;

        while (time < duration && isGrounded == false)
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

    private void FailedLandCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, failedRayLength, drivable))
        {
            Debug.Log("YOU FAILED THE LANDING!");
            isFailedLanding = true;
            ResetCar();
        }
        Debug.DrawRay(transform.position, transform.up * failedRayLength, Color.blue);
    }

    private void ResetCar(){
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashAndReset(this.gameObject));
    }

    private Coroutine flashRoutine;
    private IEnumerator FlashAndReset(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        
        for(int i = 0; i < 5; i++)
        {
            // Make invisible
            foreach (var rend in renderers)
                rend.enabled = false;

            yield return new WaitForSeconds(0.2f);

            // Make visible
            foreach (var rend in renderers)
                rend.enabled = true;
            
            yield return new WaitForSeconds(0.2f);
        }

        // Make invisible
        foreach (var rend in renderers)
            rend.enabled = false;

        HardResetRotation();

        yield return new WaitForSeconds(0.2f);

        // Make visible
        foreach (var rend in renderers)
            rend.enabled = true;

        isFailedLanding = false;

        yield return new WaitForSeconds(0.2f);
    }
}
