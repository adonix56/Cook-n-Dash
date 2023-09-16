using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterSound : MonoBehaviour
{
    public static event EventHandler OnAnyCut;

    public void OnCutDown() {
        OnAnyCut?.Invoke(this, EventArgs.Empty);
    }
}
