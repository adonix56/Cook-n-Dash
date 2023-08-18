using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recipe {
    public class BaseRecipe : MonoBehaviour {
        [Serializable]
        public struct KitchenObjectSO_GameObject {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject[] gameObject;
            public int count;
        }
        public PlateKitchenObject plateKitchenObject;
    }
}

