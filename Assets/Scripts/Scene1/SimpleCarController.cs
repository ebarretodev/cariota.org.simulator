using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    private void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        if (rightButton.isPressed)
        {
            m_horizontalInput += rightButton.dampenPress;
        }
        if (leftButton.isPressed)
        {
            m_horizontalInput -= leftButton.dampenPress;
        }

        m_verticalInput = Input.GetAxis("Vertical");
        if (gasPedal.isPressed)
        {
            m_verticalInput += gasPedal.dampenPress;
        }
        if (brakePedal.isPressed)
        {
            m_verticalInput -= brakePedal.dampenPress;
        }

        m_space = Input.GetKey(KeyCode.Space);

    }

    public void SetSpeed( float _ratioSpeedToSet) {
        motorForce = motorForceDefault / _ratioSpeedToSet;
    }

    public void ForceCarBrake(){
        m_forcedBrake = true;
    }

    public void ReleaseForceCarBrake() {
        m_forcedBrake = false;
    }
    private void Steer(){
        m_steeringAngle = maxSteeringAngle * m_horizontalInput;
        frontRightW.steerAngle = m_steeringAngle;
        frontLeftW.steerAngle = m_steeringAngle;

    }
    private void Accelerate(){
        frontRightW.motorTorque = m_verticalInput * motorForce;
        frontLeftW.motorTorque = m_verticalInput * motorForce;

    }
    private void Brake(){
        float _currentBrake = 0;
        if(m_space || m_forcedBrake){
            _currentBrake = brakeForce;
        }else{
            _currentBrake = 0;
        }

        frontLeftW.brakeTorque = _currentBrake;
        frontRightW.brakeTorque = _currentBrake;
        backLeftW.brakeTorque = _currentBrake;
        backRightW.brakeTorque = _currentBrake;
    }

    private void UpdateWheelPoses(){
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(backLeftW, backLeftT);
        UpdateWheelPose(backRightW, backRightT);

    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;

    }

    void UpdateTotalDistance(){
        float frameDistance = Vector3.Distance(lastPosition, transform.position);
        if (frameDistance>1){
            totalDistance += frameDistance;
            lastPosition = transform.position;
        }
    }

    private void Start(){
        SetSpeed(1f);
        lastPosition = transform.position;
    }
    private void FixedUpdate() {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
        UpdateTotalDistance();
    }

    //Variables
    public float totalDistance;
    private Vector3 lastPosition;
    private float m_horizontalInput;
    private float m_verticalInput;
    private bool m_space;
    private float m_steeringAngle;
    private bool m_forcedBrake = false;

    public WheelCollider frontRightW, frontLeftW, backRightW, backLeftW;
    public Transform frontRightT, frontLeftT, backRightT, backLeftT;
    public float maxSteeringAngle = 30;
    public float motorForceDefault = 500;
    private float motorForce;
    public float brakeForce = 5000;

    public Mybutton gasPedal;
    public Mybutton brakePedal;
    public Mybutton leftButton;
    public Mybutton rightButton;

}
