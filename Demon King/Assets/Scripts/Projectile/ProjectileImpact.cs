using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileImpact : MonoBehaviour
{
    [SerializeField] private UnityEvent OnImpact;
    
    private void OnCollisionEnter(Collision other)
    {
        string collidedObjectTag = other.transform.tag;
        if (collidedObjectTag.Equals("Player") || collidedObjectTag.Equals("Minion") )
        {
            Stunned stunned = other.gameObject.GetComponent<Stunned>();
            
            if (!stunned.IsStunned())
            {
                stunned.HasBeenStunned();
            }
        }
        
        OnImpact.Invoke();
        Destroy(this.gameObject);
    }
    
}
