using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject sizzlingParticlesGameObject;
    [SerializeField] private GameObject stoveOnVisualGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnFryingStateChanged += SetFrying;
    }

    public void SetFrying(object sender, StoveCounter.OnFryingStateChangedEventArgs e) {
        if (e.state == KitchenObjectSO.State.Raw || e.state == KitchenObjectSO.State.Cooked) {
            sizzlingParticlesGameObject.SetActive(true);
            stoveOnVisualGameObject.SetActive(true);
        } else {
            sizzlingParticlesGameObject.SetActive(false);
            stoveOnVisualGameObject.SetActive(false);
        }
    }
}
