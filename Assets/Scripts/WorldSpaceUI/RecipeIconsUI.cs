using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class RecipeIconsUI : MonoBehaviour {
        [SerializeField] private GameObject IconTemplate;
        [SerializeField] private PlateKitchenObject plateKitchenObject;

        private void Start() {
            plateKitchenObject.OnIngredientAdded += UpdateIcons;
        }

        private void Update() {
            transform.LookAt(Camera.main.transform);
        }

        private void UpdateIcons(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            RecipeIconSingleUI icon = Instantiate(IconTemplate, transform).GetComponent<RecipeIconSingleUI>();
            icon.SetIconFromKitchenObjectSO(e.ingredient);
        }
    }
}
