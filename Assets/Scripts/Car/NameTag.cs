using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class NameTag : MonoBehaviour {
    public NetworkView network;
    public TextMesh text;

    NetworkPlayer owner;
    Transform textTransform;
    Transform camTransform;

    void Start() {
        // Singleplayer
        if (!network || !network.enabled) {
            Destroy(gameObject);
            return;
        }

        // Dont show my own nametag
        if (network.isMine) {
            networkView.RPC("SetOwner", RPCMode.AllBuffered, Network.player);
            Destroy(gameObject);
            return;
        }
    }

    [RPC]
    public void SetOwner(NetworkPlayer owner) {
        this.owner = owner;

        text.text = Lobby.instance.GetProfile(owner).name;

        textTransform = text.transform;
        camTransform = Camera.main.transform;
    }

    void Update() {
        if (textTransform && camTransform)
            textTransform.rotation = camTransform.rotation;
    }
}
