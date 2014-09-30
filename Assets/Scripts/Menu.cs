using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    public GameObject clientPanel;
    public GameObject serverPanel;
    public GameObject lobbyPanel;

    public void ShowLobby() {
        clientPanel.SetActive(false);
        serverPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void ShowMenu() {
        clientPanel.SetActive(true);
        serverPanel.SetActive(true);
        lobbyPanel.SetActive(false);
    }
}
