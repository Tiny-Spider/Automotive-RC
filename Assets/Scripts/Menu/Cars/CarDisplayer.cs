using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(GridLayoutGroup))]
public class CarDisplayer : MonoBehaviour {
    public GameObject carEntryPrefab;
    public string carPanel;

	void Start () {
        foreach (CarManager.CarData car in CarManager.instance.cars) {
            GameObject entryGameObject = Instantiate(carEntryPrefab) as GameObject;
            CarEntry entry = entryGameObject.GetComponent<CarEntry>();

            entry.SetCar(car);
            entry.transform.SetParent(transform);
        }
	}
}
