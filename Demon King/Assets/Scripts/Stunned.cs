using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


public class Stunned : MonoBehaviour
{

    [SerializeField] private float stunTime = 5;
    [HideInInspector] public bool BeingDevoured = false;
    private bool isStunned;


    public void Stun()
    {
        StartCoroutine(StunTimer());
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    IEnumerator StunTimer()
    {
        StunHasStarted();
        yield return new WaitForSeconds(stunTime);
        //Make sure I am not currently being devoured
        if (!BeingDevoured)
        {
            StunIsFinished();
        }
    }

    protected virtual void StunHasStarted()
    {
        isStunned = true;
    }

    protected virtual void StunIsFinished()
    {
        isStunned = false;
    }

    //Used for when this is being devoured but it is interrupted and not executed fully
    public void DisableStun()
    {
        StopCoroutine(StunTimer());
        StunIsFinished();
    }
}
