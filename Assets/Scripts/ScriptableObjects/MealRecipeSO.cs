using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipe;

[CreateAssetMenu()]
public class MealRecipeSO : ScriptableObject
{
    public string recipeName;
    public BaseRecipe baseRecipe;
    public string successQtyList;

    public virtual bool AcceptFailedAttempt(string qtyList) => false;
}
