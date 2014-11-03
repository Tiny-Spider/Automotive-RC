using UnityEngine;
using System.Collections;

public class PlayerProfile {
    public const string NAME = "name";
    public const string CAR = "car";
    public const string LOADED = "loaded";
    public const string READY = "ready";
    public const string START_POSITION = "startPosition";

    private NetworkPlayer owner;

    public string name;
    public int startPosition;
    public string selectedCar;

    public bool loaded = false;
    public bool ready = false;

    public GameObject car;

    public PlayerProfile(NetworkPlayer owner) {
        this.owner = owner;
    }

    public NetworkPlayer GetOwner() {
        return owner;
    }

    public void UpdateValue(string valueName, string value) {
        switch (valueName) {
            case NAME:
                name = value;
                break;
            case CAR:
                selectedCar = value;
                break;
            case LOADED:
                loaded = value.Equals(bool.TrueString);
                break;
            case READY:
                ready = value.Equals(bool.TrueString);
                break;
            case START_POSITION:
                int.TryParse(value, out startPosition);
                break;
        }
    }
}
