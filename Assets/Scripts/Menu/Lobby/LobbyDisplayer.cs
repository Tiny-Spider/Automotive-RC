using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class LobbyDisplayer : MonoBehaviour {
    public Dictionary<NetworkPlayer, LobbyEntry> lobbyEntries = new Dictionary<NetworkPlayer, LobbyEntry>();

    public GameObject playerEntryPrefab;
    public string carPanel;

    void OnEnable() {
        Lobby lobby = Lobby.instance;

        lobby.OnJoin += OnPlayerConnect;
        lobby.OnDisconnect += OnPlayerDisconnect;
        lobby.OnUpdate += OnUpdate;
    }

    void OnDisable() {
        Lobby lobby = Lobby.instance;

        lobby.OnJoin -= OnPlayerConnect;
        lobby.OnDisconnect -= OnPlayerDisconnect;
        lobby.OnUpdate -= OnUpdate;
    }

    void OnPlayerConnect(NetworkPlayer player) {
        GameObject entryGameObject = Instantiate(playerEntryPrefab) as GameObject;
        LobbyEntry lobbyEntry = entryGameObject.GetComponent<LobbyEntry>();
        lobbyEntry.transform.SetParent(transform);

        lobbyEntry.SetPlayer(player);
        lobbyEntry.UpdateEntry();

        lobbyEntries.Add(player, lobbyEntry);
    }

    void OnPlayerDisconnect(NetworkPlayer player) {
        Destroy(lobbyEntries[player].gameObject);
        lobbyEntries.Remove(player);
    }

    void OnUpdate(NetworkPlayer player) {
        lobbyEntries[player].UpdateEntry();
    }
}
