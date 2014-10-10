using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {
    public ModeData[] modes;

    [System.Serializable]
    public struct ModeData {
        public Texture2D image;
        public string name;
        public Mode mode;
    }
}
