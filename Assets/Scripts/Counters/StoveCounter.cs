using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    private FryingRecipeSO currentRecipeSO;
    private float fryingTimer;
    /*
    private State state;
 
    private enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private void Start() {
        StartCoroutine(HandleFryTimer());
    }

    private IEnumerator HandleFryTimer() {
        yield return new WaitForSeconds(1f);
    }
    */

    private void Update() {
        if (currentRecipeSO) {
            fryingTimer += Time.deltaTime;
            if (fryingTimer > currentRecipeSO.fryingTimerMax) {
                //Fried
                fryingTimer = 0f;
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(currentRecipeSO.output, this);
                currentRecipeSO = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                if (!currentRecipeSO) {
                    stoveCounterVisual.SetFrying(false);
                }
            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(CharacterController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasFryingRecipeSO(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                stoveCounterVisual.SetFrying(true);
                player.GetKitchenObject().SetKitchenObjectParent(this);
                ResetProgress(GetKitchenObject().GetProgress());
                currentRecipeSO = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                stoveCounterVisual.SetFrying(false);
                ClearProgress();
                currentRecipeSO = null;
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
