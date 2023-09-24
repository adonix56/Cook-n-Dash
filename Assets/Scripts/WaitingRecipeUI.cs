using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRecipeUI : MonoBehaviour
{
    private const string COMPLETE = "Complete";
    private const string TIMEOUT = "Timeout";

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeCardPrefab;

    private void Start() {
        DeliveryManager.Instance.OnNewRecipeSpawn += OnNewRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeComplete;
        DeliveryManager.Instance.OnRecipeTimeout += OnRecipeTimeout;
    }

    private void OnRecipeComplete(object sender, DeliveryManager.RecipeRemoveEventArgs e) {
        container.GetChild(e.recipeCardIndex).GetComponent<RecipeCard>().EndCardLife(COMPLETE);
        //Destroy(container.GetChild(e.recipeCardIndex).gameObject);
        //container.GetChild(e.recipeCardIndex).GetR
        /*foreach (Transform child in container) {
            if (child.TryGetComponent<RecipeCard>(out RecipeCard recipeCard)) {
                if (recipeCard.isRecipe(e.mealRecipeSO)) {
                    Destroy(child.gameObject);
                    return;
                }
            }
        }*/
    }

    private void OnNewRecipeSpawn(object sender, DeliveryManager.RecipeCreateEventArgs e) {
        RecipeCard recipeCard = Instantiate(recipeCardPrefab, container).GetComponent<RecipeCard>();
        recipeCard.SetupRecipeCard(e.mealRecipeSO, 5f);
    }

    private void OnRecipeTimeout(object sender, DeliveryManager.RecipeRemoveEventArgs e) {
        container.GetChild(e.recipeCardIndex).GetComponent<RecipeCard>().EndCardLife(TIMEOUT);
        /*foreach (Transform child in container) {
            if (e.recipeCard.transform == child) {
                Destroy(child.gameObject);
                return;
            }
        }*/
    }
}
