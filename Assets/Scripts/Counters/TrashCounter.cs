using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyThrowAway;
    public override void Interact(PlayerController player) {
        DestroyObject(player);
    }

    public override void InteractAlternateStart(PlayerController player) {
        DestroyObject(player);
    }

    private void DestroyObject(PlayerController player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject)) {
                if (!plateKitchenObject.isEmpty()) {
                    OnAnyThrowAway?.Invoke(this, EventArgs.Empty);
                    player.GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(plateKitchenObject.GetKitchenObjectSO(), player);
                }
            } else {
                OnAnyThrowAway?.Invoke(this, EventArgs.Empty);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
