using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class BaseRecipe : MonoBehaviour {
        protected PlateKitchenObject plateKitchenObject;
        [SerializeField] public List<IngredientMatch> mealObjects;

        protected virtual void Start() {
            Debug.Log("Adding plateKitchenObject");
            plateKitchenObject = transform.parent.GetComponent<PlateKitchenObject>();
        }
    }
}

