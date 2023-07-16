using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private float verticalOffset = 0.075f;
    private Transform currentPlate;

    public void SpawnPlate(int plateNumber) {
        currentPlate = Instantiate(plateVisualPrefab, objectSpawnPoint);
        currentPlate.Translate(Vector3.up * verticalOffset * plateNumber);
    }

    public void RemovePlate() {
        Transform plateToRemove = currentPlate;
        currentPlate = objectSpawnPoint.GetChild(objectSpawnPoint.childCount - 1);
        Destroy(currentPlate.gameObject);
    }
}
