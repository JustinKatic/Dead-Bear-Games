using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileImpact : MonoBehaviourPun
{
    PhotonView myPV;
    float bulletLifeTime = 3f;
    private void Awake()
    {
        myPV = GetComponent<PhotonView>();
        //Destroy bullet after x seconds
        Destroy(gameObject, bulletLifeTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (myPV.IsMine)
        {
            if (other.gameObject.tag == "Player")
            {
                PhotonView PV = other.transform.GetComponent<PhotonView>();
                if (PV)
                {
                    Debug.Log("Collided with: " + PV.name);
                    PV.RPC("RPC_TakeDamage", RpcTarget.All, 1);
                }
            }
        }
        Destroy(gameObject);
    }
}
