using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    [Tooltip("The amount of seconds each recipe will take to timeout"), MinMaxSlider(Bound = true, Min = 5f, Max = 120f)]
    public Vector2 recipeTimeoutDuration;
    [Tooltip("The amount of seconds each between recipe spawns"), MinMaxSlider(Bound = true, Min = 5f, Max = 120f)]
    public Vector2 recipeSpawnCooldown;

    public event EventHandler OnStateChanged;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownTimer = 3f;
    private float gamePlayingTimer = 180f;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        state = State.WaitingToStart;
        Debug.Log(recipeSpawnCooldown);
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0f) {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            default:
                break;
        }
        Debug.Log($"{state}");
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountDown() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownTimer() {
        return countdownTimer;
    }

    public float GetGamePlayingTimer() {
        return gamePlayingTimer;
    }

    public float GetRecipeTimeout() {
        return Random.Range(recipeTimeoutDuration.x, recipeTimeoutDuration.y);
    }

    public float GetRecipeSpawnTimer() {
        return Random.Range(recipeSpawnCooldown.x, recipeSpawnCooldown.y);
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }
}
