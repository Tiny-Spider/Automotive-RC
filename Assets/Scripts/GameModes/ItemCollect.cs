using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemCollect : MonoBehaviour {

    Dictionary<NetworkPlayer, int> collected;
    public List<GameObject>CollectablePool;
    public Dictionary<GameObject, bool> spawnpoints;

    void Awake () {
	    
	}

	void Start () {
	    
	}
	
	void Update () {
	
	}

    void InitializeCars()
    {
        foreach (CarNetwork car in GameObject.FindObjectsOfType<CarNetwork>())
        {
            collected.Add(car.networkView.owner, 0);
        }
    }

    void InitialzeSpawnpoints()
    {
        //spawnpoints.    
    }

    void AddCollectableToCar(int carID)
    {
        collected[carID] += 1;
    }

    void RemoveCollectableFromCar(int carID)
    {
        collected[carID] -= 1;
    }


}
