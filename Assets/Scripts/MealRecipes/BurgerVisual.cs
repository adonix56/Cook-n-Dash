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
            if (TryIngredientMatch(e.ingredient, out KitchenObjectSO_GameObject? ingredient, out bool isBurger)) {
                AddIngredient(ingredient.Value.gameObject, ingredient.Value.count, isBurger);
            }
        }

        public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
            if (TryIngredientMatch(kitchenObjectSO, out KitchenObjectSO_GameObject? ingredient, out bool isBurger)) {
                if (isBurger) {
                    return burgerCount < burgerMax;
                }
                if (mealObjects[2].kitchenObjectSO == kitchenObjectSO) { // Cheese
                    if (state != State.Double && state != State.DoubleNoBread) {
                        return ingredient.Value.count < 1; // Only One cheese if not a double burger
                    }
                }
                return ingredient.Value.count < ingredient.Value.gameObject.Length;
            }
            return false;
        }

        private bool TryIngredientMatch(KitchenObjectSO kitchenObjectSO, out KitchenObjectSO_GameObject? ingredient, out bool isBurger) {
            foreach (KitchenObjectSO_GameObject ingredientMatch in mealObjects) {
                if (ingredientMatch.kitchenObjectSO == kitchenObjectSO) {
                    ingredient = ingredientMatch;
                    isBurger = false;
                    return true;
                }
            }
            foreach (KitchenObjectSO_GameObject burgerChoice in burgerChoices) {
                if (burgerChoice.kitchenObjectSO == kitchenObjectSO) {
                    ingredient = burgerChoice;
                    isBurger = true;
                    return true;
                }
            }
            ingredient = null;
            isBurger = false;
            return false;
        }

        private void AddIngredient(GameObject[] ingredientObjects, int count, bool isBurger) {

        }
    }
}
