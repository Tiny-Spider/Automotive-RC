using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Rollbar")]
public class CarRollbar : MonoBehaviour {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public float antiRoll = 5000.0F;

    void FixedUpdate() {
        WheelHit hit;
        float travelL = 1.0F;
        float travelR = 1.0F;

        bool groundedL = leftWheel.GetGroundHit(out hit);

        if (groundedL)
            travelL = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;

        bool groundedR = rightWheel.GetGroundHit(out hit);

        if (groundedR)
            travelR = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;

        float antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
            rigidbody.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        if (groundedR)
            rigidbody.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
    }
}
