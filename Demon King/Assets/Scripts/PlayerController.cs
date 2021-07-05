using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    public float runMultiplier = 6.0f;
    public float moveSpeed = 3.0f;
    public float rotationFactorPerFrame = 15.0f;

    bool isMovementPressed;
    bool isRunPressed;


    PhotonView PV;
    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        PV = GetComponent<PhotonView>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
    }

    private void OnRun(InputAction.CallbackContext obj)
    {
        isRunPressed = obj.ReadValueAsButton();
    }

    private void OnMovementInput(InputAction.CallbackContext obj)
    {
        currentMovementInput = obj.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * moveSpeed;
        currentMovement.z = currentMovementInput.y * moveSpeed;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;

        isMovementPressed = currentMovement.x != 0 || currentMovementInput.y != 0;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void HandleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
            animator.SetBool("isWalking", true);
        else if (!isMovementPressed && isWalking)
            animator.SetBool("isWalking", false);

        if ((isMovementPressed && isRunPressed) && !isRunning)
            animator.SetBool("isRunning", true);
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
            animator.SetBool("isRunning", false);
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;


        HandleAnimation();
        HandleGravity();
        HandleRotation();


        if (isRunPressed)
            characterController.Move(currentRunMovement * Time.deltaTime);
        else
            characterController.Move(currentMovement * Time.deltaTime);

    }


    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
