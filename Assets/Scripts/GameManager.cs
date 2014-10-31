using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
    private Menu menu;
    private Mode gameMode;
    public bool inGame = false;
    public List<GameObject> cars;
    private HUD hud;

    public string menuScene = "Menu";

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
            gameMode = gameObject.AddComponent<TimeTrial>();
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
