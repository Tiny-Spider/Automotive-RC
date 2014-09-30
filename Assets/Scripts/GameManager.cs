using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public string name = "Player";
    public string menuScene = "Menu";
    public GameObject prefab;
    private Menu menu;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start () {
        OnLevelWasLoaded(0);
        instance = this;
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
