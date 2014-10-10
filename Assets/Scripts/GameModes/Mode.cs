using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mode : MonoBehaviour{

    public float startTime;
    public  float time;
    public List<NetworkPlayer> carList = new List<NetworkPlayer>();
    public List<PlayerProfile> playerList = new List<PlayerProfile>();

    public abstract void Awake();
   
    public abstract void Start();
	
    public abstract void Update();

}
