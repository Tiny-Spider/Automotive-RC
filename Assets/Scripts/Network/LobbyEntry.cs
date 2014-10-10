﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyEntry : MonoBehaviour {
    public Text nameText;
    public Image carImage;
    public Button kickButton;

    public string car;
    public NetworkPlayer player;

    void Awake() {
        // Can't kick yourself, dummy :P
        if (player == Network.player) {
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
