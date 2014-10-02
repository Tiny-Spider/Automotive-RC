using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Wheels/Alignment")]
public class WheelAlignment : MonoBehaviour {
    public WheelCollider correspondingCollider;
    public GameObject slipPrefab;

    public float slipSpeed = 1.8F;
    private float rotationValue = 0.0F;

    void Update() {
        RaycastHit hit;
        Vector3 colliderCenterPoint = correspondingCollider.transform.TransformPoint(correspondingCollider.center);

        if (Physics.Raycast(colliderCenterPoint, -correspondingCollider.transform.up, out hit, correspondingCollider.suspensionDistance + correspondingCollider.radius)) {
            transform.position = hit.point + (correspondingCollider.transform.up * correspondingCollider.radius);
        }
        else {
            transform.position = colliderCenterPoint - (correspondingCollider.transform.up * correspondingCollider.suspensionDistance);
        }

        transform.rotation = correspondingCollider.transform.rotation * Quaternion.Euler(rotationValue, correspondingCollider.steerAngle, 0);
        rotationValue += correspondingCollider.rpm * (360 / 60) * Time.deltaTime;

        WheelHit correspondingGroundHit;
        correspondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > slipSpeed) {
            if (slipPrefab) {
                Instantiate(slipPrefab, correspondingGroundHit.point, Quaternion.identity);
            }
        }
    }
}
