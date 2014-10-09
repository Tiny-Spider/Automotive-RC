using UnityEngine;
using System.Collections;

// Disable the unused messages forOnSerializeNetworkView
#pragma warning disable 0219

[AddComponentMenu("Car/Network")]
public class CarNetwork : MonoBehaviour {
    public WheelCollider wheel_FL;
    public WheelCollider wheel_FR;
    public WheelCollider wheel_RL;
    public WheelCollider wheel_RR;

    public GameObject headLights;

    float FL_steerAngle = 0;
    float FR_steerAngle = 0;

    float FL_motorTorque = 0;
    float FR_motorTorque = 0;
    float RL_motorTorque = 0;
    float RR_motorTorque = 0;

    float FL_brakeTorque = 0;
    float FR_brakeTorque = 0;
    float RL_brakeTorque = 0;
    float RR_brakeTorque = 0;

    bool showHeadLights = false;

    Vector3 position = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    Vector3 velocity = Vector3.zero;

    void Awake() {
        if (!(Network.isServer || Network.isClient)) {
            networkView.enabled = false;
            enabled = false;
        }
    }

    void Update() {
        if (!networkView.isMine) {
            wheel_FL.steerAngle = FL_steerAngle;
            wheel_FL.motorTorque = FL_motorTorque;
            wheel_FL.brakeTorque = FL_brakeTorque;

            wheel_FR.steerAngle = FR_steerAngle;
            wheel_FR.motorTorque = FR_motorTorque;
            wheel_FR.brakeTorque = FR_brakeTorque;

            wheel_RL.motorTorque = RL_motorTorque;
            wheel_RL.brakeTorque = RL_brakeTorque;

            wheel_RR.motorTorque = RR_motorTorque;
            wheel_RR.brakeTorque = RR_brakeTorque;

            transform.position = Vector3.Lerp(transform.position, position, 8F * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 8F * Time.deltaTime);
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, velocity, 8F * Time.deltaTime);

            if (headLights.activeInHierarchy != showHeadLights) {
                headLights.SetActive(showHeadLights);
            }
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        if (stream.isWriting) {
            float FL_steerAngle = wheel_FL.steerAngle;
            float FR_steerAngle = wheel_FR.steerAngle;

            float FL_motorTorque = wheel_FL.motorTorque;
            float FR_motorTorque = wheel_FR.motorTorque;
            float RL_motorTorque = wheel_RL.motorTorque;
            float RR_motorTorque = wheel_RR.motorTorque;

            float FL_brakeTorque = wheel_FL.brakeTorque;
            float FR_brakeTorque = wheel_FR.brakeTorque;
            float RL_brakeTorque = wheel_RL.brakeTorque;
            float RR_brakeTorque = wheel_RR.brakeTorque;

            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 velocity = rigidbody.velocity;

            bool showHeadLights = headLights.activeInHierarchy;

            stream.Serialize(ref showHeadLights);

            stream.Serialize(ref FL_steerAngle);
            stream.Serialize(ref FR_steerAngle);

            stream.Serialize(ref FL_motorTorque);
            stream.Serialize(ref FR_motorTorque);
            stream.Serialize(ref FR_motorTorque);
            stream.Serialize(ref FR_motorTorque);

            stream.Serialize(ref FL_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
            stream.Serialize(ref velocity);

            stream.Serialize(ref showHeadLights);
        }
        else {
            stream.Serialize(ref FL_steerAngle);
            stream.Serialize(ref FR_steerAngle);

            stream.Serialize(ref FL_motorTorque);
            stream.Serialize(ref FR_motorTorque);
            stream.Serialize(ref FR_motorTorque);
            stream.Serialize(ref FR_motorTorque);

            stream.Serialize(ref FL_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);
            stream.Serialize(ref FR_brakeTorque);

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
            stream.Serialize(ref velocity);

            stream.Serialize(ref showHeadLights);
        }
    }
}
