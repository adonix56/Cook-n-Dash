using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private Image clockFillImage;

    private float maxTimer;

    private void Start() {
        maxTimer = KitchenGameManager.Instance.GetGamePlayingTimer();
    }

    private void Update() {
        float currentTimer = KitchenGameManager.Instance.GetGamePlayingTimer();
        clockFillImage.fillAmount = currentTimer / maxTimer;
    }
}
