using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    //public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnInteractAlternateStart;
    public event EventHandler OnInteractAlternateEnd;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        //playerInputActions.Player.InteractAlternate.performed += Interact_Alternate_performed;
        playerInputActions.Player.InteractAlternate.started += Interact_Alternate_start;
        playerInputActions.Player.InteractAlternate.canceled += Interact_Alternate_end;
    }

    /*private void Interact_Alternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }*/

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
        // Same as
        //if (OnInteractAction != null) OnInteractAction(this, EventArgs.Empty);
    }

    private void Interact_Alternate_start(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateStart?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_Alternate_end(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateEnd?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        //Old Input System
        /*
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = 1;
        }*/

        return inputVector.normalized;
    }
}
