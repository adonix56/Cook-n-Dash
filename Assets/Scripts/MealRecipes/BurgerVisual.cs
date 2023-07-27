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

    [SerializeField] private State state;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += IngredientAdded;
    }

    public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        // reactivate the game objects based on the state of the burger
    }

    public bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) {
        return false;
    }

}
