using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    private FryingRecipeSO currentRecipeSO;
    //private float fryingTimer;
    /*
    private State state;
 
    private enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    /*
    private void Start() {
        StartCoroutine(HandleFryTimer());
    }

    private IEnumerator HandleFryTimer() {
        yield return new WaitForSeconds(1f);
    }
    */

    private void Update() {
        if (currentRecipeSO) {
            //fryingTimer += Time.deltaTime;
            bool checkRaw = GetKitchenObject().GetKitchenObjectSO().state == KitchenObjectSO.State.Raw;
            AddProgress(Time.deltaTime / currentRecipeSO.fryingTimerMax, !checkRaw);
            if (IsProgressComplete()) {
                //Fried
                ClearProgress();
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(currentRecipeSO.output, this);
                currentRecipeSO = GetRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                stoveCounterVisual.SetFrying(GetKitchenObject().GetState());
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
                stoveCounterVisual.SetFrying(GetKitchenObject().GetState());
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                ClearProgress();
                currentRecipeSO = null;
                GetKitchenObject().SetKitchenObjectParent(player);
                stoveCounterVisual.SetFrying(KitchenObjectSO.State.Default);
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
