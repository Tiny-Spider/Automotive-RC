using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEntry : MonoBehaviour {
    public Text nameText;
    public NetworkPlayer player;

    public void SetName(string name) {
        nameText.text = name;
    }

    public void Kick() {
        Network.CloseConnection(player, true);
    }
}
