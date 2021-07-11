using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth = 0;
    public TMP_Text OverheadText = null;

    [HideInInspector] public float TimeBeforeHealthRegen = 3f;
    [HideInInspector] public float regenTimer = 3f;

    private Stunned stunned;

    private PlayerManager playerManager;
    private PhotonView PV;



    private void Awake()
    {
        stunned = gameObject.GetComponent<Stunned>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        //Set current health to max health
        CurrentHealth = MaxHealth;
        OverheadText.text = CurrentHealth.ToString();
    }

    private void Update()
    {
        //If current health is less then maxHealth regain health over time
        if (CurrentHealth < MaxHealth)
        {
            regenTimer -= Time.deltaTime;

            if (regenTimer <= 0)
            {
                CurrentHealth += 1;
                OverheadText.text = CurrentHealth.ToString();
                regenTimer = TimeBeforeHealthRegen;
            }
        }
    }



    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
            if (CurrentHealth <= 0)
                return;

        //If Current health is > 0 take damage 
        CurrentHealth -= 1;
        OverheadText.text = CurrentHealth.ToString();
        regenTimer = TimeBeforeHealthRegen;
        Debug.Log(CurrentHealth);

        //If current health is < 0 stun the character
        if (CurrentHealth <= 0)
        {
            stunned.Stun();
        }
    }



    public void Die()
    {
        transform.position = GameObject.FindGameObjectWithTag("SpawnPos").transform.position;
        //playerManager.Die();
    }
}
