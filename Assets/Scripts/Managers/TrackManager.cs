using UnityEngine;
using System.Collections;

public class TrackManager : Singleton<TrackManager> {
    public TrackData[] tracks;

    public static TrackData GetCurrentTrack() {
        return GetTrack(Lobby.instance.track);
    }

    public static TrackData GetTrack(string name) {
        foreach (TrackData track in TrackManager.instance.tracks) {
            if (track.name.Equals(name)) {
                return track;
            }
        }

        return null;
    }

    [System.Serializable]
    public class TrackData {
        public Sprite image;
        public string name;
        public string description;
        public string sceneName;
    }
}
