using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter = GetComponent<StoveCounter>();
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnFryingStateChanged += OnFryingStateChanged;
    }

    private void OnFryingStateChanged(object sender, StoveCounter.OnFryingStateChangedEventArgs e) {
        if (e.state == KitchenObjectSO.State.Raw || e.state == KitchenObjectSO.State.Cooked) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
