using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
    private Menu menu;
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

            ModeManager.ModeData mode = ModeManager.GetCurrentMode();

            if (mode != null) {
                Component component = gameObject.GetComponent(mode.mode);

                if (component)
                    Destroy(component);
            }
        } else {
            hud = FindObjectOfType<HUD>();
            gameObject.AddComponent(ModeManager.GetCurrentMode().mode);

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
