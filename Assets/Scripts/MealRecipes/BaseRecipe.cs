using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRecipe : MonoBehaviour
{
    public PlateKitchenObject plateKitchenObject;

    public virtual bool CanAddIngredient(KitchenObjectSO kitchenObjectSO) { return false; }
}
