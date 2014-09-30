using UnityEngine;
using System.Collections;

public class SceneLoader {
    public static bool isLoaded = true;

    public static IEnumerator LoadLevelAsync(string level) {
        AsyncOperation async = Application.LoadLevelAsync(level);

        while (!async.isDone) {
            Debug.Log(async.progress);
            yield return 0;
        }
    }

    public static IEnumerator LoadLevel(string level) {
        Application.LoadLevel(level);
        isLoaded = false;

        while (Application.loadedLevelName != level) {
            yield return new WaitForEndOfFrame();
        }

        isLoaded = true;
    }
}
