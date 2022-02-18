using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_space = Input.GetKey(KeyCode.Space);

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
        if(m_space){
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

    private void FixedUpdate() {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
    }

    //Variables
    private float m_horizontalInput;
    private float m_verticalInput;
    private bool m_space;
    private float m_steeringAngle;

    public WheelCollider frontRightW, frontLeftW, backRightW, backLeftW;
    public Transform frontRightT, frontLeftT, backRightT, backLeftT;
    public float maxSteeringAngle = 30;
    public float motorForce = 750;
    public float brakeForce = 500;



}
