using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDelivered;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void OnGameStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            gameObject.SetActive(true);
            recipesDelivered.text = DeliveryManager.Instance.GetSuccessfulRecipe().ToString();
        } else {
            gameObject.SetActive(false);
        }
    }
}
