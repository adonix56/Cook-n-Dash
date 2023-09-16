using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    //[SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private GameObject progressBarUICanvas;
    
    private Image barImage;

    private void Start() {
        Transform barImageObject = progressBarUICanvas.transform.GetChild(1);
        if (barImageObject.name == "Progress")
            barImage = barImageObject.GetComponent<Image>();
        else
            barImage = progressBarUICanvas.transform.GetChild(0).GetComponent<Image>();
    }

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
