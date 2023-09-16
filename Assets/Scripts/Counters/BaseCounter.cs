using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent 
{
    public static event EventHandler OnBuildMealOnPlayer;
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private ProgressBarUI progressBarUI;

    private KitchenObject kitchenObject;
    public virtual void Interact(PlayerController player) { }
    public virtual void InteractAlternate(PlayerController player) { }
    public virtual void InteractAlternateStart(PlayerController player) { }
    public virtual void InteractAlternateEnd(PlayerController player) { }

    public Transform GetKitchenObjectSpawnPoint() {
        return objectSpawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public bool IsProgressComplete() {
        if (HasKitchenObject())
            return kitchenObject.GetProgress() > 1f;
        return false;
    }

    public void AddProgress(float n, bool hide = false) {
        if (HasKitchenObject() && progressBarUI != null) {
            float progress = kitchenObject.AddProgress(n);
            if (!hide)
                progressBarUI.SetBar(progress);
        }
    }

    public void ClearProgress() {
        if (HasKitchenObject() && progressBarUI != null) {
            progressBarUI.ClearBar();
        }
    }

    public void ResetProgress(float n, bool hide = false) {
        if (HasKitchenObject() && progressBarUI != null && !hide) {
            ClearProgress();
            progressBarUI.SetBar(kitchenObject.GetProgress());
        }
    }

    public bool TryToBuildMeal(PlayerController player) {
        bool plateCounter = GetKitchenObject() is PlateKitchenObject;
        bool platePlayer = player.GetKitchenObject() is PlateKitchenObject;
        if (plateCounter && !platePlayer) { // Plate is on the Counter
            return BuildMeal(kitchenObject as PlateKitchenObject, player.GetKitchenObject());
        } else if (platePlayer && !plateCounter) { // Player is holding a plate
            if (BuildMeal(player.GetKitchenObject() as PlateKitchenObject, kitchenObject)) {
                OnBuildMealOnPlayer?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        return false;
    }

    private bool BuildMeal(PlateKitchenObject plateKitchenObject, KitchenObject ingredient) {
        KitchenObjectSO ingredientSO = ingredient.GetKitchenObjectSO();
        if (plateKitchenObject.CanAddIngredient(ingredientSO)) {
            plateKitchenObject.AddIngredient(ingredientSO);
            ingredient.DestroySelf();
            return true;
        }
        return false;
    }
}
