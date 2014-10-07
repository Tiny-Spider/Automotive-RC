using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class NameTag : MonoBehaviour {
    public NetworkView network;
    public Transform target; 
    public Vector3 offset = Vector3.up;
    public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
    public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped

    Camera cam;
    Transform thisTransform;
    Transform camTransform;

    void Start() {
        if (!network || !network.enabled || network.isMine) {
            Destroy(gameObject);
            return;
        }

        thisTransform = transform;
        cam = Camera.main;
        camTransform = cam.transform;

        guiText.text = FindObjectOfType<Lobby>().GetPlayerName(network.owner);
    }


    void Update() {
        if (clampToScreen) {
            Vector3 relativePosition = camTransform.InverseTransformPoint(target.position);
            relativePosition.z = Mathf.Max(relativePosition.z, 1.0f);
            thisTransform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
            thisTransform.position = new Vector3(Mathf.Clamp(thisTransform.position.x, clampBorderSize, 1.0f - clampBorderSize),
                                             Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1.0f - clampBorderSize),
                                             thisTransform.position.z);

        }
        else {
            thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
        }
    }
}
