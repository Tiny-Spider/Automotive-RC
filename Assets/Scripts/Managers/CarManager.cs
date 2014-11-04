using UnityEngine;
using System.Collections;

public class CarManager : Singleton<CarManager> {
    public CarData emptyCar;
    public CarData[] cars;

    public static CarData GetCurrentCar() {
        return GetCar(Lobby.instance.GetMyProfile().selectedCar);
    }

    public static CarData GetCar(string name) {
        foreach (CarData car in CarManager.instance.cars) {
            if (car.name.Equals(name)) {
                return car;
            }
        }

        return CarManager.instance.emptyCar;
    }

    [System.Serializable]
    public struct CarData {
        public Sprite image;
        public string name;
        public string description;
        public Car prefab;
    }
}
