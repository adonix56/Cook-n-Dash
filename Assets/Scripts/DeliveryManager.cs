using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler<DeliveryManagerEventArgs> OnNewRecipeSpawn;
    public event EventHandler<DeliveryManagerEventArgs> OnRecipeCompleted;

    public class DeliveryManagerEventArgs : EventArgs {
        public MealRecipeSO mealRecipeSO;
        public bool failedDelivery;
    }

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private int maxWaitingRecipes = 4;
    [SerializeField] private WaitingRecipeUI waitingRecipeUI;
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
                OnNewRecipeSpawn?.Invoke(this, new DeliveryManagerEventArgs {
                    mealRecipeSO = waitingRecipeSO,
                    failedDelivery = false
                });
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
                bool success = ingredientQtyList == waitingRecipeSOList[i].successQtyList;
                bool failed = waitingRecipeSOList[i].AcceptFailedAttempt(ingredientQtyList);
                if (success || failed) {
                    OnRecipeCompleted?.Invoke(this, new DeliveryManagerEventArgs {
                        mealRecipeSO = waitingRecipeSOList[i],
                        failedDelivery = failed
                    });
                    waitingRecipeSOList.RemoveAt(i);
                    return true;
                }
            }
        }
        return false;
    }
}
