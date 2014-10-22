using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    private Menu menu;
    private HUD hud;

    public string menuScene = "Menu";

    public Flare[] lensFlares = new Flare[10];

	void Start () {
        OnLevelWasLoaded(Application.loadedLevel);
	}

    void OnLevelWasLoaded(int level) {
        Debug.Log("Starting in: " + Application.loadedLevelName);

        if (Application.loadedLevelName == menuScene) {
            menu = FindObjectOfType<Menu>();
            hud = null;
        } else {
            hud = FindObjectOfType<HUD>();
            menu = null;
        }
    }

    public HUD GetHUD() {
        return hud;
    }

    public Menu GetMenu() {
        return menu;
    }
}
