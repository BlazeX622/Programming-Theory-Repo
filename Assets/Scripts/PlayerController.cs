using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;

    // User Input
    private float verticalInput;
    private float horizontalInput;

    [Header("Motor Config")]
    [SerializeField] private float motorTorque = 2000;
    [SerializeField] private float brakeTorque = 2000;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float steeringRange = 30;
    [SerializeField] private float steeringRangeAtMaxSpeed = 10;
    //[SerializeField] private float centreOfGravityOffset = -1f;

    private WheelControl[] wheels;
    [SerializeField] private Checkpoint checkpoint;

    private bool stopMovement;
    private void OnEnable()
    {
        checkpoint.onGameOver += StopMovement;
    }

    private void OnDisable()
    {
        checkpoint.onGameOver -= StopMovement;
    }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Find all child GameObjects that have the WheelControl script attached
        wheels = GetComponentsInChildren<WheelControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovement) return;
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (stopMovement)
        {
            WheelPhysics(0, 0, false);
            return;
        }
        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);

        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        // Use that to calculate how much torque is available 
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // …and to calculate how much to steer 
        // (the car steers more gently at top speed)
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Check whether the user input is in the same direction 
        // as the car's velocity
        bool isAccelerating = Mathf.Sign(verticalInput) == Mathf.Sign(forwardSpeed);
        WheelPhysics(currentMotorTorque, currentSteerRange, isAccelerating);
    }

    private void StopMovement()
    {
        stopMovement = true;
    }

    private void WheelPhysics(float currentMotorTorque, float currentSteerRange, bool isAccelerating)
    {
        foreach (var wheel in wheels)
        {
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = horizontalInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = verticalInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheel.WheelCollider.brakeTorque = Mathf.Abs(verticalInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }
}
