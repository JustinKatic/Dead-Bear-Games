using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStunned : Stunned
{
    private CharacterLocoMotion characterLocoMotion;

    private void Start()
    {
        characterLocoMotion = GetComponent<CharacterLocoMotion>();
    }

    protected override void StunHasStarted()
    {
        characterLocoMotion.DisableControls();
    }

    protected override void StunIsFinished()
    {
        characterLocoMotion.EnableControls();
    }
}
