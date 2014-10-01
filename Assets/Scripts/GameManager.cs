using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    private Menu menu;

    public string name = "Player";
    public string menuScene = "Menu";

    // TEMP
    public GameObject prefab;

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
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
    }

	void Update () {
	
	}

    public Menu GetMenu() {
        return menu;
    }

    public static GameManager GetInstance() {
        return instance; // Maybe do a check and create instance on new GameObject
    }
}
