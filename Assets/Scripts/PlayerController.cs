using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent {
    public static PlayerController Instance { get; private set; }

    public event EventHandler OnPickup;
    public event EventHandler OnPutdown;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform kitchenObjectSpawnPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Player instantiated");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        //gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        gameInput.OnInteractAlternateStart += GameInput_OnInteractAlternateStart;
        gameInput.OnInteractAlternateEnd += GameInput_OnInteractAlternateEnd;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    /*private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }*/

    private void GameInput_OnInteractAlternateStart(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternateStart(this);
        }
    }
    private void GameInput_OnInteractAlternateEnd(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternateEnd(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        isWalking = moveDir != Vector3.zero;

        if (!canMove) {
            // Try to move in the X
            Vector3 moveDirOneDirection = new Vector3(inputVector.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirOneDirection, moveDistance);

            if (canMove) {
                moveDir = moveDirOneDirection;
            } else { //Cannot move in X
                // Try to move in the Z
                moveDirOneDirection = new Vector3(0f, 0f, inputVector.y).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirOneDirection, moveDistance);
                if (canMove) {
                    moveDir = moveDirOneDirection;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }
        if (moveDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, layerMask)) {
            if (hit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter)) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter newSelectedCounter) {
        if (newSelectedCounter == null && this.selectedCounter != null)
            this.selectedCounter.InteractAlternateEnd(this);
        this.selectedCounter = newSelectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = this.selectedCounter
        });
    }

    public Transform GetKitchenObjectSpawnPoint() {
        return kitchenObjectSpawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        OnPickup?.Invoke(this, EventArgs.Empty);
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        OnPutdown?.Invoke(this, EventArgs.Empty);
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
