using UnityEngine;
using System.Collections;

public abstract class Mode : MonoBehaviour{

    public float startTime;
    public  float time;

    public abstract void Awake();
   
    public abstract void Start();
	
    public abstract void Update();

    public abstract void OnRegisterCar(GameObject car);

}
