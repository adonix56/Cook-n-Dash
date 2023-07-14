using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent 
{
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private ProgressBarUI progressBarUI;

    private KitchenObject kitchenObject;
    public virtual void Interact(CharacterController player) { }
    public virtual void InteractAlternate(CharacterController player) { }
    public virtual void InteractAlternateStart(CharacterController player) { }
    public virtual void InteractAlternateEnd(CharacterController player) { }

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
}
