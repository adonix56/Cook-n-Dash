using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour 
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;

    private void Start() {
        containerCounter.OnKitchenObjectSpawn += Anim_OnKitchenObjectSpawn;
    }

    private void Anim_OnKitchenObjectSpawn(object sender, ContainerCounter.KitchenObjectSpawnEventArgs e) {
        if (e.kitchenObjectArg != null) {
            e.kitchenObjectArg.Spawn();
        }
        animator.SetTrigger(OPEN_CLOSE);
    }
}
