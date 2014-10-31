using UnityEngine;
using System.Collections;

public class ModeDisplayer : MonoBehaviour {
    public GameObject modeEntryPrefab;
    public string modePanel;

    void Start() {
        foreach (ModeManager.ModeData mode in ModeManager.instance.modes) {
            GameObject entryGameObject = Instantiate(modeEntryPrefab) as GameObject;
            ModeEntry entry = entryGameObject.GetComponent<ModeEntry>();

            entry.SetMode(mode);
            entry.transform.SetParent(transform);
        }
    }
}
