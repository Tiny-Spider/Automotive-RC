using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 1.  InitializeCheckPoints - adds all gameobjects checkpointsX to the array and adds the CheckPoint class to the gameobject.
/// 3.  Then it will initialize the cars by getting their guid which is set to "id" ◄
/// 2.  Checkpoints are listed
/// </summary>
public class TimeTrial : Mode {

    GameObject[] checkpoints;
    Dictionary<int,int>currentCheckpoint;
    Dictionary<int, float> timeChecker;

    public override void Awake()
    {
        InitializeCheckPoints();
        InitializeCars();
    }

	public override void Start () {
        startTime = Time.deltaTime;
	}
	
	public override void Update () {
        time += Time.deltaTime;
	}

    void InitializeCheckPoints()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoints[int.Parse(checkpoint.name)] = checkpoint;
            checkpoint.AddComponent<Checkpoint>().timeTrial = this;
            checkpoint.GetComponent<Checkpoint>().checkpointNumber = int.Parse(checkpoint.name);
        }
        Debug.Log(checkpoints.Length);
    }

    void InitializeCars()
    {
        foreach (Car car in GameObject.FindObjectsOfType<Car>())
        {
            currentCheckpoint.Add(car.id, 0);
            timeChecker.Add(car.id, 0);
        }
    }

    public void OnTriggerEnter(int id, int checkpointNR, Checkpoint checkpoint)
    {
        if (currentCheckpoint[id] == checkpointNR -1)
        {
            currentCheckpoint[id] = checkpointNR;
            checkpoint.HighlightNextPoint(checkpoints[id], checkpoints[id + 1]);

            string timeShow = GetTimeDifference(timeChecker[id]);
            //send the string through to the GUI.
        }
    }

    public void RaceStart()
    {
        time = 0;
    }


    public string GetTimeDifference(float oldTime)
    {
        oldTime = Time.time - oldTime;
        string timeString = TimeSpan.FromSeconds(double.Parse(oldTime.ToString())).ToString();
        return timeString;
    }
}

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    public TimeTrial timeTrial;

    void OnTriggerEnter(Collider other)
    {
        timeTrial.OnTriggerEnter(other.gameObject.GetComponent<Car>().id,checkpointNumber,this);
    }


    public void HighlightNextPoint(GameObject lastCheckpoint, GameObject newCheckpoint)
    {
        lastCheckpoint.renderer.material.color = Color.red;
        newCheckpoint.renderer.material.color = Color.blue;
    }
}
