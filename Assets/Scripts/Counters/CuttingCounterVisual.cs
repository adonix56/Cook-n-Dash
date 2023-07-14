using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private Animator animator;

    public void PlayCuttingAnimation() {
        animator.SetBool(CUT, true);
    }

    public void StopCuttingAnimation() {
        animator.SetBool(CUT, false);
    }
}
