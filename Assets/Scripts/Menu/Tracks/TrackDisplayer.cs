using UnityEngine;
using System.Collections;

public class TrackDisplayer : MonoBehaviour {
    public GameObject trackEntryPrefab;
    public string trackPanel;

    void Start() {
        foreach (TrackManager.TrackData track in TrackManager.instance.tracks) {
            GameObject entryGameObject = Instantiate(trackEntryPrefab) as GameObject;
            TrackEntry entry = entryGameObject.GetComponent<TrackEntry>();

            entry.SetTrack(track);
            entry.transform.SetParent(transform);
        }
    }
}
