using UnityEngine;
using System.Collections;

public class PlayerProfile {
    public const string LoadedValueName = "Loaded";

    private NetworkPlayer owner;

    public string name;
    public int startPosition;
    public GameObject car;

    public bool loaded = false;

    public PlayerProfile(NetworkPlayer owner) {
        this.owner = owner;
    }

    public NetworkPlayer GetOwner() {
        return owner;
    }

    public void UpdateValue(string valueName, string value) {
        switch (valueName) {
            case LoadedValueName:
                loaded = value.Equals(bool.TrueString);
                break;
        }
    }
}
