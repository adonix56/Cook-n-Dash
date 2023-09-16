using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

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

    public bool DeliverMeal(PlateKitchenObject plateKitchenObject) {
        BaseRecipe attemptedDelivery = plateKitchenObject.GetBaseRecipe();
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            // If it's the same type of recipe
            if (attemptedDelivery.GetType() == waitingRecipeSOList[i].baseRecipe.GetType()) { 
                string ingredientQtyList = attemptedDelivery.GetIngredientQtyList();
                // If the ingredientQtyList code matches
                if (ingredientQtyList == waitingRecipeSOList[i].successQtyList || waitingRecipeSOList[i].AcceptFailedAttempt(ingredientQtyList)) {
                    waitingRecipeSOList.RemoveAt(i);
                    return true;
                }
            }
        }
        return false;
    }
}
