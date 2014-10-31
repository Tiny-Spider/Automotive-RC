using UnityEngine;
using System.Collections;

public class TrackManager : Singleton<TrackManager> {
    public TrackData[] tracks;

    public TrackData GetTrack(string name) {
        foreach (TrackData track in tracks) {
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
