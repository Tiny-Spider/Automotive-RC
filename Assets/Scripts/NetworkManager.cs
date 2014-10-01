﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Lobby))]
public class NetworkManager : MonoBehaviour {
    public GameManager gameManager;
    public Lobby lobby;

    public int lobbyPanelID = 1;

    public bool useNAT = false;
    public int port = 0;
    public int maxPlayers = 32;

    public void SetUseNat(bool useNAT) { this.useNAT = useNAT; }

    public void SetPort(InputField field) {
        if (!int.TryParse(field.value, out port)) {
            field.value = port.ToString();
        }
    }

    public void SetMaxPlayers(InputField field) {
        if (!int.TryParse(field.value, out maxPlayers)) {
            field.value = port.ToString();
        }
    }

    // ================================================================================= //

    public void StartServer() {
        NetworkConnectionError error = Network.InitializeServer(maxPlayers, port, useNAT);

        if (error == NetworkConnectionError.NoError)
            MasterServer.RegisterHost("AutoMotiveRC", "Game1", "Open Game");
        else
            Debug.LogError("Error on network initializing: " + error.ToString());
    }

    public void Connect(InputField field) {
        try {
            string[] ip = field.value.Split(':');
            Network.Connect(ip[0], int.Parse(ip[1]));
        }
        catch (Exception ex) {
            field.value = ex.Message;
            Debug.LogError("Error: " + ex); 
        }
    }

    public void Disconnect() {
        Network.Disconnect();
        lobby.Clear();

        MasterServer.UnregisterHost();
    }

    public void SendRPC(string name) {
        networkView.RPC(name, RPCMode.All);
    }

    // Client Events
    void OnConnectedToServer() {
        networkView.RPC("AddPlayer", RPCMode.AllBuffered, gameManager.name);

        Menu menu = gameManager.GetMenu();
        menu.HidePanels();
        menu.ShowPanel(lobbyPanelID);
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

        Menu menu = gameManager.GetMenu();
        menu.HidePanels();
        menu.ShowPanel(lobbyPanelID);
    }

    // Unused since we use "OnConnectedToServer" for the name
    void OnPlayerConnected(NetworkPlayer player) {

    }

    void OnPlayerDisconnected(NetworkPlayer player) {
        networkView.RPC("DisconnectPlayer", RPCMode.AllBuffered, player);
    }
}
