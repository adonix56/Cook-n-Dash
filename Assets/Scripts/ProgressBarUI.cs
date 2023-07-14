using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    //[SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject progressBarUICanvas;

    public void SetBar(float n) {
        if (n <= 0) {
            HideBar();
        } else {
            ShowBar();
        }
        barImage.fillAmount = n;
    }

    private void ShowBar() {
        progressBarUICanvas.SetActive(true);
    }

    private void HideBar() {
        progressBarUICanvas.SetActive(false);
    }
    
    public void ClearBar() {
        HideBar();
        barImage.fillAmount = 0f;
    }
}
