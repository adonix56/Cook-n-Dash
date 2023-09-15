using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] private int maxWaitingRecipes = 4;
    private LevelRecipeSO levelRecipeSO;
    private List<MealRecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        waitingRecipeSOList = new List<MealRecipeSO>();
    }

    private void Start() {
        levelRecipeSO = LevelRecipe.Instance.GetLevelRecipeSO();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < maxWaitingRecipes) {
                MealRecipeSO waitingRecipeSO = levelRecipeSO.levelRecipes[Random.Range(0, levelRecipeSO.levelRecipes.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log($"Adding {waitingRecipeSO.recipeName}");
            }
        }
    }

    public void DeliverMeal(PlateKitchenObject plateKitchenObject) {
        BaseRecipe attemptedDelivery = plateKitchenObject.GetBaseRecipe();
        Debug.Log($"Check if it's a burger {attemptedDelivery.GetType()}");
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            if (attemptedDelivery.GetType() == waitingRecipeSOList[i].baseRecipe.GetType()) { // If it's the same type of recipe
                string ingredientQtyList = attemptedDelivery.GetIngredientQtyList();
                if (ingredientQtyList == waitingRecipeSOList[i].successQtyList || waitingRecipeSOList[i].AcceptFailedAttempt(ingredientQtyList)) { // If the ingredientQtyList code matches
                    Debug.Log("Matched a meal!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        Debug.Log("No one wants this...");
    }
}
