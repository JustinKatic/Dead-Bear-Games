using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class DevourEnemy : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private float devourCheckRange = 5f;
    [SerializeField] private float devourTime = 3f;

    [HideInInspector] public bool IsDevouringEnemy = false;

    private CharacterControlls characterControlls;
    private PhotonView enemyBeingDevoured = null;
    private HealthManager enemyHealthManager;
    private Stunned stunnedEnemy = null;
    private HealthManager healthManager;

    private PhotonView PV;



    private void Start()
    {
        camera = Camera.main;
        characterControlls = GetComponent<CharacterControlls>();
        healthManager = GetComponent<HealthManager>();
        PV = GetComponent<PhotonView>();
    }

    public void CheckIfEnemyIsInRange()
    {
        if (IsDevouringEnemy)
            return;

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
                    enemyBeingDevoured = hit.transform.gameObject.GetComponent<PhotonView>();
                    StartCoroutine(DevouringEnemy());
                }
            }
        }
    }


    [PunRPC]
    void OnDevourStart(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);
        view.gameObject.GetComponent<Stunned>().BeingDevoured = true;
        view.gameObject.GetComponent<HealthManager>().OverheadText.text = "being devoured";
    }

    [PunRPC]
    void OnDevourFinished(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);
        view.gameObject.GetComponent<Stunned>().BeingDevoured = false;
        view.gameObject.GetComponent<HealthManager>().OverheadText.text = "DEAD";
    }


    IEnumerator DevouringEnemy()
    {
        //When Devour starts
        PV.RPC("OnDevourStart", RpcTarget.All, new object[] { enemyBeingDevoured.ViewID });
        IsDevouringEnemy = true;
        characterControlls.CanMove = false;
        characterControlls.PlayDevourAnim = true;
        healthManager.OverheadText.text = "Devouring";



        yield return new WaitForSeconds(devourTime);

        //When Devour finishs
        PV.RPC("OnDevourFinished", RpcTarget.All, new object[] { enemyBeingDevoured.ViewID });
        healthManager.OverheadText.text = "Finished Devouring";
        IsDevouringEnemy = false;
        characterControlls.CanMove = true;
        characterControlls.PlayDevourAnim = false;
    }

    //private void InterruptDevouring()
    //{
    //    stunnedEnemy.BeingDevoured = false;
    //    stunnedEnemy.DisableStun();
    //    IsDevouringEnemy = false;
    //    StopCoroutine(DevouringEnemy());
    //}
}
