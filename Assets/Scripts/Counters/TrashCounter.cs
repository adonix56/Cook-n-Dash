using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    public override void Interact(CharacterController player) {
        DestroyObject(player);
    }

    public override void InteractAlternateStart(CharacterController player) {
        DestroyObject(player);
    }

    private void DestroyObject(CharacterController player) {
        if (player.HasKitchenObject()) {
            if (player.GetKitchenObject().GetComponent<PlateKitchenObject>() != null) {
                player.GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            } else {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
