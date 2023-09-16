using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public override void Interact(PlayerController player) {
        DestroyObject(player);
    }

    public override void InteractAlternate(PlayerController player) {
        DestroyObject(player);
    }

    private void DestroyObject(PlayerController player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject)) {
                if (DeliveryManager.Instance.DeliverMeal(plateKitchenObject)) {
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}
