using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private bool isPause = false;
    public PauseSys ps;
    public Slider PaliwoSlider;
    private int paliwo = 10000;
    private int maxPaliwo = 10000;
    [Header("Car System")]
    public int CarId;
    public GameObject Player;
    public CarSystem carS;
    public Transform LeavePosition;
    public bool isPlayerIn = false;

    private Vector3 lastPosition;
    [Header("Ważne, nie moje")]
    #region  Nie Dotykać
    
    private float horizontalInput, verticalInput;
        private float currentSteerAngle, currentbreakForce;
        private bool isBreaking;
    
        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    
        // Wheel Colliders
        [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    
        // Wheels
        [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
        [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;
    
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
            {
                ps.Pause(1);
                isPause = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
            {
                ps.Resume();
                isPause = false;
            }

            if (!isPause && paliwo>0)
            {
                GetInput();
                HandleMotor();
                HandleSteering();
                UpdateWheels();
            }

        }
    
        private void GetInput() {
            // Steering Input
            horizontalInput = Input.GetAxis("Horizontal");
            
            // Acceleration Input
            verticalInput = Input.GetAxis("Vertical");
    
            // Breaking Input
            isBreaking = Input.GetKey(KeyCode.Space);
        }
    
        private void HandleMotor() {
            
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }
    
        private void ApplyBreaking() {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }
    
        private void HandleSteering() {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }
    
        private void UpdateWheels() {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }
    
        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
            Vector3 pos;
            Quaternion rot; 
            wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
        #endregion

        private void Update()
        {
            
            if (transform.position != lastPosition)
            {
                paliwo -= 1;
                PaliwoSlider.value = paliwo;
            }
            if (Input.GetKeyDown(KeyCode.E) && isPlayerIn)
            {
                isPlayerIn = false;
                PlayerGetOut();
            }

            if (isPlayerIn)
            {
                Player.transform.position = transform.position;
            }
            lastPosition = transform.position;
        }

        

        public void PlayerGetOut()
        {
            Player.transform.position = LeavePosition.position;
            carS.InteractionWithCar(CarId);
        }
        
}
