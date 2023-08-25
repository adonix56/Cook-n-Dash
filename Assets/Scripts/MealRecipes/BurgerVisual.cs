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
        /// cheese          0.047
        /// cheese double   0.178
        /// lettuce         0.062
        /// tomatoes        0.090
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
        [SerializeField] private GameObject burgerVisual;
        [SerializeField] private GameObject breadTopObject;
        [SerializeField] private KitchenObjectSO breadKitchenObjectSO;

        private int burgerCount = 0;
        private int burgerMax = 2;
        private float burgerSpawnY = 0.021f;

        private new void Start() {
            base.Start();
            Debug.Log("Adding AddIngredient to PlateKitchenObject");
            //plateKitchenObject.OnIngredientAdded += AddIngredient;
        }

        public void AddIngredient(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
            Debug.Log("Properly added to event!");
            if (TryIngredientMatch(e.ingredient, out int ingredientIndex, out bool isBurger)) {
                ActivateIngredient(ingredientIndex, isBurger);
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
                return mealObjects[ingredientIndex].count < mealObjects[ingredientIndex].gameObject.Count;
            }
            return false;
        }

        private bool TryIngredientMatch(KitchenObjectSO kitchenObjectSO, out int ingredientIndex, out bool isBurger) {
            for (int i = 0; i < mealObjects.Count; i++) {
                if (mealObjects[i].kitchenObjectSO == kitchenObjectSO) {
                    ingredientIndex = i;
                    isBurger = i > 3;
                    return true;
                }
            }
            ingredientIndex = -1;
            isBurger = false;
            return false;
        }

        private void ActivateIngredient(int ingredientIndex, bool isBurger) {
            if (isBurger) {
                // Add the corresponding burger patty
                GameObject meatPatty = mealObjects[ingredientIndex].kitchenObjectSO.prefab.transform.GetChild(0).GetChild(0).gameObject;
                meatPatty = Instantiate(meatPatty, burgerVisual.transform);
                meatPatty.transform.Translate(Vector3.up * burgerSpawnY);
                mealObjects[ingredientIndex].gameObject.Add(meatPatty);
                // Move the correct objects up
                // Move Bread
                if (burgerCount == 0) {
                    breadTopObject.transform.Translate(Vector3.up * 0.084f);
                    MoveObjectsUp(0.094f, true);
                } else {
                    breadTopObject.transform.Translate(Vector3.up * 0.127f);
                    MoveObjectsUp(0.131f, true);
                }
                // Change the state
                burgerCount++;
                burgerSpawnY += 0.129f; // Move Burger Spawn up for double
                state++;
            } else if (ingredientIndex == 0) { // bread
                // Activate Bread
                mealObjects[0].gameObject[0].SetActive(true);
                // Move the correct objects up
                MoveObjectsUp(0.165f, false);
                burgerSpawnY += 0.165f; //Move Burger Spawns up
                // Change the state
                state = state + 3;
                // Increase Count
                mealObjects[ingredientIndex].count += 1;
            } else {
                // Activate the correct object
                int count = mealObjects[ingredientIndex].count;
                mealObjects[ingredientIndex].gameObject[count].SetActive(true);
                // Increase Count
                mealObjects[ingredientIndex].count += 1;
            }
        }

        private void MoveObjectsUp(float moveAmount, bool ignorePatties) {
            int limit = ignorePatties ? 4 : 6;
            for (int i = 0; i < limit; i++) {
                if (mealObjects[i].kitchenObjectSO != breadKitchenObjectSO) {
                    for (int j = 0; j < mealObjects[i].gameObject.Count; j++) {
                        if (i != 2 || !(i == 2 && burgerCount == 1 && ignorePatties)) {
                            mealObjects[i].gameObject[j].transform.Translate(Vector3.up * moveAmount);
                        }
                    }
                }
            }
        }
    }
}
