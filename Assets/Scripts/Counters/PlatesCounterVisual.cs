using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;

    private float verticalOffset = 0.075f;
    private Transform currentPlate;

    private void Start() {
        platesCounter.OnPlatesSpawned += SpawnPlate;
        platesCounter.OnPlatesRemoved += RemovePlate;
    }

    public void SpawnPlate(object sender, PlatesCounter.PlatesSpawnEventArgs e) {
        currentPlate = Instantiate(plateVisualPrefab, objectSpawnPoint);
        currentPlate.Translate(Vector3.up * verticalOffset * e.plateNumber);
    }

    public void RemovePlate(object sender, System.EventArgs e) {
        Transform plateToRemove = currentPlate;
        currentPlate = objectSpawnPoint.GetChild(objectSpawnPoint.childCount - 1);
        Destroy(currentPlate.gameObject);
    }
}
