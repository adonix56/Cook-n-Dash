using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO ingredient;
    }

    private List<KitchenObjectSO> kitchenObjectSOList;
    private BaseRecipe currentMeal;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public void AddIngredient(KitchenObjectSO kitchenObjectSO) {
        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { 
            ingredient = kitchenObjectSO 
        });
    }

    public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (currentMeal == null)
            return kitchenObjectSO.plateable;
        return currentMeal.CanAddIngredient(kitchenObjectSO);
    }
}
