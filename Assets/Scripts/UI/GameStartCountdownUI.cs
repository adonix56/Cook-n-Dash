using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void Update() {
        float val = KitchenGameManager.Instance.GetCountdownTimer();
        int sec = Mathf.CeilToInt(val);
        float size = 1 + ((val - sec) / 5);
        countdownText.text = sec.ToString();
        countdownText.rectTransform.localScale = new Vector3(size, size, size);
    }

    private void OnGameStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsCountDown()) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
