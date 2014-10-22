using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerDisplayer : MonoBehaviour {
    public Text trackText;
    public Image trackImage;

    public Text modeText;
    public Image modeImage;

    string track = "none";
    string mode = "none";

    void Update() {
        Lobby lobby = Lobby.instance;

        if (!track.Equals(lobby.track)) {
            track = lobby.track;

            TrackManager.TrackData trackData = TrackManager.instance.GetTrack(track);

            if (trackData != null) {
                trackText.text = trackData.name;
                trackImage.sprite = trackData.image;
            }
            else {
                Debug.Log("Invalid track? " + track);
            }
        }

        if (!mode.Equals(lobby.mode)) {
            mode = lobby.mode;

            ModeManager.ModeData modeData = ModeManager.instance.GetMode(mode);

            if (modeData != null) {
                modeText.text = modeData.name;
                modeImage.sprite = modeData.image;
            }
            else {
                Debug.Log("Invalid mode? " + mode);
            }
        }
    }
}
