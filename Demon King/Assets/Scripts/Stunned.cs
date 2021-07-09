using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    [SerializeField] private float stunTime = 5;
    private bool hasBeenStunned = false;
    [HideInInspector]public bool beingDevoured = false;

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
        //Make sure I am not currently being devoured
        if (!beingDevoured)
        {
            StunIsFinished();
            hasBeenStunned = false;
        }
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

    //Used for when this is being devoured but it is interrupted and not executed fully
    public void DisableStun()
    {
        StopCoroutine(StunTimer());
        StunIsFinished();
        hasBeenStunned = false;
    }
    
    
}
