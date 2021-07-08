using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;


public class CharacterLocoMotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    public float moveSpeed;

    PlayerInput playerInput;
    PhotonView PV;

    private void Awake()
    {
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", moveSpeed);
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
        }
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
