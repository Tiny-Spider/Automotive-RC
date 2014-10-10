using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lobby : MonoBehaviour {
    public static Lobby instance { private set; get; }

    public int loadScreenID = 9;

    public delegate void OnPlayerJoined(PlayerProfile profile);
    public event OnPlayerJoined OnJoin = delegate { };

    public delegate void OnPlayerDisconnect(PlayerProfile profile);
    public event OnPlayerDisconnect OnDisconnect = delegate { };

    private Dictionary<NetworkPlayer, PlayerProfile> connectedPlayers = new Dictionary<NetworkPlayer, PlayerProfile>();

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    [RPC]
    public void StartGame() {
        Menu menu = FindObjectOfType<Menu>();
        menu.HidePanels();
        menu.ShowPanel(loadScreenID);

        StartCoroutine(SceneLoader.LoadLevel(GameManager.instance.levelToLoad));
        StartCoroutine(SpawnCar());
    }

    [RPC]
    public void AddPlayer(NetworkPlayer player, string name) {
        PlayerProfile profile = new PlayerProfile(player);
        profile.name = name;

        connectedPlayers.Add(player, profile);

        OnJoin(profile);

        Debug.Log("Name: " + name + " ID: " + player.ToString());
    }

    [RPC]
    public void DisconnectPlayer(NetworkPlayer networkPlayer) {
        OnDisconnect(connectedPlayers[networkPlayer]);

        connectedPlayers.Remove(networkPlayer);
    }

    private IEnumerator SpawnCar() {
        while (!SceneLoader.isLoaded) {
            yield return new WaitForEndOfFrame();
        }

        Network.Instantiate(GameManager.instance.prefab, Vector3.zero, Quaternion.identity, 0);

        // Tell everyone we're ready
        networkView.RPC("UpdateProfile", RPCMode.AllBuffered, Network.player, PlayerProfile.LoadedValueName, bool.TrueString);
    }

    // Change profile settings using this
    [RPC]
    public void UpdateProfile(NetworkPlayer player, string valueName, string value) {
        connectedPlayers[player].UpdateValue(valueName, value);
    }

    public PlayerProfile GetProfile(NetworkPlayer networkPlayer) {
        return connectedPlayers[networkPlayer];
    }

    public PlayerProfile GetMyProfile() {
        return connectedPlayers[Network.player];
    }

    public void Clear() {
        connectedPlayers.Clear();
    }
}
