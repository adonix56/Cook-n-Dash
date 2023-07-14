using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject selectedVisual;

    private void Start() {
        CharacterController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, CharacterController.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            selectedVisual.SetActive(true);
        } else {
            selectedVisual.SetActive(false);
        }
    }
}
