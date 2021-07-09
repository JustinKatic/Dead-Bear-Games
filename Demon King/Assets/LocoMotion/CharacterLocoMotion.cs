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
    CharacterMovement playerInput;
    PhotonView PV;
    private ShootProjectile shootProjectile;
    [SerializeField] private float stunTime = 5;
    

    private void Awake()
    {
        playerInput = new CharacterMovement();
        playerInput.Movement.Shoot.performed += OnFire;
        shootProjectile = GetComponent<ShootProjectile>();
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

        input = playerInput.Movement.Move.ReadValue<Vector2>();
        
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }

    private void OnEnable()
    {
        playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        playerInput.Movement.Disable();
    }

    void OnFire(InputAction.CallbackContext callback)
    {
        Debug.Log("Fire");
        shootProjectile.Shoot();
    }

    //Should be passed on to the Projectile Impact Event OnplayerImpact
    //Disables the players movement for a set time
    public void HasBeenStunned()
    {
        playerInput.Movement.Disable();
        StartCoroutine(Stunned());

    }
    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(stunTime);
        playerInput.Movement.Enable();
    }
}
