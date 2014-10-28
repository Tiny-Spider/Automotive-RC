using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyEntry : MonoBehaviour {
    public Text nameText;
    public Image carImage;
    public Button kickButton;
    public Button carButton;

    public NetworkPlayer player;

    public void SetPlayer(NetworkPlayer player) {
        this.player = player;

        // Can't kick yourself, dummy :P
        Debug.Log("Awake");
        Debug.Log(player.ToString() + " - " + Network.player.ToString());

        if (player.Equals(Network.player)) {
            Debug.Log("IZ MAH OWN");
            kickButton.gameObject.SetActive(false);
            carButton.gameObject.SetActive(true);
        }
    }

    public void UpdateEntry() {
        PlayerProfile playerProfile = Lobby.instance.GetProfile(player);

        nameText.text = playerProfile.name;

        CarManager.CarData carData = CarManager.instance.GetCar(playerProfile.selectedCar);
        carImage.sprite = carData.image;
    }

    public void OpenCarSelect() {
        FindObjectOfType<Menu>().ShowPanel(FindObjectOfType<LobbyDisplayer>().carPanel);
    }

    public void Kick() {
        Network.CloseConnection(player, true);
    }
}
