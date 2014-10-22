using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Controller")]
[RequireComponent(typeof(Car))]
public class CarController : MonoBehaviour {
    private Car car;

    public WheelCollider wheel_FL;
    public WheelCollider wheel_FR;
    public WheelCollider wheel_RL;
    public WheelCollider wheel_RR;

    // KM/U (m/s * 3.6 = km/u)
    public float speed = 0.0F;

    private float engineRPM = 0.0F;
    private float motorInput = 0.0F;

    public Transform COM;
    public GameObject headLights;
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

        car = GetComponent<Car>();

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
            motorAudio.pitch = Mathf.Abs(engineRPM / car.maxEngineRPM) + 1.0F;
            if (motorAudio.pitch > 2.0) {
                motorAudio.pitch = 2.0F;
            }
        }

        //Steering
        wheel_FL.steerAngle = car.steerAngle * Input.GetAxis("Horizontal");
        wheel_FR.steerAngle = car.steerAngle * Input.GetAxis("Horizontal");

        //Speed Limiter.
        if (motorInput < 0.0F ? speed > car.maxSpeedBackward : speed > car.maxSpeed) {
            wheel_FL.motorTorque = 0;
            wheel_FR.motorTorque = 0;
            wheel_RL.motorTorque = 0;
            wheel_RR.motorTorque = 0;
        }
        else {
            if (car.wheel_FL_power)
                wheel_FL.motorTorque = car.engineTorque * Input.GetAxis("Vertical");
            if (car.wheel_FR_power)
                wheel_FR.motorTorque = car.engineTorque * Input.GetAxis("Vertical");
            if (car.wheel_RL_power)
                wheel_RL.motorTorque = car.engineTorque * Input.GetAxis("Vertical");
            if (car.wheel_RR_power)
                wheel_RR.motorTorque = car.engineTorque * Input.GetAxis("Vertical");
        }

        //SkidAudio.
        if (skidAudio) {
            WheelHit correspondingGroundHit;
            wheel_RR.GetGroundHit(out correspondingGroundHit);

            skidAudio.enabled = Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 10.0F;
        }

        //HandBrake
        if (Input.GetButton("Jump")) {
            if (car.wheel_FL_break)
                wheel_FL.brakeTorque = car.breakPower;
            if (car.wheel_FR_break)
                wheel_FR.brakeTorque = car.breakPower;
            if (car.wheel_RL_break)
                wheel_RL.brakeTorque = car.breakPower;
            if (car.wheel_RR_break)
                wheel_RR.brakeTorque = car.breakPower;

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

        if (Input.GetButtonDown("Light")) {
            headLights.SetActive(!headLights.activeInHierarchy);
        }

        if (Input.GetButton("FReset")) {
            transform.position = Vector3.zero;
            transform.rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < 10; i++) {
            if (Input.GetKeyDown(i + "")) {
                car.SetLight(i);
                break;
            }
        }
    }
}
