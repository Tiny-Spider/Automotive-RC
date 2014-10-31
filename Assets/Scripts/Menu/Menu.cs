using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    public Panel[] panels = new Panel[0];

    public void HidePanels() {
        foreach (Panel panel in panels) {
            panel.gameObject.SetActive(false);
        }
    }

    public void HidePanel(string panelName) {
        foreach (Panel panel in panels) {
            if (panel.name.Equals(panelName))
                panel.gameObject.SetActive(false);
        }
    }

    public void ShowPanel(string panelName) {
        foreach (Panel panel in panels) {
            if (panel.name.Equals(panelName))
                panel.gameObject.SetActive(true);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    [System.Serializable]
    public struct Panel {
        public string name;
        public GameObject gameObject;
    }
}
