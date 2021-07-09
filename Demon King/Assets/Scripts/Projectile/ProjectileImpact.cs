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
        if (collidedObjectTag.Equals("Player"))
        {
            other.gameObject.GetComponent<CharacterLocoMotion>().HasBeenStunned();
        }

        if ( collidedObjectTag.Equals("Minion"))
        {
            other.gameObject.GetComponent<Wandering>().HasBeenStunned();
        }
        OnImpact.Invoke();
        Destroy(this.gameObject);
    }
    
}
