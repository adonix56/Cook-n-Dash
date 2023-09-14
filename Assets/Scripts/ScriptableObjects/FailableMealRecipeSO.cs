using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FailableMealRecipeSO : MealRecipeSO
{
    public List<string> failQtyList;

    public override bool AcceptFailedAttempt(string qtyList) {
        return failQtyList.Contains(qtyList);
    }
}
