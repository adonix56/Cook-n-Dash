using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public interface IMealRecipe {
        public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO);
        public void AddIngredient(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e);
        public BaseRecipe GetBaseRecipe();
    }
}
