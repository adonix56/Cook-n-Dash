using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler<PlatesSpawnEventArgs> OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;

    public class PlatesSpawnEventArgs : EventArgs {
        public int plateNumber;
    }

    [SerializeField, Range(0, 5)] private int platesCounterMax;
    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;
    [SerializeField] private PlatesCounterVisual platesCounterVisual;

    private int platesCount;

    private void Start() {
        platesCount = 0;
    }

    public override void Interact(PlayerController player) {
        if (!player.HasKitchenObject() && platesCount > 0) {
            OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);
            platesCount--;
        }
    }

    public override void InteractAlternateStart(PlayerController player) {
        if (platesCount < platesCounterMax) {
            OnPlatesSpawned?.Invoke(this, new PlatesSpawnEventArgs {
                plateNumber = platesCount
            });
            platesCount++;
        }
    }
}
