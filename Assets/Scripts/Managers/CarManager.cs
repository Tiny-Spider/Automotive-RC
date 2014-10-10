using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {
    public CarData[] cars;

    [System.Serializable]
    public struct CarData {
        public Texture2D image;
        public string name;
        public Car prefab;
    }
}
