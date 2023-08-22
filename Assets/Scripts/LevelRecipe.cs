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
            Destroy(this);
        }
    }

    public bool TryToFindRecipe(KitchenObjectSO kitchenObjectSO, out MealRecipeSO mealRecipeSO) {
        mealRecipeSO = null;
        foreach (LevelRecipeSO.LevelRecipeDictionaryItem item in _levelRecipeSO.levelRecipeDictionary) {
            if (item.kitchenObjectSO == kitchenObjectSO) {
                mealRecipeSO = item.mealRecipeSO;
                return true;
            }
        }
        return false;
    }
}
