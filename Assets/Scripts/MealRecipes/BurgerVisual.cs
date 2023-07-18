using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerVisual : MonoBehaviour
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

    [SerializeField] private PlateKitchenObject plateKitchenObject;


    private void Start() {
        plateKitchenObject.OnIngredientAdded += IngredientAdded;
    }

    public void IngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {

    }

}
