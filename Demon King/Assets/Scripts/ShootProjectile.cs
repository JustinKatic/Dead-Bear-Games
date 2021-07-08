using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    
    // Start is called before the first frame update
    void Start()
    {
        if (projectile != null)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Void ShootProjectileInDirection()
    {
        Vector3 shootDirection = transform.forward;
        GameObject newProjectile = Instantiate(projectile,shootDirection, Quaternion.identity);

        newProjectile.transform.TransformDirection(shootDirection);
        
    }
}
