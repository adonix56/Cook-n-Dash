using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelRecipeSO : ScriptableObject
{
    [Serializable]
    public class IngredientRecipeDictionaryItem {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject mealRecipePrefab;
    }

    public List<IngredientRecipeDictionaryItem> ingredientRecipeDictionary;
    public List<MealRecipeSO> levelRecipes;
}
