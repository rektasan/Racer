using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float maxSteerAngle = 35f;

    [SerializeField] private Vector3 centerOfMass;

    [SerializeField] private KeyCode brakeKey = KeyCode.Space;

    [SerializeField] private TrailRenderer rightTrail;
    [SerializeField] private TrailRenderer leftTrail;
    public float MaxSpeed => maxSpeed;
    private float maxSpeed = 120f;
    public float CurSpeed => carRb.velocity.magnitude * 3.6f;

    private float maxAcceleration = 1000f;
    private float brakeAcceleration = 25000f;

    private float turnSensitivity = 1.0f;

    private float moveInput;
    private float steerInput;

    private Rigidbody carRb;

    private bool blockMovement = false;

    private Wheel[] wheels = new Wheel[0];

    private MeshRenderer lights;
    private Color lightsUseColor;
    private Color lightsDefColor;

    private const string tuningEngineKey = "TuningEngine";

    public void SetWheels(Wheel[] newWheels)
    {
        wheels = newWheels;
    }

    public void SetParameters(CarDataSO parameters, MeshRenderer newLights, Color useColor, Color defColor)
    {
        maxSpeed = parameters.MaxSpeed + PlayerPrefs.GetFloat(tuningEngineKey, 0f);
        maxAcceleration = parameters.MaxAcceleration;
        brakeAcceleration = parameters.BrakeAcceleration;
        turnSensitivity = parameters.TurnSensitivity;

        lights = newLights;
        lightsUseColor = useColor;
        lightsDefColor = defColor;

        lights.materials[0].color = lightsDefColor;
        lights.materials[0].SetColor("_EmissionColor", lightsDefColor);
    }

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = centerOfMass;

        rightTrail.emitting = false;
        leftTrail.emitting = false;
    }

    public void BlockMovement(bool isNowBlocked)
    {
        blockMovement = isNowBlocked;
    }

    private void Update()
    {
        GetInputs();
        AnimateWheels();
        DoEffects();
    }

    private void FixedUpdate()
    {
        if (blockMovement)
            return;

        Move();
        Steer();
        Brake();
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        foreach (Wheel wheel in wheels)
        {
            if (CurSpeed < maxSpeed)
            {
                wheel.wheelCollider.motorTorque = moveInput * maxAcceleration;
            }
            else
            {
                wheel.wheelCollider.motorTorque = 0f;
            }
        }
    }

    private void Steer()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                float steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, steerAngle, 0.6f);
            }
        }
    }

    private void Brake()
    {
        if (Input.GetKey(brakeKey) || moveInput == 0)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration;
            }
        }
        else
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    private void AnimateWheels()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    private void DoEffects()
    {
        if (Input.GetKey(brakeKey))
        {
            rightTrail.emitting = true;
            leftTrail.emitting = true;

            lights.materials[0].color = lightsUseColor;
            lights.materials[0].SetColor("_EmissionColor", lightsUseColor);
        }
        else
        {
            rightTrail.emitting = false;
            leftTrail.emitting = false;

            lights.materials[0].color = lightsDefColor;
            lights.materials[0].SetColor("_EmissionColor", lightsDefColor);
        }
    }
}