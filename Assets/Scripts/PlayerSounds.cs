using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static event EventHandler OnFootstep;

    public void Footstep() {
        OnFootstep?.Invoke(this, EventArgs.Empty);
    }
}
