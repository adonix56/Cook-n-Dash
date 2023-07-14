using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] fryingVisualObjects;

    public void SetFrying(bool frying) {
        foreach (GameObject fryingVisualObject in fryingVisualObjects) {
            fryingVisualObject.SetActive(frying);
        }
    }
}
