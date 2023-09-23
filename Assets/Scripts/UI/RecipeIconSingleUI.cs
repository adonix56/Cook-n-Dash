using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Recipe {
    public class RecipeIconSingleUI : MonoBehaviour {
        [SerializeField] private Image icon;

        private bool fade = false;
        private float fadeStep = 1f;

        private void Update() {
            if (fade) {
                Color color = icon.color;
                color.a -= Time.deltaTime / fadeStep;
                icon.color = color;
            }
        }

        public void SetIconFromKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
            icon.sprite = kitchenObjectSO.sprite;
        }

        public void Fade(float fadeDuration) {
            fadeStep = fadeDuration;
            fade = true;
        }
    }
}
