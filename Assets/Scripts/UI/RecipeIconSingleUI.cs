using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Recipe {
    public class RecipeIconSingleUI : MonoBehaviour {
        [SerializeField] private Image icon;
        
        public void SetIconFromKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
            icon.sprite = kitchenObjectSO.sprite;
        }
    }
}
