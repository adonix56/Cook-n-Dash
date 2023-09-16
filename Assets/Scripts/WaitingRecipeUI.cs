using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRecipeUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeCardPrefab;

    private void Start() {
        DeliveryManager.Instance.OnNewRecipeSpawn += OnNewRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
    }

    private void OnRecipeCompleted(object sender, DeliveryManager.DeliveryManagerEventArgs e) {
        foreach (Transform child in container) {
            if (child.TryGetComponent<RecipeCard>(out RecipeCard recipeCard)) {
                if (recipeCard.isRecipe(e.mealRecipeSO)) {
                    Destroy(child.gameObject);
                    return;
                }
            }
        }
    }

    private void OnNewRecipeSpawn(object sender, DeliveryManager.DeliveryManagerEventArgs e) {
        RecipeCard recipeCard = Instantiate(recipeCardPrefab, container).GetComponent<RecipeCard>();
        recipeCard.SetupRecipeCard(e.mealRecipeSO);
    }
}
