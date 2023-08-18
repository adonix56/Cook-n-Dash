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
        [SerializeField] private List<KitchenObjectSO_GameObject> mealObjects;

        private void Start() {
            plateKitchenObject.OnIngredientAdded += IngredientAdded;
        }

        public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            // reactivate the game objects based on the state of the burger 
        }

        public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
            return false;
        }

    }
}
