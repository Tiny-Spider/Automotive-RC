using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ModeEntry : MonoBehaviour {
    public Image image;
    public Text description;

    private string modeName;

    public void SetMode(ModeManager.ModeData mode) {
        modeName = mode.name;

        string text = mode.name;
        text += Environment.NewLine + Environment.NewLine;
        text += mode.description;

        image.sprite = mode.image;
        description.text = text;
    }

    public void HidePanel() {
        FindObjectOfType<Menu>().HidePanel(FindObjectOfType<ModeDisplayer>().modePanel);
    }

    public void ChooseMode() {
        Lobby.instance.SetMode(modeName);
    }
}
