using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LevelRecipeSO : ScriptableObject
{
    [Serializable]
    public class LevelRecipeDictionaryItem {
        public KitchenObjectSO kitchenObjectSO;
        public MealRecipeSO mealRecipeSO;
    }

    public List<LevelRecipeDictionaryItem> levelRecipeDictionary;
}
