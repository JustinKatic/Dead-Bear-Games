using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourEnemy : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private float devourCheckRange = 5;
    
    public bool canDevourEnemy = false;
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
                //If the enemy is stunned it can now be Devoured
                if (hit.transform.gameObject.GetComponent<Stunned>().IsStunned())
                {
                    StartDevouringEnemy();
                }
            }
        }
    }
    
    private void StartDevouringEnemy()
    {
        Debug.Log("Devouring");
    }
}
