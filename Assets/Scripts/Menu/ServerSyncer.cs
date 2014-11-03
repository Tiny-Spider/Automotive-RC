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

        if (Network.isServer) {
            lobby.OnJoin += OnPlayersChanged;
            lobby.OnDisconnect += OnPlayersChanged;
        }

        lobby.OnUpdateServer += OnServerUpdate;
        lobby.OnUpdate += OnUpdate;
    }

    void OnDisable() {
        Lobby lobby = Lobby.instance;

        if (Network.isServer) {
            lobby.OnJoin -= OnPlayersChanged;
            lobby.OnDisconnect -= OnPlayersChanged;
        }

        lobby.OnUpdateServer -= OnServerUpdate;
        lobby.OnUpdate -= OnUpdate;
    }

    void OnPlayersChanged(NetworkPlayer player) {
        StartCoroutine(DelayedUpdate());
    }

    IEnumerator DelayedUpdate() {
        yield return new WaitForEndOfFrame();

        int i = 0;

        foreach (PlayerProfile playerProfile in Lobby.instance.GetProfiles()) {
            Lobby.instance.UpdateProfile(playerProfile.GetOwner(), PlayerProfile.START_POSITION, i++.ToString());
        }
    }

    void OnUpdate(NetworkPlayer player) {
        ReadyChecks();
    }

    void OnServerUpdate() {
        Lobby lobby = Lobby.instance;

        bool trackSelected = false;
        string track = lobby.track;
        TrackManager.TrackData trackData = TrackManager.instance.GetTrack(track);

        if (trackData != null) {
            trackText.text = trackData.name;
            trackImage.sprite = trackData.image;
            trackSelected = true;
        }
        else {
            Debug.Log("Invalid track: [" + track + "]");
        }

        bool modeSelected = false;
        string mode = lobby.mode;
        ModeManager.ModeData modeData = ModeManager.instance.GetMode(mode);

        if (modeData != null) {
            modeText.text = modeData.name;
            modeImage.sprite = modeData.image;
            modeSelected = true;
        }
        else {
            Debug.Log("Invalid mode: [" + mode + "]");
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
