using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Transform shootPosition = null;
    [SerializeField] private float projectileSpeed = 20;



    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void Shoot()
    {
        // Create a ray from the camera going through the middle of your screen
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(10); // You may need to change this value according to your needs
        Vector3 shootingPosition = shootPosition.position;

        GameObject newProjectile = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Projectile"), shootingPosition, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = (targetPoint - shootingPosition).normalized * projectileSpeed;
    }

}
