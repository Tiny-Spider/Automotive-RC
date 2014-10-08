using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LobbyDisplay : MonoBehaviour {
    public Dictionary<NetworkPlayer, LobbyEntry> lobbyEntries = new Dictionary<NetworkPlayer, LobbyEntry>();

    public GameObject playerEntryPrefab;
    public GameObject lobbyPanel;
    public GridLayoutGroup grid;
    public string nameText;

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

    void OnPlayerConnect(PlayerProfile profile) {
        GameObject entryGameobject = Instantiate(playerEntryPrefab) as GameObject;
        LobbyEntry lobbyEntry = entryGameobject.GetComponent<LobbyEntry>();
        lobbyEntry.transform.SetParent(grid.transform);

        NetworkPlayer player = profile.GetOwner();

        lobbyEntry.SetName(profile.name);
        lobbyEntry.player = player;

        lobbyEntries.Add(player, lobbyEntry);
    }

    void OnPlayerDisconnect(PlayerProfile profile) {
        NetworkPlayer player = profile.GetOwner();

        Destroy(lobbyEntries[player].gameObject);
        lobbyEntries.Remove(player);
    }
}
