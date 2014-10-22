using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackEntry : MonoBehaviour {
    public Image image;
    public Text description;

    private string trackName;

    public void SetTrack(TrackManager.TrackData track) {
        trackName = track.sceneName;

        image.sprite = track.image;
    }

    public void HidePanel() {
        FindObjectOfType<Menu>().HidePanel(FindObjectOfType<TrackDisplayer>().trackPanel);
    }

    public void ChooseTrack() {
        Lobby.instance.SetTrack(trackName);
    }
}
