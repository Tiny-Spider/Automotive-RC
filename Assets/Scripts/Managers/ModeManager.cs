using UnityEngine;
using System.Collections;

public class ModeManager : Singleton<ModeManager> {
    public ModeData[] modes;

    public static ModeData GetCurrentMode() {
        return GetMode(Lobby.instance.mode);
    }

    public static ModeData GetMode(string name) {
        foreach (ModeData mode in ModeManager.instance.modes) {
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
        public string mode;
    }
}
