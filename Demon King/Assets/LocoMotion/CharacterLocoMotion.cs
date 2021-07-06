using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocoMotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    public float moveSpeed;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", moveSpeed);
    }


    private void Update()
    {

        input = playerInput.CharacterControls.Move.ReadValue<Vector2>();

        Debug.Log("X = " + input.x);
        Debug.Log("Y = " + input.y);
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
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
