using UnityEngine;
using System.Collections;

public class CarManager : Singleton<CarManager> {
    public CarData emptyCar;
    public CarData[] cars;

    public CarData GetCar(string name) {
        foreach (CarData car in cars) {
            if (car.name.Equals(name)) {
                return car;
            }
        }

        return emptyCar;
    }

    [System.Serializable]
    public struct CarData {
        public Sprite image;
        public string name;
        public string description;
        public Car prefab;
    }
}
