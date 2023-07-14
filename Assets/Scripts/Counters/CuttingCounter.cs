using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    [SerializeField] private CuttingCounterVisual counterVisual;
    private CuttingRecipeSO currentRecipe;
    private bool isCutting = false;

    public override void Interact(CharacterController player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                currentRecipe = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                if (currentRecipe)
                    ResetProgress(GetKitchenObject().GetProgress());
            }
        } else {
            if (!player.HasKitchenObject()) {
                ClearProgress();
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternateStart(CharacterController player) {
        if (HasKitchenObject()) {
            if (currentRecipe != null)
                CutKitchenObject();
            /*foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
                if (kitchenObject.GetKitchenObjectSO() == cuttingRecipeSO.input) {
                    currentRecipe = cuttingRecipeSO;
                    CutKitchenObject();
                    //kitchenObject.DestroySelf();
                    //kitchenObject = KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                }
            }*/
        }
    }

    private CuttingRecipeSO GetRecipeSOFromInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    public override void InteractAlternateEnd(CharacterController player) {
        if (isCutting) {
            StopCutKitchenObject();
        }
    }

    private void Update() {
        if (isCutting) {
            if (IsProgressComplete()) {
                StopCutKitchenObject();
                ClearProgress();
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(currentRecipe.output, this);
            } else {
                AddProgress(currentRecipe.cuttingSpeed * Time.deltaTime);
            }
        }
    }

    private void CutKitchenObject() {
        isCutting = true;
        counterVisual.PlayCuttingAnimation();
    }

    private void StopCutKitchenObject() {
        isCutting = false;
        counterVisual.StopCuttingAnimation();
    }
}
