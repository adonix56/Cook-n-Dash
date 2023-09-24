using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Recipe;
using UnityEngine.UI;

public class RecipeCard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeTitle;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject iconTemplate;
    [SerializeField] private Slider sliderTimer;
    [SerializeField] private GameObject coinParticles;

    private MealRecipeSO currentRecipe;
    private bool startTimer = false;
    private Image sliderFillImage;
    private Animator animator;

    private void Start() {
        sliderFillImage = sliderTimer.fillRect.GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (startTimer) {
            sliderTimer.value -= Time.deltaTime;
            CalculateSliderColor();
            if (sliderTimer.value <= 0) {
                startTimer = false;
                DeliveryManager.Instance.RecipeTimeout(currentRecipe);
            }
        }
    }

    private void SetRecipeTitle() {
        recipeTitle.text = currentRecipe.recipeName;
    }

    private void AddRecipeIcon() {
        foreach (KitchenObjectSO kitchenObjectSO in currentRecipe.iconList) {
            RecipeIconSingleUI icon = Instantiate(iconTemplate, gridParent).GetComponent<RecipeIconSingleUI>();
            icon.SetIconFromKitchenObjectSO(kitchenObjectSO);
        }
    }

    public void SetupRecipeCard(MealRecipeSO mealRecipeSO, float time) {
        currentRecipe = mealRecipeSO;
        sliderTimer.maxValue = time;
        sliderTimer.value = time;
        startTimer = true;
        SetRecipeTitle();
        AddRecipeIcon();
    }

    public bool isRecipe(MealRecipeSO mealRecipeSO) {
        return currentRecipe == mealRecipeSO;
    }

    private void CalculateSliderColor() {
        Color color = sliderFillImage.color;
        float sliderPercentage = sliderTimer.value / sliderTimer.maxValue;
        if (sliderPercentage > 0.5f) { //Moving Green - Yellow
            // Moving Red and Blue from 0f - 1f when percentage is 1f - 0.5f
            // Equation: color = 2 - 2 * percentage
            float newColorValue = 2 - (2 * sliderPercentage);
            color.r = newColorValue;
            color.b = newColorValue;
        } else { // Moving Yellow - Red
            // Moving Green and Blue from 1f - 0f when percentage is 0.5f - 0f
            // Equation: color = 2 * percentage
            float newColorValue = 2 * sliderPercentage;
            color.b = newColorValue;
            color.g = newColorValue;
        }
        sliderFillImage.color = color;
    }

    public void ShowCoins() {
        if (!coinParticles.activeSelf) coinParticles.SetActive(true);
    }

    public void DestroyRecipeCard() {
        Destroy(gameObject);
    }

    private void FadeIcons(float fadeDuration) {
        foreach (Transform child in gridParent) {
            if (child.TryGetComponent<RecipeIconSingleUI>(out RecipeIconSingleUI singleUI)) {
                singleUI.Fade(fadeDuration);
            }
        }
    }

    public void EndCardLife(string animatorTrigger) {
        animator.SetTrigger(animatorTrigger);
    }
}
