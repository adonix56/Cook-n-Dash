using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecipe : MonoBehaviour
{
    public static LevelRecipe Instance;
    [SerializeField] private LevelRecipeSO _levelRecipeSO;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public bool TryToFindRecipe(KitchenObjectSO kitchenObjectSO, out GameObject mealRecipePrefab) {
        mealRecipePrefab = null;
        foreach (LevelRecipeSO.IngredientRecipeDictionaryItem item in _levelRecipeSO.ingredientRecipeDictionary) {
            if (item.kitchenObjectSO == kitchenObjectSO) {
                mealRecipePrefab = item.mealRecipePrefab;
                return true;
            }
        }
        return false;
    }

    public LevelRecipeSO GetLevelRecipeSO() {
        return _levelRecipeSO;
    }
}
