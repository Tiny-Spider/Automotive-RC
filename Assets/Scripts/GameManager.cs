using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance { private set; get; }
    private Menu menu;
    private HUD hud;

    public string name = "Player";
    public string menuScene = "Menu";
    public string levelToLoad = "Test World";

    public Flare[] lensFlares = new Flare[10];

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

    public HUD GetHUD() {
        return hud;
    }

    public Menu GetMenu() {
        return menu;
    }
}
