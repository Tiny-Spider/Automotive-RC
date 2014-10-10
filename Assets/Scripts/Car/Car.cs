﻿using UnityEngine;
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

    public int lightID { private set; get; }
    public Light[] lights;

    public void SetLight(int id) {
        lightID = id;

        foreach (Light light in lights) {
            light.flare = GameManager.instance.lensFlares[id];
        }
    }

    void OnNetworkInstantiate(NetworkMessageInfo info) {
        Debug.Log("OnNetworkInstantiate sender: " + info.sender.ToString());
        Lobby.instance.GetProfile(info.sender).car = gameObject;
    }
}
