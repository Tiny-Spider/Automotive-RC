using UnityEngine;
using System.Collections;

[AddComponentMenu("Car/Wheels/Skidmark Maker")]
[RequireComponent(typeof(WheelCollider))]
public class WheelSkidmarkMaker : MonoBehaviour {
    public GameObject skidCaller;
    public float startSlipValue = 6.5F;

    private WheelSkidmarks skidmarks = null;
    private int lastSkidmark = -1;
    private WheelCollider wheel_col;

    void Start () {
	    wheel_col = GetComponent<WheelCollider>();
	    skidmarks = FindObjectOfType<WheelSkidmarks>();

	    if(!skidmarks)
		    Debug.Log("No \"WheelSkidmarks\" object found. Skidmarks will not be drawn");
	}

    void FixedUpdate() {
        WheelHit groundHit;
        wheel_col.GetGroundHit(out groundHit);
        float wheelSlipAmount = Mathf.Abs(groundHit.sidewaysSlip);

        if (wheelSlipAmount > startSlipValue) { //if sideways slip is more than desired value
            Vector3 skidPoint = groundHit.point + 2 * (skidCaller.rigidbody.velocity) * Time.deltaTime;
            lastSkidmark = skidmarks.AddSkidMark(skidPoint, groundHit.normal, wheelSlipAmount / 25.0F, lastSkidmark);
        } else {
            lastSkidmark = -1;
        }
    }
}
