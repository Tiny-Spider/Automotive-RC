using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Lobby))]
public class NetworkManager : MonoBehaviour {
    public GameManager gameManager;
    public Lobby lobby;

    public bool useNAT = false;
    public string port = "";
    public string maxPlayers = "";

    public void SetUseNat(bool useNAT) { this.useNAT = useNAT; }
    public void SetPort(InputField field) { this.port = field.value; }
    public void SetMaxPlayers(InputField field) { this.maxPlayers = field.value; }

    public void StartServer() {
        try {
            int port = int.Parse(this.port);
            int maxPlayers = int.Parse(this.maxPlayers);

            Network.InitializeServer(maxPlayers, port, useNAT);
            MasterServer.RegisterHost("AutoMotiveRC", "Game1", "Open Game");
        }
        catch (Exception ex) {
            Debug.LogError("Error: " + ex); 
        }
    }

    public void Connect(InputField field) {
        try {
            string[] ip = field.value.Split(':');
            Network.Connect(ip[0], int.Parse(ip[1]));
        }
        catch (Exception ex) {
            Debug.LogError("Error: " + ex); 
        }
    }

    public void SendRPC(string name) {
        networkView.RPC(name, RPCMode.All);
    }

    // Client Events
    void OnConnectedToServer() {
        networkView.RPC("AddPlayer", RPCMode.AllBuffered, gameManager.name);
        gameManager.GetMenu().ShowLobby();
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) {
        if (Network.isServer)
            Debug.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                Debug.Log("Lost connection to the server");
            else
                Debug.Log("Successfully diconnected from the server");

        Application.LoadLevel(gameManager.menuScene);
    }

    void OnFailedToConnect(NetworkConnectionError error) {
        Debug.Log(error);
    }

    // Server Events
    void OnServerInitialized() {
        networkView.RPC("AddPlayer", RPCMode.AllBuffered, gameManager.name);
        gameManager.GetMenu().ShowLobby();
    }

    // Unused since we use "OnConnectedToServer" for the name
    void OnPlayerConnected(NetworkPlayer player) {

    }

    void OnPlayerDisconnected(NetworkPlayer player) {
        networkView.RPC("DisconnectPlayer", RPCMode.AllBuffered, player);
    }
}
