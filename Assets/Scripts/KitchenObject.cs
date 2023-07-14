using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    private const string SPAWN = "Spawn";
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Animator animator;
    private IKitchenObjectParent kitchenObjectParent;
    private float progress = 0f;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (!this.kitchenObjectParent.HasKitchenObject()) {
            this.kitchenObjectParent.SetKitchenObject(this);
            transform.parent = this.kitchenObjectParent.GetKitchenObjectSpawnPoint();
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    public void Spawn() {
        animator.SetTrigger(SPAWN);
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        GameObject kitchenObjectSpawn = Instantiate(kitchenObjectSO.prefab, parent.GetKitchenObjectSpawnPoint().position, parent.GetKitchenObjectSpawnPoint().rotation);
        KitchenObject kitchenObject = kitchenObjectSpawn.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        return kitchenObject;
    }

    public float GetProgress() {
        return progress;
    }

    public float AddProgress(float n) {
        return progress += n;
    }
}
