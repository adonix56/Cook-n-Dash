using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public enum State { 
        Raw,
        Chopped,
        Cooked,
        Burned,
        Default
    }

    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
    public State state;
    public bool plateable;
}
