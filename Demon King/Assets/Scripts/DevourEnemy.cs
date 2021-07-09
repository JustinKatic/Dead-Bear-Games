using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourEnemy : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private float devourCheckRange = 5;
    [SerializeField] private float devourTime = 3;
    
    [HideInInspector]public bool isDevouringEnemy = false;

    private GameObject enemyBeingDevoured;
    private Stunned stunnedEnemy;
    private void Start()
    {
        camera = Camera.main;
    }

    public void CheckIfEnemyIsInRange()
    {
        // Create a ray from the camera going through the middle of your screen
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));    
        
        if (Physics.Raycast(ray,out RaycastHit hit, devourCheckRange))
        {
            //Check object is either a player or minion
            string hitObjectTag = hit.transform.tag;
            if (hitObjectTag.Equals("Player") || hitObjectTag.Equals("Minion"))
            {
                stunnedEnemy = hit.transform.gameObject.GetComponent<Stunned>();
                //If the enemy is stunned it can now be Devoured
                if (stunnedEnemy.IsStunned())
                {
                    if (!isDevouringEnemy)
                    {
                        enemyBeingDevoured = hit.transform.gameObject;
                        DisableOtherPlayer();
                        StartDevouringEnemy();
                    }
                }
            }
        }
    }

    public void DisableOtherPlayer()
    {
        stunnedEnemy.beingDevoured = true;
        enemyBeingDevoured.GetComponent<CharacterLocoMotion>().DisableControls();
    }
    
    private void StartDevouringEnemy()
    {
        isDevouringEnemy = true;
        StartCoroutine(DevouringEnemy());
        Debug.Log("Devouring");
    }

    IEnumerator DevouringEnemy()
    {
        yield return new WaitForSeconds(devourTime);
        isDevouringEnemy = false;
    }

    private void InterruptDevouring()
    {
        stunnedEnemy.beingDevoured = false;
        stunnedEnemy.DisableStun();
        isDevouringEnemy = false;
        StopCoroutine(DevouringEnemy());
    }
}
