using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TrackEntry : MonoBehaviour {
    public Image image;
    public Text description;

    private string trackName;

    public void SetTrack(TrackManager.TrackData track) {
        trackName = track.name;

        image.sprite = track.image;

        string text = track.name;
        text += Environment.NewLine + Environment.NewLine;
        text += track.description;

        image.sprite = track.image;
        description.text = text;
    }

    public void HidePanel() {
        FindObjectOfType<Menu>().HidePanel(FindObjectOfType<TrackDisplayer>().trackPanel);
    }

    public void ChooseTrack() {
        Lobby.instance.SetTrack(trackName);
    }
}
