using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyEntry : MonoBehaviour {
    public Text nameText;
    public Button kickButton;
    public NetworkPlayer player;

    void Awake() {
        // Can't kick the server, dummy :P
        if (Network.isServer) {
            kickButton.gameObject.SetActive(false);
        }
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    public void Kick() {
        Network.CloseConnection(player, true);
    }
}
