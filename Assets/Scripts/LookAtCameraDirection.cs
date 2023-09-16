using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraDirection : MonoBehaviour
{
    [SerializeField] private GameObject progressBarUI;
    // Update is called once per frame
    private void Start()
    {
        Vector3 inverse = progressBarUI.transform.rotation.eulerAngles;
        progressBarUI.transform.Rotate(new Vector3(-inverse.x, -inverse.y, -inverse.z));
        //Debug.Log($"{name} Self: {transform.rotation.eulerAngles}  Progress: {progressBarUI.transform.rotation.eulerAngles}");
    }

    private void Update() {
        //progressBarUI.transform.LookAt(Camera.main.transform);
    }
}
