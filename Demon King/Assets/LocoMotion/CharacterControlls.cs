using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;


public class CharacterControlls : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed = 15;
    public bool PlayDevourAnim = false;
    public bool PlayStunAnim = false;

    [HideInInspector] public bool CanMove = true;

    Animator animator;
    Vector2 input;
    CharacterMovement playerInput;
    PhotonView PV;
    private ShootProjectile shootProjectile;
    private DevourEnemy devourEnemy;
    Camera mainCamera;


    private void Awake()
    {
        //Get my phoneView
        PV = GetComponent<PhotonView>();
        //If not mine dont continue
        if (!PV.IsMine)
            return;

        //disable and lock cursoe
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //Get Components off Player
        mainCamera = Camera.main;
        playerInput = new CharacterMovement();
        shootProjectile = GetComponent<ShootProjectile>();
        devourEnemy = GetComponent<DevourEnemy>();
        animator = GetComponent<Animator>();

        //Player Input actions
        playerInput.Movement.Shoot.performed += OnFire;
        playerInput.Movement.Devour.performed += OnDevour;

        //Set players start speed
        animator.SetFloat("MoveSpeed", moveSpeed);

    }
    private void Start()
    {
        //Makes Sure the player cant control others while networking
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
        }
    }


    private void Update()
    {
        //If not client - return
        if (!PV.IsMine)
            return;

        if (PlayDevourAnim)
        {
            animator.SetFloat("InputX", 0, 0.1f, Time.deltaTime);
            animator.SetFloat("InputY", 0, 0.1f, Time.deltaTime);
        }

        if (PlayStunAnim)
        {
            animator.SetFloat("InputX", 0, 0.1f, Time.deltaTime);
            animator.SetFloat("InputY", 0, 0.1f, Time.deltaTime);
        }

        //If can move 
        if (CanMove)
        {
            //Get value from input system for directional movement
            input = playerInput.Movement.Move.ReadValue<Vector2>();

            //Set the animators blend tree to correct animation based of inputs, with 0.1 smooth added
            animator.SetFloat("InputX", input.x, 0.1f, Time.deltaTime);
            animator.SetFloat("InputY", input.y, 0.1f, Time.deltaTime);

            //set the players rotation to the direction of the camera with a slerp smoothness
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
        }
    }



    private void OnEnable()
    {
        //If not client - return
        if (!PV.IsMine)
            return;
        //Enable this players input
        playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        //If not client - return
        if (!PV.IsMine)
            return;
        //Disable this players input
        playerInput.Movement.Disable();
    }

    //When OnFire input action is perfromed 
    void OnFire(InputAction.CallbackContext callback)
    {
        //call Shoot()
        shootProjectile.Shoot();
    }

    //When OnDevour input action is performed
    void OnDevour(InputAction.CallbackContext callback)
    {
        devourEnemy.CheckIfEnemyIsInRange();
        if (devourEnemy.IsDevouringEnemy)
        {
            //disable our movement while devouring
            CanMove = false;
        }
    }
}
