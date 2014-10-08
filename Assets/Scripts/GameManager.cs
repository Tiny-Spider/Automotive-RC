using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    private Menu menu;
    private Mode gameMode;
    public bool inGame = false;
    public List<GameObject> cars;

    public string name = "Player";
    public string menuScene = "Menu";
    public string sceneToLoad = "Demo";

    // TEMP
    public GameObject prefab;

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        gameMode = new TimeTrial();
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
        } else {
            menu = null;
        }
        if (Application.loadedLevelName == sceneToLoad)
        {
            inGame = true;
            gameMode.Awake();
            gameMode.Start();
        }
        else
        {
            inGame = false;
        }
    }

	void Update () {
        if(inGame)
        gameMode.Update();
	}

    public Menu GetMenu() {
        return menu;
    }

    public static GameManager GetInstance() {
        return instance; // Maybe do a check and create instance on new GameObject
    }

    public void RegisterCar(GameObject car)
    {
        cars.Add(car);
        gameMode.OnRegisterCar(car);
    }
}
