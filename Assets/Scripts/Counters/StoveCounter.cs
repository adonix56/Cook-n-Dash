using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public override void Interact(CharacterController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasFryingRecipeSO(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                ResetProgress(GetKitchenObject().GetProgress());
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                ClearProgress();
                GetKitchenObject().SetKitchenObjectParent(player);
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
