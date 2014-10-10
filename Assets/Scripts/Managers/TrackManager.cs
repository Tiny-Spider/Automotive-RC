using UnityEngine;
using System.Collections;

public class TrackManager : MonoBehaviour {
    public TrackData[] tracks;

    [System.Serializable]
    public struct TrackData {
        public Texture2D image;
        public string name;
        public string sceneName;
    }
}
