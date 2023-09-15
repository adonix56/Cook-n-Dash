using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO ingredient;
    }

    private List<KitchenObjectSO> kitchenObjectSOList;
    private IMealRecipe currentMeal;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public void AddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (currentMeal == null) {
            if (LevelRecipe.Instance.TryToFindRecipe(kitchenObjectSO, out GameObject mealRecipePrefab)) {
                kitchenObjectSOList.Add(kitchenObjectSO);
                currentMeal = Instantiate(mealRecipePrefab, transform).GetComponent<IMealRecipe>();
                OnIngredientAdded += currentMeal.AddIngredient;
            } else { // Redundant check. Cannot be called if can't add recipe
                return;
            }
        }
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            ingredient = kitchenObjectSO
        });
    }

    public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (currentMeal == null) {
            return LevelRecipe.Instance.TryToFindRecipe(kitchenObjectSO, out GameObject mealRecipePrefab);
        }
        return currentMeal.CanAddIngredient(kitchenObjectSO);
    }

    public BaseRecipe GetBaseRecipe() {
        return currentMeal.GetBaseRecipe();
    }
}
