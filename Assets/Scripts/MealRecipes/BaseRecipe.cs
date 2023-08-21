using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class BaseRecipe : MonoBehaviour {
        public PlateKitchenObject plateKitchenObject;
        [SerializeField] public List<IngredientMatch> mealObjects;
    }
}

