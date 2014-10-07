using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lobby : MonoBehaviour {
    private Dictionary<NetworkPlayer, string> connectedPlayers = new Dictionary<NetworkPlayer, string>();
    public string levelToLoad = "Test World";

    public delegate void OnPlayerJoined(NetworkPlayer player, string name);
    public event OnPlayerJoined OnJoin = delegate { };

    public delegate void OnPlayerDisconnect(NetworkPlayer player, string name);
    public event OnPlayerDisconnect OnDisconnect = delegate { };

    [RPC]
    public void StartGame() {
        Debug.Log("Recived start game message!");

        StartCoroutine(SceneLoader.LoadLevel(levelToLoad));
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnCar() {
        while (!SceneLoader.isLoaded) {
            yield return new WaitForEndOfFrame();
        }

        Network.Instantiate(GameManager.GetInstance().prefab, Vector3.zero, Quaternion.identity, 0);
    }

    [RPC]
    public void AddPlayer(NetworkPlayer player, string name) {
        Debug.Log("A new player connected: " + name + ", " + player.ToString());
        connectedPlayers.Add(player, name);

        OnJoin(player, name);
    }

    [RPC]
    public void DisconnectPlayer(NetworkPlayer networkPlayer) {
        Debug.Log("A player disconnected: " + networkPlayer.guid);
        OnDisconnect(networkPlayer, connectedPlayers[networkPlayer]);

        connectedPlayers.Remove(networkPlayer);
    }

    public string GetPlayerName(NetworkPlayer networkPlayer) {
        return connectedPlayers[networkPlayer];
    }

    public void Clear() {
        connectedPlayers.Clear();
    }
}
