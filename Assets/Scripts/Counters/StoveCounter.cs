using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnFryingStateChangedEventArgs> OnFryingStateChanged;

    public class OnFryingStateChangedEventArgs : EventArgs {
        public KitchenObjectSO.State state;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    
    private FryingRecipeSO currentRecipeSO;

    private void Update() {
        if (currentRecipeSO) {
            bool checkRaw = GetKitchenObject().GetKitchenObjectSO().state == KitchenObjectSO.State.Raw;
            AddProgress(Time.deltaTime / currentRecipeSO.fryingTimerMax, !checkRaw);
            if (IsProgressComplete()) {
                ClearProgress();
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(currentRecipeSO.output, this);
                currentRecipeSO = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                OnFryingStateChanged?.Invoke(this, new OnFryingStateChangedEventArgs {
                    state = GetKitchenObject().GetState()
                });
            }
        }
    }

    public override void Interact(CharacterController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasFryingRecipeSO(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                bool checkRaw = GetKitchenObject().GetKitchenObjectSO().state == KitchenObjectSO.State.Raw;
                ResetProgress(GetKitchenObject().GetProgress(), !checkRaw);
                currentRecipeSO = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                OnFryingStateChanged?.Invoke(this, new OnFryingStateChangedEventArgs {
                    state = GetKitchenObject().GetState()
                });
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                ClearProgress();
                currentRecipeSO = null;
                GetKitchenObject().SetKitchenObjectParent(player);
                OnFryingStateChanged?.Invoke(this, new OnFryingStateChangedEventArgs {
                    state = KitchenObjectSO.State.Default
                });
            }
        }
    }

    private bool HasFryingRecipeSO(KitchenObjectSO input) {
        return GetRecipeSOFromInput(input) != null;
    }

    private FryingRecipeSO GetRecipeSOFromInput(KitchenObjectSO input) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == input) {
                return fryingRecipeSO;
            }
        }
        return null;
    }
}
