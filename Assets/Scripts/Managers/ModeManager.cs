using UnityEngine;
using System.Collections;

public class ModeManager : Singleton<ModeManager> {
    public ModeData[] modes;

    public ModeData GetMode(string name) {
        foreach (ModeData mode in modes) {
            if (mode.name.Equals(name)) {
                return mode;
            }
        }

        return null;
    }

    [System.Serializable]
    public class ModeData {
        public Sprite image;
        public string name;
        public string description;
        public Mode mode;
    }
}
