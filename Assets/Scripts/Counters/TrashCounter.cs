using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(CharacterController player) {
        DestroyObject(player);
    }

    public override void InteractAlternateStart(CharacterController player) {
        DestroyObject(player);
    }

    private void DestroyObject(CharacterController player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
