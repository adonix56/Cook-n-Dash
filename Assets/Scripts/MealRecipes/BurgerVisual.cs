using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class BurgerVisual : BaseRecipe, IMealRecipe {
        public enum State {
            NoMeatNoBread,
            SingleNoBread,
            DoubleNoBread,
            NoMeat,
            Single,
            Double
        }
        /// <summary>
        ///  
        /// No Bread No Meat
        /// cheese  0.047
        /// lettuce 0.062
        /// tomatoes0.090
        /// 
        /// No Bread
        /// Meat Single     0.021
        /// CLT            +0.094
        /// Meat Double     0.150
        /// CLT            +0.131
        /// 
        /// No Meat
        /// Bread Top   0.298
        /// CLT        +0.165
        /// 
        /// Full Burger
        /// Bread Top      +0.084
        /// Meat Single    +0.187
        /// CLT            +0.023 +NoBread +NoMeat
        /// Bread Top Doub +0.127 +Single +NoMeat
        /// CLT            +0.049
        /// 
        /// </summary>

        [SerializeField] private State state;
        [SerializeField] private List<KitchenObjectSO_GameObject> burgerChoices;

        private int burgerCount = 0;
        private int burgerMax = 2;

        private void Start() {
            plateKitchenObject.OnIngredientAdded += IngredientAdded;
        }

        public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            if (TryIngredientMatch(e.ingredient, out int ingredientIndex, out bool isBurger)) {
                AddIngredient(ingredientIndex, isBurger);
            }
        }

        public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
            if (TryIngredientMatch(kitchenObjectSO, out int ingredientIndex, out bool isBurger)) {
                if (isBurger) {
                    return burgerCount < burgerMax;
                }
                if (ingredientIndex == 2) { // Cheese
                    if (state != State.Double && state != State.DoubleNoBread) {
                        return mealObjects[ingredientIndex].count < 1; // Only One cheese if not a double burger
                    }
                }
                return mealObjects[ingredientIndex].count < mealObjects[ingredientIndex].gameObject.Length;
            }
            return false;
        }

        private bool TryIngredientMatch(KitchenObjectSO kitchenObjectSO, out int ingredientIndex, out bool isBurger) {
            for (int i = 0; i < mealObjects.Count; i++) {
                if (mealObjects[i].kitchenObjectSO == kitchenObjectSO) {
                    ingredientIndex = i;
                    isBurger = false;
                    return true;
                }
            }
            for (int i = 0; i < burgerChoices.Count; i++) {
                if (burgerChoices[i].kitchenObjectSO == kitchenObjectSO) {
                    ingredientIndex = i;
                    isBurger = true;
                    return true;
                }
            }
            ingredientIndex = -1;
            isBurger = false;
            return false;
        }

        private void AddIngredient(int ingredientIndex, bool isBurger) {

        }
    }
}
