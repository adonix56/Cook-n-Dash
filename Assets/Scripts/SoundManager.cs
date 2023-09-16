using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsSO audioClipsSO;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        CuttingCounterSound.OnAnyCut += OnAnyCut;
        PlayerController.Instance.OnPickup += OnPickup;
        PlayerController.Instance.OnPutdown += OnPutdown;
        PlayerSounds.OnFootstep += OnFootstep;
        BaseCounter.OnBuildMealOnPlayer += OnPickup;
        TrashCounter.OnAnyThrowAway += OnAnyThrowAway;
    }

    private void OnAnyThrowAway(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.trash, PlayerController.Instance.transform.position);
    }

    private void OnFootstep(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.footstep, PlayerController.Instance.transform.position);
    }

    private void OnPutdown(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.objectDrop, PlayerController.Instance.transform.position);
    }

    private void OnPickup(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.objectPickup, PlayerController.Instance.transform.position);
    }

    private void OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounterSound counter = sender as CuttingCounterSound;
        PlaySound(audioClipsSO.chop, counter.transform.position);
    }

    private void OnRecipeFailed(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.deliverFail, DeliveryCounter.Instance.transform.position);
    }

    private void OnRecipeSuccess(object sender, System.EventArgs e) {
        PlaySound(audioClipsSO.deliverSuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f) {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }
}
