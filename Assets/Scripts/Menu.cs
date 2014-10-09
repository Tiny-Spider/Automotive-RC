using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    public GameObject[] panels = new GameObject[0];

    public void HidePanels() {
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
    }

    public void ShowPanel(GameObject panel) {
        panel.SetActive(true);
    }

    public void ShowPanel(int panelID) {
        panels[panelID].SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }
}
