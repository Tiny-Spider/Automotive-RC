using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles static methods or methonds on static instances
/// </summary>
public class MenuHandler : MonoBehaviour {
    public void SetName(InputField inputField) {
        // GameManager.instance.name = inputField.value;
        Lobby.instance.name = inputField.value;
    }

    public void Connect(InputField inputField) {
        NetworkManager.instance.Connect(inputField);
    }

    public void SetUseNat(bool useNAT) {
        NetworkManager.instance.useNAT = useNAT;
    }

    public void SetPort(InputField field) {
        NetworkManager.instance.SetPort(field);
    }

    public void SetMaxPlayers(InputField field) {
        NetworkManager.instance.SetMaxPlayers(field);
    }

    public void StartServer() {
        NetworkManager.instance.StartServer();
    }

    public void Disconnect() {
        NetworkManager.instance.Disconnect();
    }

    public void SendRPC(string name) {
        NetworkManager.instance.networkView.RPC(name, RPCMode.AllBuffered);
    }
}
