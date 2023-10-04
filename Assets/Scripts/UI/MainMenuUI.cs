using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Awake() {
        playBtn.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
