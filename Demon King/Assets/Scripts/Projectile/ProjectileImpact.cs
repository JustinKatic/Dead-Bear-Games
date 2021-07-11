using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileImpact : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PhotonView PV = other.transform.GetComponent<PhotonView>();
            if (PV)
            {
                PV.RPC("RPC_TakeDamage", RpcTarget.All, 1 as object);
            }
        }
        Destroy(gameObject);
    }
}
