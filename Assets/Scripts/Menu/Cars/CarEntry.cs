using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CarEntry : MonoBehaviour {
    public Image image;
    public Text description;

    private string carName;

    public void ChooseCar() {
        Lobby.instance.UpdateProfile(Network.player, PlayerProfile.CAR, carName);
    }

    public void HidePanel() {
        FindObjectOfType<Menu>().HidePanel(FindObjectOfType<CarDisplayer>().carPanel);
    }

    public void SetCar(CarManager.CarData car) {
        carName = car.name;

        image.sprite = car.image;

        string text = car.name + Environment.NewLine;
        text += car.description + Environment.NewLine;
        text += "Max Speed: " + car.prefab.maxSpeed + Environment.NewLine;
        text += "Engine Torque: " + car.prefab.engineTorque + Environment.NewLine;

        description.text = text;
    }
}
