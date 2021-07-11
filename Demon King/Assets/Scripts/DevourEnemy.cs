using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevourEnemy : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private float devourCheckRange = 5f;
    [SerializeField] private float devourTime = 3f;


    [HideInInspector] public bool IsDevouringEnemy = false;

    private CharacterControlls characterControlls;
    private GameObject enemyBeingDevoured = null;
    private Stunned stunnedEnemy = null;
    private HealthManager healthManager;



    private void Start()
    {
        camera = Camera.main;
        characterControlls = GetComponent<CharacterControlls>();
        healthManager = GetComponent<HealthManager>();
    }

    public void CheckIfEnemyIsInRange()
    {
        // Create a ray from the camera going through the middle of your screen
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, devourCheckRange))
        {
            //Check object is either a player or minion
            string hitObjectTag = hit.transform.tag;
            if (hitObjectTag.Equals("Player") || hitObjectTag.Equals("Minion"))
            {
                stunnedEnemy = hit.transform.gameObject.GetComponent<Stunned>();
                //If the enemy is stunned it can now be Devoured
                if (stunnedEnemy.IsStunned())
                {
                    if (!IsDevouringEnemy)
                    {
                        enemyBeingDevoured = hit.transform.gameObject;
                        StartCoroutine(DevouringEnemy());
                    }
                }
            }
        }
    }



    IEnumerator DevouringEnemy()
    {
        //When Devour starts
        IsDevouringEnemy = true;
        characterControlls.CanMove = false;
        characterControlls.PlayDevourAnim = true;
        enemyBeingDevoured.GetComponent<CharacterControlls>().CanMove = false;
        stunnedEnemy.BeingDevoured = true;
        healthManager.OverheadText.text = "Devouring";


        yield return new WaitForSeconds(devourTime);

        //When Devour finishs
        healthManager.OverheadText.text = "Finished Devouring";
        IsDevouringEnemy = false;
        characterControlls.CanMove = true;
        characterControlls.PlayDevourAnim = false;
        stunnedEnemy.GetComponent<HealthManager>().Die();
        stunnedEnemy.GetComponent<HealthManager>().OverheadText.text = "Devoured";
    }

    private void InterruptDevouring()
    {
        stunnedEnemy.BeingDevoured = false;
        stunnedEnemy.DisableStun();
        IsDevouringEnemy = false;
        StopCoroutine(DevouringEnemy());
    }
}
