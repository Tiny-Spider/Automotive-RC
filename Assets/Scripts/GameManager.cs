using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager instance { private set; get; }
    private Menu menu;
    private Mode gameMode;
    public bool inGame = false;
    public List<GameObject> cars;
    private HUD hud;

    public string name = "Player";
    public string menuScene = "Menu";
    public string levelToLoad = "Test World";

    // TEMP
    public GameObject prefab;

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        gameMode = new CoinCollect();
    }

    public void SetName(InputField field) { 
        name = field.value; 
    }

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

	void Update () {
        if(inGame)
        gameMode.Update();
	}
    public HUD GetHUD() {
        return hud;
    }

    public Menu GetMenu() {
        return menu;
    }

    public void RegisterCar(GameObject car)
    {
        cars.Add(car);
        gameMode.OnRegisterCar(car);
    }
}
