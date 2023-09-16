using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Recipe;

public class RecipeCard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeTitle;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject iconTemplate;

    private MealRecipeSO currentRecipe;

    private void SetRecipeTitle() {
        recipeTitle.text = currentRecipe.recipeName;
    }

    private void AddRecipeIcon() {
        foreach (KitchenObjectSO kitchenObjectSO in currentRecipe.iconList) {
            RecipeIconSingleUI icon = Instantiate(iconTemplate, gridParent).GetComponent<RecipeIconSingleUI>();
            icon.SetIconFromKitchenObjectSO(kitchenObjectSO);
        }
    }

    public void SetupRecipeCard(MealRecipeSO mealRecipeSO) {
        currentRecipe = mealRecipeSO;
        SetRecipeTitle();
        AddRecipeIcon();
    }

    public bool isRecipe(MealRecipeSO mealRecipeSO) {
        return currentRecipe == mealRecipeSO;
    }
}
