using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMealRecipe
{
    public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO);
    public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e);
}
