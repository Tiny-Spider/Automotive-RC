using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LobbyDisplay : MonoBehaviour {
    public GameObject playerEntryPrefab;
    public GameObject lobbyPanel;
    public string nameText;

    public GridLayoutGroup grid;

    public Dictionary<NetworkPlayer, PlayerEntry> playerEntries = new Dictionary<NetworkPlayer, PlayerEntry>();

    private Lobby lobby;

    void OnEnable() {
        lobby = FindObjectOfType<Lobby>();

        lobby.OnJoin += OnPlayerConnect;
        lobby.OnDisconnect += OnPlayerDisconnect;
    }

    void OnDisable() {
        lobby.OnJoin -= OnPlayerConnect;
        lobby.OnDisconnect -= OnPlayerDisconnect;
    }

    void OnPlayerConnect(NetworkPlayer player, string name) {
        GameObject playerEntryGameobject = Instantiate(playerEntryPrefab) as GameObject;
        PlayerEntry playerEntry = playerEntryGameobject.GetComponent<PlayerEntry>();

        playerEntry.SetName(name);
        playerEntry.player = player;

        playerEntry.transform.SetParent(grid.transform);
        playerEntries.Add(player, playerEntry);
    }

    void OnPlayerDisconnect(NetworkPlayer player, string name) {
        Destroy(playerEntries[player].gameObject);
        playerEntries.Remove(player);

        //foreach (KeyValuePair<NetworkPlayer, PlayerEntry> entry in playerEntries) {
        //    Destroy(entry.Value.gameObject);

        //    GameObject playerEntryGameobject = Instantiate(playerEntryPrefab) as GameObject;
        //    PlayerEntry playerEntry = playerEntryGameobject.GetComponent<PlayerEntry>();

        //    playerEntry.SetName(name);
        //    playerEntry.player = player;

        //    playerEntry.transform.SetParent(grid.transform);
        //    playerEntries.Add(entry.Key, playerEntry);
        //}
    }
}
