using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerDisplayer : MonoBehaviour {
    public Text trackText;
    public Image trackImage;

    public Text modeText;
    public Image modeImage;

    public Button serverButton;

    void OnEnable() {
        Lobby lobby = Lobby.instance;

        lobby.OnUpdateServer += OnServerUpdate;
        lobby.OnUpdate += OnUpdate;
    }

    void OnDisable() {
        Lobby lobby = Lobby.instance;

        lobby.OnUpdateServer -= OnServerUpdate;
        lobby.OnUpdate -= OnUpdate;
    }

    void OnUpdate(NetworkPlayer player) {
        ReadyChecks();
    }

    void OnServerUpdate() {
        Lobby lobby = Lobby.instance;

        string track = Lobby.instance.track;
        TrackManager.TrackData trackData = TrackManager.instance.GetTrack(track);

        if (trackData != null) {
            trackText.text = trackData.name;
            trackImage.sprite = trackData.image;
        }
        else {
            Debug.Log("Invalid track? " + track);
        }

        string mode = Lobby.instance.mode;
        ModeManager.ModeData modeData = ModeManager.instance.GetMode(mode);

        if (modeData != null) {
            modeText.text = modeData.name;
            modeImage.sprite = modeData.image;
        }
        else {
            Debug.Log("Invalid mode? " + mode);
        }

        ReadyChecks();
    }

    void ReadyChecks() {
        bool allReady = true;

        foreach (PlayerProfile playerProfile in Lobby.instance.GetProfiles()) {
            if (playerProfile.selectedCar == null) {
                allReady = false;
                break;
            }
        }

        Debug.Log("All Ready? " + allReady);

        if (allReady) {
            string track = Lobby.instance.track;
            string mode = Lobby.instance.mode;
            Debug.Log("Track & Mode [Track: " + track + " Mode: " + mode + "]");

            if (!track.Equals("") && !mode.Equals("")) {
                serverButton.interactable = true;
            }
        }
    }
}
