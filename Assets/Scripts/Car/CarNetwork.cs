using UnityEngine;
using System.Collections;

// Disable the unused messages forOnSerializeNetworkView
#pragma warning disable 0219

[AddComponentMenu("Car/Network")]
[RequireComponent(typeof(Car))]
public class CarNetwork : MonoBehaviour {
    private Car car;

    public WheelCollider[] wheels;

    public GameObject headLights;

    Vector3 position = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    Vector3 velocity = Vector3.zero;

    bool showHeadLights = false;

    void Awake() {
        if (!(Network.isServer || Network.isClient)) {
            networkView.enabled = false;
            enabled = false;
        }

        car = GetComponent<Car>();
    }

    void Update() {
        if (!networkView.isMine) {
            for (int i = 0; i < wheels.Length; i++) {

            }

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
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 velocity = rigidbody.velocity;

            bool showHeadLights = headLights.activeInHierarchy;

            foreach (WheelCollider wheelCollider in wheels) {
                float value = 0;

                value = wheelCollider.steerAngle;
                stream.Serialize(ref value);

                value = wheelCollider.motorTorque;
                stream.Serialize(ref value);

                value = wheelCollider.brakeTorque;
                stream.Serialize(ref value);
            }

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
            stream.Serialize(ref velocity);

            stream.Serialize(ref showHeadLights);
        }
        else {
            foreach (WheelCollider wheelCollider in wheels) {
                float value = 0;

                stream.Serialize(ref value);
                wheelCollider.steerAngle = value;

                stream.Serialize(ref value);
                wheelCollider.motorTorque = value;

                stream.Serialize(ref value);
                wheelCollider.brakeTorque = value;
            }

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
            stream.Serialize(ref velocity);

            stream.Serialize(ref showHeadLights);
        }
    }
}
