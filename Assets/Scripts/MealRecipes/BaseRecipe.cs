using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class BaseRecipe : MonoBehaviour {
        protected PlateKitchenObject plateKitchenObject;
        [SerializeField] public List<IngredientMatch> mealObjects;

        protected virtual void Start() {
            plateKitchenObject = transform.parent.GetComponent<PlateKitchenObject>();
        }

        public string GetIngredientQtyList() {
            string ret = "";
            foreach (IngredientMatch ingredientMatch in mealObjects) {
                ret += ingredientMatch.count;
            }
            return ret;
        }
    }
}

