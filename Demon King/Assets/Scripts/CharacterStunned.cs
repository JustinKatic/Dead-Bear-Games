using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStunned : Stunned
{
    private CharacterControlls characterControlls;
    private HealthManager healthManager;

    private void Start()
    {
        characterControlls = GetComponent<CharacterControlls>();
        healthManager = GetComponent<HealthManager>();
    }

    protected override void StunHasStarted()
    {
        base.StunHasStarted();
        characterControlls.CanMove = false;
        healthManager.OverheadText.text = "Stunned";
    }

    protected override void StunIsFinished()
    {
        base.StunIsFinished();
        characterControlls.CanMove = true;
        healthManager.CurrentHealth = healthManager.MaxHealth;
        healthManager.OverheadText.text = healthManager.CurrentHealth.ToString();
        healthManager.regenTimer = healthManager.TimeBeforeHealthRegen;
    }
}
