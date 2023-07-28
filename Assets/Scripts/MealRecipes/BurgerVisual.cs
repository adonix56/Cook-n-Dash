using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerVisual : BaseRecipe, IMealRecipe
{
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    public enum State {
        NoMeatNoBread,
        NoMeat,
        SingleNoBread,
        Single,
        DoubleNoBread,
        Double
    }
    /// <summary>
    /// 
    /// No Bread No Meat
    /// cheese  0.047
    /// lettuce 0.062
    /// tomatoes0.090
    /// 
    /// No Bread
    /// Meat Single     0.021
    /// CLT            +0.094
    /// Meat Double     0.150
    /// CLT            +0.131
    /// 
    /// No Meat
    /// Bread Top   0.298
    /// CLT        +0.165
    /// 
    /// Full Burger
    /// Bread Top      +0.084
    /// Meat Single    +0.187
    /// CLT            +0.023 +NoBread +NoMeat
    /// Bread Top Doub +0.127 +Single +NoMeat
    /// CLT            +0.049
    /// 
    /// </summary>

    [SerializeField] private State state;
    [SerializeField] private GameObject[] mealObjects;
    
    private Dictionary<State, GameObject> stateOfMeal;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += IngredientAdded;
        stateOfMeal = new Dictionary<State, GameObject>();
        stateOfMeal[State.NoMeatNoBread] = mealObjects[0];
        stateOfMeal[State.NoMeat] = mealObjects[1];
        stateOfMeal[State.SingleNoBread] = mealObjects[2];
        stateOfMeal[State.DoubleNoBread] = mealObjects[2];
        stateOfMeal[State.Single] = mealObjects[3];
        stateOfMeal[State.Double] = mealObjects[3];
    }

    public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        // reactivate the game objects based on the state of the burger
    }

    public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
        return false;
    }

}
