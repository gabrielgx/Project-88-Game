using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoviment : MonoBehaviour
{
    
    [SerializeField]
    private WheelCollider frontLeftW, frontRightW, backLeftW, backRightW;
    
    [SerializeField]
    private Transform frontLeftT, frontRightT, backLeftT, backRightT;
    
    [SerializeField]
    private Transform centerOfmass;
    
    public AnimationCurve steerWheel;
    public float[] cambio;
    public AudioSource CarAudios;
   
    [SerializeField]
    private float maxRpm = 6000;
    
    [SerializeField]
    private float minRpm = 1500f;
    
    [SerializeField]
    private float audioPitch = 4000f;

    
    public float motorForce = 2500f;

    //[SerializeField]
    //private float maxSteerAngle = 45f;

    [SerializeField]
    private float friccao = 500f;

    [SerializeField]
    private float brakeForce = 100000f;


    private float verticalInput;
    private float horizontalInput;
    private float steeringAngle;
    private float veloKMH;
    private Rigidbody rb;
    private float rpm;
    private int cambioAtual = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfmass.localPosition;
        
    }
    void FixedUpdate()
    {
        getInput();
        Steer();
        Acceleration();
        UpdateWheelPoses();
        Brake();
        SpeedController();
        audioController();
       
    }

    public void getInput()
    {      
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
    }
    private void Steer()
    {
        steeringAngle = steerWheel.Evaluate(veloKMH) * horizontalInput;
        frontLeftW.steerAngle = steeringAngle;
        frontRightW.steerAngle = steeringAngle;
    }

    private void Acceleration()
    {
            frontLeftW.motorTorque = verticalInput * motorForce;
            frontRightW.motorTorque = verticalInput * motorForce;    
    }

    private void Brake()
    {
            //frontLeftW.brakeTorque = (Input.GetKey(KeyCode.Space)) ? brakeForce :  friccao - Mathf.Abs(verticalInput * friccao);
            //frontRightW.brakeTorque = (Input.GetKey(KeyCode.Space)) ? brakeForce : friccao - Mathf.Abs(verticalInput * friccao);
            backLeftW.brakeTorque = (Input.GetKey(KeyCode.Space)) ? brakeForce :  friccao - Mathf.Abs(verticalInput * friccao);
            backRightW.brakeTorque = (Input.GetKey(KeyCode.Space)) ? brakeForce : friccao - Mathf.Abs(verticalInput * friccao);
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(backLeftW, backLeftT);
        UpdateWheelPose(backRightW, backRightT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        _collider.GetWorldPose(out pos, out quat);
        _transform.position = pos;
        _transform.rotation = quat;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 128, 32), rpm + "RPM");
        GUI.Label(new Rect(20, 40, 128, 32), (cambioAtual + 1).ToString());
        GUI.Label(new Rect(20, 60, 128, 32), veloKMH + "Km/H");
    }

    private void audioController()
    {
        CarAudios.pitch = rpm / audioPitch;
    }

    private void SpeedController()
    {
        veloKMH = rb.velocity.magnitude * 3.6f;
        rpm = veloKMH * cambio[cambioAtual] * 15f;

        if (rpm > maxRpm)
        {
            cambioAtual++;
            if (cambioAtual == cambio.Length)
            {
                cambioAtual--;
            }
        }

        if (rpm < minRpm)
        {
            cambioAtual--;
            if (cambioAtual < 0)
            {
                cambioAtual = 0;
            }

        }
    }

    private void Drift()
    {
        //frontLeftW.
    }
}