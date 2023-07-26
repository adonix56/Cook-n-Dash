using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MealRecipeSO : ScriptableObject
{
    public KitchenObjectSO[] ingredientList;
    public GameObject[] mealObjectsList;
}
