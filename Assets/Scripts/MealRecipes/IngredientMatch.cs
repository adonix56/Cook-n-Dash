using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    [Serializable]
    public class IngredientMatch {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject[] gameObject;
        public int count;
    }
}

