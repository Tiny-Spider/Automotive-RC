using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lobby : Singleton<Lobby> {
    public string loadScreenPanel;

    public string track = "none";
    public string mode = "none";
    public string name = "Player 1";

    public delegate void OnPlayerJoined(NetworkPlayer player);
    public event OnPlayerJoined OnJoin = delegate { };

    public delegate void OnPlayerDisconnect(NetworkPlayer player);
    public event OnPlayerDisconnect OnDisconnect = delegate { };

    public delegate void OnPlayerUpdate(NetworkPlayer player);
    public event OnPlayerUpdate OnUpdate = delegate { };

    private Dictionary<NetworkPlayer, PlayerProfile> connectedPlayers = new Dictionary<NetworkPlayer, PlayerProfile>();

    [RPC]
    public void StartGame() {
        Menu menu = FindObjectOfType<Menu>();
        menu.HidePanels();
        menu.ShowPanel(loadScreenPanel);

        StartCoroutine(SceneLoader.LoadLevel(track));
        StartCoroutine(SpawnCar());
    }

    [RPC]
    public void AddPlayer(NetworkPlayer player, string name) {
        PlayerProfile profile = new PlayerProfile(player);
        profile.name = name;

        connectedPlayers.Add(player, profile);

        OnJoin(player);

        Debug.Log("Name: " + name + " ID: " + player.ToString());
    }

    [RPC]
    public void DisconnectPlayer(NetworkPlayer player) {
        OnDisconnect(player);

        connectedPlayers.Remove(player);
    }

    private IEnumerator SpawnCar() {
        while (!SceneLoader.isLoaded) {
            yield return new WaitForEndOfFrame();
        }

        GameObject car = CarManager.instance.GetCar(GetMyProfile().selectedCar).prefab.gameObject;
        Network.Instantiate(car, Vector3.zero, Quaternion.identity, 0);

        // Tell everyone we're ready
        UpdateProfile(Network.player, PlayerProfile.LOADED, bool.TrueString);
    }

    [RPC]
    public void UpdateProfile(NetworkPlayer player, string valueName, string value) {
        if (Network.player.Equals(player)) {
            networkView.RPC("UpdateProfile", RPCMode.OthersBuffered, Network.player, valueName, value);
        }

        connectedPlayers[player].UpdateValue(valueName, value);
        OnUpdate(player);
    }

    [RPC]
    public void SetTrack(string track) {
        if (Network.isServer) {
            networkView.RPC("SetTrack", RPCMode.OthersBuffered, track);
        }

        this.track = track;
    }

    [RPC]
    public void SetMode(string mode) {
        if (Network.isServer) {
            networkView.RPC("SetMode", RPCMode.OthersBuffered, mode);
        }

        this.mode = mode;
    }

    public PlayerProfile GetProfile(NetworkPlayer player) {
        return connectedPlayers[player];
    }

    public PlayerProfile GetMyProfile() {
        return connectedPlayers[Network.player];
    }

    public void Clear() {
        connectedPlayers.Clear();
    }
}
