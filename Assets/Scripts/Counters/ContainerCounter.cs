using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler<KitchenObjectSpawnEventArgs> OnKitchenObjectSpawn;

    public class KitchenObjectSpawnEventArgs : EventArgs {
        public KitchenObject kitchenObjectArg;
    }

    public override void Interact(PlayerController player) {
        base.Interact(player);
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                //animator.SetTrigger(OPEN_CLOSE);
                //GameObject kitchenObjectSpawn = Instantiate(kitchenObjectSO.prefab, GetKitchenObjectSpawnPoint().position, GetKitchenObjectSpawnPoint().rotation);
                //KitchenObject kitchenObject = kitchenObjectSpawn.GetComponent<KitchenObject>();
                //kitchenObject.SetKitchenObjectParent(this);
                KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
                OnKitchenObjectSpawn?.Invoke(this, new KitchenObjectSpawnEventArgs {
                    kitchenObjectArg = kitchenObject
                });
            }
        } else if (!player.HasKitchenObject()) {
            GetKitchenObject().SetKitchenObjectParent(player);
        } else { // Player and Counter has something
            TryToBuildMeal(player);
        }
    }
}
