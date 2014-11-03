using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lobby : Singleton<Lobby> {
    public string loadScreenPanel;

    public string track = "";
    public string mode = "";
    public string name = "Player 1";

    public delegate void OnPlayerJoined(NetworkPlayer player);
    public event OnPlayerJoined OnJoin = delegate { };

    public delegate void OnPlayerDisconnect(NetworkPlayer player);
    public event OnPlayerDisconnect OnDisconnect = delegate { };

<<<<<<< HEAD
    public Dictionary<NetworkPlayer, PlayerProfile> connectedPlayers = new Dictionary<NetworkPlayer, PlayerProfile>();
=======
    public delegate void OnPlayerUpdate(NetworkPlayer player);
    public event OnPlayerUpdate OnUpdate = delegate { };
>>>>>>> c72c7668f382c9525be8761ec09b5214ea3f1f09

    public delegate void OnServerUpdate();
    public event OnServerUpdate OnUpdateServer = delegate { };

    private Dictionary<NetworkPlayer, PlayerProfile> connectedPlayers = new Dictionary<NetworkPlayer, PlayerProfile>();

    [RPC]
    public void StartGame() {
        Menu menu = FindObjectOfType<Menu>();
        menu.HidePanels();
        menu.ShowPanel(loadScreenPanel);

        string sceneName = TrackManager.instance.GetTrack(track).sceneName;

        StartCoroutine(SceneLoader.LoadLevel(sceneName));
        StartCoroutine(SpawnCar());
    }

    [RPC]
    public void AddPlayer(NetworkPlayer player, string name) {
        PlayerProfile profile = new PlayerProfile(player);
        profile.name = name;

        connectedPlayers.Add(player, profile);

        OnJoin(player);

        Debug.Log("Player Connected: (Name: [" + name + "] ID: [" + player.ToString() + "])");
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

        Track track = FindObjectOfType<Track>();
        PlayerProfile profile = GetMyProfile();
        GameObject car = CarManager.instance.GetCar(profile.selectedCar).prefab.gameObject;

        if (track) {
            int position = profile.startPosition;
            GameObject spawnPoint = position > track.spawnPoints.Length ? track.spawnPoints[position] : track.spawnPoints[0];

            Network.Instantiate(car, spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
        }
        else {
            // Fallback
            Network.Instantiate(car, Vector3.zero, Quaternion.identity, 0);
        }

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
        OnUpdateServer();
    }

    [RPC]
    public void SetMode(string mode) {
        if (Network.isServer) {
            networkView.RPC("SetMode", RPCMode.OthersBuffered, mode);
        }

        this.mode = mode;
        OnUpdateServer();
    }

    public List<PlayerProfile> GetProfiles() {
        return new List<PlayerProfile>(connectedPlayers.Values);
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
