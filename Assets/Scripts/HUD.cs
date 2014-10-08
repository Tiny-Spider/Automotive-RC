using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
    public Text pingText;
	
	void Update () {
        if (Network.isClient)
            pingText.text = Network.GetAveragePing(Network.connections[0]) + " ms.";
	}
}
