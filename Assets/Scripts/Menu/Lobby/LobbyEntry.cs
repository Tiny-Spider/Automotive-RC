using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyEntry : MonoBehaviour {
    public Text nameText;
    public Image carImage;
    public Button kickButton;
    public Button carButton;

    public NetworkPlayer player;

    void Awake() {
        // Can't kick yourself, dummy :P
        if (player == Network.player) {
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
