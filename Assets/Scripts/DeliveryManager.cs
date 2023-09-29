using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler<RecipeCreateEventArgs> OnNewRecipeSpawn;
    public event EventHandler<RecipeRemoveEventArgs> OnRecipeCompleted;
    public event EventHandler<RecipeRemoveEventArgs> OnRecipeTimeout;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public class RecipeCreateEventArgs : EventArgs {
        public MealRecipeSO mealRecipeSO;
    }

    public class RecipeRemoveEventArgs : EventArgs {
        public int recipeCardIndex;
        public bool failedDelivery;
    }

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private int maxWaitingRecipes = 4;
    private LevelRecipeSO levelRecipeSO;
    private List<MealRecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private int successfulRecipes;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        waitingRecipeSOList = new List<MealRecipeSO>();
        successfulRecipes = 0;
    }

    private void Start() {
        levelRecipeSO = LevelRecipe.Instance.GetLevelRecipeSO();
        spawnRecipeTimer = KitchenGameManager.Instance.GetRecipeSpawnTimer();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = KitchenGameManager.Instance.GetRecipeSpawnTimer();

            if (waitingRecipeSOList.Count < maxWaitingRecipes) {
                MealRecipeSO waitingRecipeSO = levelRecipeSO.levelRecipes[Random.Range(0, levelRecipeSO.levelRecipes.Count)];
                OnNewRecipeSpawn?.Invoke(this, new RecipeCreateEventArgs {
                    mealRecipeSO = waitingRecipeSO
                });
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public bool DeliverMeal(PlateKitchenObject plateKitchenObject) {
        if (!plateKitchenObject.isEmpty()) {
            BaseRecipe attemptedDelivery = plateKitchenObject.GetBaseRecipe();
            for (int i = 0; i < waitingRecipeSOList.Count; i++) {
                // If it's the same type of recipe
                if (attemptedDelivery.GetType() == waitingRecipeSOList[i].baseRecipe.GetType()) {
                    string ingredientQtyList = attemptedDelivery.GetIngredientQtyList();
                    // If the ingredientQtyList code matches
                    bool success = ingredientQtyList == waitingRecipeSOList[i].successQtyList;
                    bool failed = waitingRecipeSOList[i].AcceptFailedAttempt(ingredientQtyList);
                    if (success || failed) {
                        OnRecipeCompleted?.Invoke(this, new RecipeRemoveEventArgs {
                            recipeCardIndex = i,
                            failedDelivery = failed
                        });
                        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                        successfulRecipes++;
                        waitingRecipeSOList.RemoveAt(i);
                        return true;
                    }
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public void RecipeTimeout(MealRecipeSO recipeTimeout) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            if (waitingRecipeSOList[i] == recipeTimeout) {
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeTimeout?.Invoke(this, new RecipeRemoveEventArgs {
                    recipeCardIndex = i
                });
                return;
            }
        }
    }

    public int GetSuccessfulRecipe() {
        return successfulRecipes;
    }
}
