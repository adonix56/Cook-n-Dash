using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject sizzlingParticlesGameObject;
    [SerializeField] private GameObject stoveOnVisualGameObject;

    public void SetFrying(KitchenObjectSO.State state) {
        if (state == KitchenObjectSO.State.Raw || state == KitchenObjectSO.State.Cooked) {
            sizzlingParticlesGameObject.SetActive(true);
            stoveOnVisualGameObject.SetActive(true);
        } else {
            sizzlingParticlesGameObject.SetActive(false);
            stoveOnVisualGameObject.SetActive(false);
        }
    }
}
