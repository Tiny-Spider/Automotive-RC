using UnityEngine;
using System.Collections;

public abstract class Mode {

    public float startTime;
    public  float time;

    public abstract void Awake();
   
	// Use this for initialization
    public abstract void Start();
	
	// Update is called once per frame
    public abstract void Update();

    public abstract void OnRegisterCar(GameObject car);

}
