using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneController : MonoBehaviour
{
    [SerializeField]
    List<AeroSurface> controlSurfaces = null;
    [SerializeField]
    List<WheelCollider> wheels = null;
    [SerializeField]
    float rollControlSensitivity = 0.2f;
    [SerializeField]
    float pitchControlSensitivity = 0.2f;
    [SerializeField]
    float yawControlSensitivity = 0.2f;

    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float roll;
    [Range(0, 1)]
    public float flap;
    [SerializeField]
    Text displayText = null;

    float _thrustPercent;
    float _brakesTorque;

    AircraftPhysics _aircraftPhysics;
    Rigidbody _rb;

    private void Start()
    {
        _aircraftPhysics = GetComponent<AircraftPhysics>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        pitch = Input.GetAxis("Vertical");
        roll = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Yaw");
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thrustPercent = _thrustPercent > 0 ? 0 : 1f;
        }
    
        if (Input.GetKeyDown(KeyCode.F))
        {
            flap = flap > 0 ? 0 : 0.3f;
        }
    
        if (Input.GetKeyDown(KeyCode.B))
        {
            _brakesTorque = _brakesTorque > 0 ? 0 : 100f;
        }
    
        displayText.text = "V: " + ((int)_rb.velocity.magnitude).ToString("D3") + " m/s\n";
        displayText.text += "A: " + ((int)transform.position.y).ToString("D4") + " m\n";
        displayText.text += "T: " + (int)(_thrustPercent * 100) + "%\n";
        displayText.text += _brakesTorque > 0 ? "B: ON" : "B: OFF";
    }

    public void UpdateInput(float pitch,float roll,float yaw)
    {
        // this.pitch = pitch;
        // this.roll = roll;
        // this.yaw = yaw;

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     thrustPercent = thrustPercent > 0 ? 0 : 1f;
        // }
        //
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     Flap = Flap > 0 ? 0 : 0.3f;
        // }
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     brakesTorque = brakesTorque > 0 ? 0 : 100f;
        // }
        // displayText.text = "V: " + ((int)rb.velocity.magnitude).ToString("D3") + " m/s\n";
        // displayText.text += "A: " + ((int)transform.position.y).ToString("D4") + " m\n";
        // displayText.text += "T: " + (int)(thrustPercent * 100) + "%\n";
        // displayText.text += brakesTorque > 0 ? "B: ON" : "B: OFF";
    }

    private void FixedUpdate()
    {
        SetControlSurfecesAngles(pitch, roll, yaw, flap);
        _aircraftPhysics.SetThrustPercent(_thrustPercent);
        foreach (var wheel in wheels)
        {
            wheel.brakeTorque = _brakesTorque;
            // small torque to wake up wheel collider
            wheel.motorTorque = 0.01f;
        }
    }

    public void SetControlSurfecesAngles(float pitch, float roll, float yaw, float flap)
    {
        foreach (var surface in controlSurfaces)
        {
            if (surface == null || !surface.isControlSurface) continue;
            switch (surface.inputType)
            {
                case ControlInputType.Pitch:
                    surface.SetFlapAngle(pitch * pitchControlSensitivity * surface.inputMultiplyer);
                    break;
                case ControlInputType.Roll:
                    surface.SetFlapAngle(roll * rollControlSensitivity * surface.inputMultiplyer);
                    break;
                case ControlInputType.Yaw:
                    surface.SetFlapAngle(yaw * yawControlSensitivity * surface.inputMultiplyer);
                    break;
                case ControlInputType.Flap:
                    surface.SetFlapAngle(this.flap * surface.inputMultiplyer);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            SetControlSurfecesAngles(pitch, roll, yaw, flap);
    }
}
