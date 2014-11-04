using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mode : MonoBehaviour {
    public float startTime;
    public float time;
    public List<NetworkPlayer> carList = new List<NetworkPlayer>();
    public List<PlayerProfile> playerList = new List<PlayerProfile>();

    public abstract void Awake() ;

    public abstract void Start() ;

    public abstract void Update();

    public abstract void InitializePlayers();

    IEnumerator Countdown() {
        int countdown = 4;

        while (countdown > 0) {
            countdown--;
            yield return new WaitForSeconds(1f);
        }

        EnableCarControllers(true);
    }

    public void EnableCarControllers(bool enable) {
        foreach (PlayerProfile playerProfile in Lobby.instance.connectedPlayers.Values) {

            Debug.Log(playerProfile.car.name);

            if (enable)
                playerProfile.car.GetComponent<CarController>().enabled = true;
            else
                playerProfile.car.GetComponent<CarController>().enabled = false;
            
        }
    }

    public IEnumerator PlayerCheck() {
        bool allLoaded = false;

        while (!allLoaded) {
            allLoaded = true;

            foreach (PlayerProfile profile in Lobby.instance.connectedPlayers.Values) {
                if (!profile.loaded) {
                    allLoaded = false;
                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        InitializePlayers();
        EnableCarControllers(true);
    }
}
