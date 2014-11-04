using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    public float maxSpeed = 100.0F;
    public float maxSpeedBackward = 35.0F;
    public float engineTorque = 225.0F;
    public float maxEngineRPM = 650.0F;
    public float minEngineRPM = 45.0F;

    public bool wheel_FL_power = true;
    public bool wheel_FR_power = true;
    public bool wheel_RL_power = false;
    public bool wheel_RR_power = false;

    public float breakPower = 400.0F;

    public bool wheel_FL_break = false;
    public bool wheel_FR_break = false;
    public bool wheel_RL_break = true;
    public bool wheel_RR_break = true;

    public float steerAngle = 4.0F;

    public NetworkPlayer player;

    void Start() {
        if (networkView.isMine) {
            networkView.RPC("SetOwner", RPCMode.AllBuffered, Network.player);
        }
    }

    [RPC]
    public void SetOwner(NetworkPlayer player) {
        this.player = player;
    }
}
