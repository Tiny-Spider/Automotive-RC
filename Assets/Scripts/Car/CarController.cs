using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Controller")]
public class CarController : MonoBehaviour {
    public WheelCollider wheel_FL;
    public WheelCollider wheel_FR;
    public WheelCollider wheel_RL;
    public WheelCollider wheel_RR;

    public bool wheel_FL_power = false;
    public bool wheel_FR_power = false;
    public bool wheel_RL_power = true;
    public bool wheel_RR_power = true;

    public float engineTorque = 600.0F;
    public float maxEngineRPM = 3000.0F;
    public float minEngineRPM = 1000.0F;
    public float steerAngle = 10.0F;

    public Transform COM;
    public float speed = 0.0F;
    public float maxSpeed = 150.0F;
    public float maxSpeedBackward = 30.0F;
    public AudioSource skidAudio;
    private float engineRPM = 0.0F;
    private float motorInput = 0.0F;

    void Awake() {
        // Multiplayer
        if (Network.isClient || Network.isServer) {
            if (!networkView.isMine) {
                enabled = false;
                return;
            }
        }

        FindObjectOfType<CarCamera>().target = transform;
    }

    void Start() {
        rigidbody.centerOfMass = new Vector3(COM.localPosition.x * transform.localScale.x, COM.localPosition.y * transform.localScale.y, COM.localPosition.z * transform.localScale.z);
    }

    void Update() {
        speed = rigidbody.velocity.magnitude * 3.6f;
        rigidbody.drag = rigidbody.velocity.magnitude / 100;
        engineRPM = (wheel_FL.rpm + wheel_FR.rpm) / 2;

        //Input For MotorInput.
        motorInput = Input.GetAxis("Vertical");

        //Audio
        audio.pitch = Mathf.Abs(engineRPM / maxEngineRPM) + 1.0F;
        if (audio.pitch > 2.0) {
            audio.pitch = 2.0F;
        }

        //Steering
        wheel_FL.steerAngle = steerAngle * Input.GetAxis("Horizontal");
        wheel_FR.steerAngle = steerAngle * Input.GetAxis("Horizontal");

        //Speed Limiter.
        if (speed > (motorInput < 0.0F ? maxSpeedBackward : maxSpeed)) {
            if (wheel_FL_power) 
                wheel_FL.motorTorque = 0;
            if (wheel_FR_power)
                wheel_FR.motorTorque = 0;
            if (wheel_RL_power)
                wheel_RL.motorTorque = 0;
            if (wheel_RR_power)
                wheel_RR.motorTorque = 0;
        }
        else {
            if (wheel_FL_power)
                wheel_FL.motorTorque = engineTorque * Input.GetAxis("Vertical");
            if (wheel_FR_power)
                wheel_FR.motorTorque = engineTorque * Input.GetAxis("Vertical");
            if (wheel_RL_power)
                wheel_RL.motorTorque = engineTorque * Input.GetAxis("Vertical");
            if (wheel_RR_power)
                wheel_RR.motorTorque = engineTorque * Input.GetAxis("Vertical");
        }

        //Input.
        if (motorInput <= 0) {
            wheel_RL.brakeTorque = 30;
            wheel_RR.brakeTorque = 30;
        }
        else if (motorInput >= 0) {
            wheel_RL.brakeTorque = 0;
            wheel_RR.brakeTorque = 0;
        }

        //SkidAudio.
        if (skidAudio) {
            WheelHit correspondingGroundHit;
            wheel_RR.GetGroundHit(out correspondingGroundHit);

            skidAudio.enabled = Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 10.0F;
        }

        //HandBrake
        if (Input.GetButton("Jump")) {
            wheel_FL.brakeTorque = 100;
            wheel_FR.brakeTorque = 100;
        }

        //if (Input.GetButtonUp("Jump")) {
        //    wheel_FL.brakeTorque = 0;
        //    wheel_FR.brakeTorque = 0;
        //}

        if (Input.GetButton("Reset")) {
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetButton("FReset")) {
            transform.position = Vector3.zero;
            transform.rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
