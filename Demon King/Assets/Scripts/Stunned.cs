using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    [SerializeField] private float stunTime = 5;
    private bool hasBeenStunned = false;
    
    //Should be passed on to the Projectile Impact Event OnplayerImpact
    //Disables the players movement for a set time
    public void HasBeenStunned()
    {
        hasBeenStunned = true;
        StunHasStarted();
        StartCoroutine(StunTimer());
    }
    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        StunIsFinished();
        hasBeenStunned = false;
    }

    public bool IsStunned()
    {
        return hasBeenStunned;
    }

    protected virtual void StunIsFinished()
    {
        
    }
    protected virtual void StunHasStarted()
    {
        
    }
}
