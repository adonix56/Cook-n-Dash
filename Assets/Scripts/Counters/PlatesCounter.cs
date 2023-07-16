using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField, Range(0, 5)] private int platesCounterMax;
    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;
    [SerializeField] private PlatesCounterVisual platesCounterVisual;

    private int platesCount;

    private void Start() {
        platesCount = 0;
    }

    public override void Interact(CharacterController player) {
        if (!player.HasKitchenObject() && platesCount > 0) {
            platesCounterVisual.RemovePlate();
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);
            platesCount--;
        }
    }

    public override void InteractAlternateStart(CharacterController player) {
        if (platesCount < platesCounterMax)
            platesCounterVisual.SpawnPlate(platesCount++);
    }
}
