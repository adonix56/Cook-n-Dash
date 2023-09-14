using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    private LevelRecipeSO levelRecipeSO;
    private List<MealRecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Awake() {
        waitingRecipeSOList = new List<MealRecipeSO>();
    }

    private void Start() {
        levelRecipeSO = LevelRecipe.Instance.GetLevelRecipeSO();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            MealRecipeSO waitingRecipeSO = levelRecipeSO.levelRecipes[Random.Range(0, levelRecipeSO.levelRecipes.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
            Debug.Log($"Adding {waitingRecipeSO.recipeName}");
        }
    }
}
