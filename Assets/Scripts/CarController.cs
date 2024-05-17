using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum DriveType
{

    FrontWheelDrive,
    RearWheelDrive,
    AllWheelDrive
}

public class CarController : MonoBehaviour
{
    [SerializeField] private AudioClip forwardSound;
    [SerializeField] private AudioClip backSound;
    [SerializeField] private AudioClip stopSound;
    [SerializeField] private AudioClip brakeSound;
    [SerializeField] public Text speedometerText;
    private AudioSource audioSource;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;

    public InputAction Horizontal;
    public InputAction Vertical;
    public InputAction Freno;

    // Settings
    [SerializeField] private DriveType driveType = DriveType.AllWheelDrive;
    [SerializeField] private float motorForce = 1000f;
    [SerializeField] private float breakForce = 2000f;
    [SerializeField] private float maxSteerAngle = 30f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void Start()
    {
        Horizontal.Enable();
        Vertical.Enable();
        Freno.Enable();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        PlayEngineSound();
        UpdateSpeedometer();
    }

    private void GetInput()
    {
        Vector2 movimiento = Horizontal.ReadValue<Vector2>();
        Vector2 vert = Vertical.ReadValue<Vector2>();

        // Steering Input
        horizontalInput = movimiento.x;

        verticalInput = vert.y; // Entrada del teclado
                                      // Braking Input
        Freno.started += ctx => isBreaking = true;
        Freno.canceled += ctx => isBreaking = false;


    }

    private void HandleMotor()
    {
        switch (driveType)
        {
            case DriveType.FrontWheelDrive:
                HandleFrontWheelDrive();
                break;
            case DriveType.RearWheelDrive:
                HandleRearWheelDrive();
                break;
            case DriveType.AllWheelDrive:
                HandleAllWheelDrive();
                break;
        }
    }

    private void HandleFrontWheelDrive()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        ApplyBreaking();
    }

    private void HandleRearWheelDrive()
    {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        ApplyBreaking();
    }

    private void HandleAllWheelDrive()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        currentBreakForce = isBreaking ? breakForce : 0f;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void UpdateSpeedometer()
    {
        // Calcular la velocidad del coche
        float speed = GetSpeed();

        // Actualizar el texto del velocímetro
        speedometerText.text = $"{speed:F2} m/s";
    }

    private float GetSpeed()
    {
        // Obtener la velocidad lineal promedio de los cuatro neumáticos
        float avgWheelSpeed = (frontLeftWheelCollider.rpm + frontRightWheelCollider.rpm +
                               rearLeftWheelCollider.rpm + rearRightWheelCollider.rpm) / 4f;

        // Convertir RPM a m/s
        float speedMS = avgWheelSpeed * (2f * Mathf.PI * frontLeftWheelCollider.radius) / 60f;

        return speedMS;
    }

    void OnDisable()
    {
        // Deshabilita la InputAction cuando el script se desactiva o se destruye
        Horizontal.Disable();
        Vertical.Disable();
        Freno.Disable();
    }

    private void PlayEngineSound()
    {
        if (verticalInput > 0) // Si el auto está avanzando
        {
            if (!audioSource.isPlaying || audioSource.clip != forwardSound)
            {
                audioSource.clip = forwardSound;
                audioSource.Play();
            }
        }
        else if (verticalInput < 0) // Si el auto está retrocediendo
        {
            if (!audioSource.isPlaying || audioSource.clip != backSound)
            {
                audioSource.clip = backSound;
                audioSource.Play();
            }
        }
        else if (isBreaking) // Si el auto está frenando
        {
            if (!audioSource.isPlaying || audioSource.clip != brakeSound)
            {
                audioSource.clip = brakeSound;
                audioSource.Play();
            }
        }
        else // Si el auto está parado
        {
            if (!audioSource.isPlaying || audioSource.clip != stopSound)
            {
                audioSource.clip = stopSound;
                audioSource.Play();
            }
        }
    }

}
