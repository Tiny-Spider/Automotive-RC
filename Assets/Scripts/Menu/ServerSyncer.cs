using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerSyncer : MonoBehaviour {
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

        bool trackSelected = false;
        TrackManager.TrackData trackData = TrackManager.GetCurrentTrack();

        if (trackData != null) {
            trackText.text = trackData.name;
            trackImage.sprite = trackData.image;
            trackSelected = true;
        }
        else {
            Debug.Log("Invalid track: [" + Lobby.instance.track + "]");
        }

        bool modeSelected = false;
        ModeManager.ModeData modeData = ModeManager.GetCurrentMode();

        if (modeData != null) {
            modeText.text = modeData.name;
            modeImage.sprite = modeData.image;
            modeSelected = true;
        }
        else {
            Debug.Log("Invalid mode: [" + Lobby.instance.mode + "]");
        }

        if (trackSelected && modeSelected && lobby.GetMyProfile().selectedCar != null) {
            lobby.UpdateProfile(Network.player, PlayerProfile.READY, bool.TrueString);
        }  

        ReadyChecks();
    }

    void ReadyChecks() {
        foreach (PlayerProfile playerProfile in Lobby.instance.GetProfiles()) {
            if (!playerProfile.ready) {
                return;
            }
        }

        string track = Lobby.instance.track;
        string mode = Lobby.instance.mode;

        Debug.Log("Track & Mode (Track: [" + track + "] Mode: [" + mode + "])");

        if (!track.Equals("") && !mode.Equals("")) {
            serverButton.interactable = true;
        }
    }
}
