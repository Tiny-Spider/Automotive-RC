using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Controller")]
public class CarController : MonoBehaviour {
    public WheelCollider wheel_FL;
    public WheelCollider wheel_FR;
    public WheelCollider wheel_RL;
    public WheelCollider wheel_RR;

    public float maxSpeed = 150.0F;
    public float maxSpeedBackward = 30.0F;
    public float engineTorque = 600.0F;
    public float maxEngineRPM = 3000.0F;
    public float minEngineRPM = 1000.0F;

    public bool wheel_FL_power = true;
    public bool wheel_FR_power = true;
    public bool wheel_RL_power = false;
    public bool wheel_RR_power = false;

    public float breakPower = 600.0F;

    public bool wheel_FL_break = false;
    public bool wheel_FR_break = false;
    public bool wheel_RL_break = true;
    public bool wheel_RR_break = true;

    public float steerAngle = 10.0F;
    // KM/U (m/s * 3.6 = km/u)
    public float speed = 0.0F;

    private float engineRPM = 0.0F;
    private float motorInput = 0.0F;

    public Transform COM;
    public AudioSource skidAudio;
    public AudioSource motorAudio;

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
        if (motorAudio) {
            motorAudio.pitch = Mathf.Abs(engineRPM / maxEngineRPM) + 1.0F;
            if (motorAudio.pitch > 2.0) {
                motorAudio.pitch = 2.0F;
            }
        }

        //Steering
        wheel_FL.steerAngle = steerAngle * Input.GetAxis("Horizontal");
        wheel_FR.steerAngle = steerAngle * Input.GetAxis("Horizontal");

        //Speed Limiter.
        if (motorInput < 0.0F ? speed > maxSpeedBackward : speed > maxSpeed) {
            wheel_FL.motorTorque = 0;
            wheel_FR.motorTorque = 0;
            wheel_RL.motorTorque = 0;
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

        //SkidAudio.
        if (skidAudio) {
            WheelHit correspondingGroundHit;
            wheel_RR.GetGroundHit(out correspondingGroundHit);

            skidAudio.enabled = Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 10.0F;
        }

        //HandBrake
        if (Input.GetButton("Jump")) {
            if (wheel_FL_break)
                wheel_FL.brakeTorque = breakPower;
            if (wheel_FR_break)
                wheel_FR.brakeTorque = breakPower;
            if (wheel_RL_break)
                wheel_RL.brakeTorque = breakPower;
            if (wheel_RR_break)
                wheel_RR.brakeTorque = breakPower;

            wheel_FL.motorTorque = 0;
            wheel_FR.motorTorque = 0;
            wheel_RL.motorTorque = 0;
            wheel_RR.motorTorque = 0;
        }
        else {
            wheel_FL.brakeTorque = 0;
            wheel_FR.brakeTorque = 0;
            wheel_RL.brakeTorque = 0;
            wheel_RR.brakeTorque = 0;
        }

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
