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
    Dictionary<NetworkPlayer,int>currentCheckpoint = new Dictionary<NetworkPlayer,int>();
    Dictionary<NetworkPlayer, float> timeChecker = new  Dictionary<NetworkPlayer, float>();
    List<NetworkPlayer> finishedPlayers;

    public override void Awake()
    {
        InitializeCheckPoints();
    }

	public override void Start () {
        startTime = Time.deltaTime;
        finishedPlayers.Clear();
	}
	
	public override void Update () {
        time += Time.deltaTime;
	}

    public override void OnRegisterCar(GameObject car)
    {
        currentCheckpoint.Add(car.networkView.owner, 0);
        timeChecker.Add(car.networkView.owner, 0);
        Debug.Log(car.gameObject.name);
    }

    void InitializeCheckPoints()
    {
        GameObject[] tempCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpoints = new GameObject[tempCheckpoints.Length];
        foreach (GameObject checkpoint in tempCheckpoints)
        {
            int name = int.Parse(checkpoint.name);
            checkpoints[name] = checkpoint;
            Checkpoint tempCheckpoint = checkpoint.AddComponent<Checkpoint>();
            tempCheckpoint.timeTrial = this;
            tempCheckpoint.checkpointNumber = int.Parse(checkpoint.name);
            if (name != 0)
            {
                checkpoint.renderer.material.SetColor("_TintColor", Color.black);
            }
            else
            {
                checkpoint.renderer.material.SetColor("_TintColor", Color.green);
            }
            
        }
        Debug.Log("Amount of checkpoints: " + checkpoints.Length);
    }

    public void OnTriggerEnter(NetworkPlayer player, int checkpointNR, Checkpoint checkpoint)
    {
        Debug.Log("Target checkpoint NR: " + currentCheckpoint[player]);
        Debug.Log("Trigger enter 1");
        if (currentCheckpoint[player] == checkpointNR)
        {
            currentCheckpoint[player]++;
            Debug.Log("THIS CHECKPOINT NR: " + checkpointNR + " NEXT TARGET CHECKPOINT: " + currentCheckpoint[player]);
            if (checkpointNR + 1 < checkpoints.Length)
            {
                Debug.Log("Trigger enter 3");
                checkpoint.HighlightNextPoint(checkpoints[currentCheckpoint[player] - 1], checkpoints[checkpointNR + 1]);
                Debug.Log("Next checkpointNR = " + checkpoints[checkpointNR + 1]);
                Debug.Log("Last checkpointNR = " + checkpoints[currentCheckpoint[player] - 1]);

                Debug.Log("CHECKPOINT TIME DIFFERENCE: " + GetTimeDifference(player));
                Debug.Log("TOTAL RACING TIME: " + GetTotalRaceTime(player));

                //string timeShow = GetTimeDifference(player, timeChecker[player]);

                //send the string through to the GUI.
            }
            else
            {
                finishedPlayers.Add(player);
                checkpoint.HighlightNextPoint(checkpoints[currentCheckpoint[player] - 1]);
                //FINISH CODEHERE.
            }
        }
    }

    public void RaceStart()
    {
        time = 0;
    }


    public string GetTimeDifference(NetworkPlayer player)
    {
        string timeBetween;
        timeBetween = (Time.time - timeChecker[player]).ToString();
        timeChecker[player] = Time.time; 
        timeBetween = TimeSpan.FromSeconds(double.Parse(timeBetween.ToString())).ToString();
        return timeBetween;
    }

    /// <summary>
    /// Gets the total race time.
    /// CALL THIS AFTER GetTimeDifference() else it will take a previous unaccurate time.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public string GetTotalRaceTime(NetworkPlayer player)
    {
        string totalRaceTime;
        totalRaceTime = (startTime - timeChecker[player]).ToString();
        totalRaceTime = TimeSpan.FromSeconds(double.Parse(totalRaceTime.ToString())).ToString();
        return totalRaceTime;
    }
}

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    public TimeTrial timeTrial;

    /// <summary>
    /// Get the trigger collision with the car and then check if that is the next checkpoint for the given car.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint Number: " + checkpointNumber +" Car: "+  other.gameObject.transform.parent.parent.gameObject.networkView.owner);
        timeTrial.OnTriggerEnter(other.gameObject.transform.parent.parent.gameObject.networkView.owner, checkpointNumber, this);
    }

    /// <summary>
    /// Used on the last checkpoint of the list to highlight the final checkpoint.
    /// </summary>
    public void HighlightNextPoint(GameObject lastCheckpoint)
    {
        lastCheckpoint.gameObject.renderer.material.SetColor("_TintColor", Color.red);
    }

    /// <summary>
    /// Used to highlight the next and previous checkpoint to their respective color.
    /// </summary>
    public void HighlightNextPoint(GameObject lastCheckpoint, GameObject newCheckpoint)
    {
        lastCheckpoint.gameObject.renderer.material.SetColor("_TintColor", Color.red); 
        newCheckpoint.renderer.material.SetColor("_TintColor", Color.green);
    }
}
