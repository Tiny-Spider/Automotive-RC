using UnityEngine;
using System.Collections;

public class PlayerProfile {
    private NetworkPlayer owner;

    public string name;
    public int startPosition;

    public PlayerProfile(NetworkPlayer owner) {
        this.owner = owner;
    }

    public NetworkPlayer GetOwner() {
        return owner;
    }
}
