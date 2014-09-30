using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour {
    public Transform target;
    private Transform myTransform;

    public float targetHeight = 2.0F;
    public float targetRight = 0.0F;
    public float distance = 6.0F;

    public bool prevButtonRight = false;

    public float maxDistance = 20.0F;
    public float minDistance = 5.0F;

    public float xSpeed = 250.0F;
    public float ySpeed = 120.0F;

    public float yMinLimit = -20.0F;
    public float yMaxLimit = 80.0F;

    public float zoomRate = 1.0F;

    public float rotationDampening = 3.0F;

    public float theta2 = 0.5F;

    private float x = 0.0F;
    private float y = 0.0F;

    //private Vector3 fwd = new Vector3();
    //private Vector3 rightVector = new Vector3();
    //private Vector3 upVector = new Vector3();
    //private Vector3 movingVector = new Vector3();
    //private Vector3 collisionVector = new Vector3();
    //private bool isColliding = false;

    private float distmod = 0.0F;

    void Start() {
        myTransform = transform;
        Vector3 angles = myTransform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    void LateUpdate() {
        if (!target)
            return;

        if (Input.GetMouseButtonUp(0))
            prevButtonRight = false;
        if (Input.GetMouseButtonUp(1))
            prevButtonRight = true;

        // If either mouse buttons are down, let them govern camera position
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02F;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02F;

            // otherwise, ease behind the target if any of the directional keys are pressed
        }
        else if (prevButtonRight) {
            var targetRotationAngle = target.eulerAngles.y;
            var currentRotationAngle = myTransform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomRate * Mathf.Abs(distance);// * Time.deltaTime
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 targetMod = new Vector3(0, -targetHeight, 0) - (rotation * Vector3.right * targetRight);
        var layerMask = 1 << 8;
        layerMask = ~layerMask;
        var position = target.position - (rotation * Vector3.forward * (distance - distmod) + targetMod);
        //var position2 = target.position - (rotation * Vector3.forward * (0.1F) + targetMod);
        /* 
         // Check to see if we have a collision
         if ((Physics.CheckSphere (position, 0.4, layerMask)||Physics.Linecast (position2, position, layerMask))&&(distmod<distance))
         {
            position = target.position - (rotation * Vector3.forward * (distance-distmod) + Vector3(0,-targetHeight,0));
            distmod=Mathf.Lerp(distmod,distance,Time.deltaTime*2);
         }
         else
         {
            var newdistmod=Mathf.Lerp(distmod,0.0,Time.deltaTime*2);
            if (newdistmod<0.1) newdistmod=0.0;
            if (!Physics.CheckSphere (target.position - (rotation * Vector3.forward * (distance-newdistmod) + targetMod), .4, layerMask)&&!Physics.Linecast (position2, target.position - (rotation * Vector3.forward * (distance-newdistmod) + targetMod), layerMask)&&(distmod!=0.0)){
               distmod=newdistmod;
            }
         }
       */
        //position = Vector3.Slerp(transform.position, position, Time.deltaTime * 100);   

        myTransform.rotation = rotation;
        myTransform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
