using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Stunned : MonoBehaviourPun
{

    [SerializeField] private float stunTime = 5;
    [HideInInspector] public bool BeingDevoured = false;
    private HealthManager healthManager;
    private CharacterControlls characterControlls;
    private bool isStunned;
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        healthManager = GetComponent<HealthManager>();
        characterControlls = GetComponent<CharacterControlls>();
    }

    public void Stun()
    {
        StartCoroutine(StunTimer());
    }

    [PunRPC]
    void RPC_StunStarted()
    {
        isStunned = true;
    }

    [PunRPC]
    void RPC_StunFinished()
    {
        isStunned = false;
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    IEnumerator StunTimer()
    {
        PV.RPC("RPC_StunStarted", RpcTarget.All);
        Debug.Log("StunTimer");
        healthManager.OverheadText.text = "Stunned";
        characterControlls.CanMove = false;
        characterControlls.PlayStunAnim = true;

        yield return new WaitForSeconds(stunTime);

        if (!BeingDevoured)
        {
            PV.RPC("RPC_StunFinished", RpcTarget.All);
            characterControlls.CanMove = true;
            healthManager.CurrentHealth = 3;
            healthManager.OverheadText.text = healthManager.CurrentHealth.ToString();
            characterControlls.PlayStunAnim = false;

        }
    }
}
