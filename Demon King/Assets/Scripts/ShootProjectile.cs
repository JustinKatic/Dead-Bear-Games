using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    // Drag & drop the main camera in the inspector
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Shoot()
    {
        // Create a ray from the camera going through the middle of your screen
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit ;
        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint ;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint( 10 ) ; // You may need to change this value according to your needs
        
        Vector3 shootDirection = transform.forward;
        GameObject newProjectile = Instantiate(projectile,shootDirection, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = (targetPoint - transform.position).normalized * 10; 
    }

}
