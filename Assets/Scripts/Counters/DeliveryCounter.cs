using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    public override void Interact(CharacterController player) {
        DestroyObject(player);
    }

    public override void InteractAlternate(CharacterController player) {
        DestroyObject(player);
    }

    private void DestroyObject(CharacterController player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject)) {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
