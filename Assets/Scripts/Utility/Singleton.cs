using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    public static T instance { private set; get; }

    void Awake() {
        if (!instance) {
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
