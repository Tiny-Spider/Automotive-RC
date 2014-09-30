﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lobby : MonoBehaviour {
    public Dictionary<NetworkPlayer, string> connectedPlayers = new Dictionary<NetworkPlayer, string>();

    [RPC]
    public void StartGame() {
        Debug.Log("Recived start game message!");

        StartCoroutine(SceneLoader.LoadLevel("Dev"));
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar() {
        while (!SceneLoader.isLoaded) {
            yield return new WaitForEndOfFrame();
        }

        Network.Instantiate(GameManager.GetInstance().prefab, Vector3.zero, Quaternion.identity, 0);
    }

    [RPC]
    public void AddPlayer(string name, NetworkMessageInfo info) {
        Debug.Log("A new player connected: " + name);
        connectedPlayers.Add(info.sender, name);
    }

    [RPC]
    public void DisconnectPlayer(NetworkPlayer networkPlayer) {
        Debug.Log("A player disconnected: " + networkPlayer.guid);
        connectedPlayers.Remove(networkPlayer);
    }
}
